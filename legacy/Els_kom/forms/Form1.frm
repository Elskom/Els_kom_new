VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Els_kom v1.4.6.9"
   ClientHeight    =   2190
   ClientLeft      =   45
   ClientTop       =   345
   ClientWidth     =   4560
   Icon            =   "Form1.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   2190
   ScaleWidth      =   4560
   StartUpPosition =   2  'CenterScreen
   Begin VB.Timer Timer4 
      Enabled         =   0   'False
      Interval        =   1
      Left            =   0
      Top             =   0
   End
   Begin VB.Timer Timer3 
      Enabled         =   0   'False
      Interval        =   1
      Left            =   0
      Top             =   0
   End
   Begin VB.CommandButton Command5 
      Caption         =   "Launcher"
      Height          =   255
      Left            =   120
      TabIndex        =   7
      Top             =   1560
      Width           =   975
   End
   Begin VB.Timer Timer6 
      Interval        =   1
      Left            =   0
      Top             =   0
   End
   Begin VB.CommandButton Command4 
      Caption         =   "Test Mods"
      Height          =   255
      Left            =   120
      TabIndex        =   6
      Top             =   1200
      Width           =   975
   End
   Begin VB.CommandButton Command3 
      Caption         =   "About"
      Height          =   255
      Left            =   120
      TabIndex        =   5
      Top             =   840
      Width           =   975
   End
   Begin VB.Timer Timer2 
      Enabled         =   0   'False
      Interval        =   1
      Left            =   0
      Top             =   0
   End
   Begin VB.Timer Timer1 
      Enabled         =   0   'False
      Interval        =   1
      Left            =   0
      Top             =   0
   End
   Begin VB.Frame Frame1 
      Height          =   2055
      Left            =   1200
      TabIndex        =   2
      Top             =   0
      Width           =   3255
      Begin VB.Label Label1 
         Height          =   1695
         Left            =   120
         TabIndex        =   3
         Top             =   120
         Width           =   3015
      End
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Unpack"
      Height          =   255
      Left            =   120
      TabIndex        =   1
      Top             =   480
      Width           =   975
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Pack"
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   975
   End
   Begin VB.Label Label2 
      BackStyle       =   0  'Transparent
      Height          =   255
      Left            =   0
      TabIndex        =   4
      Top             =   1920
      Width           =   1215
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
'User-defined variable to pass to the Shell_NotiyIcon function
Private Type NOTIFYICONDATA
        cbSize As Long
        hwnd As Long
        uId As Long
        uFlags As Long
        uCallBackMessage As Long
        hIcon As Long
        szTip As String * 64
End Type

'Constants for the Shell_NotifyIcon function
Private Const NIM_ADD = &H0
Private Const NIM_MODIFY = &H1
Private Const NIM_DELETE = &H2

Private Const WM_MOUSEMOVE = &H200

Private Const NIF_MESSAGE = &H1
Private Const NIF_ICON = &H2
Private Const NIF_TIP = &H4

Private Const WM_LBUTTONDBLCLK = &H203
Private Const WM_LBUTTONDOWN = &H201
Private Const WM_LBUTTONUP = &H202
Private Const WM_RBUTTONDBLCLK = &H206
Private Const WM_RBUTTONDOWN = &H204
Private Const WM_RBUTTONUP = &H205

'Declare the API function call
Private Declare Function Shell_NotifyIcon Lib "shell32" Alias "Shell_NotifyIconA" _
        (ByVal dwMessage As Long, pnid As NOTIFYICONDATA) As Boolean

'Private Declare Function EnumWindows Lib "user32" (ByVal lpEnumFunc _
As Long, ByVal lparam As Long) As Long

Dim nid As NOTIFYICONDATA

Public Sub AddIcon(ByVal ToolTip As String)

        On Error GoTo ErrorHandler
    
        'Add icon to system tray
        With nid
                .cbSize = Len(nid)
                .hwnd = Me.hwnd
                .uId = vbNull
                .uFlags = NIF_ICON Or NIF_TIP Or NIF_MESSAGE
                .uCallBackMessage = WM_MOUSEMOVE
                .hIcon = Me.Icon
                .szTip = ToolTip & vbNullChar
        End With
        Call Shell_NotifyIcon(NIM_ADD, nid)
        
Exit Sub
ErrorHandler: 'Display error message
        Screen.MousePointer = vbDefault
        MsgBox Err.Description, vbInformation, App.ProductName & " - " & Me.Caption

