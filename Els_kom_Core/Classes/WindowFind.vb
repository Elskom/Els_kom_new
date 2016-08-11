Option Strict Off
Option Explicit On

Partial Class Classes
	''' <summary>
	''' Do Not use this Class. It WILL be remvoed by Els_kom v1.5.0.0 Release with Algorithm 3. Maybe I could try to make a way to build in the kom unpackers to this Core. However it would need Crypto++ for Algorithm 3.
	''' </summary>
	Public Class WindowFind

		Private Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer

		Public Shared TargetName As String
		Public Shared TargetHwnd As Integer
		''' <summary>
		''' Old Do not use this Will be Replaced with something more Accurate even if the process **has** NO WINDOW.
		''' </summary>
		Public Shared Function WindowEnumerator(ByVal app_hwnd As Integer, ByVal lparam As Integer) As Integer
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
	End Class
End Class