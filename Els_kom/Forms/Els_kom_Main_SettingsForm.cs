// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    internal partial class SettingsForm : System.Windows.Forms.Form
    {
        private static string label1 = string.Empty;

        internal SettingsForm() => this.InitializeComponent();

        internal static string Label1
        {
            get
            {
                if (label1 == string.Empty)
                {
                    label1 = "0";
                }

                return label1;
            }

            private set => label1 = value;
        }

        private void SettingsForm_Load(object sender, System.EventArgs e)
        {
            // This new member is to ensure the data from
            // the control load function does not try to get the data
            // from the Settings XMLObject in Designer mode
            // (which contains no instance there anyway) that causes
            // the Designer to fail to load the form the control is on.
            this.SettingsControl1.InitControl();
            Label1 = "1";
        }

        private void SettingsForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Label1 = "0";
            this.SettingsControl1.SaveSettings();
        }

        private void SettingsControl1_OpenPluginsForm(object sender, System.EventArgs e)
        {
            var pluginsForm = new PluginsForm();
            pluginsForm.ShowDialog();
        }
    }
}
