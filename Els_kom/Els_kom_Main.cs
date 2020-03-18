// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing.Imaging;
    using System.Messaging;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Els_kom.Forms;
    using Elskom.Generic.Libs;

    internal static class Els_kom_Main
    {
        [STAThread]
        [SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification = "Wrapped in a using block and is never checked for null.")]
        internal static int Main(string[] args)
        {
            MiniDump.DumpMessage += MiniDump_DumpMessage;

            // MiniDump.DumpFailed += MiniDump_DumpFailed;
            GitInformation.ApplyAssemblyAttributes(typeof(Els_kom_Main).Assembly);

            // _ = Assembly.GetEntryAssembly().GetCustomAttributes(false);
            KOMManager.MessageEvent += KOMManager_MessageEvent;
            PluginUpdateCheck.MessageEvent += PluginUpdateCheck_MessageEvent;

            // have to pass in string to this as otherwise it will not compile.
            GenericPluginLoader<string>.PluginLoaderMessage += GenericPluginLoader_PluginLoaderMessage;

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

        private static void PluginUpdateCheck_MessageEvent(object sender, MessageEventArgs e)
            => _ = MessageManager.ShowInfo(e.Text, e.Caption, Convert.ToBoolean(Convert.ToInt32(!string.IsNullOrEmpty(SettingsFile.Settingsxml?.TryRead("UseNotifications")) ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));

        private static void KOMManager_MessageEvent(object sender, MessageEventArgs e)
            => _ = MessageManager.ShowError(e.Text, e.Caption, Convert.ToBoolean(Convert.ToInt32(!string.IsNullOrEmpty(SettingsFile.Settingsxml?.TryRead("UseNotifications")) ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));

        /*
        private static void MiniDump_DumpFailed(object sender, MessageEventArgs e)
            => _ = MessageManager.ShowError(e.Text, e.Caption, false);
        */

        private static void MiniDump_DumpMessage(object sender, MessageEventArgs e)
        {
            _ = MessageManager.ShowError(e.Text, e.Caption, false);
            if (!e.Text.StartsWith("Mini-dumping failed with Code: "))
            {
                // save screenshot of crash.
                using (var screenshot = NativeMethods.ScreenShots.MakeScreenShot())
                {
                    screenshot.Save(SettingsFile.MiniDumpPath.Replace(".mdmp", ".png"), ImageFormat.Png);
                }

                Environment.Exit(1);
            }
        }

        private static void GenericPluginLoader_PluginLoaderMessage(object sender, MessageEventArgs e)
            => _ = MessageManager.ShowError(e.Text, e.Caption, Convert.ToBoolean(Convert.ToInt32(!string.IsNullOrEmpty(SettingsFile.Settingsxml?.TryRead("UseNotifications")) ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
    }
}
