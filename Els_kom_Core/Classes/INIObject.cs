using System.Runtime.InteropServices;
using System.Text;

namespace Els_kom_Core
{
	/// <summary>
	/// Namespace that Holds Classes that Allows the Core of Els_kom to do certain things.
	/// </summary>
	namespace Classes
	{
		/// <summary>
		/// Class in the Core that allows Reading and Writing of INI Files (Don't Forget to Initialize it with the INI's file name 1st before trying to Read / write).
		/// </summary>
		public class INIObject
		{
			private string filePath;

			[DllImport("kernel32")]
			private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
			[DllImport("kernel32")]
			private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

			public INIObject(string filePath)
			{
				this.filePath = filePath;
			}

			public void Write(string section, string key, string value)
			{
				WritePrivateProfileString(section, key, value, this.filePath);
			}

			public string Read(string section, string key)
			{
				StringBuilder SB = new StringBuilder(255);
				int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
				return SB.ToString();
			}

			public string FilePath
			{
				get
				{
					return this.filePath;
				}
				set
				{
					this.filePath = value;
				}
			}
		}
	}
}
