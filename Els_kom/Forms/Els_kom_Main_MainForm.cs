// Copyright (c) 2014-2019, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using Els_kom.Classes;
    using Els_kom.Enums;
    using Elskom.Generic.Libs;
    using XmlAbstraction;

    internal partial class MainForm : Form
    {
        private Form aboutfrm;
        private Form settingsfrm;
        private string elsDir = string.Empty;
        private string showintaskbarValue = string.Empty;
        private string showintaskbarValue2 = string.Empty;
        private string showintaskbarTempvalue = string.Empty;
        private string showintaskbarTempvalue2 = string.Empty;
        private string elsDirTemp = string.Empty;

        internal MainForm() => this.InitializeComponent();

        internal static List<PluginUpdateCheck> PluginUpdateChecks { get; set; }

        private bool Enablehandlers { get; set; }

        protected override void WndProc(ref Message m)
        {
            this.Enablehandlers = !this.ShowInTaskbar;
            if (this.Enablehandlers && m.Msg == (int)SYSCOMMANDS.WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == (int)SYSCOMMANDS.SC_MINIMIZE)
                {
                    this.Hide();
                    m.Result = IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() == (int)SYSCOMMANDS.SC_MAXIMIZE)
                {
                    this.Show();
                    this.Activate();
                    m.Result = IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() == (int)SYSCOMMANDS.SC_RESTORE)
                {
                    this.Show();
                    this.Activate();
                    m.Result = IntPtr.Zero;
                    return;
                }
            }

            base.WndProc(ref m);
        }

        private static bool AbleToClose() => ExecutionManager.RunningElsword ||
                ExecutionManager.RunningElswordDirectly ||
                KOMManager.PackingState ||
                KOMManager.UnpackingState ? false
                : true;

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var cancel = e.Cancel;

            // CloseReason UnloadMode = e->CloseReason; <-- Removed because not used.
            if (!AbleToClose() && !ForceClosure.ForceClose)
            {
                cancel = true;
                MessageManager.ShowInfo("Cannot close Els_kom while packing, unpacking, testing mods, or updating the game.", "Info!", Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
            }

            if (!cancel)
            {
                SettingsFile.Settingsxml = null;
            }

            e.Cancel = cancel;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
            var closing = false;
            if (!Directory.Exists(Application.StartupPath + "\\koms"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\koms");
            }

            if (ExecutionManager.IsElsKomRunning() == true)
            {
                MessageManager.ShowError("Sorry, Only 1 Instance is allowed at a time.", "Error!", false);
                closing = true;
            }
            else
            {
                SettingsFile.Settingsxml = new XmlObject(SettingsFile.Path, "<Settings></Settings>");
                this.elsDir = SettingsFile.Settingsxml?.TryRead("ElsDir");
                if (this.elsDir.Length < 1)
                {
                    MessageManager.ShowInfo("Welcome to Els_kom." + Environment.NewLine + "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + Environment.NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!", false);

                    // avoids an issue where more than 1 settings form can be opened at the same time.
                    if (this.settingsfrm == null && this.aboutfrm == null)
                    {
                        this.settingsfrm = new SettingsForm();
                        this.settingsfrm.ShowDialog();
                        this.settingsfrm.Dispose();
                        this.settingsfrm = null;
                    }
                }

                if (!int.TryParse(SettingsFile.Settingsxml?.TryRead("SaveToZip"), out var saveToZip1))
                {
                    // do nothing to silence a compile error.
                }

                if (!int.TryParse(SettingsFile.Settingsxml?.TryRead("LoadPDB"), out var loadPDB1))
                {
                    // do nothing to silence a compile error.
                }

                var komplugins = new GenericPluginLoader<IKomPlugin>().LoadPlugins("plugins", Convert.ToBoolean(saveToZip1), Convert.ToBoolean(loadPDB1));
                KOMManager.Komplugins.AddRange(komplugins);
                var callbackplugins = new GenericPluginLoader<ICallbackPlugin>().LoadPlugins("plugins", Convert.ToBoolean(saveToZip1), Convert.ToBoolean(loadPDB1));
                KOMManager.Callbackplugins.AddRange(callbackplugins);
                if (!Git.IsMaster)
                {
                    MessageManager.ShowInfo("This branch is not the master branch, meaning this is a feature branch to test changes. When finished please pull request them for the possibility of them getting merged into master.", "Info!", Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
                }

                if (Git.IsDirty)
                {
                    var resp = MessageBox.Show("This build was compiled with Uncommitted changes. As a result, this build might be unstable. Are you sure you want to run this build to test some changes to the code?", "Info!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (resp == DialogResult.No)
                    {
                        closing = true;
                    }
                }
            }

            if (!closing)
            {
                this.MessageManager1.Icon = this.Icon;
                this.MessageManager1.Text = this.Text;
                this.MessageManager1.Visible = true;
                var pluginTypes = new List<Type>();
                foreach (var callbackplugin in KOMManager.Callbackplugins)
                {
                    pluginTypes.Add(callbackplugin.GetType());
                }

                foreach (var komplugin in KOMManager.Komplugins)
                {
                    pluginTypes.Add(komplugin.GetType());
                }

                PluginUpdateChecks = PluginUpdateCheck.CheckForUpdates(
                    SettingsFile.Settingsxml?.TryRead("Sources", "Source", null),
                    pluginTypes);
                foreach (var pluginUpdateCheck in PluginUpdateChecks)
                {
                    // discard result.
                    var result = pluginUpdateCheck.ShowMessage;
                }

                this.Show();
                this.Activate();
            }
            else
            {
                SettingsFile.Settingsxml = null;
                this.aboutfrm?.Close();
                this.settingsfrm?.Close();
                this.Close();
            }
        }

        private void MainForm_MouseLeave(object sender, EventArgs e) => this.Label1.Text = string.Empty;

        private void Command1_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            var tr2 = new Thread(KOMManager.PackKoms)
            {
                Name = "Classes.KOMManager.PackKoms",
            };
            tr2.Start();
            this.packingTmr.Enabled = true;
        }

        private void Command1_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = "This option uses plugins to Pack koms.";

        private void Command2_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            var tr1 = new Thread(KOMManager.UnpackKoms)
            {
                Name = "Classes.KOMManager.UnpackKoms",
            };
            tr1.Start();
            this.unpackingTmr.Enabled = true;
        }

        private void Command2_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = "This option uses plugins to Unpack koms.";

        private void Command3_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;

            // prevent from having multiple about forms opening at the same time in case as well.
            if (this.aboutfrm == null && this.settingsfrm == null)
            {
                this.aboutfrm = new AboutForm();
                this.aboutfrm.ShowDialog();
                this.aboutfrm.Dispose();
                this.aboutfrm = null;
            }
        }

        private void Command3_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed.";

        private void Command4_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.TestMods();
        }

        private void Command4_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = "Test the mods you made.";

        private void Command5_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            var tr4 = new Thread(ExecutionManager.RunElswordLauncher)
            {
                Name = "Classes.ExecutionManager.RunElswordLauncher",
            };
            tr4.Start();
            this.launcherTmr.Enabled = true;
        }

        private void Command5_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens (you might have to remake some mods for some files).";

        private void Command6_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;

            // avoids an issue where more than 1 settings form can be opened at the same time.
            if (this.settingsfrm == null && this.aboutfrm == null)
            {
                this.settingsfrm = new SettingsForm();
                this.settingsfrm.ShowDialog();
                this.settingsfrm.Dispose();
                this.settingsfrm = null;
            }
        }

        private void Command6_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = "Shows the Settings Window. Here you can easily change the Settings to Els_kom.";

        private void Label1_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = string.Empty;

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cancel = false;
            if (ExecutionManager.RunningElsword ||
                ExecutionManager.RunningElswordDirectly ||
                KOMManager.PackingState ||
                KOMManager.UnpackingState)
            {
                cancel = true;
                MessageManager.ShowInfo("Cannot close Els_kom while packing, unpacking, testing mods, or updating the game.", "Info!", Convert.ToBoolean(Convert.ToInt32(SettingsFile.Settingsxml?.TryRead("UseNotifications") != string.Empty ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
            }

            if (!cancel)
            {
                SettingsFile.Settingsxml = null;
                this.aboutfrm?.Close();
                this.settingsfrm?.Close();
                this.Close();
                foreach (var pluginUpdateCheck in PluginUpdateChecks)
                {
                    pluginUpdateCheck.Dispose();
                }
            }
        }

        private void LauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            var tr4 = new Thread(ExecutionManager.RunElswordLauncher)
            {
                Name = "Classes.ExecutionManager.RunElswordLauncher",
            };
            tr4.Start();
            this.launcherTmr.Enabled = true;
        }

        private void UnpackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tr1 = new Thread(KOMManager.UnpackKoms)
            {
                Name = "Classes.KOMManager.UnpackKoms",
            };
            tr1.Start();
            this.unpackingTmr.Enabled = true;
        }

        private void TestModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.TestMods();
        }

        private void PackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tr2 = new Thread(KOMManager.PackKoms)
            {
                Name = "Classes.KOMManager.PackKoms",
            };
            tr2.Start();
            this.packingTmr.Enabled = true;
        }

        private void MessageManager1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((AboutForm.Label1 != null && AboutForm.Label1 == "1") || (SettingsForm.Label9 != null && SettingsForm.Label9 == "1"))
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
                    else if (this.MessageManager1.Visible)
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

        // Handles Packing on the Main Form.
        private void Packing(object sender, EventArgs e)
        {
            if (KOMManager.PackingState)
            {
                if (this.Command1.Enabled)
                {
                    this.Command1.Enabled = false;
                }

                if (this.Command2.Enabled)
                {
                    this.Command2.Enabled = false;
                }

                if (this.Command4.Enabled)
                {
                    this.Command4.Enabled = false;
                }

                if (this.Command5.Enabled)
                {
                    this.Command5.Enabled = false;
                }

                if (this.packToolStripMenuItem.Enabled)
                {
                    this.packToolStripMenuItem.Enabled = false;
                }

                if (this.unpackToolStripMenuItem.Enabled)
                {
                    this.unpackToolStripMenuItem.Enabled = false;
                }

                if (this.testModsToolStripMenuItem.Enabled)
                {
                    this.testModsToolStripMenuItem.Enabled = false;
                }

                if (this.launcherToolStripMenuItem.Enabled)
                {
                    this.launcherToolStripMenuItem.Enabled = false;
                }

                if (string.Equals(this.Label2.Text, string.Empty))
                {
                    this.Label2.Text = "Packing...";
                }

                if (!string.Equals(this.MessageManager1.Text, this.Label2.Text))
                {
                    this.MessageManager1.Text = this.Label2.Text;
                }
            }
            else
            {
                this.Command1.Enabled = true;
                this.Command2.Enabled = true;
                this.Command4.Enabled = true;
                this.Command5.Enabled = true;
                this.packToolStripMenuItem.Enabled = true;
                this.unpackToolStripMenuItem.Enabled = true;
                this.testModsToolStripMenuItem.Enabled = true;
                this.launcherToolStripMenuItem.Enabled = true;
                this.Label2.Text = string.Empty;
                this.MessageManager1.Text = this.Text;
                this.packingTmr.Enabled = false;
            }
        }

        // Handles Unpacking on the Main Form.
        private void Unpacking(object sender, EventArgs e)
        {
            if (KOMManager.UnpackingState)
            {
                if (this.Command1.Enabled)
                {
                    this.Command1.Enabled = false;
                }

                if (this.Command2.Enabled)
                {
                    this.Command2.Enabled = false;
                }

                if (this.Command4.Enabled)
                {
                    this.Command4.Enabled = false;
                }

                if (this.Command5.Enabled)
                {
                    this.Command5.Enabled = false;
                }

                if (this.packToolStripMenuItem.Enabled)
                {
                    this.packToolStripMenuItem.Enabled = false;
                }

                if (this.unpackToolStripMenuItem.Enabled)
                {
                    this.unpackToolStripMenuItem.Enabled = false;
                }

                if (this.testModsToolStripMenuItem.Enabled)
                {
                    this.testModsToolStripMenuItem.Enabled = false;
                }

                if (this.launcherToolStripMenuItem.Enabled)
                {
                    this.launcherToolStripMenuItem.Enabled = false;
                }

                if (string.Equals(this.Label2.Text, string.Empty))
                {
                    this.Label2.Text = "Unpacking...";
                }

                if (!string.Equals(this.MessageManager1.Text, this.Label2.Text))
                {
                    this.MessageManager1.Text = this.Label2.Text;
                }
            }
            else
            {
                this.Command1.Enabled = true;
                this.Command2.Enabled = true;
                this.Command4.Enabled = true;
                this.Command5.Enabled = true;
                this.packToolStripMenuItem.Enabled = true;
                this.unpackToolStripMenuItem.Enabled = true;
                this.testModsToolStripMenuItem.Enabled = true;
                this.launcherToolStripMenuItem.Enabled = true;
                this.Label2.Text = string.Empty;
                this.MessageManager1.Text = this.Text;
                this.unpackingTmr.Enabled = false;
            }
        }

        // Handles Testing Mods on the Main Form (before Elsword is executed).
        private void TestMods()
        {
            this.Command1.Enabled = false;
            this.Command2.Enabled = false;
            this.Command4.Enabled = false;
            this.Command5.Enabled = false;
            this.packToolStripMenuItem.Enabled = false;
            this.unpackToolStripMenuItem.Enabled = false;
            this.testModsToolStripMenuItem.Enabled = false;
            this.launcherToolStripMenuItem.Enabled = false;
            var di = new DirectoryInfo(Application.StartupPath + "\\koms");
            foreach (var fi in di.GetFiles("*.kom"))
            {
                var kom_file = fi.Name;

                // do not copy kom files that are in the koms directory but cannot be found to copy from taget directory to the backup directory to restore later.
                KOMManager.CopyKomFiles(kom_file, Application.StartupPath + "\\koms\\", this.elsDir + "\\data");
            }

            var tr3 = new Thread(ExecutionManager.RunElswordDirectly)
            {
                Name = "Classes.ExecutionManager.RunElswordDirectly",
            };
            tr3.Start();
            this.testModsTmr.Enabled = true;
        }

        // Handles Testing Mods on the Main Form (when Elsword (the game window) closes).
        private void TestMods2(object sender, EventArgs e)
        {
            var executing = ProcessExtensions.Executing;
            if (!executing)
            {
                if (ExecutionManager.RunningElswordDirectly)
                {
                    KOMManager.DeployCallBack(ExecutionManager.RunningElswordDirectly);
                    if (string.Equals(this.Label2.Text, string.Empty))
                    {
                        this.Label2.Text = "Testing Mods...";
                    }
                }
                else
                {
                    var di = new DirectoryInfo(Application.StartupPath + "\\koms");
                    foreach (var fi in di.GetFiles("*.kom"))
                    {
                        var kom_file = fi.Name;
                        KOMManager.MoveOriginalKomFilesBack(kom_file, this.elsDir + "\\data\\backup", this.elsDir + "\\data");
                    }

                    this.Command1.Enabled = true;
                    this.Command2.Enabled = true;
                    this.Command4.Enabled = true;
                    this.Command5.Enabled = true;
                    this.packToolStripMenuItem.Enabled = true;
                    this.unpackToolStripMenuItem.Enabled = true;
                    this.testModsToolStripMenuItem.Enabled = true;
                    this.launcherToolStripMenuItem.Enabled = true;
                    this.Label2.Text = string.Empty;

                    // restore window state from before testing mods.
                    this.WindowState = FormWindowState.Normal;
                    this.Show();
                    this.Activate();
                    this.testModsTmr.Enabled = false;
                }
            }
        }

        // Handles Updating the Game but disables the controls while it is updating to avoid unpacking,
        // packing, and testing mods.
        private void Launcher(object sender, EventArgs e)
        {
            if (!ProcessExtensions.Executing)
            {
                if (ExecutionManager.RunningElsword)
                {
                    if (this.Command1.Enabled)
                    {
                        this.Command1.Enabled = false;
                    }

                    if (this.Command2.Enabled)
                    {
                        this.Command2.Enabled = false;
                    }

                    if (this.Command4.Enabled)
                    {
                        this.Command4.Enabled = false;
                    }

                    if (this.Command5.Enabled)
                    {
                        this.Command5.Enabled = false;
                    }

                    if (this.packToolStripMenuItem.Enabled)
                    {
                        this.packToolStripMenuItem.Enabled = false;
                    }

                    if (this.unpackToolStripMenuItem.Enabled)
                    {
                        this.unpackToolStripMenuItem.Enabled = false;
                    }

                    if (this.testModsToolStripMenuItem.Enabled)
                    {
                        this.testModsToolStripMenuItem.Enabled = false;
                    }

                    if (this.launcherToolStripMenuItem.Enabled)
                    {
                        this.launcherToolStripMenuItem.Enabled = false;
                    }

                    if (string.Equals(this.Label2.Text, string.Empty))
                    {
                        this.Label2.Text = "Updating...";
                    }
                }
                else
                {
                    this.Command1.Enabled = true;
                    this.Command2.Enabled = true;
                    this.Command4.Enabled = true;
                    this.Command5.Enabled = true;
                    this.packToolStripMenuItem.Enabled = true;
                    this.unpackToolStripMenuItem.Enabled = true;
                    this.testModsToolStripMenuItem.Enabled = true;
                    this.launcherToolStripMenuItem.Enabled = true;
                    this.Label2.Text = string.Empty;

                    // restore window state from before updating the game.
                    this.WindowState = FormWindowState.Normal;
                    this.Show();
                    this.Activate();
                    this.launcherTmr.Enabled = false;
                }
            }
        }

        private void CheckSettings(object sender, EventArgs e)
        {
            SettingsFile.Settingsxml?.ReopenFile();
            this.showintaskbarTempvalue = SettingsFile.Settingsxml?.TryRead("IconWhileElsNotRunning");
            this.showintaskbarTempvalue2 = SettingsFile.Settingsxml?.TryRead("IconWhileElsRunning");
            this.elsDirTemp = SettingsFile.Settingsxml?.TryRead("ElsDir");
            this.Icon = Icons.FormIcon;
            this.MessageManager1.Icon = this.Icon;
            if (!string.Equals(this.elsDir, this.elsDirTemp))
            {
                this.elsDir = this.elsDirTemp;
            }

            if (AbleToClose())
            {
                if (this.showintaskbarValue != this.showintaskbarTempvalue)
                {
                    this.showintaskbarValue = this.showintaskbarTempvalue;
                }

                if (this.showintaskbarValue.Equals("0"))
                {
                    // Taskbar only!!!
                    this.MessageManager1.Visible = false;
                    this.ShowInTaskbar = true;
                }

                if (this.showintaskbarValue.Equals("1"))
                {
                    // Tray only!!!
                    this.MessageManager1.Visible = true;
                    this.ShowInTaskbar = false;
                }

                if (this.showintaskbarValue.Equals("2"))
                {
                    // Both!!!
                    this.MessageManager1.Visible = true;
                    this.ShowInTaskbar = true;
                }
            }
            else
            {
                if (this.showintaskbarValue2 != this.showintaskbarTempvalue2)
                {
                    this.showintaskbarValue2 = this.showintaskbarTempvalue2;
                }

                if (this.showintaskbarValue2.Equals("0"))
                {
                    // Taskbar only!!!
                    this.MessageManager1.Visible = false;
                    this.ShowInTaskbar = true;
                }

                if (this.showintaskbarValue2.Equals("1"))
                {
                    // Tray only!!!
                    this.MessageManager1.Visible = true;
                    this.ShowInTaskbar = false;
                }

                if (this.showintaskbarValue2.Equals("2"))
                {
                    // Both!!!
                    this.MessageManager1.Visible = true;
                    this.ShowInTaskbar = true;
                }
            }
        }
    }
}
