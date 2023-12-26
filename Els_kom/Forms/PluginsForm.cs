// Copyright (c) 2014-2023, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using Els_kom.Controls;
    using Microsoft.Extensions.DependencyInjection;

    internal partial class PluginsForm : /*Form*/ThemedForm
    {
        private bool pluginChanges;
        private int saveToZip;

        public PluginsForm()
            => this.InitializeComponent();

        private void PluginsForm_Load(object sender, EventArgs e)
        {
            this.saveToZip = SettingsFile.SettingsJson!.SaveToZip;
            var pluginTypes = new List<Type>();
            pluginTypes.AddRange(KOMManager.Callbackplugins.Select((x) => x.GetType()));
            pluginTypes.AddRange(KOMManager.Komplugins.Select((x) => x.GetType()));

            // update the list if there were new sources added during the program execution.
            _ = FormsApplication.ServiceProvider!.GetRequiredService<PluginUpdateCheck>().CheckForUpdates(
                SettingsFile.SettingsJson.Sources,
                pluginTypes);
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (this.ListView1.SelectedItems.Count > 0)
            {
                var pluginUpdateCheck = FormsApplication.ServiceProvider!.GetRequiredService<PluginUpdateCheck>();
                var pluginUpdataDatas = pluginUpdateCheck.PluginUpdateDatas;

                // install only the selected plugin.
                foreach (var pluginUpdateData in pluginUpdataDatas.Where(
                    pluginUpdateData => pluginUpdateData.PluginName.Equals(this.ListView1.SelectedItems[0].SubItems[0].Text, StringComparison.Ordinal)))
                {
                    var result = pluginUpdateCheck.Install(pluginUpdateData, Convert.ToBoolean(this.saveToZip));

                    // avoid resetting the value back to false if "Install" returns "false".
                    if (!this.pluginChanges && result)
                    {
                        this.pluginChanges = result;
                    }
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (this.pluginChanges)
            {
                _ = MessageManager.ShowInfo("A plugin was installed, uninstalled or updated. All plugins are about to be reloaded.", "Info!", Convert.ToBoolean(SettingsFile.SettingsJson!.UseNotifications));
                var genericPluginLoader = FormsApplication.ServiceProvider!.GetRequiredService<GenericPluginLoader>();
                genericPluginLoader.UnloadPlugins();
                KOMManager.Komplugins.Clear();
                ((List<IKomPlugin>)KOMManager.Komplugins).AddRange(
                    genericPluginLoader.LoadPlugins<IKomPlugin>(
                        "plugins",
                        Convert.ToBoolean(SettingsFile.SettingsJson.SaveToZip)));
                KOMManager.Callbackplugins.Clear();
                ((List<ICallbackPlugin>)KOMManager.Callbackplugins).AddRange(
                    genericPluginLoader.LoadPlugins<ICallbackPlugin>(
                        "plugins",
                        Convert.ToBoolean(SettingsFile.SettingsJson.SaveToZip)));
                KOMManager.Encryptionplugins.Clear();
                ((List<IEncryptionPlugin>)KOMManager.Encryptionplugins).AddRange(
                    genericPluginLoader.LoadPlugins<IEncryptionPlugin>(
                        "plugins",
                        Convert.ToBoolean(SettingsFile.SettingsJson.SaveToZip)));
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
                    var pluginUpdateCheck = FormsApplication.ServiceProvider!.GetRequiredService<PluginUpdateCheck>();
                    var pluginUpdataDatas = pluginUpdateCheck.PluginUpdateDatas;

                    // uninstall only the selected plugin.
                    foreach (var pluginUpdateData in pluginUpdataDatas.Where(
                        pluginUpdateData => pluginUpdateData.PluginName.Equals(this.ListView1.SelectedItems[0].SubItems[0].Text, StringComparison.Ordinal)))
                    {
                        var result = pluginUpdateCheck.Uninstall(pluginUpdateData, Convert.ToBoolean(this.saveToZip));

                        // avoid resetting the value back to false if "Uninstall" returns "false".
                        if (!this.pluginChanges && result)
                        {
                            this.pluginChanges = result;
                        }
                    }
                }
                else
                {
                    _ = MessageManager.ShowInfo(
                        "The selected plugin is not installed, or the plugin was installed and this program was not restarted yet to know that it was.",
                        "Info!",
                        Convert.ToBoolean(SettingsFile.SettingsJson!.UseNotifications));
                }
            }
        }
    }
}
