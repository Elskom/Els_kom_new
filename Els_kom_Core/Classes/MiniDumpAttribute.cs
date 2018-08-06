// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    // do not use this attribute for anything but methods.
    /// <summary>
    /// Attribute for creating MiniDumps.
    ///
    /// This registers Thread and Unhandled exception
    /// handlers to do it.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class MiniDumpAttribute : System.Attribute
    {
        private readonly string text;
        /// <summary>
        /// Title of the unhandled exception messagebox.
        /// </summary>
        public string ExceptionTitle;
        /// <summary>
        /// Title of the unhandled thread exception messagebox.
        /// </summary>
        public string ThreadExceptionTitle;

        /// <summary>
        /// Creates a new instance of MiniDumpAttribute.
        /// </summary>
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, Flags = System.Security.Permissions.SecurityPermissionFlag.ControlAppDomain)]
        public MiniDumpAttribute(string text)
        {
            this.text = text;
            var currentDomain = System.AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(ExceptionHandler);
            System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadExceptionHandler);
        }

        internal void ExceptionHandler(object sender, System.UnhandledExceptionEventArgs args)
        {
            var e = (System.Exception)args.ExceptionObject;
            var exceptionData = e.GetType().ToString() + ": " + e.Message + System.Environment.NewLine + e.StackTrace + System.Environment.NewLine;
            var outputData = System.Text.Encoding.ASCII.GetBytes(exceptionData);
            // do not dump or close if in a debugger.
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Controls.MainControl._closable = true;
                var fileStream = System.IO.File.OpenWrite(SettingsFile.ErrorLogPath);
                fileStream.Write(outputData, 0, outputData.Length);
                fileStream.Dispose();
                MiniDump.FullMiniDumpToFile(SettingsFile.MiniDumpPath);
                MessageManager.ShowError(string.Format(text, SettingsFile.ErrorLogPath), ExceptionTitle);
                System.Windows.Forms.Application.Exit();
            }
        }

        internal void ThreadExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            var exceptionData = ex.GetType().ToString() + ": " + ex.Message + System.Environment.NewLine + ex.StackTrace + System.Environment.NewLine;
            var outputData = System.Text.Encoding.ASCII.GetBytes(exceptionData);
            // do not dump or close if in a debugger.
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Controls.MainControl._closable = true;
                var fileStream = System.IO.File.OpenWrite(SettingsFile.ErrorLogPath);
                fileStream.Write(outputData, 0, outputData.Length);
                fileStream.Dispose();
                MiniDump.FullMiniDumpToFile(SettingsFile.MiniDumpPath);
                MessageManager.ShowError(string.Format(text, SettingsFile.ErrorLogPath), ThreadExceptionTitle);
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
