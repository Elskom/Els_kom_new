// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    using System;
    using System.Windows.Forms;
    using Els_kom_Core.Classes;
    using Elskom.Generic.Libs;

    /// <summary>
    /// PluginsControl control for Els_kom's Plugins installer/updater form.
    /// </summary>
    public partial class PluginsControl : UserControl
    {
        private bool pluginChanges = false;
        private int saveToZip = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsControl"/> class.
        /// </summary>
        public PluginsControl() => this.InitializeComponent();

        /// <summary>
        /// Initializes the Plugins control for Els_kom's Plugins installer/updater form.
        /// </summary>
        public void InitControl()
        {
            // var closing = false;
            // this.doc = new List<XDocument>();
            SettingsFile.Settingsxml?.ReopenFile();
            int.TryParse(SettingsFile.Settingsxml?.TryRead("SaveToZip"), out this.saveToZip);

            // update the list if there were new sources added during the program execution.
            MainControl.PluginUpdateChecks = PluginUpdateCheck.CheckForUpdates(
                SettingsFile.Settingsxml?.TryRead("Sources", "Source", null));
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (this.ListView1.SelectedItems.Count > 0)
            {
                foreach (var pluginUpdateCheck in MainControl.PluginUpdateChecks)
                {
                    // install only the selected plugin.
                    if (pluginUpdateCheck.PluginName.Equals(this.ListView1.SelectedItems[0].SubItems[0].Text))
                    {
                        var result = pluginUpdateCheck.Install(Convert.ToBoolean(this.saveToZip));

                        // avoid resetting the value back to false if "Uninstall" returns "false".
                        if (!this.pluginChanges)
                        {
                            if (result)
                            {
                                this.pluginChanges = result;
                            }
                        }
                    }
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (this.pluginChanges)
            {
                var result = MessageManager.ShowQuestion(
                    "A plugin was installed, uninstalled or updated. Do you want to restart this program now for the changes to take effect?",
                    "Info!");
                if (result == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
            else
            {
                this.FindForm()?.Close();
            }
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            if (this.ListView1.SelectedItems.Count > 0)
            {
                if (!this.ListView1.SelectedItems[0].SubItems[2].Text.Equals(string.Empty))
                {
                    foreach (var pluginUpdateCheck in MainControl.PluginUpdateChecks)
                    {
                        // install only the selected plugin.
                        if (pluginUpdateCheck.PluginName.Equals(this.ListView1.SelectedItems[0].SubItems[0].Text))
                        {
                            var result = pluginUpdateCheck.Uninstall(Convert.ToBoolean(this.saveToZip));

                            // avoid resetting the value back to false if "Uninstall" returns "false".
                            if (!this.pluginChanges)
                            {
                                if (result)
                                {
                                    this.pluginChanges = result;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageManager.ShowInfo(
                        "The selected plugin is not installed, or the plugin was installed and this program was not restarted yet to know that it was.",
                        "Info!",
                        PluginUpdateCheck.NotifyIcon,
                        Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
                }
            }
        }

        private void PluginsControl_Paint(object sender, PaintEventArgs e)
            => this.Label1.Text = "Select Plugins to install from the list Below. "
                + "If a Plugin you expect is not shown Configure a Plugin "
                + "Source Repository to look in for Plugins to install from."
                + Environment.NewLine + Environment.NewLine
                + "Note: Plugin Source Repsitories should only contain an "
                + "Plugins.xml file which lists the version, Plugin Source "
                + "Code GitHub Repository Releases url, and a list of files "
                + "to download from there.";
    }
}
