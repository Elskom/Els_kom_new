Option Strict Off
Option Explicit On
Friend Class Form1
	Inherits Form

	Private ElsDir As String
	Private Sub Form1_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Load
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\Settings.ini") Then
			ElsDir = Els_kom_Core.Classes.INIread.ReadIniValue(My.Application.Info.DirectoryPath & "\Settings.ini", "Settings.ini", "ElsDir")
			If ElsDir.Length > 0 Then
				If IO.File.Exists(ElsDir & "\data\x2.exe") Then
					Els_kom_Core.Classes.Process.Shell(ElsDir & "\data\x2.exe", "pxk19slammsu286nfha02kpqnf729ck", False, False, False, AppWinStyle.NormalFocus, ElsDir & "\data\")
					Me.Close()
				Else
					MsgBox("Can't find '" & ElsDir & "\data\x2.exe'. Make sure the File Exists and try to Test your mods Again!", MsgBoxStyle.Critical, "Error!")
					Me.Close()
				End If
			Else
				MsgBox("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to Test your mods Again!", MsgBoxStyle.Critical, "Error!")
				Me.Close()
			End If
		Else
			MsgBox("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to Test your mods Again!", MsgBoxStyle.Critical, "Error!")
			Me.Close()
		End If
	End Sub
End Class