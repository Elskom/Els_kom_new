// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

internal static class Els_kom_Main
{
    [System.STAThread]
    [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, Flags = System.Security.Permissions.SecurityPermissionFlag.ControlAppDomain)]
    internal static int Main()
    {
        System.AppDomain currentDomain = System.AppDomain.CurrentDomain;
        currentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(ExceptionHandler);
        System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadExceptionHandler);
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        System.Windows.Forms.Application.Run(new Els_kom.Forms.MainForm());
        return 0;
    }

    static void ExceptionHandler(object sender, System.UnhandledExceptionEventArgs args)
    {
        System.Windows.Forms.MessageBox.Show("Please send a copy of " + Els_kom_Core.Classes.SettingsFile.ErrorLogPath + " to https://github.com/Elskom/Els_kom_new/issues by making an issue and attaching the log(s) and mini-dump(s).", "Unhandled Exception!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        System.Exception e = (System.Exception)args.ExceptionObject;
        string exceptionData = e.Message + System.Environment.NewLine + e.StackTrace;
        byte[] outputData = System.Text.Encoding.ASCII.GetBytes(exceptionData);
        System.IO.FileStream fileStream = System.IO.File.OpenWrite(Els_kom_Core.Classes.SettingsFile.ErrorLogPath);
        fileStream.Write(outputData, 0, outputData.Length);
        fileStream.Dispose();
        Els_kom_Core.Classes.MiniDump.FullMiniDumpToFile(Els_kom_Core.Classes.SettingsFile.MiniDumpPath);
        System.Windows.Forms.Application.Exit();
    }

    static void ThreadExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
        System.Windows.Forms.MessageBox.Show("Please send a copy of " + Els_kom_Core.Classes.SettingsFile.ErrorLogPath + " to https://github.com/Elskom/Els_kom_new/issues by making an issue and attaching the log(s) and mini-dump(s).", "Unhandled Thread Exception!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        System.Exception ex = e.Exception;
        string exceptionData = ex.Message + System.Environment.NewLine + ex.StackTrace;
        byte[] outputData = System.Text.Encoding.ASCII.GetBytes(exceptionData);
        System.IO.FileStream fileStream = System.IO.File.OpenWrite(Els_kom_Core.Classes.SettingsFile.ErrorLogPath);
        fileStream.Write(outputData, 0, outputData.Length);
        fileStream.Dispose();
        Els_kom_Core.Classes.MiniDump.FullMiniDumpToFile(Els_kom_Core.Classes.SettingsFile.MiniDumpPath);
        System.Windows.Forms.Application.Exit();
    }
}
