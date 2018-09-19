// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System;
    using System.Windows.Forms;
    using Els_kom_Core.Classes;
    using Els_kom_Core.Controls;

    internal partial class MainForm : Form
    {
        private Form aboutfrm;
        private Form settingsfrm;

        internal MainForm() => this.InitializeComponent();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var cancel = e.Cancel;

            // CloseReason UnloadMode = e->CloseReason; <-- Removed because not used.
            if (!this.MainControl1.AbleToClose() && !MainControl.Closable)
            {
                cancel = true;
                MessageBox.Show("Cannot close Els_kom while packing, unpacking, testing mods, or updating the game.", "Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (!cancel)
            {
                SettingsFile.Settingsxml = null;
                this.MainControl1.End_settings_loop = true;
            }

            e.Cancel = cancel;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
            if (this.MainControl1.VersionCheck())
            {
                this.MainControl1.LoadControl();
            }
        }

        private void MainForm_MouseLeave(object sender, EventArgs e) => this.MainControl1.Label1.Text = string.Empty;

        private void MainControl1_CloseForm(object sender, EventArgs e)
        {
            this.aboutfrm?.Close();
            this.settingsfrm?.Close();
            this.Close();
        }

        private void MainControl1_TrayNameChange(object sender, EventArgs e) => this.MainControl1.NotifyIcon1.Text = this.Text;

        private void MainControl1_TrayClick(object sender, MouseEventArgs e)
        {
            if ((AboutForm.Label1 != null && AboutForm.Label1 == "1") || (SettingsForm.Label1 != null && SettingsForm.Label1 == "1"))
            {
                // I have to Sadly disable left button on the Notify Icon to prevent a bug with AboutForm Randomly Unloading or not reshowing.
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (this.ShowInTaskbar)
                    {
                        if (this.WindowState == FormWindowState.Minimized)
                        {
                            this.WindowState = FormWindowState.Normal;
                            this.Activate();
                        }
                        else
                        {
                            this.WindowState = FormWindowState.Minimized;
                        }
                    }
                    else if (this.MainControl1.NotifyIcon1.Visible)
                    {
                        if (this.WindowState == FormWindowState.Minimized)
                        {
                            this.WindowState = FormWindowState.Normal;
                            this.Show();
                        }
                        else
                        {
                            this.Hide();
                            this.WindowState = FormWindowState.Minimized;
                        }
                    }
                }
            }
        }

        private void MainControl1_AboutForm(object sender, EventArgs e)
        {
            // prevent from having multiple about forms opening at the same time in case as well.
            if (this.aboutfrm == null && this.settingsfrm == null)
            {
                this.aboutfrm = new AboutForm();
                this.aboutfrm.ShowDialog();
                this.aboutfrm.Dispose();
                this.aboutfrm = null;
            }
        }

        private void MainControl1_ConfigForm(object sender, EventArgs e)
        {
            // avoids an issue where more than 1 settings form can be opened at the same time.
            if (this.settingsfrm == null && this.aboutfrm == null)
            {
                this.settingsfrm = new SettingsForm();
                this.settingsfrm.ShowDialog();
                this.settingsfrm.Dispose();
                this.settingsfrm = null;
            }
        }

        private void MainControl1_TrayIconChange(object sender, EventArgs e)
        {
            // this seem to not update the form icon at runtime...
            this.Icon = Icons.FormIcon;
            this.MainControl1.NotifyIcon1.Icon = this.Icon;
        }
    }
}
