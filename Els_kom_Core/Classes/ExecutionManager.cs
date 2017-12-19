using System.Linq;

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class in the Core that allows executing Elsword directly or it's launcher.
    /// </summary>
    public static class ExecutionManager
    {
        private static string ElsDir;
        private static bool RunningElsword = false;
        private static bool RunningElswordDirectly = false;

        /// <summary>
        /// Gets if the launcher to Elsword is running.
        /// </summary>
        /// <returns>bool</returns>
        public static bool GetRunningElsword()
        {
            return RunningElsword;
        }

        /// <summary>
        /// Gets if Elsword is running Directly.
        /// </summary>
        /// <returns>bool</returns>
        public static bool GetRunningElswordDirectly()
        {
            return RunningElswordDirectly;
        }

        /// <summary>
        /// Overload for Shell() Function that Allows Overloading of the Working directory Variable. It must be a String but can be variables that returns strings.
        /// </summary>
        /// <returns>0</returns>
        public static object Shell(string FileName, string Arguments, bool RedirectStandardOutput, bool UseShellExecute, bool CreateNoWindow, System.Diagnostics.ProcessWindowStyle WindowStyle, string WorkingDirectory, bool WaitForProcessExit)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = FileName;
            proc.StartInfo.Arguments = Arguments;
            proc.StartInfo.RedirectStandardOutput = RedirectStandardOutput;
            proc.StartInfo.UseShellExecute = UseShellExecute;
            proc.StartInfo.CreateNoWindow = CreateNoWindow;
            proc.StartInfo.WindowStyle = WindowStyle;
            proc.StartInfo.WorkingDirectory = WorkingDirectory;
            proc.Start();
            if (WaitForProcessExit)
            {
                proc.WaitForExit();
            }
            // Required to have Detection on the process running to work right.
            return 0;
        }

        /// <summary>
        /// Bypasses Elsword's Integrity Check (makes it read checkkom.xml locally).
        /// </summary>
        public static bool BypassIntegrityChecks()
        {
            // TODO: Add code that can bypass Integrity Checks without needing Fidler as GameGuard detects it.
            return true;
        }

        /// <summary>
        /// Gets if Els_kom.exe is already Running. If So, Helps with Closing any new Instances.
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsElsKomRunning()
        {
            System.Diagnostics.Process[] els_komexe = System.Diagnostics.Process.GetProcessesByName("Els_kom");
            return els_komexe.Count() > 1;
        }

        /// <summary>
        /// Runs Elsword Directly.
        /// This is an blocking call that has to run in an separate thread from Els_kom's main thread.
        /// NEVER UNDER ANY CIRCUMSTANCES RUN THIS IN THE MAIN THREAD, YOU WILL DEADLOCK ELS_KOM!!!
        /// </summary>
        public static void RunElswordDirectly()
        {
            if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\Settings.ini"))
            {
                INIObject settingsini = new INIObject(System.Windows.Forms.Application.StartupPath + "\\Settings.ini");
                ElsDir = settingsini.Read("Settings.ini", "ElsDir");
                if (ElsDir.Length > 0)
                {
                    if (System.IO.File.Exists(ElsDir + "\\data\\x2.exe"))
                    {
                        RunningElswordDirectly = true;
                        Shell(ElsDir + "\\data\\x2.exe", "pxk19slammsu286nfha02kpqnf729ck", false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir + "\\data\\", true);
                        RunningElswordDirectly = false;
                    }
                    else
                    {
                        MessageManager.ShowError("Can't find '" + ElsDir + "\\data\\x2.exe'. Make sure the File Exists and try to Test your mods Again!", "Error!");
                    }
                }
                else
                {
                    MessageManager.ShowError("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to Test your mods Again!", "Error!");
                }
            }
            else
            {
                MessageManager.ShowError("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to Test your mods Again!", "Error!");
            }
        }

        /// <summary>
        /// Runs Elsword Launcher.
        /// This is an blocking call that has to run in an separate thread from Els_kom's main thread.
        /// NEVER UNDER ANY CIRCUMSTANCES RUN THIS IN THE MAIN THREAD, YOU WILL DEADLOCK ELS_KOM!!!
        /// </summary>
        public static void RunElswordLauncher()
        {
            if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\Settings.ini"))
            {
                INIObject settingsini = new INIObject(System.Windows.Forms.Application.StartupPath + "\\Settings.ini");
                ElsDir = settingsini.Read("Settings.ini", "ElsDir");
                if (ElsDir.Length > 0)
                {
                    if (System.IO.File.Exists(ElsDir + "\\voidels.exe"))
                    {
                        RunningElsword = true;
                        Shell(ElsDir + "\\voidels.exe", "", false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir, true);
                        RunningElsword = false;
                    }
                    else
                    {
                        if (System.IO.File.Exists(ElsDir + "\\elsword.exe"))
                        {
                            RunningElsword = true;
                            Shell(ElsDir + "\\elsword.exe", "", false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir, true);
                            RunningElsword = false;
                        }
                        else
                        {
                            MessageManager.ShowError("Can't find '" + ElsDir + "\\elsword.exe'. Make sure the File Exists and try to update Elsword Again!", "Error!");
                        }
                    }
                }
                else
                {
                    MessageManager.ShowError("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to update Elsword Again!", "Error!");
                }
            }
            else
            {
                MessageManager.ShowError("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to update Elsword Again!", "Error!");
            }
        }
    }
}
