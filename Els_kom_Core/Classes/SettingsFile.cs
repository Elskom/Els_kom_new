// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class that handles the settings for this Application.
    /// </summary>
    public static class SettingsFile
    {
        private static XMLObject _settingsxml;

        /// <summary>
        /// The path to the Els_kom Settings file.
        ///
        /// Creates the folder if needed.
        /// </summary>
        public static string Path
        {
            get
            {
                // We cannot use System.Windows.Forms.Application.LocalUserAppDataPath as it would
                // Create annoying folders, and throw annoying Exceptions making it harder to
                // debug as it spams the debugger. Also then we would not need to Replace
                // everything added to the path obtained from System.Environment.GetFolderPath.
                string localPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.LocalApplicationData);
                localPath += "\\Els_kom";
                if (!System.IO.Directory.Exists(localPath))
                {
                    System.IO.Directory.CreateDirectory(localPath);
                }
                // do not create settings file, just pass this path to XMLObject.
                // if we create it ourselves the new optimized class will fail
                // to work right if it is empty.
                localPath += "\\Settings.xml";
                return localPath;
            }
        }

        /// <summary>
        /// The path to the Els_kom Error Log file.
        ///
        /// Creates the Error Log file if needed.
        /// </summary>
        public static string ErrorLogPath
        {
            get
            {
                string localPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.LocalApplicationData);
                localPath += "\\Els_kom";
                System.Diagnostics.Process thisProcess = System.Diagnostics.Process.GetCurrentProcess();
                localPath += "\\" + thisProcess.ProcessName + "-" + thisProcess.Id.ToString() + ".log";
                thisProcess.Dispose();
                return localPath;
            }
        }

        /// <summary>
        /// Gets the settings file XMLObject instance.
        ///
        /// This is designed so there is globally only
        /// a single instance to save time, and memory.
        /// </summary>
        public static XMLObject Settingsxml
        {
            get
            {
                return _settingsxml;
            }
            set
            {
                _settingsxml = value;
            }
        }
 
        /// <summary>
        /// The path to the Els_kom Mini-Dump file.
        /// </summary>
        public static string MiniDumpPath
        {
            get
            {
                string localPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.LocalApplicationData);
                localPath += "\\Els_kom";
                System.Diagnostics.Process thisProcess = System.Diagnostics.Process.GetCurrentProcess();
                localPath += "\\" + thisProcess.ProcessName + "-" + thisProcess.Id.ToString() + ".mdmp";
                thisProcess.Dispose();
                return localPath;
            }
        }
    }
}
