// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Els_kom_Core.Controls;
    using Els_kom_Core.Interfaces;
    using Elskom.Generic.Libs;

    /// <summary>
    /// Class in the Core that allows executing Elsword directly or it's launcher.
    /// </summary>
    internal static class ExecutionManager
    {
        private static List<ICallbackPlugin> callbackplugins;

        /// <summary>
        /// Gets the list of callback plugins.
        /// </summary>
        internal static List<ICallbackPlugin> Callbackplugins
        {
            get
            {
                if (callbackplugins == null)
                {
                    callbackplugins = new List<ICallbackPlugin>();
                }

                return callbackplugins;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the launcher to Elsword is running.
        /// </summary>
        /// <returns>A value indicating if Elsword is running.</returns>
        internal static bool RunningElsword { get; private set; } = false;

        /// <summary>
        /// Gets a value indicating whether Elsword is running Directly.
        /// </summary>
        /// <returns>A value indicating if Elsword is running directly.</returns>
        internal static bool RunningElswordDirectly { get; private set; } = false;

        /// <summary>
        /// Gets a value indicating whether Elsword is still getting ready to execute. False if executing.
        /// </summary>
        /// <returns>A value indicating if Elsword is getting ready to execute.</returns>
        internal static bool ExecutingElsword { get; private set; } = false;

        private static string ElsDir { get; set; }

        /// <summary>
        /// Overload for Shell() Function that Allows Overloading of the Working directory Variable.
        /// It must be a String but can be variables that returns strings.
        /// </summary>
        /// <param name="fileName">Process file name to execute.</param>
        /// <param name="arguments">Commands to pass to the process file to execute.</param>
        /// <param name="redirectStandardOutput">redirects stdout of the target process.</param>
        /// <param name="redirectStandardError">redirects stderr of the target process.</param>
        /// <param name="useShellExecute">uses shell execute instead.</param>
        /// <param name="createNoWindow">Creates no new window for the process.</param>
        /// <param name="windowStyle">Window style for the target process.</param>
        /// <param name="workingDirectory">Working directory for the target process.</param>
        /// <param name="waitForProcessExit">Waits for the target process to terminate.</param>
        /// <returns>empty string, process stdout data, process stderr data.</returns>
        internal static string Shell(string fileName, string arguments, bool redirectStandardOutput, bool redirectStandardError, bool useShellExecute, bool createNoWindow, ProcessWindowStyle windowStyle, string workingDirectory, bool waitForProcessExit)
        {
            var ret = string.Empty;
            var proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.RedirectStandardOutput = redirectStandardOutput;
            proc.StartInfo.RedirectStandardError = redirectStandardError;
            proc.StartInfo.UseShellExecute = useShellExecute;
            proc.StartInfo.CreateNoWindow = createNoWindow;
            proc.StartInfo.WindowStyle = windowStyle;
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.Start();

            // so that way main form Test mods functionality actually works (Lame ass hack I think tbh)...
            if (ExecutingElsword)
            {
                ExecutingElsword = false;
            }

            if (redirectStandardError)
            {
                ret = proc.StandardError.ReadToEnd();
            }

            if (redirectStandardOutput)
            {
                ret = proc.StandardOutput.ReadToEnd();
            }

            if (waitForProcessExit)
            {
                proc.WaitForExit();
            }

            // Required to have Detection on the process running to work right.
            return ret;
        }

        /// <summary>
        /// Gets if Els_kom.exe is already Running. If So, Helps with Closing any new Instances.
        /// </summary>
        /// <returns>Boolean.</returns>
        internal static bool IsElsKomRunning()
        {
            var els_komexe = Process.GetProcessesByName("Els_kom");
            return els_komexe.Count() > 1;
        }

        /// <summary>
        /// Runs Elsword Directly.
        /// This is an blocking call that has to run in an separate thread from Els_kom's main thread.
        /// NEVER UNDER ANY CIRCUMSTANCES RUN THIS IN THE MAIN THREAD, YOU WILL DEADLOCK ELS_KOM!!!.
        /// </summary>
        internal static void RunElswordDirectly()
        {
            ExecutingElsword = true;
            if (File.Exists(SettingsFile.Path))
            {
                SettingsFile.Settingsxml?.ReopenFile();
                ElsDir = SettingsFile.Settingsxml?.TryRead("ElsDir");
                if (ElsDir.Length > 0)
                {
                    if (File.Exists(ElsDir + "\\data\\x2.exe"))
                    {
                        RunningElswordDirectly = true;
                        Shell(ElsDir + "\\data\\x2.exe", "pxk19slammsu286nfha02kpqnf729ck", false, false, false, false, ProcessWindowStyle.Normal, ElsDir + "\\data\\", true);
                        RunningElswordDirectly = false;
                    }
                    else
                    {
                        MessageManager.ShowError("Can't find '" + ElsDir + "\\data\\x2.exe'. Make sure the File Exists and try to Test your mods Again!", "Error!", MainControl.NotifyIcon1, Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
                    }
                }
                else
                {
                    MessageManager.ShowError("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to Test your mods Again!", "Error!", MainControl.NotifyIcon1, Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
                }
            }
            else
            {
                MessageManager.ShowError("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to Test your mods Again!", "Error!", MainControl.NotifyIcon1, Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
            }

            // avoid bad UI bug.
            ExecutingElsword = ExecutingElsword ? false : ExecutingElsword;
        }

        /// <summary>
        /// Runs Elsword Launcher.
        /// This is an blocking call that has to run in an separate thread from Els_kom's main thread.
        /// NEVER UNDER ANY CIRCUMSTANCES RUN THIS IN THE MAIN THREAD, YOU WILL DEADLOCK ELS_KOM!!!.
        /// </summary>
        internal static void RunElswordLauncher()
        {
            // for the sake of sanity and the need to disable the pack, unpack, and test mods
            // buttons in UI while updating game.
            ExecutingElsword = true;
            if (File.Exists(SettingsFile.Path))
            {
                SettingsFile.Settingsxml?.ReopenFile();
                ElsDir = SettingsFile.Settingsxml?.TryRead("ElsDir");
                if (ElsDir.Length > 0)
                {
                    if (File.Exists(ElsDir + "\\voidels.exe"))
                    {
                        RunningElsword = true;
                        Shell(ElsDir + "\\voidels.exe", string.Empty, false, false, false, false, ProcessWindowStyle.Normal, ElsDir, true);
                        RunningElsword = false;
                    }
                    else
                    {
                        if (File.Exists(ElsDir + "\\elsword.exe"))
                        {
                            RunningElsword = true;
                            Shell(ElsDir + "\\elsword.exe", string.Empty, false, false, false, false, ProcessWindowStyle.Normal, ElsDir, true);
                            RunningElsword = false;
                        }
                        else
                        {
                            MessageManager.ShowError("Can't find '" + ElsDir + "\\elsword.exe'. Make sure the File Exists and try to update Elsword Again!", "Error!", MainControl.NotifyIcon1, Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
                        }
                    }
                }
                else
                {
                    MessageManager.ShowError("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to update Elsword Again!", "Error!", MainControl.NotifyIcon1, Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
                }
            }
            else
            {
                MessageManager.ShowError("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to update Elsword Again!", "Error!", MainControl.NotifyIcon1, Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
            }

            // avoid bad UI bug.
            ExecutingElsword = ExecutingElsword ? false : ExecutingElsword;
        }

        /// <summary>
        /// Deploys the Test Mods callback functions provided by plugins.
        /// This is an blocking call that has to run in an separate thread from Els_kom's main thread.
        /// NEVER UNDER ANY CIRCUMSTANCES RUN THIS IN THE MAIN THREAD, YOU WILL DEADLOCK ELS_KOM!!!.
        /// </summary>
        internal static void DeployCallBack()
        {
            if (RunningElswordDirectly)
            {
                foreach (var plugin in Callbackplugins)
                {
                    plugin.TestModsCallback();
                }
            }
        }
    }
}
