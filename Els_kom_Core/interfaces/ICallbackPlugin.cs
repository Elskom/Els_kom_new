// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.interfaces
{
    /// <summary>
    /// Interface for Els_kom Test Mods callback plugins.
    /// </summary>
    public interface ICallbackPlugin
    {
        /// <summary>
        /// Name of the Test Mods Callback plugin.
        /// </summary>
        string PluginName { get; }
        /// <summary>
        /// Returns if this plugin has it's own settings window that should display from the settings window.
        /// </summary>
        bool SupportsSettings { get; }
        /// <summary>
        /// Returns the plugin's actual settings window for executing in Els_kom's core at runtime.
        /// </summary>
        System.Windows.Forms.Form SettingsWindow { get; }

        /// <summary>
        /// Test Mods Callback Function.
        ///
        /// Do not code in an loop that runs kom spoofing stuff manually.
        /// Instead Els_kom will invoke this in an indefinite loop until
        /// game process is closed. However an for loop for every kom
        /// file to be spoofed or something is probably ok.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Thrown when a plugin does not have this implemented yet.</exception>
        void TestModsCallback();
    }
}
