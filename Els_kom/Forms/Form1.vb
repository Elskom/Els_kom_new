Option Strict Off
Option Explicit On
Friend Class Form1
	Inherits Form

	Private Sub Command1_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Command1.Click
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\pack2.bat") Then
			Timer2.Enabled = True
			Shell(My.Application.Info.DirectoryPath & "\pack2.bat", AppWinStyle.Hide)
		Else
			MsgBox("Can't find 'pack2.bat' and maybe also 'pack.bat'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub Command1_MouseMove(ByVal eventSender As Object, ByVal eventArgs As MouseEventArgs) Handles Command1.MouseMove
		Label1.Text = "This uses kompact_new.exe to Pack koms."
	End Sub

	Private Sub Command2_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Command2.Click
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\unpack2.bat") Then
			Timer1.Enabled = True
			Shell(My.Application.Info.DirectoryPath & "\unpack2.bat", AppWinStyle.Hide)
		Else
			MsgBox("Can't find 'unpack2.bat' and maybe also 'unpack.bat'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub Command2_MouseMove(ByVal eventSender As Object, ByVal eventArgs As MouseEventArgs) Handles Command2.MouseMove
		Label1.Text = "This uses komextract_new.exe to Unpack koms."
	End Sub

	Private Sub Command3_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Command3.Click
		Form2.ShowDialog()
	End Sub

	Private Sub Command3_MouseMove(ByVal eventSender As Object, ByVal eventArgs As MouseEventArgs) Handles Command3.MouseMove
		Label1.Text = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed."
	End Sub

	Private Sub Command4_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Command4.Click
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\Test_Mods.exe") Then
			If Me.WindowState = FormWindowState.Normal Then
				Me.WindowState = FormWindowState.Minimized
			End If
			If Me.ShowInTaskbar = True Then
				Me.ShowInTaskbar = False
			End If
			Label1.Text = ""
			Shell(My.Application.Info.DirectoryPath & "\Test_Mods.exe")
		Else
			MsgBox("Can't find 'Test_Mods.exe'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub Command4_MouseMove(ByVal eventSender As Object, ByVal eventArgs As MouseEventArgs) Handles Command4.MouseMove
		Label1.Text = "Test the mods you made.(kom files must be in same folder as this program as for a unknown reason.)"
	End Sub

	Private Sub Command5_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Command5.Click
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\Launcher.exe") Then
			If Me.WindowState = FormWindowState.Normal Then
				Me.WindowState = FormWindowState.Minimized
			End If
			If Me.ShowInTaskbar = True Then
				Me.ShowInTaskbar = False
			End If
			Label1.Text = ""
			Shell(My.Application.Info.DirectoryPath & "\Launcher.exe")
		Else
			MsgBox("Can't find 'Launcher.exe'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub Command5_MouseMove(ByVal eventSender As Object, ByVal eventArgs As MouseEventArgs) Handles Command5.MouseMove
		Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens.(you might have to remake some mods for some files.)"
	End Sub

	Private Sub Form1_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Load
		Me.Icon = Els_kom_Core.My.Resources.els_kom_icon
		NotifyIcon1.Icon = Me.Icon
		NotifyIcon1.Text = Me.Text
	End Sub

	Private Sub Form1_MouseMove(ByVal eventSender As Object, ByVal eventArgs As MouseEventArgs) Handles MyBase.MouseMove
		Label1.Text = ""
	End Sub

	Private Sub Form1_FormClosing(ByVal eventSender As Object, ByVal eventArgs As FormClosingEventArgs) Handles Me.FormClosing
		Dim Cancel As Boolean = eventArgs.Cancel
		Dim UnloadMode As CloseReason = eventArgs.CloseReason
		eventArgs.Cancel = Cancel
	End Sub

	Private Sub Timer1_Tick(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Timer1.Tick
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\unpacking.unpack") Then
			Timer6.Enabled = False
			Command1.Enabled = False
			Command2.Enabled = False
			Command4.Enabled = False
			Command5.Enabled = False
			PackToolStripMenuItem.Enabled = False
			UnpackToolStripMenuItem.Enabled = False
			TestModsToolStripMenuItem.Enabled = False
			LauncherToolStripMenuItem.Enabled = False
			Label2.Text = "Unpacking..."
			NotifyIcon1.Text = Label2.Text
		Else
			Timer1.Enabled = False
			Timer6.Enabled = True
			Command1.Enabled = True
			Command2.Enabled = True
			Command4.Enabled = True
			Command5.Enabled = True
			PackToolStripMenuItem.Enabled = True
			UnpackToolStripMenuItem.Enabled = True
			TestModsToolStripMenuItem.Enabled = True
			LauncherToolStripMenuItem.Enabled = True
			Label2.Text = ""
			NotifyIcon1.Text = Me.Text
		End If
	End Sub

	Private Sub Timer2_Tick(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Timer2.Tick
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\packing.pack") Then
			Timer6.Enabled = False
			Command1.Enabled = False
			Command2.Enabled = False
			Command4.Enabled = False
			Command5.Enabled = False
			PackToolStripMenuItem.Enabled = False
			UnpackToolStripMenuItem.Enabled = False
			TestModsToolStripMenuItem.Enabled = False
			LauncherToolStripMenuItem.Enabled = False
			Label2.Text = "Packing..."
			NotifyIcon1.Text = Label2.Text
		Else
			Timer2.Enabled = False
			Timer6.Enabled = True
			Command1.Enabled = True
			Command2.Enabled = True
			Command4.Enabled = True
			Command5.Enabled = True
			PackToolStripMenuItem.Enabled = True
			UnpackToolStripMenuItem.Enabled = True
			TestModsToolStripMenuItem.Enabled = True
			LauncherToolStripMenuItem.Enabled = True
			Label2.Text = ""
			NotifyIcon1.Text = Me.Text
		End If
	End Sub

	Private Sub Timer6_Tick(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Timer6.Tick
		Dim checkiflauncherstubexists As Boolean
		Dim checkiftestmodsstubexists As Boolean

		If Me.WindowState = FormWindowState.Normal Then
			Me.Height = 184
			Me.Width = 320
		End If
		If checkiflauncherstubexists = False Then
			checkiflauncherstubexists = True
		End If
		If checkiftestmodsstubexists = False Then
			checkiftestmodsstubexists = True
		End If
		If checkiflauncherstubexists = True Then
			If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\Launcher.exe") Then
				LauncherToolStripMenuItem.Enabled = True
				Command5.Enabled = True
				checkiflauncherstubexists = False
			Else
				LauncherToolStripMenuItem.Enabled = False
				Command5.Enabled = False
				checkiflauncherstubexists = False
			End If
		End If
		If checkiftestmodsstubexists = True Then
			If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\Test_Mods.exe") Then
				TestModsToolStripMenuItem.Enabled = True
				Command4.Enabled = True
				checkiftestmodsstubexists = False
			Else
				TestModsToolStripMenuItem.Enabled = False
				Command4.Enabled = False
				checkiftestmodsstubexists = False
			End If
		End If
	End Sub

	Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
		Try
			If Form2.ActiveForm.WindowState = FormWindowState.Normal Then
				'I have to Sadly disable left button on the Notify Icon to prevent a bug with Form2 Randomly Unloading or not reshowing.
			End If
		Catch ex As Exception
			If e.Button = MouseButtons.Left Then
				If Me.WindowState = FormWindowState.Minimized Then
					If Me.ShowInTaskbar = False Then
						Me.ShowInTaskbar = True
					End If
					Me.WindowState = FormWindowState.Normal
				Else
					Me.WindowState = FormWindowState.Minimized
				End If
			End If
		End Try
	End Sub

	Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
		Timer1.Enabled = False
		Timer2.Enabled = False
		Timer6.Enabled = False
		Form2.Close()
		Me.Close()
	End Sub

	Private Sub LauncherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LauncherToolStripMenuItem.Click
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\Launcher.exe") Then
			If Me.WindowState = FormWindowState.Normal Then
				Me.WindowState = FormWindowState.Minimized
			End If
			If Me.ShowInTaskbar = True Then
				Me.ShowInTaskbar = False
			End If
			Shell(My.Application.Info.DirectoryPath & "\Launcher.exe")
		Else
			Label1.Text = ""
			MsgBox("Can't find 'Launcher.exe'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub UnpackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnpackToolStripMenuItem.Click
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\unpack2.bat") Then
			Timer1.Enabled = True
			Shell(My.Application.Info.DirectoryPath & "\unpack2.bat", AppWinStyle.Hide)
		Else
			MsgBox("Can't find 'unpack2.bat' and maybe also 'unpack.bat'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub TestModsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestModsToolStripMenuItem.Click
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\Test_Mods.exe") Then
			If Me.WindowState = FormWindowState.Normal Then
				Me.WindowState = FormWindowState.Minimized
			End If
			If Me.ShowInTaskbar = True Then
				Me.ShowInTaskbar = False
			End If
			Shell(My.Application.Info.DirectoryPath & "\Test_Mods.exe")
		Else
			Label1.Text = ""
			MsgBox("Can't find 'Test_Mods.exe'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub PackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PackToolStripMenuItem.Click
		If Els_kom_Core.Module1.FileExists(My.Application.Info.DirectoryPath & "\pack2.bat") Then
			Timer2.Enabled = True
			Shell(My.Application.Info.DirectoryPath & "\pack2.bat", AppWinStyle.Hide)
		Else
			MsgBox("Can't find 'pack2.bat' and maybe also 'pack.bat'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub
End Class