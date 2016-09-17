Option Strict Off
Option Explicit On
Friend Class Form1
	Inherits Form

	Private ElsDir As String
	Private Only1InstanceError As Boolean

	Private Sub Command1_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Command1.Click
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\pack.bat") Then
			IO.File.Create(My.Application.Info.DirectoryPath & "\packing.pack").Close()
			Shell(My.Application.Info.DirectoryPath & "\pack.bat", AppWinStyle.Hide)
			Timer2.Enabled = True
		Else
			MsgBox("Can't find 'pack.bat'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub Command1_MouseMove(ByVal eventSender As Object, ByVal eventArgs As MouseEventArgs) Handles Command1.MouseMove
		Label1.Text = "This uses kompact_new.exe to Pack koms."
	End Sub

	Private Sub Command2_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Command2.Click
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\unpack.bat") Then
			IO.File.Create(My.Application.Info.DirectoryPath & "\unpacking.unpack").Close()
			Shell(My.Application.Info.DirectoryPath & "\unpack.bat", AppWinStyle.Hide)
			Timer1.Enabled = True
		Else
			MsgBox("Can't find 'unpack.bat'.", MsgBoxStyle.Critical, "Error!")
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
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\Test_Mods.exe") Then
			If Me.WindowState = FormWindowState.Normal Then
				Me.WindowState = FormWindowState.Minimized
			End If
			If Me.ShowInTaskbar = True Then
				Me.ShowInTaskbar = False
			End If
			Label1.Text = ""
			Timer3.Enabled = True
		Else
			MsgBox("Can't find '" & My.Application.Info.DirectoryPath & "\Test_Mods.exe'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub Command4_MouseMove(ByVal eventSender As Object, ByVal eventArgs As MouseEventArgs) Handles Command4.MouseMove
		Label1.Text = "Test the mods you made."
	End Sub

	Private Sub Command5_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Command5.Click
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\Launcher.exe") Then
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
		Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens. (you might have to remake some mods for some files)"
	End Sub

	Private Sub Form1_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Load
		Dim previnstance As Boolean
		Me.Icon = Els_kom_Core.My.Resources.els_kom_icon
		NotifyIcon1.Icon = Me.Icon
		NotifyIcon1.Text = Me.Text
		previnstance = Els_kom_Core.Classes.Process.IsElsKomRunning()
		If previnstance = True Then
			Only1InstanceError = True
			MsgBox("Sorry, Only 1 Instance is allowed at a time.", MsgBoxStyle.Critical, "Error!")
			Me.Close()
		End If
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\Settings.ini") Then
			ElsDir = Els_kom_Core.Classes.INIread.ReadIniValue(My.Application.Info.DirectoryPath & "\Settings.ini", "Settings.ini", "ElsDir")
			If ElsDir.Length > 0 Then
				'The Setting actually exists and is not a empty String so we do not need to open the dialog again.
			Else
				MsgBox("Welcome to Els_kom." & Environment.NewLine & "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." & Environment.NewLine & "Also if you encounter any bugs or other things take a look at the Issue Tracker.", MsgBoxStyle.Information, "Welcome!")
				Form3.ShowDialog()
			End If
		Else
			MsgBox("Welcome to Els_kom." & Environment.NewLine & "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." & Environment.NewLine & "Also if you encounter any bugs or other things take a look at the Issue Tracker.", MsgBoxStyle.Information, "Welcome!")
			Form3.ShowDialog()
		End If
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
		If IO.File.Exists(My.Application.Info.DirectoryPath & " \ unpacking.unpack") Then
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
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\packing.pack") Then
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
			If IO.File.Exists(My.Application.Info.DirectoryPath & "\Launcher.exe") Then
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
			If IO.File.Exists(My.Application.Info.DirectoryPath & "\Test_Mods.exe") Then
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
		If Only1InstanceError = True Then
			'The shell "Tray" Icon should do nothing.
		Else
			If Form2.Label1.Text = 1 Then
				'I have to Sadly disable left button on the Notify Icon to prevent a bug with Form2 Randomly Unloading or not reshowing.
			Else
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
			End If
		End If
	End Sub

	Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
		Timer1.Enabled = False
		Timer2.Enabled = False
		Timer6.Enabled = False
		Form2.Close()
		Me.Close()
	End Sub

	Private Sub LauncherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LauncherToolStripMenuItem.Click
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\Launcher.exe") Then
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
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\unpack.bat") Then
			IO.File.Create(My.Application.Info.DirectoryPath & "\unpacking.unpack").Close()
			Shell(My.Application.Info.DirectoryPath & "\unpack.bat", AppWinStyle.Hide)
			Timer1.Enabled = True
		Else
			MsgBox("Can't find 'unpack.bat'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub TestModsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestModsToolStripMenuItem.Click
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\Test_Mods.exe") Then
			If Me.WindowState = FormWindowState.Normal Then
				Me.WindowState = FormWindowState.Minimized
			End If
			If Me.ShowInTaskbar = True Then
				Me.ShowInTaskbar = False
			End If
			Timer3.Enabled = True
		Else
			Label1.Text = ""
			MsgBox("Can't find '" & My.Application.Info.DirectoryPath & "\Test_Mods.exe'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub PackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PackToolStripMenuItem.Click
		If IO.File.Exists(My.Application.Info.DirectoryPath & "\pack.bat") Then
			IO.File.Create(My.Application.Info.DirectoryPath & "\packing.pack").Close()
			Shell(My.Application.Info.DirectoryPath & "\pack.bat", AppWinStyle.Hide)
			Timer2.Enabled = True
		Else
			MsgBox("Can't find 'pack.bat'.", MsgBoxStyle.Critical, "Error!")
		End If
	End Sub

	Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
		Form3.ShowDialog()
	End Sub

	Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
		Timer6.Enabled = False
		Timer3.Enabled = False
		Command1.Enabled = False
		Command2.Enabled = False
		Command4.Enabled = False
		Command5.Enabled = False
		PackToolStripMenuItem.Enabled = False
		UnpackToolStripMenuItem.Enabled = False
		TestModsToolStripMenuItem.Enabled = False
		LauncherToolStripMenuItem.Enabled = False
		' TODO: Copy All KOM Files on this part.
		Shell(My.Application.Info.DirectoryPath & "\Test_Mods.exe")
		Timer4.Interval = 1
		Timer4.Enabled = True
	End Sub

	Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
		Dim x2bool As Boolean
		x2bool = Els_kom_Core.Classes.Process.IsX2Running()
		If (x2bool = True) Then
			Timer4.Interval = 1
			Label2.Text = "Testing Mods..."
		Else
			Command1.Enabled = True
			Command2.Enabled = True
			Command4.Enabled = True
			Command5.Enabled = True
			PackToolStripMenuItem.Enabled = True
			UnpackToolStripMenuItem.Enabled = True
			TestModsToolStripMenuItem.Enabled = True
			LauncherToolStripMenuItem.Enabled = True
			Label2.Text = ""
			Timer6.Enabled = True
			Timer4.Enabled = False
		End If
	End Sub
End Class