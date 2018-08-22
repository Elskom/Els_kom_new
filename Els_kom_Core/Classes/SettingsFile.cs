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
        /// <summary>
        /// Gets or sets the settings file XMLObject instance.
        ///
        /// This is designed so there is globally only
        /// a single instance to save time, and memory.
        /// </summary>
        public static XMLObject Settingsxml { get; set; }

        /// <summary>
        /// Gets the path to the Els_kom Settings file.
        ///
        /// Creates the folder if needed.
        /// </summary>
        internal static string Path
        {
            get
            {
                // We cannot use System.Windows.Forms.Application.LocalUserAppDataPath as it would
                // Create annoying folders, and throw annoying Exceptions making it harder to
                // debug as it spams the debugger. Also then we would not need to Replace
                // everything added to the path obtained from System.Environment.GetFolderPath.
                var localPath = System.Environment.GetFolderPath(
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
        /// Gets the path to the Els_kom Error Log file.
        ///
        /// Creates the Error Log file if needed.
        /// </summary>
        internal static string ErrorLogPath
        {
            get
            {
                var localPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.LocalApplicationData);
                localPath += "\\Els_kom";
                var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
                localPath += "\\" + thisProcess.ProcessName + "-" + thisProcess.Id.ToString() + ".log";
                thisProcess.Dispose();
                return localPath;
            }
        }

        /// <summary>
        /// Gets the path to the Els_kom Mini-Dump file.
        /// </summary>
        internal static string MiniDumpPath
        {
            get
            {
                var localPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.LocalApplicationData);
                localPath += "\\Els_kom";
                var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
                localPath += System.IO.Path.DirectorySeparatorChar + thisProcess.ProcessName + "-" + thisProcess.Id.ToString() + ".mdmp";
                thisProcess.Dispose();
                return localPath;
            }
        }
    }
}
