// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using Els_kom_Core.Classes;

    /// <summary>
    /// PluginsControl control for Els_kom's Plugins installer/updater form.
    /// </summary>
    public partial class PluginsControl : UserControl
    {
        private string[] sources;
        private List<XDocument> doc;
        private WebClient webClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsControl"/> class.
        /// </summary>
        public PluginsControl() => this.InitializeComponent();

        /// <summary>
        /// Initializes the Plugins control for Els_kom's Plugins installer/updater form.
        /// </summary>
        public void InitControl()
        {
            this.doc = new List<XDocument>();
            SettingsFile.Settingsxml?.ReopenFile();
            this.sources = SettingsFile.Settingsxml?.Read("Sources", "Source", null);
            for (var i = 0; i < this.sources.Length; i++)
            {
                this.sources[i] = this.sources[i].Replace(
                    "https://github.com/",
                    "https://raw.githubusercontent.com/") + (
                    this.sources[i].EndsWith("/") ? "master/plugins.xml" : "/master/plugins.xml");
            }

            this.webClient = new WebClient();
            foreach (var source in this.sources)
            {
                var doc = XDocument.Parse(this.webClient.DownloadString(source));
                var elements = doc.Root.Elements("Plugin");
                foreach (var element in elements)
                {
                    var items = new string[3];
                    items[0] = element.Attribute("Name").Value;
                    items[1] = element.Attribute("Version").Value;
                    foreach (var callbackplugin in ExecutionManager.Callbackplugins)
                    {
                        if (items[0].Equals(callbackplugin.GetType().Namespace))
                        {
                            items[2] = callbackplugin.GetType().Assembly.GetName().Version.ToString();
                        }
                    }

                    foreach (var komplugin in KOMManager.Komplugins)
                    {
                        if (items[0].Equals(komplugin.GetType().Namespace))
                        {
                            items[2] = komplugin.GetType().Assembly.GetName().Version.ToString();
                        }
                    }

                    this.ListView1.Items.Add(new ListViewItem(items));
                }

                this.doc.Add(doc);
            }
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            var downloadUrl = string.Empty;
            var downloadFiles = new string[] { };
            foreach (var doc in this.doc)
            {
                var elements = doc.Root.Elements("Plugin");
                if (elements != null)
                {
                    foreach (var element in elements)
                    {
                        var name = element.Attribute("Name").Value;
                        if (name == this.ListView1.SelectedItems[0].SubItems[0].Text)
                        {
                            var attr = element.Attribute("DownloadUrl").Value;
                            downloadUrl = $"{attr}/{this.ListView1.SelectedItems[0].SubItems[1].Text}/";
                            downloadFiles = element.Descendants("DownloadFile").Select(y => y.Attribute("Name").Value).ToArray();
                        }
                    }
                }
            }

            foreach (var downloadFile in downloadFiles)
            {
                this.webClient.DownloadFile(
                    $"{downloadUrl}{downloadFile}",
                    $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}{downloadFile}");
            }

            // MessageManager.ShowInfo(this.ListView1.SelectedItems[0].SubItems[0].Text, "Debug!");
            // MessageManager.ShowInfo(this.ListView1.SelectedItems[0].SubItems[1].Text, "Debug!");
            // MessageManager.ShowInfo(this.ListView1.SelectedItems[0].SubItems[2].Text, "Debug!");
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
            this.webClient.Dispose();
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            // first verify plugin is installed though.
            // uninstall the selected plugin.
            // TODO: Add plugin uninstall code here.
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
