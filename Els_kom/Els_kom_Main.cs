// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing.Imaging;
    using System.Net.Http;
    using System.Windows.Forms;
    using Els_kom.Forms;
    using Microsoft.Extensions.DependencyInjection;

    [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "🖕")]
    internal static class Els_kom_Main
    {
        internal static ServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        internal static int Main(string[] args)
        {
            MiniDumpAttribute.DumpMessage += MiniDump_DumpMessage;
            GitInformation.ApplyAssemblyAttributes(typeof(Els_kom_Main).Assembly);
            KOMManager.MessageEvent += KOMManager_MessageEvent;
            PluginUpdateCheck.MessageEvent += PluginUpdateCheck_MessageEvent;
            ServiceProvider ??= ConfigureServices();

            // have to pass in string to this as otherwise it will not compile.
            GenericPluginLoader.PluginLoaderMessage += GenericPluginLoader_PluginLoaderMessage;
            if ((args.Length - 1) > -1)
            {
                ReleasePackaging.PackageRelease(args);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using var mainForm = new MainForm();
                Application.Run(mainForm);
            }

            return 0;
        }

        private static void PluginUpdateCheck_MessageEvent(object sender, MessageEventArgs e)
            => _ = MessageManager.ShowInfo(e.Text, e.Caption, Convert.ToBoolean(SettingsFile.SettingsJson.UseNotifications));

        private static void KOMManager_MessageEvent(object sender, MessageEventArgs e)
            => _ = MessageManager.ShowError(e.Text, e.Caption, Convert.ToBoolean(SettingsFile.SettingsJson.UseNotifications));

        private static void MiniDump_DumpMessage(object sender, MessageEventArgs e)
        {
            _ = MessageManager.ShowError(e.Text, e.Caption, false);
            if (!e.Text.StartsWith("Mini-dumping failed with Code: "))
            {
                // save screenshot of crash.
                using (var screenshot = ScreenShots.MakeScreenShot())
                {
                    screenshot.Save(SettingsFile.MiniDumpPath.Replace(".mdmp", ".png"), ImageFormat.Png);
                }

                Environment.Exit(1);
            }
        }

        private static void GenericPluginLoader_PluginLoaderMessage(object sender, MessageEventArgs e)
            => _ = MessageManager.ShowError(e.Text, e.Caption, Convert.ToBoolean(SettingsFile.SettingsJson.UseNotifications));

        private static ServiceProvider ConfigureServices()
            => new ServiceCollection()
            .AddSingleton<HttpClient>()
            .AddGenericPluginLoader()
            .AddPluginUpdateCheck()
            .BuildServiceProvider();
    }
}
