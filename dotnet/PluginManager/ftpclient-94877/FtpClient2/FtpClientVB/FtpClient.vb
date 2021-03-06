Imports System.Collections.Generic
Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions


Namespace Utilities.FTP

#Region "FTP client class"
	''' <summary>
	''' A wrapper class for .NET 2.0 FTP
	''' </summary>
	''' <remarks>
	''' 
	''' v1.0 - original version
	''' 
	''' v1.1 - added support for EnableSSL, UsePassive and Proxy connections
	'''        Also added support for downloading correct date/time from FTP server for each file
	'''        Added FtpDirectoryExists function to check for existence of remote directory
	''' 
	''' v1.2 - Currently this class does not hold open an FTP connection but 
	'''        instead is stateless: for each FTP request it connects, performs the request and disconnects.
	'''        Also added CreateURI function to centralise this and add Escape sequence to payload.
	'''        Amended Upload to throw exception if encountered
	'''
	''' </remarks> 
	Public Class FTPclient
		Implements IFTPClient

#Region "CONSTRUCTORS"
		''' <summary>
		''' Blank constructor
		''' </summary>
		''' <remarks>Hostname, username and password must be set manually</remarks>
		Sub New()
		End Sub

        ''' <summary>
        ''' Constructor just taking the hostname
        ''' </summary>
        ''' <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
        ''' <remarks></remarks>
        Sub New(ByVal Hostname As String)
			_hostname = Hostname
		End Sub

        ''' <summary>
        ''' Constructor taking hostname, username and password
        ''' </summary>
        ''' <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
        ''' <param name="Username">Leave blank to use 'anonymous' but set password to your email</param>
        ''' <param name="Password">Password for username</param>
        ''' <param name="KeepAlive">[optional] keep connections alive between requests (v1.1)</param>
        ''' <remarks></remarks>
        Sub New(ByVal Hostname As String, ByVal Username As String, ByVal Password As String, Optional ByVal KeepAlive As Boolean = False)
			_hostname = Hostname
			_username = Username
			_password = Password
			_keepAlive = KeepAlive
		End Sub
#End Region

#Region "Directory functions"
		''' <summary>
		''' Return a simple directory listing
		''' </summary>
		''' <param name="directory">Directory to list, e.g. /pub</param>
		''' <returns>A list of filenames and directories as a List(of String)</returns>
		''' <remarks>
		''' For a detailed directory listing, use ListDirectoryDetail
		''' </remarks>
		Public Function ListDirectory(Optional ByVal directory As String = "") As List(Of String) Implements IFTPClient.ListDirectory
			'return a simple list of filenames in directory
			Dim URI As String = GetDirectory(directory)
			Dim ftp As Net.FtpWebRequest = GetRequest(URI)
            'Set request to do simple list
            ftp.Method = Net.WebRequestMethods.Ftp.ListDirectory

			Dim str As String = GetStringResponse(ftp)
            'replace CRLF to CR, remove last instance
            str = str.Replace(vbCrLf, vbCr).TrimEnd(Chr(13))
            'split the string into a list
            Dim result As New List(Of String)
			result.AddRange(str.Split(Chr(13)))
			Return result
		End Function

        ''' <summary>
        ''' Return a detailed directory listing
        ''' </summary>
        ''' <param name="directory">Directory to list, e.g. /pub/etc</param>
        ''' <param name="doDateTimeStamp">Boolean: set to True to also download the file date/time stamps</param>
        ''' <returns>An FTPDirectory object</returns>
        Public Function ListDirectoryDetail(Optional ByVal directory As String = "",
											Optional ByVal doDateTimeStamp As Boolean = False) As FTPdirectory Implements IFTPClient.ListDirectoryDetail
			Dim URI As String = GetDirectory(directory)
			Dim ftp As Net.FtpWebRequest = GetRequest(URI)
            'Set request to do simple list
            ftp.Method = Net.WebRequestMethods.Ftp.ListDirectoryDetails

			Dim str As String = GetStringResponse(ftp)
            'replace CRLF to CR, remove last instance
            str = str.Replace(vbCrLf, vbCr).TrimEnd(Chr(13))
            'split the string into a list
            Dim dir As New FTPdirectory(str, _lastDirectory)

            'Download timestamps if requested?
            If doDateTimeStamp Then
				For Each fi As FTPfileInfo In dir
					fi.FileDateTime = Me.GetDateTimestamp(fi)
				Next
			End If

			Return dir
		End Function

