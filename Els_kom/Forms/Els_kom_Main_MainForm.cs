// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    internal partial class MainForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Form aboutfrm;
        private System.Windows.Forms.Form settingsfrm;

        internal MainForm() => this.InitializeComponent();

        private bool Enablehandlers { get; set; }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            this.Enablehandlers = !this.ShowInTaskbar ? true : false;
            if (this.Enablehandlers && m.Msg == this.MainControl1.GetSysCommand())
            {
                if (m.WParam.ToInt32() == this.MainControl1.GetMinimizeCommand())
                {
                    this.Hide();
                    m.Result = System.IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() == this.MainControl1.GetMaximizeCommand())
                {
                    this.Show();
                    this.Activate();
                    m.Result = System.IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() == this.MainControl1.GetRestoreCommand())
                {
                    this.Show();
                    this.Activate();
                    m.Result = System.IntPtr.Zero;
                    return;
                }
            }

            base.WndProc(ref m);
        }

        private void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            var cancel = e.Cancel;

            // CloseReason UnloadMode = e->CloseReason; <-- Removed because not used.
            if (!this.MainControl1.AbleToClose() && !Els_kom_Core.Controls.MainControl.Closable)
            {
                cancel = true;
                System.Windows.Forms.MessageBox.Show("Cannot close Els_kom while packing, unpacking, testing mods, or updating the game.", "Info!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

            if (!cancel)
            {
                Els_kom_Core.Classes.SettingsFile.Settingsxml?.Dispose();
                this.MainControl1.End_settings_loop = true;
            }

            e.Cancel = cancel;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            this.Hide();
            if (this.MainControl1.VersionCheck())
            {
                this.MainControl1.LoadControl();
            }
        }

        private void MainForm_MouseLeave(object sender, System.EventArgs e) => this.MainControl1.Label1.Text = string.Empty;

        private void MainControl1_CloseForm(object sender, System.EventArgs e)
        {
            this.aboutfrm?.Close();
            this.settingsfrm?.Close();
            this.Close();
        }

        private void MainControl1_TrayNameChange(object sender, System.EventArgs e) => this.MainControl1.NotifyIcon1.Text = this.Text;

        private void MainControl1_TrayClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((AboutForm.Label1 != null && AboutForm.Label1 == "1") || (SettingsForm.Label1 != null && SettingsForm.Label1 == "1"))
            {
                // I have to Sadly disable left button on the Notify Icon to prevent a bug with AboutForm Randomly Unloading or not reshowing.
            }
            else
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (this.ShowInTaskbar)
                    {
                        if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
                        {
                            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                            this.Activate();
                        }
                        else
                        {
                            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                        }
                    }
                    else if (this.MainControl1.NotifyIcon1.Visible)
                    {
                        if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
                        {
                            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                            this.Show();
                        }
                        else
                        {
                            this.Hide();
                            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                        }
                    }
                }
            }
        }

        private void MainControl1_AboutForm(object sender, System.EventArgs e)
        {
            // prevent from having multiple about forms opening at the same time in case as well.
            if (this.aboutfrm == null && this.settingsfrm == null)
            {
                this.aboutfrm = new AboutForm();
                this.aboutfrm.ShowDialog();
                this.aboutfrm = null;
            }
        }

        private void MainControl1_ConfigForm(object sender, System.EventArgs e)
        {
            // avoids an issue where more than 1 settings form can be opened at the same time.
            if (this.settingsfrm == null && this.aboutfrm == null)
            {
                this.settingsfrm = new SettingsForm();
                this.settingsfrm.ShowDialog();
                this.settingsfrm = null;
            }
        }

        private void MainControl1_TrayIconChange(object sender, System.EventArgs e)
        {
            // this seem to not update the form icon at runtime...
            this.Icon = Icons.FormIcon;
            this.MainControl1.NotifyIcon1.Icon = this.Icon;
        }
    }
}