End Sub
Public Sub ModifyIcon(ByVal ToolTip As String)

        On Error GoTo ErrorHandler
    
        'Add icon to system tray
        With nid
                .cbSize = Len(nid)
                .hwnd = Me.hwnd
                .uId = vbNull
                .uFlags = NIF_ICON Or NIF_TIP Or NIF_MESSAGE
                .uCallBackMessage = WM_MOUSEMOVE
                .hIcon = Me.Icon
                .szTip = ToolTip & vbNullChar
        End With
        Call Shell_NotifyIcon(NIM_MODIFY, nid)
        
Exit Sub
ErrorHandler: 'Display error message
        Screen.MousePointer = vbDefault
        MsgBox Err.Description, vbInformation, App.ProductName & " - " & Me.Caption

End Sub

Private Sub Command1_Click()
        Timer2.Enabled = True
        If FileExists(App.Path & "\pack2.bat") Then
                Shell App.Path & "\pack2.bat", vbHide
        Else
                MsgBox "Can't find 'pack2.bat' and maybe also 'pack.bat'.", vbCritical, "Error!"
        End If
End Sub

Private Sub Command1_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
        Label1.Caption = "This command uses Python 2.7  to Pack koms."
End Sub

Private Sub Command2_Click()
        Timer1.Enabled = True
        If FileExists(App.Path & "\unpack2.bat") Then
                Shell App.Path & "\unpack2.bat", vbHide
        Else
                MsgBox "Can't find 'unpack2.bat' and maybe also 'unpack.bat'.", vbCritical, "Error!"
        End If
End Sub

Private Sub Command2_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
        Label1.Caption = "This command uses Python 2.7  to Unpack koms."
End Sub

Private Sub Command3_Click()
        Form2.Show vbModal
End Sub

Private Sub Command3_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
        Label1.Caption = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed."
End Sub

Private Sub Command4_Click()
        'Timer6.Enabled = False
        'Command1.Enabled = False
        'Command2.Enabled = False
        'Command4.Enabled = False
        'Command5.Enabled = False
        'Form3.mnuPack.Enabled = False
        'Form3.mnuUnpack.Enabled = False
        'Form3.mnuTestMods.Enabled = False
        'Form3.mnuLauncher.Enabled = False
        'Form1.WindowState = vbMinimized
        Label2.Caption = "Testing Mods..."
        Call ModifyIcon(Label2.Caption)
        Shell App.Path & "\Test_Mods.exe"
        'Timer3.Enabled = True
End Sub

Private Sub Command4_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
        Label1.Caption = "Test the mods you made.(kom files must be in same folder as this program as for a unknown reason.)"
End Sub

Private Sub Command5_Click()
        'Timer6.Enabled = False
        'Command1.Enabled = False
        'Command2.Enabled = False
        'Command4.Enabled = False
        'Command5.Enabled = False
        'Form3.mnuPack.Enabled = False
        'Form3.mnuUnpack.Enabled = False
        'Form3.mnuTestMods.Enabled = False
        'Form3.mnuLauncher.Enabled = False
        'Form1.WindowState = vbMinimized
        Shell App.Path & "\Launcher.exe"
        'Timer3.Enabled = True
End Sub

Private Sub Command5_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
        Label1.Caption = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens.(you might have to remake some mods  for some files.)"
End Sub

Private Sub Form_Load()
        Form1.Icon = LoadResPicture(1, vbResIcon)
        Call AddIcon(Form1.Caption)
End Sub

Private Sub Form_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
        Label1.Caption = ""
Dim msg As Long

        On Error GoTo ErrorHandler
    
        'Respond to user interaction
        msg = X / Screen.TwipsPerPixelX
        Select Case msg
                        
                Case WM_LBUTTONDBLCLK
                        'nothing
        
                Case WM_LBUTTONDOWN
                        'nothing
        
                Case WM_LBUTTONUP
                        If Me.WindowState = vbMinimized Then
                                Me.WindowState = vbNormal
                                'Me.Show
                                'Me.ShowInTaskbar = True
                        Else
                                Me.WindowState = vbMinimized
                                'Me.Hide
                                'Me.ShowInTaskbar = True
                        End If
                
                Case WM_RBUTTONDBLCLK
                        'nothing
                
                Case WM_RBUTTONDOWN
                        'nothing
                
                Case WM_RBUTTONUP
                        Call PopupMenu(Form3.mnuShell, vbPopupMenuRightAlign)
        End Select
