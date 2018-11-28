VERSION 5.00
Begin VB.Form Form3
	BorderStyle	=	1	'Fixed Single
	ClientHeight	=	15
	ClientLeft	=	45
	ClientTop	=	375
	ClientWidth	=	945
	LinkTopic	=	"Form3"
	MaxButton	=	0	'False
	MinButton	=	0	'False
	ScaleHeight	=	15
	ScaleWidth	=	945
	ShowInTaskbar	=	0	'False
	StartUpPosition	=	2	'CenterScreen
	Begin VB.Menu mnuShell
		Caption		=	"Shell"
		Begin VB.Menu mnuPack
			Caption		=	"Pack"
		End
		Begin VB.Menu mnuUnpack
			Caption		=	"Unpack"
		End
		Begin VB.Menu mnuSep
			Caption		=	"-"
		End
		Begin VB.Menu mnuTestMods
			Caption		=	"Test Mods"
		End
		Begin VB.Menu mnuLauncher
			Caption		=	"Launcher"
		End
		Begin VB.Menu mnuSep2
			Caption		=	"-"
		End
		Begin VB.Menu mnuExit
			Caption		=	"Exit"
		End
	End
End
Attribute VB_Name = "Form3"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub mnuExit_Click()
	'KillApp ("Els_kom.exe")
	'Call Form1.Form_QueryUnload
	Form1.Timer1.Enabled = False
	Form1.Timer2.Enabled = False
	Form1.Timer3.Enabled = False
	Form1.Timer4.Enabled = False
	Form1.Timer6.Enabled = False
	Unload Form2
	Unload Form3
	Unload Form4
	Unload Form1
End Sub

Private Sub mnuLauncher_Click()
	Form1.Timer6.Enabled = False
	Form1.Command1.Enabled = False
	Form1.Command2.Enabled = False
	Form1.Command4.Enabled = False
	Form1.Command5.Enabled = False
	mnuPack.Enabled = False
	mnuUnpack.Enabled = False
	mnuTestMods.Enabled = False
	mnuLauncher.Enabled = False
	WindowState = vbMinimized
	Form1.Timer3.Enabled = True
	Shell App.Path & "\elsword.exe"
End Sub

Private Sub mnuPack_Click()
	Form1.Timer2.Enabled = True
	If FileExists(App.Path & "\pack2.bat") Then
		Shell App.Path & "\pack2.bat", vbHide
	Else
		MsgBox "Can't find 'pack2.bat' and maybe also 'pack.bat'.", vbCritical, "Error!"
	End If
End Sub

Private Sub mnuTestMods_Click()
	'Disable a timer when doing this.
	'This has to be so that this timer doesnt conflict with another timer.
	Form1.Timer6.Enabled = False
	'Disable controls so that they cant do anything when Testing Mods.
	Form1.Command1.Enabled = False
	Form1.Command2.Enabled = False
	Form1.Command4.Enabled = False
	Form1.Command5.Enabled = False
	'Disable menu options so that they dont do anything while Testing Mods.
	mnuPack.Enabled = False
	mnuUnpack.Enabled = False
	mnuTestMods.Enabled = False
	mnuLauncher.Enabled = False
	'Set the Els_kom window to Minimized while Game is running with Mods.
	Form1.WindowState = vbMinimized
	'Enable Detection of Elsword Window so that way if Elsword isnt running to reenable controls.
	Shell App.Path & "\Test_Mods.exe"
	Form1.Timer3.Enabled = True
End Sub

Private Sub mnuUnpack_Click()
	Form1.Timer1.Enabled = True
	If FileExists(App.Path & "\unpack2.bat") Then
		Shell App.Path & "\unpack2.bat", vbHide
	Else
		MsgBox "Can't find 'unpack2.bat' and maybe also 'unpack.bat'.", vbCritical, "Error!"
	End If
End Sub
