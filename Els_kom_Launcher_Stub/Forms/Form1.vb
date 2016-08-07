Option Strict Off
Option Explicit On
Friend Class Form1
	Inherits System.Windows.Forms.Form

	Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		If FileExists(My.Application.Info.DirectoryPath & "\voidels.exe") Then
			Shell(My.Application.Info.DirectoryPath & "\voidels.exe", AppWinStyle.NormalFocus)
			Me.Close()
		Else
			If FileExists(My.Application.Info.DirectoryPath & "\elsword.exe") Then
				Shell(My.Application.Info.DirectoryPath & "\elsword.exe", AppWinStyle.NormalFocus)
				Me.Close()
			Else
				MsgBox("Can't find '" & My.Application.Info.DirectoryPath & "\elsword.exe'. Make sure the File Exists and try to update Elsword Again!", MsgBoxStyle.Critical, "Error!")
				Me.Close()
			End If
		End If
	End Sub
End Class