#End Region

#Region "Upload: File transfer TO ftp server"
		''' <summary>
		''' Copy a local file to the FTP server (local filename as string)
		''' </summary>
		''' <param name="localFilename">Full path of the local file</param>
		''' <param name="targetFilename">Target filename, if required</param>
		''' <returns></returns>
		''' <remarks>If the target filename is blank, the source filename is used
		''' (assumes current directory). Otherwise use a filename to specify a name
		''' or a full path and filename if required.</remarks>
		Public Function Upload(ByVal localFilename As String, Optional ByVal targetFilename As String = "") As Boolean
			'1. check source
			If Not File.Exists(localFilename) Then
				Throw New ApplicationException("File " & localFilename & " not found")
			End If
            'copy to FI
            Dim fi As New FileInfo(localFilename)
			Return Upload(fi, targetFilename)
		End Function

        ''' <summary>
        ''' Upload a local file to the FTP server (local file as FileInfo)
        ''' </summary>
        ''' <param name="fi">Source file</param>
        ''' <param name="targetFilename">Target filename (optional)</param>
        ''' <returns>
        ''' 1.2 [HR] amended code to not duplicate the file conversion (now in other upload)
        ''' </returns>
        Public Function Upload(ByVal fi As FileInfo, ByVal targetFilename As String) As Boolean
            'copy the file specified to target file: target file can be full path or just filename (uses current dir)
            '1. check target
            Dim target As String = targetFilename
			If String.IsNullOrEmpty(targetFilename) Then
                'Blank target: use source filename & current dir
                target = Me.CurrentDirectory + fi.Name
			End If

			Using fs As FileStream = fi.OpenRead()
				Try
					Return Upload(fs, target)

				Catch ex As Exception
                    'ensure source file is closed
                    fs.Close()
                    'do not handle, throw
                    Throw

				Finally

                    'ensure file closed
                    fs.Close()
				End Try
			End Using

			Return False
		End Function

        ''' <summary>
        ''' Upload a source stream to the FTP server 
        ''' </summary>
        ''' <param name="sourceStream">Source Stream</param>
        ''' <param name="targetFilename">Target filename</param>
        ''' <returns>true if downloaded, otherwise false</returns>
        ''' <remarks>
        ''' 1.2: [HR] added CreateURI
        ''' 1.2: [HR] amended routine to throw an exception on error
        ''' </remarks>
        Public Function Upload(ByVal sourceStream As Stream, ByVal targetFilename As String) As Boolean Implements IFTPClient.Upload
            'validate target file
            If String.IsNullOrEmpty(targetFilename) Then
				Throw New ApplicationException("Target filename must be specified")
			End If

            'Build full target URL
            Dim URI As String = CreateURI(targetFilename)
            'perform copy
            Dim ftp As System.Net.FtpWebRequest = GetRequest(URI)

            'Set request to upload a file in binary
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile
			ftp.UseBinary = True

            'Notify FTP of the expected size
            ftp.ContentLength = sourceStream.Length

            'create byte array to store: ensure at least 1 byte!
            Const BufferSize As Integer = 2048
			Dim content(BufferSize) As Byte
			Dim dataRead As Integer

            'open file for reading
            Using sourceStream
                'wrap in error handling
                Try
					sourceStream.Position = 0
                    'open request to send
                    Using rs As Stream = ftp.GetRequestStream()
						Do
							dataRead = sourceStream.Read(content, 0, BufferSize)
							rs.Write(content, 0, dataRead)
						Loop Until (dataRead < BufferSize)
						rs.Close()
					End Using

                    'ensure file closed
                    sourceStream.Close()
					Return True

				Catch
					sourceStream.Close()
					Throw

				End Try
			End Using

		End Function


#End Region

#Region "Download: File transfer FROM ftp server"
		''' <summary>
		''' Download: Copy a file from FTP server to local
		''' </summary>
		''' <param name="sourceFilename">Target filename, if required</param>
		''' <param name="localFilename">Full path of the local file</param>
		''' <returns></returns>
		''' <remarks>Target can be blank (use same filename), or just a filename
		''' (assumes current directory) or a full path and filename</remarks>
		Public Function Download(ByVal sourceFilename As String, ByVal localFilename As String, Optional ByVal PermitOverwrite As Boolean = False) As Boolean
            '2. determine target file
            Dim fi As New FileInfo(localFilename)
			Return Me.Download(sourceFilename, fi, PermitOverwrite)
		End Function

        ''' <summary>
        ''' Download: version taking FtpFileInfo and string filename
        ''' </summary>
        ''' <param name="file"></param>
        ''' <param name="localFilename"></param>
        ''' <param name="PermitOverwrite"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Download(ByVal file As FTPfileInfo, ByVal localFilename As String, Optional ByVal PermitOverwrite As Boolean = False) As Boolean
			Return Me.Download(file.FullName, localFilename, PermitOverwrite)
		End Function

        ''' <summary>
        ''' Download: version taking FTPFileInfo and FileInfo
        ''' </summary>
        ''' <param name="file"></param>
        ''' <param name="localFI"></param>
        ''' <param name="PermitOverwrite"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Download(ByVal file As FTPfileInfo, ByVal localFI As FileInfo, Optional ByVal PermitOverwrite As Boolean = False) As Boolean
			Return Me.Download(file.FullName, localFI, PermitOverwrite)
		End Function

        ''' <summary>
        ''' Download FTP file: version taking string/FileInfo
        ''' </summary>
        ''' <param name="sourceFilename"></param>
        ''' <param name="targetFI"></param>
        ''' <param name="PermitOverwrite"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 1.2 [HR] added CreateURI
        ''' </remarks>
        Public Function Download(ByVal sourceFilename As String, ByVal targetFI As FileInfo, Optional ByVal PermitOverwrite As Boolean = False) As Boolean
            '1. check target
            If targetFI.Exists And Not (PermitOverwrite) Then Throw New ApplicationException("Target file already exists")

            '2. check source
            If sourceFilename.Trim = "" Then Throw New ApplicationException("File not specified")

            '3. perform copy
            Dim URI As String = CreateURI(sourceFilename)
			Dim ftp As Net.FtpWebRequest = GetRequest(URI)

            'Set request to download a file in binary mode
            ftp.Method = Net.WebRequestMethods.Ftp.DownloadFile
			ftp.UseBinary = True

            'open request and get response stream
            Using response As FtpWebResponse = CType(ftp.GetResponse, FtpWebResponse)
				Using responseStream As Stream = response.GetResponseStream
                    'loop to read & write to file
                    Using fs As FileStream = targetFI.OpenWrite
						Try
							Dim buffer(2047) As Byte
							Dim read As Integer = 0
							Do
								read = responseStream.Read(buffer, 0, buffer.Length)
								fs.Write(buffer, 0, read)
							Loop Until read = 0
							responseStream.Close()
							fs.Flush()
							fs.Close()
						Catch
                            'catch error and delete file only partially downloaded
                            fs.Close()
                            'delete target file as it's incomplete
                            targetFI.Delete()
							Throw
						End Try
					End Using
					responseStream.Close()
				End Using
				response.Close()
			End Using

			Return True
		End Function

        ''' <summary>
        ''' Downloads the specified file from the remote FTP server.
        ''' </summary>
        ''' <param name="sourceFilename">The fully qualified path of the file to download (relative to the .</param>
        ''' <returns>A MemoryStream containing the file</returns>
        ''' <remarks>Added by DavidA, performs a download without performing local Disk IO</remarks>
        Public Function DownloadFile(ByVal sourceFilename As String) As MemoryStream Implements IFTPClient.DownloadFile
            '3. perform copy
            Dim URI As String = CreateURI(sourceFilename)
			Dim ftp As Net.FtpWebRequest = GetRequest(URI)

            'Set request to download a file in binary mode
            ftp.Method = Net.WebRequestMethods.Ftp.DownloadFile
			ftp.UseBinary = True

			Dim fs As New MemoryStream()

            'open request and get response stream
            Using response As FtpWebResponse = CType(ftp.GetResponse, FtpWebResponse)
				Using responseStream As Stream = response.GetResponseStream
                    'loop to read & write to file

                    Try
						Dim buffer(2047) As Byte
						Dim read As Integer = 0
						Do
							read = responseStream.Read(buffer, 0, buffer.Length)
							fs.Write(buffer, 0, read)
						Loop Until read = 0
						responseStream.Close()
						fs.Flush()
					Catch
                        'catch error and delete file only partially downloaded
                        fs.Close()
						Throw
					End Try
					responseStream.Close()
				End Using
				response.Close()
			End Using

			Return fs
		End Function



#End Region

#Region "Other functions: Delete rename etc."
		''' <summary>
		''' Delete remote file
		''' </summary>
		''' <param name="filename">filename or full path</param>
		''' <returns>True if file deleted successfully</returns>
		''' <remarks></remarks>
		Public Function FtpDelete(ByVal filename As String) As Boolean
            'Determine if file or full path
            Dim URI As String = CreateURI(filename)
			Dim ftp As Net.FtpWebRequest = GetRequest(URI)
            'Set request to delete
            ftp.Method = Net.WebRequestMethods.Ftp.DeleteFile
			Try
                'get response but ignore it
                Dim str As String = GetStringResponse(ftp)
				Return True

			Catch
				Throw
			End Try
		End Function

        ''' <summary>
        ''' Determine if file exists on remote FTP site
        ''' </summary>
        ''' <param name="filename">Filename (for current dir) or full path</param>
        ''' <returns></returns>
        ''' <remarks>Note this only works for files</remarks>
        Public Function FtpFileExists(ByVal filename As String) As Boolean
            'Try to obtain filesize: if we get error msg containing "550"
            'the file does not exist
            Try
				Dim size As Long = GetFileSize(filename)
				Return True

			Catch webex As System.Net.WebException
                'Dir not exists message is 550
                If webex.Message.Contains("550") Then Return False
                'other errors - fail
                Throw

			Catch
                'all other errors - throw
                Throw
			End Try
		End Function

        ''' <summary>
        ''' Determine if a directory exists on remote ftp server
        ''' </summary>
        ''' <param name="remoteDir">Directory path, e.g. /pub/test</param>
        ''' <returns>True if directory exists, otherwise false</returns>
        ''' <remarks></remarks>
        Public Function FtpDirectoryExists(ByVal remoteDir As String) As Boolean
			Try
                'Attempt directory listing - if it fails we catch the exception
                Dim files As List(Of String) = Me.ListDirectory(remoteDir)
				Return True

			Catch webex As System.Net.WebException
                'Should contain 550 error code if directory not found
                If webex.Message.Contains("550") Then Return False
                'all other errors, throw
                Throw

			Catch
                'all other exceptions throw
                Throw
			End Try
		End Function

        ''' <summary>
        ''' Determine size of remote file
        ''' </summary>
        ''' <param name="filename"></param>
        ''' <returns></returns>
        ''' <remarks>Throws an exception if file does not exist</remarks>
        Public Function GetFileSize(ByVal filename As String) As Long
			Dim URI As String = CreateURI(filename)
			Dim ftp As Net.FtpWebRequest = GetRequest(URI)
            'Try to get info on file/dir?
            ftp.Method = Net.WebRequestMethods.Ftp.GetFileSize
			Dim tmp As String = Me.GetStringResponse(ftp)
			Return GetSize(ftp)
		End Function

        ''' <summary>
        ''' Rename a remote file on FTP server
        ''' </summary>
        ''' <param name="sourceFilename">Full/partial remote file </param>
        ''' <param name="newName"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Source and target names should both be same type (partial or full paths)
        ''' 1.2: [HR] added CreateURI
        ''' </remarks>
        Public Function FtpRename(ByVal sourceFilename As String, ByVal newName As String) As Boolean
            'Does file exist?
            Dim source As String = GetFullPath(sourceFilename)
			If Not FtpFileExists(source) Then
				Throw New FileNotFoundException("File " & source & " not found")
			End If

            'build target name, ensure it does not exist
            Dim target As String = GetFullPath(newName)
			If target = source Then
				Throw New ApplicationException("Source and target are the same")
			ElseIf FtpFileExists(target) Then
				Throw New ApplicationException("Target file " & target & " already exists")
			End If

            'perform rename
            Dim URI As String = CreateURI(source)
			Dim ftp As Net.FtpWebRequest = GetRequest(URI)
            'Set request to delete
            ftp.Method = Net.WebRequestMethods.Ftp.Rename
			ftp.RenameTo = target
			Try
                'get response but ignore it
                Dim str As String = GetStringResponse(ftp)
			Catch
				Throw
			End Try
			Return True
		End Function

        ''' <summary>
        ''' Create a directory on remote FTP server
        ''' </summary>
        ''' <param name="dirpath"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 1.2 [HR] added CreateURI
        ''' </remarks>
        Public Function FtpCreateDirectory(ByVal dirpath As String) As Boolean Implements IFTPClient.FtpCreateDirectory
            'perform create	
            Dim URI As String = CreateURI(dirpath)
			Dim ftp As Net.FtpWebRequest = GetRequest(URI)
            'Set request to MkDir
            ftp.Method = Net.WebRequestMethods.Ftp.MakeDirectory
			Try
                'get response but ignore it
                Dim str As String = GetStringResponse(ftp)
			Catch
				Throw
			End Try
			Return True
		End Function

        ''' <summary>
        ''' Delete a directory on remote FTP server
        ''' </summary>
        ''' <param name="dirpath"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FtpDeleteDirectory(ByVal dirpath As String) As Boolean
            'perform remove
            Dim URI As String = CreateURI(dirpath)
            'Dim URI As String = Me.Hostname & AdjustDir(dirpath)
            Dim ftp As Net.FtpWebRequest = GetRequest(URI)
            'Set request to RmDir
            ftp.Method = Net.WebRequestMethods.Ftp.RemoveDirectory
			Try
                'get response but ignore it
                Dim str As String = GetStringResponse(ftp)
			Catch
				Throw
			End Try
			Return True
		End Function

        ''' <summary>
        ''' Obtain datetimestamp for remote file
        ''' </summary>
        ''' <param name="file"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDateTimestamp(ByVal file As FTPfileInfo) As Date
			Dim result As Date = Me.GetDateTimestamp(file.Filename)
			file.FileDateTime = result
			Return result
		End Function

        ''' <summary>
        ''' Obtain datetimestamp for remote file
        ''' </summary>
        ''' <param name="filename"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDateTimestamp(ByVal filename As String) As DateTime
			Dim URI As String = CreateURI(filename)
			Dim ftp As Net.FtpWebRequest = GetRequest(URI)
            'Try to get info on file/dir?
            ftp.Method = Net.WebRequestMethods.Ftp.GetDateTimestamp
			Return Me.GetLastModified(ftp)
		End Function

#End Region

#Region "private supporting fns"

		''' <summary>
		''' Ensure the data payload for URI is properly escaped
		''' </summary>
		''' <param name="filename">data after hostname to be escaped</param>
		''' <returns>properly escaped URI string</returns>
		''' <remarks>
		''' 1.2: added for fix to special chars issue
		''' 
		''' This function also checks for an adds relative path if required
		''' </remarks>
		Private Function CreateURI(ByVal filename As String) As String
            'adjust path
            Dim path As String
			If filename.Contains("/") Then
				path = AdjustDir(filename)
			Else
				path = Me.CurrentDirectory & filename
			End If
            'Escape path
            Dim escapedPath As String = GetEscapedPath(path)
			Return Me.Hostname & escapedPath
		End Function

        ''' <summary>
        ''' Get the basic FtpWebRequest object with the common settings and security
        ''' </summary>
        ''' <param name="URI"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetRequest(ByVal URI As String) As FtpWebRequest
            'create request
            Dim result As FtpWebRequest = CType(FtpWebRequest.Create(URI), FtpWebRequest)
            'Set the login details
            result.Credentials = GetCredentials()
            'Set SSL state 
            result.EnableSsl = EnableSSL
            'determine if connection should be kept alive
            result.KeepAlive = KeepAlive
            'set passivity? not supported though
            result.UsePassive = UsePassive
            'Support for proxy setttings
            result.Proxy = Proxy

			Return result
		End Function

        ''' <summary>
        ''' Ensure chars in path are correctly escaped e.g. #
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetEscapedPath(ByVal path As String) As String
            'split into directory parts
            Dim parts() As String = path.Split("/")
			Dim result As String = ""
			For Each part As String In parts
                'if not blank, escape with / prefix
                If Not String.IsNullOrEmpty(part) Then _
					result &= "/" & Uri.EscapeDataString(part)
			Next
			Return result
		End Function

        ''' <summary>
        ''' Get the credentials from username/password
        ''' </summary>
        ''' <remarks>
        ''' Updated to store the credentials on first use. This will
        ''' make KeepAlive=true work correctly when logging into server
        ''' </remarks>
        Private Function GetCredentials() As Net.ICredentials
			If _credentials Is Nothing Then
				_credentials = New Net.NetworkCredential(Username, Password)
			End If
			Return _credentials
		End Function
		Private _credentials As Net.NetworkCredential = Nothing


        ''' <summary>
        ''' returns a full path using CurrentDirectory for a relative file reference
        ''' </summary>
        Private Function GetFullPath(ByVal file As String) As String
			If file.Contains("/") Then
				Return AdjustDir(file)
			Else
				Return Me.CurrentDirectory & file
			End If
		End Function

        ''' <summary>
        ''' Amend an FTP path so that it always starts with /
        ''' </summary>
        ''' <param name="path">Path to adjust</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function AdjustDir(ByVal path As String) As String
			Return CStr(IIf(path.StartsWith("/"), "", "/")) & path
		End Function

        ''' <summary>
        ''' Build directory request URI
        ''' </summary>
        ''' <param name="directory"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 1.2 [HR] added CreateURI
        ''' </remarks>
        Private Function GetDirectory(Optional ByVal directory As String = "") As String
			Dim URI As String
			If directory = "" Then
                'build from current
                URI = CreateURI(Me.CurrentDirectory)
				_lastDirectory = Me.CurrentDirectory
			Else
				If Not directory.StartsWith("/") Then Throw New ApplicationException("Directory should start with /")
				URI = CreateURI(directory)
				_lastDirectory = directory
			End If
			Return URI
		End Function

        'stores last retrieved/set directory
        Private _lastDirectory As String = ""

        ''' <summary>
        ''' Obtains a response stream as a string
        ''' </summary>
        ''' <param name="ftp">current FTP request</param>
        ''' <returns>String containing response</returns>
        ''' <remarks>
        ''' FTP servers typically return strings with CR and
        ''' not CRLF. Use respons.Replace(vbCR, vbCRLF) to convert
        ''' to an MSDOS string
        ''' </remarks>
        Private Function GetStringResponse(ByVal ftp As FtpWebRequest) As String
            'Get the result, streaming to a string
            Dim result As String = ""
			Using response As FtpWebResponse = CType(ftp.GetResponse, FtpWebResponse)
				Dim size As Long = response.ContentLength
				Using datastream As Stream = response.GetResponseStream
					Using sr As New StreamReader(datastream, System.Text.Encoding.UTF8)
						result = sr.ReadToEnd()
						sr.Close()
					End Using
					datastream.Close()
				End Using
				response.Close()
			End Using
			Return result
		End Function

        ''' <summary>
        ''' Gets the size of an FTP request
        ''' </summary>
        ''' <param name="ftp"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetSize(ByVal ftp As FtpWebRequest) As Long
			Dim size As Long
			Using response As FtpWebResponse = CType(ftp.GetResponse, FtpWebResponse)
				size = response.ContentLength
				response.Close()
			End Using
			Return size
		End Function

        ''' <summary>
        ''' Internal function to get the modified datetime stamp via FTP
        ''' </summary>
        ''' <param name="ftp"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetLastModified(ByVal ftp As FtpWebRequest) As Date
			Dim lastmodified As Date
			Using response As FtpWebResponse = CType(ftp.GetResponse, FtpWebResponse)
				lastmodified = response.LastModified
				response.Close()
			End Using
			Return lastmodified
		End Function
#End Region

#Region "Properties"
		''' <summary>
		''' Hostname
		''' </summary>
		''' <value></value>
		''' <remarks>Hostname can be in either the full URL format
		''' ftp://ftp.myhost.com or just ftp.myhost.com
		''' </remarks>
		Public Property Hostname() As String Implements IFTPClient.Hostname
			Get
				If _hostname.StartsWith("ftp://") Then
					Return _hostname
				Else
					Return "ftp://" & _hostname
				End If
			End Get
			Set(ByVal value As String)
				_hostname = value
			End Set
		End Property
		Private _hostname As String

        ''' <summary>
        ''' Username property
        ''' </summary>
        ''' <value></value>
        ''' <remarks>Can be left blank, in which case 'anonymous' is returned</remarks>
        Public Property Username() As String
			Get
				Return IIf(_username = "", "anonymous", _username)
			End Get
			Set(ByVal value As String)
				_username = value
			End Set
		End Property
		Private _username As String

        ''' <summary>
        ''' Password for account
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Password() As String Implements IFTPClient.Password
			Get
				Return _password
			End Get
			Set(ByVal value As String)
				_password = value
			End Set
		End Property
		Private _password As String

        ''' <summary>
        ''' The CurrentDirectory value
        ''' </summary>
        ''' <remarks>Defaults to the root '/'</remarks>
        Public Property CurrentDirectory() As String Implements IFTPClient.CurrentDirectory
			Get
                'return directory, ensure it ends with /
                Return _currentDirectory & CStr(IIf(_currentDirectory.EndsWith("/"), "", "/"))
			End Get
			Set(ByVal value As String)
				If Not value.StartsWith("/") Then Throw New ApplicationException("Directory should start with /")
				_currentDirectory = value
			End Set
		End Property
		Private _currentDirectory As String = "/"

        ''' <summary>
        ''' Support for FTP over SSL
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>
        ''' Please note that FTP over SSL is NOT the same
        ''' as SFTP (secure FTP). I have no FTP server for this
        ''' so have not been able to test this.
        ''' </remarks>
        Public Property EnableSSL() As Boolean Implements IFTPClient.EnableSSL
			Get
				Return _enableSSL
			End Get
			Set(ByVal value As Boolean)
				_enableSSL = value
			End Set
		End Property
		Private _enableSSL As Boolean = False

        ''' <summary>
        ''' Support for KeepAlive (reusing FTP connection)
        ''' </summary>
        ''' <remarks></remarks>
        Public Property KeepAlive() As Boolean
			Get
				Return _keepAlive
			End Get
			Set(ByVal value As Boolean)
				_keepAlive = value
			End Set
		End Property
		Private _keepAlive As Boolean = False

        ''' <summary>
        ''' Provided for pass-through visibility but not supported/tested!
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UsePassive() As Boolean
			Get
				Return _usePassive
			End Get
			Set(ByVal value As Boolean)
				_usePassive = value
			End Set
		End Property
		Private _usePassive As Boolean = False

        ''' <summary>
        ''' Provided for pass-through visibility but not supported/tested
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Proxy() As System.Net.IWebProxy
			Get
				Return _proxy
			End Get
			Set(ByVal value As System.Net.IWebProxy)
				_proxy = value
			End Set
		End Property
		Private _proxy As IWebProxy = Nothing

#End Region

	End Class
#End Region

End Namespace
