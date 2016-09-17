Option Strict Off
Option Explicit On

Partial Class Classes
	''' <summary>
	''' Hosts a method that Overloads the Shell() Function. This is for Overloading the WorkingDirectory Method that bypasses some issues in x2.exe when shelling it. It Also Holds means of Detecting if the Launcher or x2.exe is currently Running.
	''' </summary>
	Public Class Process
		''' <summary>
		''' Overload for Shell() Function that Allows Overloading of the Working directory Variable. It must be a String but can be variables that returns strings.
		''' </summary>
		''' <returns>0</returns>
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
			proc.WaitForExit()  ' Required to have Detection on the process runnign to work right.
			Return 0
		End Function

		''' <summary>
		''' Gets if x2.exe is Running or Not.
		''' </summary>
		''' <returns>Boolean</returns>
		Public Shared Function IsX2Running() As Boolean
			Dim x2exe As Diagnostics.Process() = Diagnostics.Process.GetProcessesByName("Test_Mods")  ' Only Way for Detection to Work Right sadly.
			If x2exe.Count > 0 Then
				Return True
			Else
				Return False
			End If
			Return False
		End Function

		''' <summary>
		''' Gets if elsword.exe or voidels.exe is Running or Not.
		''' </summary>
		''' <returns>Boolean</returns>
		Public Shared Function IsLauncherRunning() As Boolean
			Dim elswordexe As Diagnostics.Process() = Diagnostics.Process.GetProcessesByName("elsword")
			Dim voidelsexe As Diagnostics.Process() = Diagnostics.Process.GetProcessesByName("voidels")
			If elswordexe.Count > 0 Then
				Return True
			Else
				'Due to support is needed for detecting the Void elsword Launcher it has to be here before returning False
				' to get if it is Void's Launcher being run instead of the official one.
				If voidelsexe.Count > 0 Then
					Return True
				Else
					Return False
				End If
			End If
			Return False
		End Function

		''' <summary>
		''' Bypasses Elsword's Integrity Check (makes it read checkkom.xml locally).
		''' </summary>
		Public Shared Function BypassIntegrityChecks() As Boolean
			' TODO: Add code that can bypass Integrity Checks without needing Fidler as GameGuard detects it.
			Return 0
		End Function

		''' <summary>
		''' Gets if Els_kom.exe is already Running. If So, Helps with Closing any new Instances.
		''' </summary>
		''' <returns>Boolean</returns>
		Public Shared Function IsElsKomRunning() As Boolean
			Dim els_komexe As Diagnostics.Process() = Diagnostics.Process.GetProcessesByName("Els_kom")
			If els_komexe.Count > 1 Then
				Return True
			Else
				Return False
			End If
			Return False
		End Function
	End Class
End Class