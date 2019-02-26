// Copyright (c) 2014-2019, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

using System;
using System.Reflection;
using System.Windows.Forms;
using Els_kom.Forms;
using Elskom.Generic.Libs;

internal static class Els_kom_Main
{
    [STAThread]
    internal static int Main(string[] args)
    {
        MiniDumpAttribute.DumpGenerated += MiniDumpAttribute_DumpGenerated;
        MiniDump.DumpFailed += MiniDump_DumpFailed;
        Assembly.GetEntryAssembly().GetCustomAttributes(false);
        KOMManager.MessageEvent += KOMManager_MessageEvent;

        // execute our attribute.
        // uncomment if the attribute is on the assembly:
        // Assembly.GetEntryAssembly().GetCustomAttributes(false);
        // or:
        // typeof(TheClassWithTheAttribute).Assembly.GetCustomAttributes(false);
        // uncomment if the attribute is on the class:
        // Assembly.GetEntryAssembly().EntryPoint.ReflectedType.GetCustomAttributes(false);
        // or:
        // typeof(TheClassWithTheAttribute).GetCustomAttributes(false);
        // uncomment if you want the attribute on the class:
        // Assembly.GetEntryAssembly().EntryPoint.GetCustomAttributes(false);
        // or:
        // typeof(TheClassWithTheAttribute).GetMethod(nameof(Main), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetCustomAttributes(false);
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

    private static void KOMManager_MessageEvent(object sender, MessageEventArgs e)
        => MessageManager.ShowError(e.Text, e.Caption, Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));

    private static void MiniDump_DumpFailed(object sender, MiniDumpEventArgs e)
        => MessageManager.ShowError(e.Text, e.Caption, false);

    private static void MiniDumpAttribute_DumpGenerated(object sender, MiniDumpEventArgs e)
    {
        MessageManager.ShowError(e.Text, e.Caption, false);
        Environment.Exit(1);
    }
}
