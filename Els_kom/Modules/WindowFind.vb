Option Strict Off
Option Explicit On
Module WindowFind

	Private Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer

	Public TargetName As String
	Public TargetHwnd As Integer
	' Return False to stop the enumeration.
	Public Function WindowEnumerator(ByVal app_hwnd As Integer, ByVal lparam As Integer) As Integer
		Dim buf As String
		Dim title As String
		Dim length As Integer

		' Get the window's title.
#Disable Warning BC42104
		length = GetWindowText(app_hwnd, buf.ToString(), 256)
#Enable Warning BC42104
		title = Left(buf.ToString(), length)

		' See if the title contains the target.
		If InStr(title, TargetName) > 0 Then
			' Save the hwnd and end the enumeration.
			TargetHwnd = app_hwnd
			WindowEnumerator = False
		Else
			' Continue the enumeration.
			WindowEnumerator = True
		End If
	End Function
End Module