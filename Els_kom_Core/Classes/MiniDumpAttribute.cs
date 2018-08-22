// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    // do not use this attribute for anything but classes.

    /// <summary>
    /// Attribute for creating MiniDumps.
    ///
    /// This registers Thread and Unhandled exception
    /// handlers to do it.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    internal class MiniDumpAttribute : System.Attribute
    {
        private readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="MiniDumpAttribute"/> class.
        /// </summary>
        /// <param name="text">Exception message text.</param>
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, Flags = System.Security.Permissions.SecurityPermissionFlag.ControlAppDomain)]
        internal MiniDumpAttribute(string text)
        {
            this.text = text;
            var currentDomain = System.AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(this.ExceptionHandler);
            System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(this.ThreadExceptionHandler);
        }

        /// <summary>
        /// Gets or sets the title of the unhandled exception messagebox.
        /// </summary>
        public string ExceptionTitle { get; set; }

        /// <summary>
        /// Gets or sets the title of the unhandled thread exception messagebox.
        /// </summary>
        public string ThreadExceptionTitle { get; set; }

        private void ExceptionHandler(object sender, System.UnhandledExceptionEventArgs args)
        {
            var e = (System.Exception)args.ExceptionObject;
            var exceptionData = e.GetType().ToString() + ": " + e.Message + System.Environment.NewLine + e.StackTrace + System.Environment.NewLine;
            var outputData = System.Text.Encoding.ASCII.GetBytes(exceptionData);

            // do not dump or close if in a debugger.
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Controls.MainControl.Closable = true;
                var fileStream = System.IO.File.OpenWrite(SettingsFile.ErrorLogPath);
                fileStream.Write(outputData, 0, outputData.Length);
                fileStream.Dispose();
                MiniDump.FullMiniDumpToFile(SettingsFile.MiniDumpPath);
                MessageManager.ShowError(string.Format(this.text, SettingsFile.ErrorLogPath), this.ExceptionTitle);
                System.Windows.Forms.Application.Exit();
            }
        }

        private void ThreadExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            var exceptionData = ex.GetType().ToString() + ": " + ex.Message + System.Environment.NewLine + ex.StackTrace + System.Environment.NewLine;
            var outputData = System.Text.Encoding.ASCII.GetBytes(exceptionData);

            // do not dump or close if in a debugger.
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Controls.MainControl.Closable = true;
                var fileStream = System.IO.File.OpenWrite(SettingsFile.ErrorLogPath);
                fileStream.Write(outputData, 0, outputData.Length);
                fileStream.Dispose();
                MiniDump.FullMiniDumpToFile(SettingsFile.MiniDumpPath);
                MessageManager.ShowError(string.Format(this.text, SettingsFile.ErrorLogPath), this.ThreadExceptionTitle);
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
