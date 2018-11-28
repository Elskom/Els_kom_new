VERSION 5.00
Begin VB.Form Form2 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "About"
   ClientHeight    =   3855
   ClientLeft      =   45
   ClientTop       =   375
   ClientWidth     =   5730
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3855
   ScaleMode       =   0  'User
   ScaleWidth      =   5125.816
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.PictureBox Picture3 
      AutoSize        =   -1  'True
      BorderStyle     =   0  'None
      Height          =   225
      Left            =   3840
      ScaleHeight     =   225
      ScaleWidth      =   990
      TabIndex        =   8
      Top             =   3000
      Width           =   990
   End
   Begin VB.TextBox Text1 
      BackColor       =   &H8000000F&
      Height          =   975
      Left            =   120
      Locked          =   -1  'True
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   7
      Text            =   "Form2.frx":0000
      Top             =   1200
      Width           =   5415
   End
   Begin VB.CommandButton cmdOK 
      Cancel          =   -1  'True
      Caption         =   "OK"
      Default         =   -1  'True
      Height          =   345
      Left            =   4200
      TabIndex        =   2
      Top             =   3360
      Width           =   1260
   End
   Begin VB.PictureBox picIcon 
      BorderStyle     =   0  'None
      ClipControls    =   0   'False
      Height          =   720
      Left            =   270
      ScaleHeight     =   379.26
      ScaleMode       =   0  'User
      ScaleWidth      =   417.736
      TabIndex        =   1
      Top             =   240
      Width           =   720
   End
   Begin VB.PictureBox Picture1 
      AutoSize        =   -1  'True
      BorderStyle     =   0  'None
      Height          =   225
      Left            =   3840
      ScaleHeight     =   225
      ScaleWidth      =   990
      TabIndex        =   0
      Top             =   2400
      Width           =   990
   End
   Begin VB.PictureBox Picture2 
      AutoSize        =   -1  'True
      BorderStyle     =   0  'None
      Height          =   225
      Left            =   3840
      ScaleHeight     =   225
      ScaleWidth      =   990
      TabIndex        =   3
      Top             =   2400
      Width           =   990
   End
   Begin VB.PictureBox Picture4 
      AutoSize        =   -1  'True
      BorderStyle     =   0  'None
      Height          =   225
      Left            =   3840
      ScaleHeight     =   225
      ScaleWidth      =   990
      TabIndex        =   9
      Top             =   3000
      Width           =   990
   End
   Begin VB.Label Label1 
      Caption         =   "Visit the Eloquence Topic today if you would like to Join or you want more information about Eloquence."
      Height          =   615
      Left            =   120
      TabIndex        =   10
      Top             =   3000
      Width           =   3615
   End
   Begin VB.Label lblDisclaimer 
      Caption         =   "Visit my Forum topic here if you would like to post something about my Els_kom."
      ForeColor       =   &H00000000&
      Height          =   465
      Left            =   120
      TabIndex        =   6
      Top             =   2400
      Width           =   3870
      WordWrap        =   -1  'True
   End
   Begin VB.Label lblTitle 
      BackStyle       =   0  'Transparent
      Caption         =   "Els_kom v1.4.6.9"
      ForeColor       =   &H00000000&
      Height          =   360
      Left            =   1110
      TabIndex        =   5
      Top             =   480
      Width           =   3885
   End
   Begin VB.Label lblDescription 
      BackStyle       =   0  'Transparent
      Caption         =   "This tool allows you to Edit koms freely. Also this is a tool that replaces gPatcher but with some limitations. l0l"
      ForeColor       =   &H00000000&
      Height          =   450
      Left            =   1080
      TabIndex        =   4
      Top             =   720
      Width           =   3885
   End
   Begin VB.Line Line3 
      BorderColor     =   &H00808080&
      BorderStyle     =   6  'Inside Solid
      Index           =   2
      X1              =   -67.092
      X2              =   5475.588
      Y1              =   2265
      Y2              =   2265
   End
   Begin VB.Line Line4 
      BorderColor     =   &H00FFFFFF&
      Index           =   3
      X1              =   -67.092
      X2              =   5481.85
      Y1              =   2280
      Y2              =   2280
   End
End
Attribute VB_Name = "Form2"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long

' Open the default browser on a given URL
' Returns True if successful, False otherwise
Public Function OpenBrowser(ByVal URL As String) As Boolean
        Dim res As Long
        ' it is mandatory that the URL is prefixed with http:// or https://
        If InStr(1, URL, "http", vbTextCompare) <> 1 Then
                URL = "http://" & URL
        End If
        res = ShellExecute(0&, "open", URL, vbNullString, vbNullString, _
                vbNormalFocus)
        OpenBrowser = (res > 32)
End Function

Private Sub cmdOK_Click()
        Unload Me
End Sub

Private Sub Form_Load()
        Form2.Icon = LoadResPicture(1, vbResIcon)
        picIcon.Picture = LoadResPicture(1, vbResIcon)
        Picture1.Picture = LoadResPicture(100, vbResBitmap)
        Picture2.Picture = LoadResPicture(101, vbResBitmap)
        Picture3.Picture = LoadResPicture(100, vbResBitmap)
        Picture4.Picture = LoadResPicture(101, vbResBitmap)
End Sub

Private Sub Form_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
        Picture1.Visible = True
        Picture2.Visible = False
        Picture3.Visible = True
        Picture4.Visible = False
End Sub

Private Sub Picture1_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
        Picture1.Visible = False
        Picture2.Visible = True
End Sub

Private Sub Picture2_Click()
        Picture1.Visible = True
        Picture2.Visible = False
        OpenBrowser "www.elsword.to/forum/index.php?/topic/51000-updated-els-kom-v1469-working-as-of-8-21-15/"
End Sub

Private Sub Picture3_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
        Picture3.Visible = False
        Picture4.Visible = True
End Sub

Private Sub Picture4_Click()
        Picture3.Visible = True
        Picture4.Visible = False
        OpenBrowser "http://tinyurl.com/voideloquence"
End Sub
