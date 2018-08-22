// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    /// <summary>
    /// PluginsControl control for Els_kom's Plugins installer/updater form.
    /// </summary>
    internal partial class PluginsControl : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsControl"/> class.
        /// </summary>
        internal PluginsControl() => this.InitializeComponent();

        /// <summary>
        /// Initializs the Plugins control for Els_kom's Plugins installer/updater form.
        /// </summary>
        public void InitControl()
        {
            // TODO: Get the XML Data from the saved plugins source urls
            // (converting each one to a raw github user content url pointing to Plugins.xml).
        }

        private void InstallButton_Click(object sender, System.EventArgs e)
        {
            // install the selected plugin.
            // TODO: Add plugin install code here.
            // first check if plugin is installed already, then uninstall the old version. before installing.
            Classes.MessageManager.ShowInfo(this.ListView1.SelectedItems[0].SubItems[0].Text, "Debug!");
            Classes.MessageManager.ShowInfo(this.ListView1.SelectedItems[0].SubItems[1].Text, "Debug!");
        }

        private void OkButton_Click(object sender, System.EventArgs e) => this.FindForm()?.Close();

        private void UninstallButton_Click(object sender, System.EventArgs e)
        {
            // uninstall the selected plugin.
            // TODO: Add plugin uninstall code here.
            // first verify plugin is installed though.
        }

        private void PluginsControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e) => this.Label1.Text = "Select Plugins to install from the list Below. "
                + "If a Plugin you expect is not shown Configure a Plugin "
                + "Source Repository to look in for Plugins to install from."
                + System.Environment.NewLine + System.Environment.NewLine
                + "Note: Plugin Source Repsitories should only contain an "
                + "Plugins.xml file which lists the version, Plugin Source "
                + "Code GitHub Repository Releases url, and a list of files "
                + "to download from there.";
    }
}
