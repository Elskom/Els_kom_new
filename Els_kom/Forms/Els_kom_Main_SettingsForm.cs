// Copyright (c) 2014-2019, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Elskom.Generic.Libs;

    internal partial class SettingsForm : Form
    {
        internal SettingsForm() => this.InitializeComponent();

        internal static string Label1 { get; private set; } = "0";

        private static void OpenPluginSettings(Form plugsettingfrm, IWin32Window owner)
            => plugsettingfrm.ShowDialog(owner);

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // This new member is to ensure the data from
            // the control load function does not try to get the data
            // from the Settings XMLObject in Designer mode
            // (which contains no instance there anyway) that causes
            // the Designer to fail to load the form the control is on.
            this.SettingsControl1.InitControl();
            Label1 = "1";
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Label1 = "0";
            this.SettingsControl1.SaveSettings();
        }

        private void SettingsControl1_OpenPluginsForm(object sender, EventArgs e)
        {
            using (var pluginsForm = new PluginsForm())
            {
                pluginsForm.ShowDialog();
            }
        }

        private void SettingsControl1_OpenPluginsSettings(object sender, EventArgs e)
        {
            for (var i = 0; i < this.SettingsControl1.ListView1.SelectedItems.Count; i++)
            {
                var selitem = this.SettingsControl1.ListView1.SelectedItems[i];
                foreach (var callbackplugin in KOMStream.Callbackplugins)
                {
                    if (callbackplugin.PluginName.Equals(selitem.Text))
                    {
                        var plugsettingfrm = callbackplugin.SettingsWindow;
                        plugsettingfrm.Icon = Icons.FormIcon;
                        Task.Run(
                            () => OpenPluginSettings(
                                plugsettingfrm,
                                callbackplugin.ShowModal ? this : null));
                    }
                }
            }
        }
    }
}
