Attribute VB_Name = "WindowFind"
Option Explicit

Private Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String, ByVal cch As Long) As Long

Public TargetName As String
Public TargetHwnd As Long
' Return False to stop the enumeration.
Public Function WindowEnumerator(ByVal app_hwnd As Long, ByVal lparam As Long) As Long
Dim buf As String * 256
Dim title As String
Dim length As Long

    ' Get the window's title.
    length = GetWindowText(app_hwnd, buf, Len(buf))
    title = Left$(buf, length)

    ' See if the title contains the target.
    If InStr(title, TargetName) > 0 Then
        ' Save the hwnd and end the enumeration.
        TargetHwnd = app_hwnd
        WindowEnumerator = False
    Else
        ' Continue the enumeration.
        WindowEnumerator = True
    End If
End Function
