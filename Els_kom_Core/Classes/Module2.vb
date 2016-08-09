Option Strict Off
Option Explicit On

Partial Class Classes
	Public Class Module2

		Private Const MAX_PATH As Integer = 260

		Private Declare Function TerminateProcess Lib "kernel32" (ByVal ApphProcess As Integer, ByVal uExitCode As Integer) As Integer
		Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal blnheritHandle As Integer, ByVal dwAppProcessId As Integer) As Integer
		Private Declare Function ProcessFirst Lib "kernel32" Alias "Process32First" (ByVal hSnapshot As Integer, ByRef uProcess As PROCESSENTRY32) As Integer
		Private Declare Function ProcessNext Lib "kernel32" Alias "Process32Next" (ByVal hSnapshot As Integer, ByRef uProcess As PROCESSENTRY32) As Integer
		Private Declare Function CreateToolhelpSnapshot Lib "kernel32" Alias "CreateToolhelp32Snapshot" (ByVal lFlags As Integer, ByRef lProcessID As Integer) As Integer
		Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer

		Private Structure LUID
			Dim lowpart As Integer
			Dim highpart As Integer
		End Structure

		Private Structure TOKEN_PRIVILEGES
			Dim PrivilegeCount As Integer
			Dim LuidUDT As LUID
			Dim Attributes As Integer
		End Structure

		Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20
		Const TOKEN_QUERY As Integer = &H8
		Const SE_PRIVILEGE_ENABLED As Integer = &H2
		Const PROCESS_ALL_ACCESS As Integer = &H1F0FFF

		Private Declare Function GetVersion Lib "kernel32" () As Integer
		Private Declare Function GetCurrentProcess Lib "kernel32" () As Integer
		Private Declare Function OpenProcessToken Lib "advapi32" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByRef TokenHandle As Integer) As Integer
		Private Declare Function LookupPrivilegeValue Lib "advapi32" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Integer
		Private Declare Function AdjustTokenPrivileges Lib "advapi32" (ByVal TokenHandle As Integer, ByVal DisableAllPrivileges As Boolean, ByRef NewState As TOKEN_PRIVILEGES, ByVal BufferLength As Integer, ByRef PreviousState As Integer, ByRef ReturnLength As Integer) As Integer

		Private Structure PROCESSENTRY32
			Dim dwSize As Integer
			Dim cntUsage As Integer
			Dim th32ProcessID As Integer
			Dim th32DefaultHeapID As Integer
			Dim th32ModuleID As Integer
			Dim cntThreads As Integer
			Dim th32ParentProcessID As Integer
			Dim pcPriClassBase As Integer
			Dim dwFlags As Integer
			<VBFixedString(MAX_PATH), Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=MAX_PATH)> Public szexeFile As Char()
		End Structure

		Public Shared Function KillApp(ByRef myName As String) As Boolean
			Const TH32CS_SNAPPROCESS As Integer = 2
			Const PROCESS_ALL_ACCESS As Short = 0
			Dim uProcess As PROCESSENTRY32
			Dim rProcessFound As Integer
			Dim hSnapshot As Integer
			Dim szExename As String
#Disable Warning BC42024
			Dim exitCode As Integer
			Dim myProcess As Integer
			Dim AppKill As Boolean
#Enable Warning BC42024
			Dim appCount As Short
			Dim I As Short
			Try
#Disable Warning BC42109 ' Variable is used before it has been assigned a value
				uProcess.dwSize = Len(uProcess)
#Enable Warning BC42109 ' Variable is used before it has been assigned a value
				hSnapshot = CreateToolhelpSnapshot(TH32CS_SNAPPROCESS, 0)
				rProcessFound = ProcessFirst(hSnapshot, uProcess)
				Do While rProcessFound
					I = InStr(1, uProcess.szexeFile, Chr(0))
					szExename = LCase(Left(uProcess.szexeFile, I - 1))
					If Right(szExename, Len(myName)) = LCase(myName) Then
						KillApp = True
						appCount = appCount + 1
						myProcess = OpenProcess(PROCESS_ALL_ACCESS, False, uProcess.th32ProcessID)
						If KillProcess(uProcess.th32ProcessID, 0) Then
							'For debug.... Remove this
						End If

					End If
					rProcessFound = ProcessNext(hSnapshot, uProcess)
				Loop
				Call CloseHandle(hSnapshot)
#Disable Warning S3385
				Exit Function
#Enable Warning S3385
			Catch ex As Exception
				MsgBox("Error on Killing the Process.", MsgBoxStyle.Critical, "Error!")
			End Try
#Disable Warning BC42353
		End Function
#Enable Warning BC42353

		'Terminate any application and return an exit code to Windows.
#Disable Warning S2360
		Public Shared Function KillProcess(ByVal hProcessID As Integer, Optional ByVal exitCode As Integer = 0) As Boolean
#Enable Warning S2360
			Dim hToken As Integer
			Dim hProcess As Integer
			Dim tp As TOKEN_PRIVILEGES


			If GetVersion() >= 0 Then

				If OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, hToken) = 0 Then
					GoTo CleanUp
				End If

				If LookupPrivilegeValue("", "SeDebugPrivilege", tp.LuidUDT) = 0 Then
					GoTo CleanUp
				End If

				tp.PrivilegeCount = 1
				tp.Attributes = SE_PRIVILEGE_ENABLED

				If AdjustTokenPrivileges(hToken, False, tp, 0, 0, 0) = 0 Then
					GoTo CleanUp
				End If
			End If

			hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, hProcessID)
			If hProcess Then

				KillProcess = (TerminateProcess(hProcess, exitCode) <> 0)
				' close the process handle
				CloseHandle(hProcess)
			End If

			If GetVersion() >= 0 Then
				' under NT restore original privileges
				tp.Attributes = 0
				AdjustTokenPrivileges(hToken, False, tp, 0, 0, 0)

CleanUp:
				If hToken Then CloseHandle(hToken)
			End If
			Return 0
		End Function
	End Class
End Class