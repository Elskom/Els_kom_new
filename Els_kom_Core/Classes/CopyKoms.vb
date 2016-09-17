Option Strict Off
Option Explicit On

Partial Class Classes
	''' <summary>
	''' Holds Methods of Coping KOM Files to the Directory to Elsword that Els_kom was set to
	''' </summary>
	Public Class CopyKoms
		''' <summary>
		''' Copies Modified KOM files to the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory.
		''' </summary>
		Public Shared Function CopyKomFiles(ByRef FileName As String, ByRef OrigFileDir As String, ByRef DestFileDir As String)
			If IO.File.Exists(FileName) Then
				If (Not IO.Directory.Exists(DestFileDir)) Then
					Return 1
				Else
					MoveOriginalKomFiles(FileName, DestFileDir, DestFileDir & "\backup")
					IO.File.Copy(OrigFileDir & FileName, DestFileDir & FileName)
				End If
			End If
			Return 0
		End Function

		''' <summary>
		''' Backs up Original KOM files to a sub folder in the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory. USED INSIDE OF CopyKomFiles SO, USE THAT FUNCTION INSTEAD.
		''' </summary>
		Public Shared Function MoveOriginalKomFiles(ByRef FileName As String, ByRef OrigFileDir As String, ByRef DestFileDir As String)
			If IO.File.Exists(FileName) Then
				If (Not IO.Directory.Exists(DestFileDir)) Then
					IO.Directory.CreateDirectory(DestFileDir)
				End If
				IO.File.Move(OrigFileDir & FileName, DestFileDir & FileName)
			End If
			Return 0
		End Function
	End Class
End Class