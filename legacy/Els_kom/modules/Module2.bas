Attribute VB_Name = "Module2"
    Option Explicit
    Const MAX_PATH& = 260
     
    Declare Function TerminateProcess _
    Lib "kernel32" (ByVal ApphProcess As Long, _
    ByVal uExitCode As Long) As Long
    Declare Function OpenProcess Lib _
    "kernel32" (ByVal dwDesiredAccess As Long, _
    ByVal blnheritHandle As Long, _
    ByVal dwAppProcessId As Long) As Long
    Declare Function ProcessFirst _
    Lib "kernel32" Alias "Process32First" _
    (ByVal hSnapshot As Long, _
    uProcess As PROCESSENTRY32) As Long
    Declare Function ProcessNext _
    Lib "kernel32" Alias "Process32Next" _
    (ByVal hSnapshot As Long, _
    uProcess As PROCESSENTRY32) As Long
    Declare Function CreateToolhelpSnapshot _
    Lib "kernel32" Alias "CreateToolhelp32Snapshot" _
    (ByVal lFlags As Long, _
    lProcessID As Long) As Long
    Declare Function CloseHandle _
    Lib "kernel32" (ByVal hObject As Long) As Long
     
    Private Type LUID
    lowpart As Long
    highpart As Long
    End Type
     
    Private Type TOKEN_PRIVILEGES
    PrivilegeCount As Long
    LuidUDT As LUID
    Attributes As Long
    End Type
     
    Const TOKEN_ADJUST_PRIVILEGES = &H20
    Const TOKEN_QUERY = &H8
    Const SE_PRIVILEGE_ENABLED = &H2
    Const PROCESS_ALL_ACCESS = &H1F0FFF
     
    Private Declare Function GetVersion _
    Lib "kernel32" () As Long
    Private Declare Function GetCurrentProcess _
    Lib "kernel32" () As Long
    Private Declare Function OpenProcessToken _
    Lib "advapi32" (ByVal ProcessHandle As Long, _
    ByVal DesiredAccess As Long, _
    TokenHandle As Long) As Long
    Private Declare Function LookupPrivilegeValue _
    Lib "advapi32" Alias "LookupPrivilegeValueA" _
    (ByVal lpSystemName As String, _
    ByVal lpName As String, _
    lpLuid As LUID) As Long
    Private Declare Function AdjustTokenPrivileges _
    Lib "advapi32" (ByVal TokenHandle As Long, _
    ByVal DisableAllPrivileges As Long, _
    NewState As TOKEN_PRIVILEGES, _
    ByVal BufferLength As Long, _
    PreviousState As Any, _
    ReturnLength As Any) As Long
     
    Type PROCESSENTRY32
    dwSize As Long
    cntUsage As Long
    th32ProcessID As Long
    th32DefaultHeapID As Long
    th32ModuleID As Long
    cntThreads As Long
    th32ParentProcessID As Long
    pcPriClassBase As Long
    dwFlags As Long
    szexeFile As String * MAX_PATH
    End Type
    '---------------------------------------
    Public Function KillApp(myName As String) As Boolean
    Const TH32CS_SNAPPROCESS As Long = 2&
    Const PROCESS_ALL_ACCESS = 0
    Dim uProcess As PROCESSENTRY32
    Dim rProcessFound As Long
    Dim hSnapshot As Long
    Dim szExename As String
    Dim exitCode As Long
    Dim myProcess As Long
    Dim AppKill As Boolean
    Dim appCount As Integer
    Dim I As Integer
    On Local Error GoTo Finish
    appCount = 0
     
    uProcess.dwSize = Len(uProcess)
    hSnapshot = CreateToolhelpSnapshot(TH32CS_SNAPPROCESS, 0&)
    rProcessFound = ProcessFirst(hSnapshot, uProcess)
    Do While rProcessFound
    I = InStr(1, uProcess.szexeFile, Chr(0))
    szExename = LCase$(Left$(uProcess.szexeFile, I - 1))
    If Right$(szExename, Len(myName)) = LCase$(myName) Then
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
    Exit Function
Finish:
    MsgBox "Error on Killing the Process.", vbCritical, "Error!"
    End Function
     
    'Terminate any application and return an exit code to Windows.
    Function KillProcess(ByVal hProcessID As Long, Optional ByVal exitCode As Long) As Boolean
    Dim hToken As Long
    Dim hProcess As Long
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
     
    If AdjustTokenPrivileges(hToken, False, tp, 0, ByVal 0&, ByVal 0&) = 0 Then
    GoTo CleanUp
    End If
    End If
     
    hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, hProcessID)
    If hProcess Then
     
    KillProcess = (TerminateProcess(hProcess, exitCode) <> 0)
    ' close the process handle
    CloseHandle hProcess
    End If
     
    If GetVersion() >= 0 Then
    ' under NT restore original privileges
    tp.Attributes = 0
    AdjustTokenPrivileges hToken, False, tp, 0, ByVal 0&, ByVal 0&
     
CleanUp:
    If hToken Then CloseHandle hToken
    End If
     
    End Function

