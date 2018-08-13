// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class in the Core that allows executing Elsword directly or it's launcher.
    /// </summary>
    internal static class ExecutionManager
    {
        private static string ElsDir;
        private static bool RunningElsword = false;
        private static bool RunningElswordDirectly = false;
        private static bool ExecutingElsword = false;
        internal static System.Collections.Generic.List<interfaces.ICallbackPlugin> callbackplugins = new System.Collections.Generic.List<interfaces.ICallbackPlugin>();

        /// <summary>
        /// Gets if the launcher to Elsword is running.
        /// </summary>
        internal static bool GetRunningElsword() => RunningElsword;
        /// <summary>
        /// Gets if Elsword is running Directly.
        /// </summary>
        internal static bool GetRunningElswordDirectly() => RunningElswordDirectly;
        /// <summary>
        /// Gets if Elsword is still getting ready to execute. False if executing.
        /// </summary>
        internal static bool GetExecutingElsword() => ExecutingElsword;

        /// <summary>
        /// Overload for Shell() Function that Allows Overloading of the Working directory Variable.
        /// It must be a String but can be variables that returns strings.
        /// </summary>
        /// <returns>0, process stdout data, process stderr data</returns>
        internal static string Shell(string FileName, string Arguments, bool RedirectStandardOutput, bool RedirectStandardError, bool UseShellExecute, bool CreateNoWindow, System.Diagnostics.ProcessWindowStyle WindowStyle, string WorkingDirectory, bool WaitForProcessExit)
        {
            var ret = string.Empty;
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = FileName;
            proc.StartInfo.Arguments = Arguments;
            proc.StartInfo.RedirectStandardOutput = RedirectStandardOutput;
            proc.StartInfo.RedirectStandardError = RedirectStandardError;
            proc.StartInfo.UseShellExecute = UseShellExecute;
            proc.StartInfo.CreateNoWindow = CreateNoWindow;
            proc.StartInfo.WindowStyle = WindowStyle;
            proc.StartInfo.WorkingDirectory = WorkingDirectory;
            proc.Start();
            // so that way main form Test mods functionality actually works (Lame ass hack I think tbh)...
            if (ExecutingElsword)
            {
                ExecutingElsword = false;
            }
            if (RedirectStandardError)
            {
                ret = proc.StandardError.ReadToEnd();
            }
            if (RedirectStandardOutput)
            {
                ret = proc.StandardOutput.ReadToEnd();
            }
            if (WaitForProcessExit)
            {
                proc.WaitForExit();
            }
            // Required to have Detection on the process running to work right.
            return ret;
        }

        /// <summary>
        /// Gets if Els_kom.exe is already Running. If So, Helps with Closing any new Instances.
        /// </summary>
        /// <returns>Boolean</returns>
        internal static bool IsElsKomRunning()
        {
            var els_komexe = System.Diagnostics.Process.GetProcessesByName("Els_kom");
            return System.Linq.Enumerable.Count(els_komexe) > 1;
        }

        /// <summary>
        /// Runs Elsword Directly.
        /// This is an blocking call that has to run in an separate thread from Els_kom's main thread.
        /// NEVER UNDER ANY CIRCUMSTANCES RUN THIS IN THE MAIN THREAD, YOU WILL DEADLOCK ELS_KOM!!!
        /// </summary>
        internal static void RunElswordDirectly()
        {
            ExecutingElsword = true;
            if (System.IO.File.Exists(SettingsFile.Path))
            {
                SettingsFile.Settingsxml?.ReopenFile();
                ElsDir = SettingsFile.Settingsxml?.Read("ElsDir");
                if (ElsDir.Length > 0)
                {
                    if (System.IO.File.Exists(ElsDir + "\\data\\x2.exe"))
                    {
                        RunningElswordDirectly = true;
                        Shell(ElsDir + "\\data\\x2.exe", "pxk19slammsu286nfha02kpqnf729ck", false, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir + "\\data\\", true);
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
            // avoid bad UI bug.
            ExecutingElsword = ExecutingElsword ? false : ExecutingElsword;
        }

        /// <summary>
        /// Runs Elsword Launcher.
        /// This is an blocking call that has to run in an separate thread from Els_kom's main thread.
        /// NEVER UNDER ANY CIRCUMSTANCES RUN THIS IN THE MAIN THREAD, YOU WILL DEADLOCK ELS_KOM!!!
        /// </summary>
        internal static void RunElswordLauncher()
        {
            // for the sake of sanity and the need to disable the pack, unpack, and test mods
            // buttons in UI while updating game.
            ExecutingElsword = true;
            if (System.IO.File.Exists(SettingsFile.Path))
            {
                SettingsFile.Settingsxml?.ReopenFile();
                ElsDir = SettingsFile.Settingsxml?.Read("ElsDir");
                if (ElsDir.Length > 0)
                {
                    if (System.IO.File.Exists(ElsDir + "\\voidels.exe"))
                    {
                        RunningElsword = true;
                        Shell(ElsDir + "\\voidels.exe", "", false, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir, true);
                        RunningElsword = false;
                    }
                    else
                    {
                        if (System.IO.File.Exists(ElsDir + "\\elsword.exe"))
                        {
                            RunningElsword = true;
                            Shell(ElsDir + "\\elsword.exe", "", false, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir, true);
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
            // avoid bad UI bug.
            ExecutingElsword = ExecutingElsword ? false : ExecutingElsword;
        }

        /// <summary>
        /// Deploys the Test Mods callback functions provided by plugins.
        /// This is an blocking call that has to run in an separate thread from Els_kom's main thread.
        /// NEVER UNDER ANY CIRCUMSTANCES RUN THIS IN THE MAIN THREAD, YOU WILL DEADLOCK ELS_KOM!!!
        /// </summary>
        internal static void DeployCallBack()
        {
            if (RunningElswordDirectly)
            {
                foreach (var plugin in callbackplugins)
                {
                    plugin.TestModsCallback();
                }
            }
        }
    }
}
