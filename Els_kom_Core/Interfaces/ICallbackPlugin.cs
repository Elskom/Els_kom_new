// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Interfaces
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Interface for Els_kom Test Mods callback plugins.
    /// </summary>
    public interface ICallbackPlugin
    {
        /// <summary>
        /// Gets name of the Test Mods Callback plugin.
        /// </summary>
        /// <value>
        /// Name of the Test Mods Callback plugin.
        /// </value>
        string PluginName { get; }

        /// <summary>
        /// Gets a value indicating whether this plugin has it's own settings window that should display from the settings window.
        /// </summary>
        /// <value>
        /// A value indicating whether this plugin has it's own settings window that should display from the settings window.
        /// </value>
        bool SupportsSettings { get; }

        /// <summary>
        /// Gets a value indicating whether the form should be shown as Modal Dialog Window.
        /// </summary>
        /// <value>
        /// A value indicating whether the form should be shown as Modal Dialog Window.
        /// </value>
        bool ShowModal { get; }

        /// <summary>
        /// Gets the plugin's actual settings window for showing from Els_kom's core at runtime.
        ///
        /// Note: Create instance before returning.
        /// </summary>
        /// <value>
        /// The plugin's actual settings window for showing from Els_kom's core at runtime.
        ///
        /// Note: Create instance before returning.
        /// </value>
        Form SettingsWindow { get; }

        /// <summary>
        /// Test Mods Callback Function.
        ///
        /// Do not code in an loop that runs kom spoofing stuff manually.
        /// Instead Els_kom will invoke this in an indefinite loop until
        /// game process is closed. However an for loop for every kom
        /// file to be spoofed or something is probably ok.
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown when a plugin does not have this implemented yet.</exception>
        void TestModsCallback();
    }
}
