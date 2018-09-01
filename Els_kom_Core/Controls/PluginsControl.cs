// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
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
            var closing = false;
            this.doc = new List<XDocument>();
            SettingsFile.Settingsxml?.ReopenFile();
            this.sources = SettingsFile.Settingsxml?.Read("Sources", "Source", null);
            int.TryParse(SettingsFile.Settingsxml?.Read("SaveToZip"), out this.saveToZip);
            for (var i = 0; i < this.sources.Length; i++)
            {
                this.sources[i] = this.sources[i].Replace(
                    "https://github.com/",
                    "https://raw.githubusercontent.com/") + (
                    this.sources[i].EndsWith("/") ? "master/plugins.xml" : "/master/plugins.xml");
            }

            this.webClient = new WebClient();
            try
            {
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
            catch (WebException ex)
            {
                // prevent form from flickering.
                MessageManager.ShowError(
                    $"Failed to download the plugins sources list.{Environment.NewLine}Reason: {ex.Message}",
                    "Error!");
                closing = true;
            }

            if (closing)
            {
                this.FindForm()?.Close();
                this.webClient.Dispose();
            }
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (this.ListView1.SelectedItems.Count > 0)
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

                try
                {
                    foreach (var downloadFile in downloadFiles)
                    {
                        var path = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}{downloadFile}";
                        this.webClient.DownloadFile($"{downloadUrl}{downloadFile}", path);
                        var saveToZip = Convert.ToBoolean(this.saveToZip);
                        if (saveToZip)
                        {
                            var zippath = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}plugins.zip";
                            var zipFile = ZipFile.Open(zippath, ZipArchiveMode.Update);
                            foreach (var entry in zipFile.Entries)
                            {
                                if (entry.FullName.Equals(downloadFile))
                                {
                                    entry.Delete();
                                }
                            }

                            zipFile.CreateEntryFromFile(path, downloadFile);
                            File.Delete(path);
                            zipFile.Dispose();
                        }
                    }

                    this.pluginChanges = true;
                }
                catch (WebException ex)
                {
                    MessageManager.ShowError(
                        $"Failed to install the selected plugin.{Environment.NewLine}Reason: {ex.Message}",
                        "Error!");
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
                this.webClient.Dispose();
            }
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            if (this.ListView1.SelectedItems.Count > 0)
            {
                if (!this.ListView1.SelectedItems[0].SubItems[2].Text.Equals(string.Empty))
                {
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
                                    downloadFiles = element.Descendants("DownloadFile").Select(y => y.Attribute("Name").Value).ToArray();
                                }
                            }
                        }
                    }

                    foreach (var downloadFile in downloadFiles)
                    {
                        var path = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}{downloadFile}";
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        var saveToZip = Convert.ToBoolean(this.saveToZip);
                        if (saveToZip)
                        {
                            var zippath = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}plugins.zip";
                            var zipFile = ZipFile.Open(zippath, ZipArchiveMode.Update);
                            foreach (var entry in zipFile.Entries)
                            {
                                if (entry.FullName.Equals(downloadFile))
                                {
                                    entry.Delete();
                                }
                            }

                            var entries = zipFile.Entries.Count;
                            zipFile.Dispose();
                            if (entries == 0)
                            {
                                File.Delete(zippath);
                            }
                        }
                    }

                    this.pluginChanges = true;
                }
                else
                {
                    MessageManager.ShowInfo(
                        "The selected plugin is not installed, or the plugin was installed and this program was not restarted yet to know that it was.",
                        "Info!");
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
