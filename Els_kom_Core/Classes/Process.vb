Option Strict Off
Option Explicit On

Partial Class Classes
	Public Class Process
		Public Shared Function Shell(FileName As String, Arguments As String, RedirectStandardOutput As Boolean, UseShellExecute As Boolean, CreateNoWindow As Boolean, WindowStyle As ProcessWindowStyle, WorkingDirectory As String)
			Dim proc As New Diagnostics.Process
			proc.StartInfo.FileName = FileName
			proc.StartInfo.Arguments = Arguments
			proc.StartInfo.RedirectStandardOutput = RedirectStandardOutput
			proc.StartInfo.UseShellExecute = UseShellExecute
			proc.StartInfo.CreateNoWindow = CreateNoWindow
			proc.StartInfo.WindowStyle = WindowStyle
			proc.StartInfo.WorkingDirectory = WorkingDirectory
			proc.Start()
			Return proc.Id
		End Function
	End Class
End Class