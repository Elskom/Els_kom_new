<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class Form1
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public WithEvents Command5 As System.Windows.Forms.Button
	Public WithEvents Timer6 As System.Windows.Forms.Timer
	Public WithEvents Command4 As System.Windows.Forms.Button
	Public WithEvents Command3 As System.Windows.Forms.Button
	Public WithEvents Timer2 As System.Windows.Forms.Timer
	Public WithEvents Timer1 As System.Windows.Forms.Timer
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents Command2 As System.Windows.Forms.Button
	Public WithEvents Command1 As System.Windows.Forms.Button
	Public WithEvents Label2 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.Command5 = New System.Windows.Forms.Button()
		Me.Timer6 = New System.Windows.Forms.Timer(Me.components)
		Me.Command4 = New System.Windows.Forms.Button()
		Me.Command3 = New System.Windows.Forms.Button()
		Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
		Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.Frame1 = New System.Windows.Forms.GroupBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Command2 = New System.Windows.Forms.Button()
		Me.Command1 = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.PackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.UnpackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
		Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
		Me.TestModsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.LauncherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
		Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
		Me.Timer4 = New System.Windows.Forms.Timer(Me.components)
		Me.Frame1.SuspendLayout()
		Me.ContextMenuStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Command5
		'
		Me.Command5.BackColor = System.Drawing.Color.Transparent
		Me.Command5.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Command5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command5.Location = New System.Drawing.Point(8, 104)
		Me.Command5.Name = "Command5"
		Me.Command5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command5.Size = New System.Drawing.Size(65, 21)
		Me.Command5.TabIndex = 7
		Me.Command5.Text = "Launcher"
		Me.Command5.UseVisualStyleBackColor = False
		'
		'Timer6
		'
		Me.Timer6.Enabled = True
		Me.Timer6.Interval = 1
		'
		'Command4
		'
		Me.Command4.BackColor = System.Drawing.Color.Transparent
		Me.Command4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Command4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command4.Location = New System.Drawing.Point(8, 80)
		Me.Command4.Name = "Command4"
		Me.Command4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command4.Size = New System.Drawing.Size(65, 21)
		Me.Command4.TabIndex = 6
		Me.Command4.Text = "Test Mods"
		Me.Command4.UseVisualStyleBackColor = False
		'
		'Command3
		'
		Me.Command3.BackColor = System.Drawing.Color.Transparent
		Me.Command3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Command3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command3.Location = New System.Drawing.Point(8, 56)
		Me.Command3.Name = "Command3"
		Me.Command3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command3.Size = New System.Drawing.Size(65, 21)
		Me.Command3.TabIndex = 5
		Me.Command3.Text = "About"
		Me.Command3.UseVisualStyleBackColor = False
		'
		'Timer2
		'
		Me.Timer2.Interval = 1
		'
		'Timer1
		'
		Me.Timer1.Interval = 1
		'
		'Frame1
		'
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Controls.Add(Me.Label1)
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.Location = New System.Drawing.Point(80, 0)
		Me.Frame1.Name = "Frame1"
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Size = New System.Drawing.Size(217, 137)
		Me.Frame1.TabIndex = 2
		Me.Frame1.TabStop = False
		'
		'Label1
		'
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Location = New System.Drawing.Point(8, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.Size = New System.Drawing.Size(203, 122)
		Me.Label1.TabIndex = 3
		'
		'Command2
		'
		Me.Command2.BackColor = System.Drawing.Color.Transparent
		Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command2.Location = New System.Drawing.Point(8, 32)
		Me.Command2.Name = "Command2"
		Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command2.Size = New System.Drawing.Size(65, 21)
		Me.Command2.TabIndex = 1
		Me.Command2.Text = "Unpack"
		Me.Command2.UseVisualStyleBackColor = False
		'
		'Command1
		'
		Me.Command1.BackColor = System.Drawing.Color.Transparent
		Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command1.Location = New System.Drawing.Point(8, 8)
		Me.Command1.Name = "Command1"
		Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command1.Size = New System.Drawing.Size(65, 21)
		Me.Command1.TabIndex = 0
		Me.Command1.Text = "Pack"
		Me.Command1.UseVisualStyleBackColor = False
		'
		'Label2
		'
		Me.Label2.BackColor = System.Drawing.Color.Transparent
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Location = New System.Drawing.Point(0, 128)
		Me.Label2.Name = "Label2"
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.Size = New System.Drawing.Size(81, 17)
		Me.Label2.TabIndex = 4
		'
		'NotifyIcon1
		'
		Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
		Me.NotifyIcon1.Visible = True
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PackToolStripMenuItem, Me.UnpackToolStripMenuItem, Me.ToolStripMenuItem3, Me.SettingsToolStripMenuItem, Me.ToolStripMenuItem1, Me.TestModsToolStripMenuItem, Me.LauncherToolStripMenuItem, Me.ToolStripMenuItem2, Me.ExitToolStripMenuItem})
		Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
		Me.ContextMenuStrip1.Size = New System.Drawing.Size(130, 154)
		'
		'PackToolStripMenuItem
		'
		Me.PackToolStripMenuItem.Name = "PackToolStripMenuItem"
		Me.PackToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
		Me.PackToolStripMenuItem.Text = "Pack"
		'
		'UnpackToolStripMenuItem
		'
		Me.UnpackToolStripMenuItem.Name = "UnpackToolStripMenuItem"
		Me.UnpackToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
		Me.UnpackToolStripMenuItem.Text = "Unpack"
		'
		'ToolStripMenuItem3
		'
		Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
		Me.ToolStripMenuItem3.Size = New System.Drawing.Size(126, 6)
		'
		'SettingsToolStripMenuItem
		'
		Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
		Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
		Me.SettingsToolStripMenuItem.Text = "Settings"
		'
		'ToolStripMenuItem1
		'
		Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
		Me.ToolStripMenuItem1.Size = New System.Drawing.Size(126, 6)
		'
		'TestModsToolStripMenuItem
		'
		Me.TestModsToolStripMenuItem.Name = "TestModsToolStripMenuItem"
		Me.TestModsToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
		Me.TestModsToolStripMenuItem.Text = "Test Mods"
		'
		'LauncherToolStripMenuItem
		'
		Me.LauncherToolStripMenuItem.Name = "LauncherToolStripMenuItem"
		Me.LauncherToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
		Me.LauncherToolStripMenuItem.Text = "Launcher"
		'
		'ToolStripMenuItem2
		'
		Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
		Me.ToolStripMenuItem2.Size = New System.Drawing.Size(126, 6)
		'
		'ExitToolStripMenuItem
		'
		Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
		Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
		Me.ExitToolStripMenuItem.Text = "Exit"
		'
		'Timer3
		'
		Me.Timer3.Interval = 1
		'
		'Timer4
		'
		Me.Timer4.Interval = 1
		'
		'Form1
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ClientSize = New System.Drawing.Size(304, 146)
		Me.Controls.Add(Me.Command5)
		Me.Controls.Add(Me.Command4)
		Me.Controls.Add(Me.Command3)
		Me.Controls.Add(Me.Frame1)
		Me.Controls.Add(Me.Command2)
		Me.Controls.Add(Me.Command1)
		Me.Controls.Add(Me.Label2)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Location = New System.Drawing.Point(3, 23)
		Me.MaximizeBox = False
		Me.Name = "Form1"
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Els_kom v1.4.9.5"
		Me.Frame1.ResumeLayout(False)
		Me.ContextMenuStrip1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents NotifyIcon1 As NotifyIcon
	Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
	Friend WithEvents PackToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents UnpackToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
	Friend WithEvents TestModsToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents LauncherToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
	Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem3 As ToolStripSeparator
	Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents Timer3 As Timer
	Friend WithEvents Timer4 As Timer
#End Region
End Class