Exit Sub
ErrorHandler: 'Display error message
        Screen.MousePointer = vbDefault
        MsgBox Err.Description, vbInformation, App.ProductName & " - " & Me.Caption

End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer3.Enabled = False
        Timer4.Enabled = False
        'Timer5.Enabled = False
        Timer6.Enabled = False
        'Timer7.Enabled = False
End Sub

Private Sub Timer1_Timer()
        If FileExists(App.Path & "\unpacking.unpack") Then
                Timer6.Enabled = False
                Command1.Enabled = False
                Command2.Enabled = False
                Command4.Enabled = False
                Command5.Enabled = False
                Form3.mnuPack.Enabled = False
                Form3.mnuUnpack.Enabled = False
                Form3.mnuTestMods.Enabled = False
                Form3.mnuLauncher.Enabled = False
                Label2.Caption = "Unpacking..."
                Call ModifyIcon(Label2.Caption)
        Else
                Timer6.Enabled = True
                Command1.Enabled = True
                Command2.Enabled = True
                Command4.Enabled = True
                Command5.Enabled = True
                Form3.mnuPack.Enabled = True
                Form3.mnuUnpack.Enabled = True
                Form3.mnuTestMods.Enabled = True
                Form3.mnuLauncher.Enabled = True
                Label2.Caption = ""
                Call ModifyIcon(Form1.Caption)
        End If
End Sub

Private Sub Timer2_Timer()
        If FileExists(App.Path & "\packing.pack") Then
                Timer6.Enabled = False
                Command1.Enabled = False
                Command2.Enabled = False
                Command4.Enabled = False
                Command5.Enabled = False
                Form3.mnuPack.Enabled = False
                Form3.mnuUnpack.Enabled = False
                Form3.mnuTestMods.Enabled = False
                Form3.mnuLauncher.Enabled = False
                Label2.Caption = "Packing..."
                Call ModifyIcon(Label2.Caption)
        Else
                Timer6.Enabled = True
                Command1.Enabled = True
                Command2.Enabled = True
                Command4.Enabled = True
                Command5.Enabled = True
                Form3.mnuPack.Enabled = True
                Form3.mnuUnpack.Enabled = True
                Form3.mnuTestMods.Enabled = True
                Form3.mnuLauncher.Enabled = True
                Label2.Caption = ""
                Call ModifyIcon(Form1.Caption)
        End If
End Sub

Private Sub Timer3_Timer()
        ' Use EnumWindows to see if the window exists.
        'TargetName = "Elsword - "
        'TargetHwnd = 0
        
        ' Examine the window names.
        'EnumWindows AddressOf WindowEnumerator, 0
        
        ' See if we got an hwnd.
        'If TargetHwnd = 0 Then
                'Timer4.Enabled = True
                'Timer3.Enabled = False
        'Else
        'End If
End Sub

Private Sub Timer4_Timer()
        ' Use EnumWindows to see if the window exists.
        'TargetName = "Elsword - "
        'TargetHwnd = 0
        
        ' Examine the window names.
        'EnumWindows AddressOf WindowEnumerator, 0
        
        ' See if we got an hwnd.
        'If TargetHwnd = 0 Then
                'Timer4.Enabled = True
                'Timer6.Enabled = False
                'Command1.Enabled = False
                'Command2.Enabled = False
                'Command4.Enabled = False
                'Command5.Enabled = False
                'Form3.mnuPack.Enabled = False
                'Form3.mnuUnpack.Enabled = False
                'Form3.mnuTestMods.Enabled = False
                'Form3.mnuLauncher.Enabled = False
                
        'Else
                'Timer4.Enabled = False
                'Timer6.Enabled = True
                'Command1.Enabled = True
                'Command2.Enabled = True
                'Command4.Enabled = True
                'Command5.Enabled = True
                'Form3.mnuPack.Enabled = True
                'Form3.mnuUnpack.Enabled = True
                'Form3.mnuTestMods.Enabled = True
                'Form3.mnuLauncher.Enabled = True
                'Label2.Caption = ""
                'Call ModifyIcon(Form1.Caption)
        'End If
End Sub

Private Sub Timer6_Timer()
        If Me.WindowState = vbNormal Then
                Form1.Height = 2595
                Form1.Width = 4650
        End If
        If FileExists(App.Path & "\Elsword.exe") Then
                Command5.Enabled = True
        Else
                Command5.Enabled = False
        End If
        If FileExists(App.Path & "\data\x2.exe") Then
                Command4.Enabled = True
        Else
                Command4.Enabled = False
        End If
End Sub
