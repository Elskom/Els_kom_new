<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class Form2
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
	Public WithEvents Text1 As System.Windows.Forms.TextBox
	Public WithEvents cmdOK As System.Windows.Forms.Button
	Public WithEvents picIcon As System.Windows.Forms.PictureBox
	Public WithEvents Picture1 As System.Windows.Forms.PictureBox
	Public WithEvents Picture2 As System.Windows.Forms.PictureBox
	Public WithEvents lblDisclaimer As System.Windows.Forms.Label
	Public WithEvents lblTitle As System.Windows.Forms.Label
	Public WithEvents lblDescription As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
		Me.Text1 = New System.Windows.Forms.TextBox()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.Picture1 = New System.Windows.Forms.PictureBox()
		Me.Picture2 = New System.Windows.Forms.PictureBox()
		Me.lblDisclaimer = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.lblDescription = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Picture2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Text1
		'
		Me.Text1.AcceptsReturn = True
		Me.Text1.BackColor = System.Drawing.SystemColors.Control
		Me.Text1.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.Text1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Text1.ForeColor = System.Drawing.SystemColors.WindowText
		Me.Text1.Location = New System.Drawing.Point(8, 80)
		Me.Text1.MaxLength = 0
		Me.Text1.Multiline = True
		Me.Text1.Name = "Text1"
		Me.Text1.ReadOnly = True
		Me.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Text1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.Text1.Size = New System.Drawing.Size(361, 65)
		Me.Text1.TabIndex = 7
		Me.Text1.Text = resources.GetString("Text1.Text")
		'
		'cmdOK
		'
		Me.cmdOK.BackColor = System.Drawing.Color.Transparent
		Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdOK.Location = New System.Drawing.Point(280, 195)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdOK.Size = New System.Drawing.Size(84, 23)
		Me.cmdOK.TabIndex = 2
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = False
		'
		'picIcon
		'
		Me.picIcon.BackColor = System.Drawing.Color.Transparent
		Me.picIcon.Cursor = System.Windows.Forms.Cursors.Default
		Me.picIcon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.picIcon.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picIcon.Location = New System.Drawing.Point(18, 16)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 1
		Me.picIcon.TabStop = False
		'
		'Picture1
		'
		Me.Picture1.BackColor = System.Drawing.Color.Transparent
		Me.Picture1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Picture1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Picture1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Picture1.Location = New System.Drawing.Point(256, 160)
		Me.Picture1.Name = "Picture1"
		Me.Picture1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Picture1.Size = New System.Drawing.Size(66, 15)
		Me.Picture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
		Me.Picture1.TabIndex = 0
		Me.Picture1.TabStop = False
		'
		'Picture2
		'
		Me.Picture2.BackColor = System.Drawing.Color.Transparent
		Me.Picture2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Picture2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Picture2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Picture2.Location = New System.Drawing.Point(256, 160)
		Me.Picture2.Name = "Picture2"
		Me.Picture2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Picture2.Size = New System.Drawing.Size(66, 15)
		Me.Picture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
		Me.Picture2.TabIndex = 3
		Me.Picture2.TabStop = False
		'
		'lblDisclaimer
		'
		Me.lblDisclaimer.BackColor = System.Drawing.SystemColors.Control
		Me.lblDisclaimer.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblDisclaimer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDisclaimer.ForeColor = System.Drawing.Color.Black
		Me.lblDisclaimer.Location = New System.Drawing.Point(8, 160)
		Me.lblDisclaimer.Name = "lblDisclaimer"
		Me.lblDisclaimer.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblDisclaimer.Size = New System.Drawing.Size(249, 31)
		Me.lblDisclaimer.TabIndex = 6
		Me.lblDisclaimer.Text = "Visit my Forum topic here if you would like to post something about my Els_kom."
		'
		'lblTitle
		'
		Me.lblTitle.BackColor = System.Drawing.Color.Transparent
		Me.lblTitle.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblTitle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.ForeColor = System.Drawing.Color.Black
		Me.lblTitle.Location = New System.Drawing.Point(74, 32)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblTitle.Size = New System.Drawing.Size(259, 16)
		Me.lblTitle.TabIndex = 5
		Me.lblTitle.Text = "Els_kom v1.4.9.4"
		'
		'lblDescription
		'
		Me.lblDescription.BackColor = System.Drawing.Color.Transparent
		Me.lblDescription.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDescription.ForeColor = System.Drawing.Color.Black
		Me.lblDescription.Location = New System.Drawing.Point(72, 48)
		Me.lblDescription.Name = "lblDescription"
		Me.lblDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblDescription.Size = New System.Drawing.Size(282, 30)
		Me.lblDescription.TabIndex = 4
		Me.lblDescription.Text = "This tool allows you to Edit koms freely. Also this is a tool that replaces gPatc" &
	"her but with some limitations. l0l"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(137, 201)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(13, 14)
		Me.Label1.TabIndex = 12
		Me.Label1.Text = "0"
		Me.Label1.Visible = False
		'
		'Form2
		'
		Me.AcceptButton = Me.cmdOK
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.CancelButton = Me.cmdOK
		Me.ClientSize = New System.Drawing.Size(382, 227)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Text1)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.Picture1)
		Me.Controls.Add(Me.Picture2)
		Me.Controls.Add(Me.lblDisclaimer)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.lblDescription)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Location = New System.Drawing.Point(3, 25)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "Form2"
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "About"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Picture2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Label1 As Label
#End Region
End Class