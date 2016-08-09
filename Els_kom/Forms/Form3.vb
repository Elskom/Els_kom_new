Public Class Form3

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		FolderBrowserDialog1.ShowDialog()
		Timer1.Enabled = True
	End Sub

	Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
		If TextBox1.Text.Length > 0 Then
			Els_kom_Core.Classes.INIwrite.WriteIniValue(My.Application.Info.DirectoryPath & "\Settings.ini", "Settings.ini", "ElsDir", TextBox1.Text)
		End If
	End Sub

	Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.Icon = Els_kom_Core.My.Resources.els_kom_icon
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\Settings.ini") Then
			TextBox1.Text = Els_kom_Core.Classes.INIread.ReadIniValue(My.Application.Info.DirectoryPath & "\Settings.ini", "Settings.ini", "ElsDir")
		End If
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Me.Close()
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		If FolderBrowserDialog1.SelectedPath.Length > 0 Then
			TextBox1.Text = FolderBrowserDialog1.SelectedPath
			Timer1.Enabled = False
		End If
	End Sub
End Class