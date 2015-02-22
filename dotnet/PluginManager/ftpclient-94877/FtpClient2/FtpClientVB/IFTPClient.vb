Imports System.IO

Namespace Utilities.FTP
	Public Interface IFTPClient
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
		Property EnableSSL As Boolean

		''' <summary>
		''' Return a simple directory listing
		''' </summary>
		''' <param name="directory">Directory to list, e.g. /pub</param>
		''' <returns>A list of filenames and directories including file extensions.</returns>
		''' <remarks>
		''' For a detailed directory listing, use ListDirectoryDetail
		''' </remarks>
		Function ListDirectory(Optional ByVal directory As String = "") As List(Of String)

		''' <summary>
		''' Create a directory on remote FTP server
		''' </summary>
		''' <param name="folderName"></param>
		''' <returns></returns>
		''' <remarks>
		''' 1.2 [HR] added CreateURI
		''' </remarks>
		Function FtpCreateDirectory(folderName As String) As Boolean

		''' <summary>
		''' Copy a local file to the FTP server (local filename as string)
		''' </summary>
		''' <param name="memoryStream">Contents of the file to upload</param>
		''' <param name="targetFilename">Target filename, if required</param>
		''' <returns></returns>
		''' <remarks>If the target filename is blank, the source filename is used
		''' (assumes current directory). Otherwise use a filename to specify a name
		''' or a full path and filename if required.</remarks>
		Function Upload(ByVal memoryStream As Stream, ByVal targetFilename As String) As Boolean


		''' <summary>
		''' The CurrentDirectory value
		''' </summary>
		Property CurrentDirectory As String

		''' <summary>
		''' Hostname
		''' </summary>
		''' <value></value>
		''' <remarks>Hostname can be in either the full URL format
		''' ftp://ftp.myhost.com or just ftp.myhost.com
		''' </remarks>
		Property Hostname As String

		''' <summary>
		''' Password for account
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Property Password As String

		''' <summary>
		''' Downloads the specified file from the remote FTP server.
		''' </summary>
		''' <param name="sourceFilename">The fully qualified path of the file to download (relative to the .</param>
		''' <returns>A MemoryStream containing the file</returns>
		Function DownloadFile(ByVal sourceFilename As String) As MemoryStream

		''' <summary>
		''' Return a detailed directory listing
		''' </summary>
		''' <param name="directory">Directory to list, e.g. /pub/etc</param>
		''' <param name="doDateTimeStamp">Boolean: set to True to also download the file date/time stamps</param>
		''' <returns>An FTPDirectory object</returns>
		Function ListDirectoryDetail(
									Optional ByVal directory As String = "",
									Optional ByVal doDateTimeStamp As Boolean = False) As FTPdirectory
	End Interface
End Namespace
