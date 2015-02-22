Imports System.IO

Namespace Utilities.FTP
	Public Interface IFTPClient
		Property EnableSSL As Boolean
		Function ListDirectory(Optional ByVal directory As String = "") As List(Of String)
		Function FtpCreateDirectory(folderName As String) As Boolean
		Function Upload(ByVal memoryStream As Stream, ByVal targetFilename As String) As Boolean
		Property CurrentDirectory As String
		Property Hostname As String
		Property Password As String
		Function DownloadFile(ByVal sourceFilename As String) As MemoryStream
		Function ListDirectoryDetail(
									Optional ByVal directory As String = "",
									Optional ByVal doDateTimeStamp As Boolean = False) As FTPdirectory
	End Interface
End Namespace
