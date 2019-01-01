// Copyright (c) 2014-2019, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

using System;
using System.Windows.Forms;
using Els_kom.Forms;
using Elskom.Generic.Libs;

[MiniDump(
    "Please send a copy of {0} to https://github.com/Elskom/Els_kom_new/issues by making an issue and attaching the log(s) and mini-dump(s).",
    ExceptionTitle = "Unhandled Exception!",
    ThreadExceptionTitle = "Unhandled Thread Exception!")]
internal static class Els_kom_Main
{
    [STAThread]
    internal static int Main(string[] args)
    {
        var classType = typeof(Els_kom_Main);

        // execute our attribute.
        classType.GetCustomAttributes(false);
        if ((args.Length - 1) > -1)
        {
            ReleasePackaging.PackageRelease(args);
        }
        else
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var mainForm = new MainForm())
            {
                Application.Run(mainForm);
            }
        }

        return 0;
    }
}
