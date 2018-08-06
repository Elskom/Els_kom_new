// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    internal partial class SettingsForm : System.Windows.Forms.Form
    {
        internal static string Label1 = "0";
        internal SettingsForm() => InitializeComponent();

        private void SettingsForm_Load(object sender, System.EventArgs e)
        {
            // This new member is to ensure the data from
            // the control load function does not try to get the data
            // from the Settings XMLObject in Designer mode
            // (which contains no instance there anyway) that causes
            // the Designer to fail to load the form the control is on.
            SettingsControl1.InitControl();
            Label1 = "1";
        }

        private void SettingsForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Label1 = "0";
            SettingsControl1.SaveSettings();
        }

        private void SettingsControl1_OpenPluginsForm(object sender, System.EventArgs e)
        {
            var pluginsForm = new PluginsForm();
            pluginsForm.ShowDialog();
        }
    }
}
