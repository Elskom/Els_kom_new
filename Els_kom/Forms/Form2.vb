Option Strict Off
Option Explicit On
Friend Class Form2
	Inherits Form
	Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

	Public Function OpenBrowser(ByVal URL As String) As Boolean
		Dim res As Integer
		If InStr(1, URL, "http", CompareMethod.Text) <> 1 Then
			URL = "http://" & URL
		End If
		res = ShellExecute(0, "open", URL, vbNullString, vbNullString, AppWinStyle.NormalFocus)
		OpenBrowser = (res > 32)
	End Function

	Private Sub cmdOK_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdOK.Click
		Me.Close()
	End Sub

	Private Sub Form2_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Load
		Label1.Text = "1"
		Me.Icon = Els_kom_Core.My.Resources.els_kom_icon
		picIcon.Image = Els_kom_Core.My.Resources.els_kom
		Picture1.Image = Els_kom_Core.My.Resources.bmp100
		Picture2.Image = Els_kom_Core.My.Resources.bmp101
	End Sub

	Private Sub Form2_MouseMove(ByVal eventSender As Object, ByVal eventArgs As MouseEventArgs) Handles MyBase.MouseMove
		Picture1.Visible = True
		Picture2.Visible = False
	End Sub

	Private Sub Picture1_MouseMove(ByVal eventSender As Object, ByVal eventArgs As MouseEventArgs) Handles Picture1.MouseMove
		Picture1.Visible = False
		Picture2.Visible = True
	End Sub

	Private Sub Picture2_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Picture2.Click
		Picture1.Visible = True
		Picture2.Visible = False
		OpenBrowser("www.elsword.to/forum/index.php?/topic/51000-updated-els-kom-v1492-working-as-of-8-7-16/")
	End Sub

	Private Sub lblDisclaimer_MouseMove(sender As Object, e As MouseEventArgs) Handles lblDisclaimer.MouseMove
		Picture1.Visible = True
		Picture2.Visible = False
	End Sub

	Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
		Label1.Text = "0"
	End Sub
End Class