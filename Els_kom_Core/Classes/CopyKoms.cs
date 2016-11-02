
namespace Els_kom_Core
{
	public partial class Classes
	{
		/// <summary>
		/// Holds Methods of Coping KOM Files to the Directory to Elsword that Els_kom was set to
		/// </summary>
		public static class CopyKoms
		{
			/// <summary>
			/// Copies Modified KOM files to the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory.
			/// </summary>
			public static object CopyKomFiles(string FileName, string OrigFileDir, string DestFileDir)
			{
				if (System.IO.File.Exists(FileName)) {
					if ((!System.IO.Directory.Exists(DestFileDir))) {
						return 1;
					} else {
						MoveOriginalKomFiles(FileName, DestFileDir, DestFileDir + "\\backup");
						System.IO.File.Copy(OrigFileDir + FileName, DestFileDir + FileName);
					}
				}
				return 0;
			}

			/// <summary>
			/// Backs up Original KOM files to a sub folder in the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory. USED INSIDE OF CopyKomFiles SO, USE THAT FUNCTION INSTEAD.
			/// </summary>
			public static object MoveOriginalKomFiles(string FileName, string OrigFileDir, string DestFileDir)
			{
				if (System.IO.File.Exists(FileName)) {
					if ((!System.IO.Directory.Exists(DestFileDir))) {
						System.IO.Directory.CreateDirectory(DestFileDir);
					}
					System.IO.File.Move(OrigFileDir + FileName, DestFileDir + FileName);
				}
				return 0;
			}
		}
	}
}