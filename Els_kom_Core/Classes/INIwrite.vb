Option Strict Off
Option Explicit On

Partial Class Classes
	Public Class INIwrite

		Public Shared Function WriteIniValue(ByRef INIpath As String, ByRef PutKey As String, ByRef PutVariable As String, ByRef PutValue As String) As Object
			Dim temp As String
			Dim LcaseTemp As String
			Dim ReadKey As String
			Dim ReadVariable As String
			Dim LOKEY As Short
			Dim HIKEY As Short
			Dim KEYLEN As Short
			Dim VAR As Short
			Dim VARENDOFLINE As Short
			Dim NF As Short
#Disable Warning BC42024
			Dim X As Short
#Enable Warning BC42024

AssignVariables:
			NF = FreeFile()
			ReadKey = vbCrLf & "[" & LCase(PutKey) & "]" & Chr(13)
			KEYLEN = Len(ReadKey)
			ReadVariable = Chr(10) & LCase(PutVariable) & "="

EnsureFileExists:
			FileOpen(NF, INIpath, OpenMode.Binary)
			FileClose(NF)
			SetAttr(INIpath, FileAttribute.Archive)

LoadFile:
			FileOpen(NF, INIpath, OpenMode.Input)
			temp = InputString(NF, LOF(NF))
			temp = vbCrLf & temp & "[]"
			FileClose(NF)
			LcaseTemp = LCase(temp)

LogicMenu:
			LOKEY = InStr(LcaseTemp, ReadKey)
			If LOKEY = 0 Then GoTo AddKey
			HIKEY = InStr(LOKEY + KEYLEN, LcaseTemp, "[")
			VAR = InStr(LOKEY, LcaseTemp, ReadVariable)
			If VAR > HIKEY Or VAR < LOKEY Then GoTo AddVariable
			GoTo RenewVariable

AddKey:
			temp = Left(temp, Len(temp) - 2)
			temp = temp & vbCrLf & vbCrLf & "[" & PutKey & "]" & vbCrLf & PutVariable & "=" & PutValue
			GoTo TrimFinalString

AddVariable:
			temp = Left(temp, Len(temp) - 2)
			temp = Left(temp, LOKEY + KEYLEN) & PutVariable & "=" & PutValue & vbCrLf & Mid(temp, LOKEY + KEYLEN + 1)
			GoTo TrimFinalString

RenewVariable:
			temp = Left(temp, Len(temp) - 2)
			VARENDOFLINE = InStr(VAR, temp, Chr(13))
			temp = Left(temp, VAR) & PutVariable & "=" & PutValue & Mid(temp, VARENDOFLINE)
			GoTo TrimFinalString

TrimFinalString:
			temp = Mid(temp, 2)
			Do Until InStr(temp, vbCrLf & vbCrLf & vbCrLf) = 0
				temp = Replace(temp, vbCrLf & vbCrLf & vbCrLf, vbCrLf & vbCrLf)
			Loop

			Do Until Right(temp, 1) > Chr(13)
				temp = Left(temp, Len(temp) - 1)
			Loop

			Do Until Left(temp, 1) > Chr(13)
				temp = Mid(temp, 2)
			Loop

OutputAmendedINIFile:
			FileOpen(NF, INIpath, OpenMode.Output)
			PrintLine(NF, temp)
			FileClose(NF)
#Disable Warning BC42105
		End Function
#Enable Warning BC42105
	End Class
End Class