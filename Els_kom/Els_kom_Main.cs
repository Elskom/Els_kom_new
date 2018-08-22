// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

[Els_kom_Core.Classes.MiniDump(
    "Please send a copy of {0} to https://github.com/Elskom/Els_kom_new/issues by making an issue and attaching the log(s) and mini-dump(s).",
    ExceptionTitle = "Unhandled Exception!",
    ThreadExceptionTitle = "Unhandled Thread Exception!")]
internal static class Els_kom_Main
{
    [System.STAThread]
    internal static int Main(string[] args)
    {
        var classType = typeof(Els_kom_Main);

        // execute our attribute.
        classType.GetCustomAttributes(false);
        if ((args.Length - 1) > -1)
        {
            Els_kom_Core.Classes.ReleasePackaging.PackageRelease(args);
        }
        else
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Els_kom.Forms.MainForm());
        }

        return 0;
    }
}
