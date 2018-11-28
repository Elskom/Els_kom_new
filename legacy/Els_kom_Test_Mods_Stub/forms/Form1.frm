VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Els_kom_Test_Mods_Stub"
   ClientHeight    =   630
   ClientLeft      =   45
   ClientTop       =   345
   ClientWidth     =   1350
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   630
   ScaleWidth      =   1350
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Visible         =   0   'False
   Begin VB.TextBox Text1 
      Height          =   375
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   1095
   End
   Begin VB.Timer Timer2 
      Interval        =   1
      Left            =   0
      Top             =   0
   End
   Begin VB.Timer Timer1 
      Interval        =   1
      Left            =   0
      Top             =   0
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
'there is a known issue with this EnumWindows Function where the Application would not
'want to close Unless you use Task Manager.
'Do not USE unless you know how to fix this issue.
'Private Declare Function EnumWindows Lib "user32" (ByVal lpEnumFunc As Long, ByVal lparam As Long) As Long

Private Sub Form_Load()
    If FileExists(App.Path & "\data\x2.exe") Then
        Shell App.Path & "\data\x2.exe" & " pxk19slammsu286nfha02kpqnf729ck"
        'Timer1.Enabled = True
        Unload Me
    Else
        MsgBox "Can't find '" & App.Path & "\data\x2.exe'. Make sure the File Exists and try to Test your mods Again!", vbCritical, "Error!"
        Unload Me
    End If
End Sub

'Private Sub Timer1_Timer()
        ' Use EnumWindows to see if the window exists.
'        TargetName = "Elsword - "
'        TargetHwnd = 0

        ' Examine the window names.
'        EnumWindows AddressOf WindowEnumerator, 0
'
        ' See if we got an hwnd.
'        If TargetHwnd = 0 Then
'                Timer2.Enabled = True
'                Timer1.Enabled = False
'        Else
'        End If
'End Sub

'Private Sub Timer2_Timer()
        ' Use EnumWindows to see if the window exists.
'        TargetName = "Elsword - "
'        TargetHwnd = 0
'
        ' Examine the window names.
'        EnumWindows AddressOf WindowEnumerator, 0
'
        ' See if we got an hwnd.
'        If TargetHwnd = 0 Then
'        Else
'                Timer2.Enabled = False
'                Unload Me
'        End If
'End Sub
