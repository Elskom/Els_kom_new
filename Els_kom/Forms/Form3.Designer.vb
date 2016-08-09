<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
		Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.SuspendLayout()
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(95, 13)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(248, 20)
		Me.TextBox1.TabIndex = 0
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(349, 12)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(60, 20)
		Me.Button1.TabIndex = 1
		Me.Button1.Text = "Browse"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(2, 14)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(92, 13)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "Elsword Directory:"
		'
		'Button2
		'
		Me.Button2.Location = New System.Drawing.Point(342, 49)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(68, 20)
		Me.Button2.TabIndex = 3
		Me.Button2.Text = "Ok"
		Me.Button2.UseVisualStyleBackColor = True
		'
		'FolderBrowserDialog1
		'
		Me.FolderBrowserDialog1.Description = "Select the Folder that Your Elsword Install is in (Must be the one that either el" &
	"sword.exe or voidels.exe is in)."
		Me.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer
		Me.FolderBrowserDialog1.ShowNewFolderButton = False
		'
		'Timer1
		'
		Me.Timer1.Interval = 1
		'
		'Form3
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(418, 81)
		Me.Controls.Add(Me.Button2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Button1)
		Me.Controls.Add(Me.TextBox1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Name = "Form3"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Settings"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents Button1 As Button
	Friend WithEvents Label1 As Label
	Friend WithEvents Button2 As Button
	Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
	Friend WithEvents Timer1 As Timer
End Class
