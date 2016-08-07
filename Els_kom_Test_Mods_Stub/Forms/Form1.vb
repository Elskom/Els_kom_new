Option Strict Off
Option Explicit On
Friend Class Form1
	Inherits Form

	Private Sub Form1_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Load
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\data\x2.exe") Then
			Shell(My.Application.Info.DirectoryPath & "\data\x2.exe" & " pxk19slammsu286nfha02kpqnf729ck", AppWinStyle.NormalFocus)
			Me.Close()
		Else
			MsgBox("Can't find '" & My.Application.Info.DirectoryPath & "\data\x2.exe'. Make sure the File Exists and try to Test your mods Again!", MsgBoxStyle.Critical, "Error!")
			Me.Close()
		End If
	End Sub
End Class