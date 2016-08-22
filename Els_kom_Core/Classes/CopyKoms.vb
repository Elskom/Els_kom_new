Option Strict Off
Option Explicit On

Partial Class Classes
	''' <summary>
	''' Holds Methods of Coping KOM Files to the Directory to Elsword that was set to
	''' </summary>
	Public Class CopyKoms
		''' <summary>
		''' Copies Modified KOM files to the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory.
		''' </summary>
		Public Shared Function CopyKomFiles(ByRef FileName As String, ByRef OrigFileDir As String, ByRef DestFileDir As String)
			' TODO: Add Code to copy kom files to the data folder within the configured Elsword Directory.
			' NOTE: The original ones would have to be backed up just like how gPatcher does it.
			Return 0
		End Function

		''' <summary>
		''' Backs up Original KOM files to a sub folder in the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory.
		''' </summary>
		Public Shared Function MoveOriginalKomFiles(ByRef FileName As String, ByRef OrigFileDir As String, ByRef DestFileDir As String)
			' TODO: Add Code to backup original kom files to a sub folder in the data folder within the configured Elsword Directory.
			' NOTE: This should be how the Backups are made. Also This should hopefully check if a backup folder exists. If not Create it.
			Return 0
		End Function
	End Class
End Class