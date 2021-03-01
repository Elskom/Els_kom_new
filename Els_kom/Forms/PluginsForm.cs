// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Els_kom.Controls;
    using Elskom.Generic.Libs;

    internal partial class PluginsForm : /*Form*/ThemedForm
    {
        private bool pluginChanges;
        private int saveToZip;

        internal PluginsForm()
            => this.InitializeComponent();

        private void PluginsForm_Load(object sender, EventArgs e)
        {
            this.saveToZip = SettingsFile.SettingsJson.SaveToZip;
            var pluginTypes = new List<Type>();
            pluginTypes.AddRange(KOMManager.Callbackplugins.Select((x) => x.GetType()));
            pluginTypes.AddRange(KOMManager.Komplugins.Select((x) => x.GetType()));

            // update the list if there were new sources added during the program execution.
            MainForm.PluginUpdateChecks = PluginUpdateCheck.CheckForUpdates(
                SettingsFile.SettingsJson.Sources.Source.ToArray(),
                pluginTypes,
                Els_kom_Main.ServiceProvider);
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (this.ListView1.SelectedItems.Count > 0)
            {
                foreach (var pluginUpdateCheck in MainForm.PluginUpdateChecks)
                {
                    // install only the selected plugin.
                    if (pluginUpdateCheck.PluginName.Equals(this.ListView1.SelectedItems[0].SubItems[0].Text, StringComparison.Ordinal))
                    {
                        var result = pluginUpdateCheck.Install(Convert.ToBoolean(this.saveToZip));

                        // avoid resetting the value back to false if "Install" returns "false".
                        if (!this.pluginChanges && result)
                        {
                            this.pluginChanges = result;
                        }
                    }
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (this.pluginChanges)
            {
                _ = MainForm.MessageManager.ShowInfo("A plugin was installed, uninstalled or updated. All plugins are about to be reloaded.", "Info!", Convert.ToBoolean(SettingsFile.SettingsJson.UseNotifications));
                var komloader = new GenericPluginLoader<IKomPlugin>();
                var callbackloader = new GenericPluginLoader<ICallbackPlugin>();
                var encryptionloader = new GenericPluginLoader<IEncryptionPlugin>();
#if NET5_0_OR_GREATER
                komloader.UnloadPlugins(MainForm.Contexts);
                MainForm.Contexts.Clear();
#else
                komloader.UnloadPlugins(MainForm.Domains);
                MainForm.Domains.Clear();
#endif
                var komplugins = komloader.LoadPlugins("plugins", out var domains1, Convert.ToBoolean(SettingsFile.SettingsJson.SaveToZip));
                KOMManager.Komplugins.AddRange(komplugins);
                var callbackplugins = callbackloader.LoadPlugins("plugins", out var domains2, Convert.ToBoolean(SettingsFile.SettingsJson.SaveToZip));
                KOMManager.Callbackplugins.AddRange(callbackplugins);
                var encryptionplugins = encryptionloader.LoadPlugins("plugins", out var domains3, Convert.ToBoolean(SettingsFile.SettingsJson.SaveToZip));
                KOMManager.Encryptionplugins.AddRange(encryptionplugins);
#if NET5_0_OR_GREATER
                MainForm.Contexts.AddRange(domains1);
                MainForm.Contexts.AddRange(domains2);
                MainForm.Contexts.AddRange(domains3);
#else
                MainForm.Domains.AddRange(domains1);
                MainForm.Domains.AddRange(domains2);
                MainForm.Domains.AddRange(domains3);
#endif
            }
            else
            {
                this.Close();
            }
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            if (this.ListView1.SelectedItems.Count > 0)
            {
                if (!this.ListView1.SelectedItems[0].SubItems[2].Text.Equals(string.Empty, StringComparison.Ordinal))
                {
                    foreach (var pluginUpdateCheck in MainForm.PluginUpdateChecks)
                    {
                        // install only the selected plugin.
                        if (pluginUpdateCheck.PluginName.Equals(this.ListView1.SelectedItems[0].SubItems[0].Text, StringComparison.Ordinal))
                        {
                            var result = pluginUpdateCheck.Uninstall(Convert.ToBoolean(this.saveToZip));

                            // avoid resetting the value back to false if "Uninstall" returns "false".
                            if (!this.pluginChanges && result)
                            {
                                this.pluginChanges = result;
                            }
                        }
                    }
                }
                else
                {
                    _ = MainForm.MessageManager.ShowInfo(
                        "The selected plugin is not installed, or the plugin was installed and this program was not restarted yet to know that it was.",
                        "Info!",
                        Convert.ToBoolean(SettingsFile.SettingsJson.UseNotifications));
                }
            }
        }
    }
}
