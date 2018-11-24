// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Net;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using Els_kom_Core.Controls;

    internal class PluginUpdateCheck : IDisposable
    {
        private bool disposedValue = false;

        internal PluginUpdateCheck()
        {
        }

        internal static WebClient WebClient { get; private protected set; }

        internal static NotifyIcon NotifyIcon { get; set; }

        internal static string[] PluginUrls { get; private protected set; }

        internal DialogResult ShowMessage
            => !this.InstalledVersion.Equals(this.CurrentVersion) && !string.IsNullOrEmpty(this.InstalledVersion)
                ? MessageManager.ShowInfo(
                    $"Update {this.CurrentVersion} for plugin {this.PluginName} is availible.",
                    "New plugin update.",
                    NotifyIcon)
                : DialogResult.OK;

        internal string PluginName { get; private protected set; }

        internal string CurrentVersion { get; private protected set; }

        internal string InstalledVersion { get; private protected set; }

        internal string DownloadUrl { get; private protected set; }

        internal string[] DownloadFiles { get; private protected set; }

        public void Dispose() => this.Dispose(true);

        /// <summary>
        /// Checks for plugin updates from the provided plugin source urls.
        /// </summary>
        /// <param name="pluginURLs">The repository urls to the plugins.</param>
        // catches the plugin urls and uses that cache to detect added urls, and only appends those to the list.
        internal static List<PluginUpdateCheck> CheckForUpdates(string[] pluginURLs)
        {
            var pluginUpdateChecks = new List<PluginUpdateCheck>();

            // fixup the github urls (if needed).
            for (var i = 0; i < pluginURLs.Length; i++)
            {
                pluginURLs[i] = pluginURLs[i].Replace(
                    "https://github.com/",
                    "https://raw.githubusercontent.com/") + (
                    pluginURLs[i].EndsWith("/") ? "master/plugins.xml" : "/master/plugins.xml");
            }

            if (WebClient == null)
            {
                WebClient = new WebClient();
            }

            if (PluginUrls == null)
            {
                PluginUrls = new List<string>().ToArray();
            }

            foreach (var pluginURL in pluginURLs)
            {
                if (!PluginUrls.Contains(pluginURL))
                {
                    try
                    {
                        var doc = XDocument.Parse(WebClient.DownloadString(pluginURL));
                        var elements = doc.Root.Elements("Plugin");
                        foreach (var element in elements)
                        {
                            var currentVersion = element.Attribute("Version").Value;
                            var pluginName = element.Attribute("Name").Value;
                            var found = false;
                            foreach (var callbackplugin in ExecutionManager.Callbackplugins)
                            {
                                if (pluginName.Equals(callbackplugin.GetType().Namespace))
                                {
                                    found = true;
                                    var installedVersion = callbackplugin.GetType().Assembly.GetName().Version.ToString();
                                    var pluginUpdateCheck = new PluginUpdateCheck
                                    {
                                        CurrentVersion = currentVersion,
                                        InstalledVersion = installedVersion,
                                        PluginName = pluginName,
                                        DownloadUrl = $"{element.Attribute("DownloadUrl").Value}/{currentVersion}/",
                                        DownloadFiles = element.Descendants("DownloadFile").Select(y => y.Attribute("Name").Value).ToArray(),
                                    };
                                    pluginUpdateChecks.Add(pluginUpdateCheck);
                                }
                            }

                            foreach (var komplugin in KOMManager.Komplugins)
                            {
                                if (pluginName.Equals(komplugin.GetType().Namespace))
                                {
                                    found = true;
                                    var installedVersion = komplugin.GetType().Assembly.GetName().Version.ToString();
                                    var pluginUpdateCheck = new PluginUpdateCheck
                                    {
                                        CurrentVersion = currentVersion,
                                        InstalledVersion = installedVersion,
                                        PluginName = pluginName,
                                        DownloadUrl = $"{element.Attribute("DownloadUrl").Value}/{currentVersion}/",
                                        DownloadFiles = element.Descendants("DownloadFile").Select(y => y.Attribute("Name").Value).ToArray(),
                                    };
                                    pluginUpdateChecks.Add(pluginUpdateCheck);
                                }
                            }

                            // list these as well so the plugin updater form works.
                            if (!found)
                            {
                                var pluginUpdateCheck = new PluginUpdateCheck
                                {
                                    CurrentVersion = currentVersion,
                                    InstalledVersion = string.Empty,
                                    PluginName = pluginName,
                                    DownloadUrl = $"{element.Attribute("DownloadUrl").Value}/{currentVersion}/",
                                    DownloadFiles = element.Descendants("DownloadFile").Select(y => y.Attribute("Name").Value).ToArray(),
                                };
                                pluginUpdateChecks.Add(pluginUpdateCheck);
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        MessageManager.ShowError(
                            $"Failed to download the plugins sources list.{Environment.NewLine}Reason: {ex.Message}",
                            "Error!",
                            NotifyIcon);
                    }
                }

                // append the string to the cache.
                PluginUrls = PluginUrls.Append(pluginURL).ToArray();
            }

            return pluginUpdateChecks;
        }

        internal bool Install(bool saveToZip)
        {
            foreach (var downloadFile in this.DownloadFiles)
            {
                try
                {
                    var path = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}{downloadFile}";
                    WebClient.DownloadFile($"{this.DownloadUrl}{downloadFile}", path);
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

                    return true;
                }
                catch (WebException ex)
                {
                    MessageManager.ShowError(
                        $"Failed to install the selected plugin.{Environment.NewLine}Reason: {ex.Message}",
                        "Error!",
                        MainControl.NotifyIcon1);
                }
            }

            return false;
        }

        internal bool Uninstall(bool saveToZip)
        {
            foreach (var downloadFile in this.DownloadFiles)
            {
                var path = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}{downloadFile}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

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

                return true;
            }

            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (WebClient != null)
                    {
                        WebClient.Dispose();
                        WebClient = null;
                    }
                }

                this.disposedValue = true;
            }
        }
    }
}
