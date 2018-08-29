// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    /// <summary>
    /// PluginsControl control for Els_kom's Plugins installer/updater form.
    /// </summary>
    public partial class PluginsControl : System.Windows.Forms.UserControl
    {
        private string[] sources;
        private System.Xml.Linq.XDocument doc;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsControl"/> class.
        /// </summary>
        public PluginsControl() => this.InitializeComponent();

        /// <summary>
        /// Initializes the Plugins control for Els_kom's Plugins installer/updater form.
        /// </summary>
        public void InitControl()
        {
            Classes.SettingsFile.Settingsxml?.ReopenFile();
            this.sources = Classes.SettingsFile.Settingsxml?.Read("Sources", "Source", null);
            for (var i = 0; i < this.sources.Length; i++)
            {
                this.sources[i] = this.sources[i].Replace(
                    "https://github.com/",
                    "https://raw.githubusercontent.com/") + (
                    this.sources[i].EndsWith("/") ? "master/plugins.xml" : "/master/plugins.xml");
            }

            foreach (var source in this.sources)
            {
                var request = System.Net.WebRequest.Create(source);
                var response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                var reader = new System.IO.StreamReader(dataStream);
                var xmlResponse = reader.ReadToEnd();
                reader.Dispose();
                response.Dispose();
                this.doc = System.Xml.Linq.XDocument.Parse(xmlResponse);
                var elements = this.doc.Root.Elements("Plugin");
                foreach (var element in elements)
                {
                    var items = new string[3];
                    items[0] = element.Attribute("Name").Value;
                    items[1] = element.Attribute("Version").Value;
                    foreach (var callbackplugin in Classes.ExecutionManager.Callbackplugins)
                    {
                        if (items[0].Equals(callbackplugin.GetType().Namespace))
                        {
                            items[2] = callbackplugin.GetType().Assembly.GetName().Version.ToString();
                        }
                    }

                    foreach (var komplugin in Classes.KOMManager.Komplugins)
                    {
                        if (items[0].Equals(komplugin.GetType().Namespace))
                        {
                            items[2] = komplugin.GetType().Assembly.GetName().Version.ToString();
                        }
                    }

                    this.ListView1.Items.Add(new System.Windows.Forms.ListViewItem(items));
                }
            }
        }

        private void InstallButton_Click(object sender, System.EventArgs e)
        {
            // first check if plugin is installed already, then uninstall the old version. before installing.
            // install the selected plugin.
            // TODO: Add plugin install code here.
            Classes.MessageManager.ShowInfo(this.ListView1.SelectedItems[0].SubItems[0].Text, "Debug!");
            Classes.MessageManager.ShowInfo(this.ListView1.SelectedItems[0].SubItems[1].Text, "Debug!");
            Classes.MessageManager.ShowInfo(this.ListView1.SelectedItems[0].SubItems[2].Text, "Debug!");
        }

        private void OkButton_Click(object sender, System.EventArgs e) => this.FindForm()?.Close();

        private void UninstallButton_Click(object sender, System.EventArgs e)
        {
            // first verify plugin is installed though.
            // uninstall the selected plugin.
            // TODO: Add plugin uninstall code here.
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
