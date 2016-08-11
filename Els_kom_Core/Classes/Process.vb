Option Strict Off
Option Explicit On

Partial Class Classes
	''' <summary>
	''' Hosts a method that Overloads the Shell() Function. This is for Overloadign the WorkingDirectory Method that bypasses some issues in x2.exe when shelling it.
	''' </summary>
	Public Class Process
		''' <summary>
		''' Overload for Shell() Function that Allows Overloading of the Working directory Mariable. It must be a String but can be variables that returns strings.
		''' </summary>
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