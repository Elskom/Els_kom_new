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

            [System.Runtime.InteropServices.DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
            [System.Runtime.InteropServices.DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

            /// <summary>
            /// Sets the INI File to open.
            /// </summary>
            /// <param name="filePath"></param>
            public INIObject(string filePath)
            {
                this.filePath = filePath;
            }

            /// <summary>
            /// Writes an INI setting in the INI file that was told to open.
            /// </summary>
            public void Write(string section, string key, string value)
            {
                WritePrivateProfileString(section, key, value, this.filePath);
            }

            /// <summary>
            /// Reads an INI setting in the INI file that was told to open.
            /// </summary>
            public string Read(string section, string key)
            {
                System.Text.StringBuilder SB = new System.Text.StringBuilder(255);
                int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
                return SB.ToString();
            }
        }
    }
}
