// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    internal partial class MainForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Allows the user to enable or disable their event
        /// handler at will if they only want it to sometimes
        /// fire.
        /// </summary>
        private bool Enablehandlers;
        private System.Windows.Forms.Form aboutfrm;
        private System.Windows.Forms.Form settingsfrm;

        internal MainForm() => InitializeComponent();

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            Enablehandlers = !ShowInTaskbar ? true : false;
            if (Enablehandlers && m.Msg == MainControl1.GetSysCommand())
            {
                if (m.WParam.ToInt32() == MainControl1.GetMinimizeCommand())
                {
                    Hide();
                    m.Result = System.IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() == MainControl1.GetMaximizeCommand())
                {
                    Show();
                    Activate();
                    m.Result = System.IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() == MainControl1.GetRestoreCommand())
                {
                    Show();
                    Activate();
                    m.Result = System.IntPtr.Zero;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            var Cancel = e.Cancel;
            // CloseReason UnloadMode = e->CloseReason; <-- Removed because not used.
            if (!MainControl1.AbleToClose() && !Els_kom_Core.Controls.MainControl._closable)
            {
                Cancel = true;
                System.Windows.Forms.MessageBox.Show("Cannot close Els_kom while packing, unpacking, testing mods, or updating the game.", "Info!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            if (!Cancel)
            {
                Els_kom_Core.Classes.SettingsFile.Settingsxml?.Dispose();
                MainControl1.end_settings_loop = true;
            }
            e.Cancel = Cancel;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            Hide();
            if (MainControl1.VersionCheck())
            {
                MainControl1.LoadControl();
            }
        }

        private void MainForm_MouseLeave(object sender, System.EventArgs e) => MainControl1.Label1.Text = "";

        private void MainControl1_CloseForm(object sender, System.EventArgs e)
        {
            aboutfrm?.Close();
            settingsfrm?.Close();
            Close();
        }

        private void MainControl1_TrayNameChange(object sender, System.EventArgs e) => MainControl1.NotifyIcon1.Text = Text;

        private void MainControl1_TrayClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((AboutForm.Label1 != null && AboutForm.Label1 == "1") || (SettingsForm.Label1 != null && SettingsForm.Label1 == "1"))
            {
                //I have to Sadly disable left button on the Notify Icon to prevent a bug with AboutForm Randomly Unloading or not reshowing.
            }
            else
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (ShowInTaskbar)
                    {
                        if (WindowState == System.Windows.Forms.FormWindowState.Minimized)
                        {
                            WindowState = System.Windows.Forms.FormWindowState.Normal;
                            Activate();
                        }
                        else
                        {
                            WindowState = System.Windows.Forms.FormWindowState.Minimized;
                        }
                    }
                    else if (MainControl1.NotifyIcon1.Visible)
                    {
                        if (WindowState == System.Windows.Forms.FormWindowState.Minimized)
                        {
                            WindowState = System.Windows.Forms.FormWindowState.Normal;
                            Show();
                        }
                        else
                        {
                            Hide();
                            WindowState = System.Windows.Forms.FormWindowState.Minimized;
                        }
                    }
                }
            }
        }

        private void MainControl1_AboutForm(object sender, System.EventArgs e)
        {
            // prevent from having multiple about forms opening at the same time in case as well.
            if (aboutfrm == null && settingsfrm == null)
            {
                aboutfrm = new AboutForm();
                aboutfrm.ShowDialog();
                aboutfrm = null;
            }
        }

        private void MainControl1_ConfigForm(object sender, System.EventArgs e)
        {
            // avoids an issue where more than 1 settings form can be opened at the same time.
            if (settingsfrm == null && aboutfrm == null)
            {
                settingsfrm = new SettingsForm();
                settingsfrm.ShowDialog();
                settingsfrm = null;
            }
        }
    }
}
