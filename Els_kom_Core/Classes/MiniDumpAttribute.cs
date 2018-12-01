// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Security.Permissions;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using Elskom.Generic.Libs;

    // do not use this attribute for anything but classes.

    /// <summary>
    /// Attribute for creating MiniDumps.
    ///
    /// This registers Thread and Unhandled exception
    /// handlers to do it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class MiniDumpAttribute : Attribute
    {
        private readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="MiniDumpAttribute"/> class.
        /// </summary>
        /// <param name="text">Exception message text.</param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        internal MiniDumpAttribute(string text)
        {
            this.text = text;
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.ExceptionHandler);
            Application.ThreadException += new ThreadExceptionEventHandler(this.ThreadExceptionHandler);
        }

        /// <summary>
        /// Gets or sets the title of the unhandled exception messagebox.
        /// </summary>
        public string ExceptionTitle { get; set; }

        /// <summary>
        /// Gets or sets the title of the unhandled thread exception messagebox.
        /// </summary>
        public string ThreadExceptionTitle { get; set; }

        private void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception)args.ExceptionObject;
            var exceptionData = e.GetType().ToString() + ": " + e.Message + Environment.NewLine + e.StackTrace + Environment.NewLine;
            var outputData = Encoding.ASCII.GetBytes(exceptionData);

            // do not dump or close if in a debugger.
            if (!Debugger.IsAttached)
            {
                MainControl.Closable = true;
                var fileStream = File.OpenWrite(SettingsFile.ErrorLogPath);
                fileStream.Write(outputData, 0, outputData.Length);
                fileStream.Dispose();
                MiniDump.FullMiniDumpToFile(SettingsFile.MiniDumpPath);
                MessageManager.ShowError(string.Format(this.text, SettingsFile.ErrorLogPath), this.ExceptionTitle, PluginUpdateCheck.NotifyIcon, Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
                Application.Exit();
            }
        }

        private void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            var exceptionData = ex.GetType().ToString() + ": " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine;
            var outputData = Encoding.ASCII.GetBytes(exceptionData);

            // do not dump or close if in a debugger.
            if (!Debugger.IsAttached)
            {
                var fileStream = File.OpenWrite(SettingsFile.ErrorLogPath);
                fileStream.Write(outputData, 0, outputData.Length);
                fileStream.Dispose();
                MiniDump.FullMiniDumpToFile(SettingsFile.MiniDumpPath);
                MessageManager.ShowError(string.Format(this.text, SettingsFile.ErrorLogPath), this.ThreadExceptionTitle, PluginUpdateCheck.NotifyIcon, Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
                Application.Exit();
            }
        }
    }
}
