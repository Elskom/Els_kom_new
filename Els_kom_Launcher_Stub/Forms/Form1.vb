Option Strict Off
Option Explicit On
Friend Class Form1
	Inherits Form

	Private ElsDir As String
	Private Sub Form1_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Load
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\Settings.ini") Then
			ElsDir = Els_kom_Core.Classes.INIread.ReadIniValue(My.Application.Info.DirectoryPath & "\Settings.ini", "Settings.ini", "ElsDir")
			If ElsDir.Length > 0 Then
				If IO.File.Exists(ElsDir & "\voidels.exe") Then
					Els_kom_Core.Classes.Process.Shell(ElsDir & "\voidels.exe", "", False, False, False, AppWinStyle.NormalFocus, ElsDir)
					Me.Close()
				Else
					If IO.File.Exists(ElsDir & "\elsword.exe") Then
						Els_kom_Core.Classes.Process.Shell(ElsDir & "\elsword.exe", "", False, False, False, AppWinStyle.NormalFocus, ElsDir)
						Me.Close()
					Else
						MsgBox("Can't find '" & ElsDir & "\elsword.exe'. Make sure the File Exists and try to update Elsword Again!", MsgBoxStyle.Critical, "Error!")
						Me.Close()
					End If
				End If
			Else
				MsgBox("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to update Elsword Again!", MsgBoxStyle.Critical, "Error!")
				Me.Close()
			End If
		Else
			MsgBox("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to update Elsword Again!", MsgBoxStyle.Critical, "Error!")
			Me.Close()
		End If
	End Sub
End Class