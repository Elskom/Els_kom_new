// Copyright (c) 2014-2023, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System.Diagnostics.CodeAnalysis;
    using Els_kom.Controls;
    using Els_kom.Themes;
    using Microsoft.Extensions.DependencyInjection;
    using TerraFX.Interop.Windows;

    internal partial class MainForm : /*Form*/ThemedForm
    {
        private static NotifyIcon? notifyIcon;
        [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Automatically disposed of by DependencyInjection.")]
        private Form? aboutfrm;
        [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Automatically disposed of by DependencyInjection.")]
        private Form? settingsfrm;
        private string elsDir = string.Empty;
        private int showintaskbarValue;
        private int showintaskbarValue2;
        private int showintaskbarTempvalue;
        private int showintaskbarTempvalue2;
        private string elsDirTemp = string.Empty;

        public MainForm()
        {
            this.InitializeComponent();
            this.SuspendLayout();
            MessageManager.Notification += this.MessageManager_Notification;
            notifyIcon = new NotifyIcon(this.Components)
            {
                ContextMenuStrip = this.contextMenuStrip1,
            };
            notifyIcon.MouseClick += new MouseEventHandler(this.NotifyIcon_MouseClick);
            ApplicationResources.ApplyTheme(this.contextMenuStrip1);
            this.ResumeLayout(false);
        }

        private bool Enablehandlers { get; set; }

        protected override void WndProc(ref Message m)
        {
            this.Enablehandlers = !this.ShowInTaskbar;
            if (this.Enablehandlers && m.Msg == WM.WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == SC.SC_MINIMIZE)
                {
                    this.Hide();
                    m.Result = IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() is SC.SC_MAXIMIZE or SC.SC_RESTORE)
                {
                    this.Show();
                    this.Activate();
                    m.Result = IntPtr.Zero;
                    return;
                }
            }

            base.WndProc(ref m);
        }

        private static bool AbleToClose()
            => !ExecutionManager.RunningElsword &&
            !ExecutionManager.RunningElswordDirectly &&
            !KOMManager.PackingState &&
            !KOMManager.UnpackingState;

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var cancel = e.Cancel;

            // CloseReason UnloadMode = e->CloseReason; <-- Removed because not used.
            if (!AbleToClose() && !MiniDumpAttribute.ForceClose)
            {
                cancel = true;
                _ = MessageManager.ShowInfo("Cannot close Els_kom while packing, unpacking, testing mods, or updating the game.", "Info!", Convert.ToBoolean(SettingsFile.SettingsJson!.UseNotifications));
            }

            if (!cancel)
            {
                SettingsFile.SettingsJson = null;
            }

            e.Cancel = cancel;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();

            // var textColor = Color.FromArgb((int)Windows.GetThemeSysColor(Windows.GetWindowTheme((HWND)this.Handle), Windows.TMT_WINDOWTEXT));
            this.packToolStripMenuItem.ConvertSVGTo16x16Image(Properties.Resources.archive!, ApplicationResources.Theme!.TextColor);
            this.unpackToolStripMenuItem.ConvertSVGTo16x16Image(Properties.Resources.unarchive!, ApplicationResources.Theme!.TextColor);
            this.testModsToolStripMenuItem.ConvertSVGTo16x16Image(Properties.Resources.vial_solid!, ApplicationResources.Theme!.TextColor);
            this.launcherToolStripMenuItem.ConvertSVGTo16x16Image(Properties.Resources.launch!, ApplicationResources.Theme!.TextColor);
            this.exitToolStripMenuItem.Image = GetNativeMenuItemImage(HBMMENU.HBMMENU_POPUP_CLOSE, true);
            var closing = false;
            if (!Directory.Exists(Application.StartupPath + "\\koms"))
            {
                _ = Directory.CreateDirectory(Application.StartupPath + "\\koms");
            }

            if (ExecutionManager.IsElsKomRunning())
            {
                _ = MessageManager.ShowError("Sorry, Only 1 Instance is allowed at a time.", "Error!", false);
                closing = true;
            }
            else
            {
                SettingsFile.SettingsJson = SettingsFile.SettingsJson!.ReopenFile();
                this.elsDir = SettingsFile.SettingsJson!.ElsDir;
                var komplugins = FormsApplication.ServiceProvider!.GetRequiredService<GenericPluginLoader>().LoadPlugins<IKomPlugin>("plugins", Convert.ToBoolean(SettingsFile.SettingsJson.SaveToZip));
                KOMManager.Komplugins.AddRange(komplugins);
                var callbackplugins = FormsApplication.ServiceProvider!.GetRequiredService<GenericPluginLoader>().LoadPlugins<ICallbackPlugin>("plugins", Convert.ToBoolean(SettingsFile.SettingsJson.SaveToZip));
                KOMManager.Callbackplugins.AddRange(callbackplugins);
                var encryptionplugins = FormsApplication.ServiceProvider!.GetRequiredService<GenericPluginLoader>().LoadPlugins<IEncryptionPlugin>("plugins", Convert.ToBoolean(SettingsFile.SettingsJson.SaveToZip));
                KOMManager.Encryptionplugins.AddRange(encryptionplugins);
                if (this.elsDir.Length < 1)
                {
                    _ = MessageManager.ShowInfo(SettingsFile.SettingsPath, "Debug!", false);
                    _ = MessageManager.ShowInfo($"Welcome to Els_kom.{Environment.NewLine}Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops.{Environment.NewLine}Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!", false);

                    // avoids an issue where more than 1 settings form can be opened at the same time.
                    if (this.settingsfrm == null && this.aboutfrm == null)
                    {
                        this.settingsfrm = FormsApplication.ServiceProvider!.GetRequiredService<SettingsForm>();
                        _ = this.settingsfrm.ShowDialog();
                        this.settingsfrm = null!;
                    }
                }

                if (!GitInformation.GetAssemblyInstance(typeof(FormsApplication))?.IsMain ?? false)
                {
                    _ = MessageManager.ShowInfo("This branch is not the main branch, meaning this is a feature branch to test changes. When finished please pull request them for the possibility of them getting merged into main.", "Info!", Convert.ToBoolean(SettingsFile.SettingsJson.UseNotifications));
                }

                if (GitInformation.GetAssemblyInstance(typeof(FormsApplication))?.IsDirty ?? false)
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
                notifyIcon!.Icon = this.Icon;
                notifyIcon!.Text = this.Text;
                var pluginTypes = new List<Type>();
                pluginTypes.AddRange(KOMManager.Callbackplugins.Select((x) => x.GetType()));
                pluginTypes.AddRange(KOMManager.Komplugins.Select((x) => x.GetType()));
                pluginTypes.AddRange(KOMManager.Encryptionplugins.Select((x) => x.GetType()));
                var pluginUpdateCheck = FormsApplication.ServiceProvider!.GetRequiredService<PluginUpdateCheck>();

                // discard results.
                _ = pluginUpdateCheck.CheckForUpdates(SettingsFile.SettingsJson!.Sources, pluginTypes);
                _ = pluginUpdateCheck.ShowMessage;
                notifyIcon!.Visible = true;
                this.Show();
                this.Activate();
            }
            else
            {
                SettingsFile.SettingsJson = null;
                this.aboutfrm?.Close();
                this.settingsfrm?.Close();
                this.Close();
            }
        }

        private void MainForm_MouseLeave(object sender, EventArgs e)
            => this.Label1.Text = string.Empty;

        private void Command1_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            var tr2 = new Thread(KOMManager.PackKoms)
            {
                Name = nameof(KOMManager.PackKoms),
            };
            tr2.Start();
            this.SetControlState(false, "Packing...", "Packing...");
            this.packingTmr.Enabled = true;
        }

        private void Command1_MouseMove(object sender, MouseEventArgs e)
            => this.Label1.Text = "This option uses plugins to Pack koms.";

        private void Command2_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            var tr1 = new Thread(KOMManager.UnpackKoms)
            {
                Name = nameof(KOMManager.UnpackKoms),
            };
            tr1.Start();
            this.SetControlState(false, "Unpacking...", "Unpacking...");
            this.unpackingTmr.Enabled = true;
        }

        private void Command2_MouseMove(object sender, MouseEventArgs e)
            => this.Label1.Text = "This option uses plugins to Unpack koms.";

        private void Command3_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;

            // prevent from having multiple about forms opening at the same time in case as well.
            if (this.aboutfrm == null && this.settingsfrm == null)
            {
                this.aboutfrm = FormsApplication.ServiceProvider!.GetRequiredService<AboutForm>();
                _ = this.aboutfrm.ShowDialog();
                this.aboutfrm = null!;
            }
        }

        private void Command3_MouseMove(object sender, MouseEventArgs e)
            => this.Label1.Text = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed.";

        private void Command4_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.TestMods();
        }

        private void Command4_MouseMove(object sender, MouseEventArgs e)
            => this.Label1.Text = "Test the mods you made.";

        private void Command5_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            var tr4 = new Thread(ExecutionManager.RunElswordLauncher)
            {
                Name = nameof(ExecutionManager.RunElswordLauncher),
            };
            tr4.Start();
            this.SetControlState(false, "Updating...", "Updating...");
            this.launcherTmr.Enabled = true;
        }

        private void Command5_MouseMove(object sender, MouseEventArgs e)
            => this.Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens (you might have to remake some mods for some files).";

        private void Command6_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;

            // avoids an issue where more than 1 settings form can be opened at the same time.
            if (this.settingsfrm == null && this.aboutfrm == null)
            {
                this.settingsfrm = FormsApplication.ServiceProvider!.GetRequiredService<SettingsForm>();
                _ = this.settingsfrm.ShowDialog();
                this.settingsfrm = null!;
            }
        }

        private void Command6_MouseMove(object sender, MouseEventArgs e)
            => this.Label1.Text = "Shows the Settings Window. Here you can easily change the Settings to Els_kom.";

        private void Label1_MouseMove(object sender, MouseEventArgs e)
            => this.Label1.Text = string.Empty;

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cancel = false;
            if (!AbleToClose())
            {
                cancel = true;
                _ = MessageManager.ShowInfo("Cannot close Els_kom while packing, unpacking, testing mods, or updating the game.", "Info!", Convert.ToBoolean(SettingsFile.SettingsJson!.UseNotifications));
            }

            if (!cancel)
            {
                SettingsFile.SettingsJson = null;
                this.aboutfrm?.Close();
                this.settingsfrm?.Close();
                this.Close();
            }
        }

        private void LauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            var tr4 = new Thread(ExecutionManager.RunElswordLauncher)
            {
                Name = nameof(ExecutionManager.RunElswordLauncher),
            };
            tr4.Start();
            this.SetControlState(false, "Updating...", "Updating...");
            this.launcherTmr.Enabled = true;
        }

        private void UnpackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tr1 = new Thread(KOMManager.UnpackKoms)
            {
                Name = nameof(KOMManager.UnpackKoms),
            };
            tr1.Start();
            this.SetControlState(false, "Unpacking...", "Unpacking...");
            this.unpackingTmr.Enabled = true;
        }

        private void TestModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.TestMods();
        }

        private void PackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tr2 = new Thread(KOMManager.PackKoms)
            {
                Name = nameof(KOMManager.PackKoms),
            };
            tr2.Start();
            this.SetControlState(false, "Packing...", "Packing...");
            this.packingTmr.Enabled = true;
        }

        private void MessageManager_Notification(object? sender, NotificationEventArgs e)
        {
            if (e.UseNotifications)
            {
                notifyIcon!.ShowBalloonTip(e.TimeOut, e.Title, e.Text, (ToolTipIcon)e.Icon);
            }
            else
            {
                MessageBox.Show(
                    FromHandle(this.Handle),
                    e.Text,
                    e.Title,
                    (MessageBoxButtons)e.MessageBoxButtons,
                    (MessageBoxIcon)e.MessageBoxIcon);
            }
        }

        private void NotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (AboutForm.Label1 == 1 || SettingsForm.Label9 == 1)
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
                    else if (notifyIcon!.Visible)
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
            if (!KOMManager.PackingState)
            {
                this.SetControlState(true, string.Empty, this.Text);
                this.packingTmr.Enabled = false;
            }
        }

        // Handles Unpacking on the Main Form.
        private void Unpacking(object sender, EventArgs e)
        {
            if (!KOMManager.UnpackingState)
            {
                this.SetControlState(true, string.Empty, this.Text);
                this.unpackingTmr.Enabled = false;
            }
        }

        // Handles Testing Mods on the Main Form (before Elsword is executed).
        private void TestMods()
        {
            this.SetControlState(false, "Testing Mods...", "Testing Mods...");
            var di = new DirectoryInfo(Application.StartupPath + "\\koms");
            foreach (var kom_file in di.GetFiles("*.kom").Select(fi => fi.Name))
            {
                // do not copy kom files that are in the koms directory but cannot be found to copy from taget directory to the backup directory to restore later.
                KOMManager.CopyKomFiles(kom_file, Application.StartupPath + "\\koms\\", this.elsDir + "\\data");
            }

            var tr3 = new Thread(ExecutionManager.RunElswordDirectly)
            {
                Name = nameof(ExecutionManager.RunElswordDirectly),
            };
            tr3.Start();
            this.testModsTmr.Enabled = true;
        }

        // Handles Testing Mods on the Main Form (when Elsword (the game window) closes).
        private void TestMods2(object sender, EventArgs e)
        {
            if (!ExecutionManager.IsExecuting(false))
            {
                if (ExecutionManager.RunningElswordDirectly)
                {
                    KOMManager.DeployCallBack(ExecutionManager.RunningElswordDirectly);
                }
                else
                {
                    var di = new DirectoryInfo(Application.StartupPath + "\\koms");
                    foreach (var kom_file in di.GetFiles("*.kom").Select(fi => fi.Name))
                    {
                        KOMManager.MoveOriginalKomFilesBack(kom_file, this.elsDir + "\\data\\backup", this.elsDir + "\\data");
                    }

                    this.SetControlState(true, string.Empty, this.Text);

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
            if (!ExecutionManager.IsExecuting(true) && !ExecutionManager.RunningElsword)
            {
                this.SetControlState(true, string.Empty, this.Text);

                // restore window state from before updating the game.
                this.WindowState = FormWindowState.Normal;
                this.Show();
                this.Activate();
                this.launcherTmr.Enabled = false;
            }
        }

        private void CheckSettings(object sender, EventArgs e)
        {
            this.showintaskbarTempvalue = SettingsFile.SettingsJson!.IconWhileElsNotRunning;
            this.showintaskbarTempvalue2 = SettingsFile.SettingsJson!.IconWhileElsRunning;
            this.elsDirTemp = SettingsFile.SettingsJson!.ElsDir;
            if (!Icons.IconEquals(this.Icon, Icons.FormIcon))
            {
                this.Icon = Icons.FormIcon;
            }

            if (notifyIcon!.Icon == null || !Icons.IconEquals(notifyIcon.Icon, this.Icon))
            {
                notifyIcon.Icon = this.Icon;
            }

            if (!string.Equals(this.elsDir, this.elsDirTemp, StringComparison.Ordinal))
            {
                this.elsDir = this.elsDirTemp;
            }

            if (AbleToClose())
            {
                if (!this.showintaskbarValue.Equals(this.showintaskbarTempvalue))
                {
                    this.showintaskbarValue = this.showintaskbarTempvalue;
                }

                this.SetTaskbarShowValue(this.showintaskbarValue);
            }
            else
            {
                if (!this.showintaskbarValue2.Equals(this.showintaskbarTempvalue2))
                {
                    this.showintaskbarValue2 = this.showintaskbarTempvalue2;
                }

                this.SetTaskbarShowValue(this.showintaskbarValue2);
            }
        }

        private void SetTaskbarShowValue(int val)
        {
            switch (val)
            {
                case 0:
                {
                    // Taskbar only!!!
                    notifyIcon!.Visible = false;
                    this.ShowInTaskbar = true;
                    break;
                }

                case 1:
                {
                    // Tray only!!!
                    notifyIcon!.Visible = true;
                    this.ShowInTaskbar = false;
                    break;
                }

                case 2:
                {
                    // Both!!!
                    notifyIcon!.Visible = true;
                    this.ShowInTaskbar = true;
                    break;
                }

                default:
                    // nothing to do.
                    break;
            }
        }

        private void SetControlState(bool enabled, string status, string notifyiconstate)
        {
            this.Command1.Enabled = enabled;
            this.Command2.Enabled = enabled;
            this.Command4.Enabled = enabled;
            this.Command5.Enabled = enabled;
            this.packToolStripMenuItem.Enabled = enabled;
            this.unpackToolStripMenuItem.Enabled = enabled;
            this.testModsToolStripMenuItem.Enabled = enabled;
            this.launcherToolStripMenuItem.Enabled = enabled;
            this.Label2.Text = status;
            notifyIcon!.Text = notifyiconstate;
        }
    }
}
