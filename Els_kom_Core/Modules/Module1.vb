Option Strict Off
Option Explicit On

Public Class Module1
	Public Const MAX_PATH As Integer = 260
	Private Const ERROR_NO_MORE_FILES As Integer = 18
	Private Const FILE_ATTRIBUTE_NORMAL As Integer = &H80

	Private Structure FILETIME
		Dim dwLowDateTime As Integer
		Dim dwHighDateTime As Integer
	End Structure

	Private Structure WIN32_FIND_DATA
		Dim dwFileAttributes As Integer
		Dim ftCreationTime As FILETIME
		Dim ftLastAccessTime As FILETIME
		Dim ftLastWriteTime As FILETIME
		Dim nFileSizeHigh As Integer
		Dim nFileSizeLow As Integer
		Dim dwReserved0 As Integer
		Dim dwReserved1 As Integer
		<VBFixedString(MAX_PATH), Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=MAX_PATH)> Public cFileName As Char()
		<VBFixedString(14), Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=14)> Public cAlternate As Char()
	End Structure

	Private Declare Function FindFirstFile Lib "kernel32" Alias "FindFirstFileA" (ByVal lpFileName As String, ByRef lpFindFileData As WIN32_FIND_DATA) As Integer
	Private Declare Function FindClose Lib "kernel32" (ByVal hFindFile As Integer) As Integer
	Private Declare Function FindNextFile Lib "kernel32" Alias "FindNextFileA" (ByVal hFindFile As Integer, ByRef lpFindFileData As WIN32_FIND_DATA) As Integer

	Public Shared Function FileExists(ByVal sFile As String) As Boolean
		Dim lpFindFileData As WIN32_FIND_DATA
		Dim lFileHandle As Integer
		Dim lRet As Integer
		Dim sTemp As String
		Dim sFileExtension As String
		Dim sFileName As String
		Dim sFileData As String()
		Dim sFileToCompare As String

		Try
			If IsDirectory(sFile) = True Then
				sFile = AddSlash(sFile) & "*.*"
			End If

			If InStr(sFile, ".") > 0 Then
				sFileToCompare = GetFileTitle(sFile)
				sFileData = Split(sFileToCompare, ".")
				sFileName = sFileData(0)
				sFileExtension = sFileData(1)
			Else
#Disable Warning S3385
				Exit Function
#Enable Warning S3385
			End If

			' get a file handle
#Disable Warning BC42108
			lFileHandle = FindFirstFile(sFile, lpFindFileData)
#Enable Warning BC42108
			If lFileHandle <> -1 Then
				If sFileName = "*" Or sFileExtension = "*" Then
					FileExists = True
				Else
					Do Until lRet = ERROR_NO_MORE_FILES
						' if it is a file
						If (lpFindFileData.dwFileAttributes And FILE_ATTRIBUTE_NORMAL) = vbNormal Then
							sTemp = StrConv(RemoveNull(lpFindFileData.cFileName), VbStrConv.ProperCase)

							'remove LCase$ if you want the search to be case sensitive
							If LCase(sTemp) = LCase(sFileToCompare) Then
								FileExists = True ' file found
#Disable Warning S3385
								Exit Do
#Enable Warning S3385
							End If
						End If
						'based on the file handle iterate through all files and dirs
						lRet = FindNextFile(lFileHandle, lpFindFileData)
#Disable Warning S3385
						If lRet = 0 Then Exit Do
#Enable Warning S3385
					Loop
				End If
			End If

			' close the file handle
			lRet = FindClose(lFileHandle)
		Catch Ex As Exception
			'Do nothing here.
		End Try
#Disable Warning BC42353
	End Function
#Enable Warning BC42353

	Private Shared Function IsDirectory(ByVal sFile As String) As Boolean
		IsDirectory = ((GetAttr(sFile) And FileAttribute.Directory) = FileAttribute.Directory)
	End Function

	Private Shared Function RemoveNull(ByVal strString As String) As String
		Dim intZeroPos As Short

		intZeroPos = InStr(strString, Chr(0))
		If intZeroPos > 0 Then
			RemoveNull = Left(strString, intZeroPos - 1)
		Else
			RemoveNull = strString
		End If
	End Function

	Public Shared Function GetFileTitle(ByVal sFileName As String) As String
		GetFileTitle = Right(sFileName, Len(sFileName) - InStrRev(sFileName, "\"))
	End Function

	Public Shared Function AddSlash(ByVal strDirectory As String) As String
		If InStrRev(strDirectory, "\") <> Len(strDirectory) Then
			strDirectory = strDirectory & "\"
		End If
		AddSlash = strDirectory
	End Function
End Class