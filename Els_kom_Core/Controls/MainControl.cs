// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    /// <summary>
    /// MainControl control for Els_kom's Main form.
    /// </summary>
    public partial class MainControl : System.Windows.Forms.UserControl
    {
        private string elsDir;
        private string showintaskbarValue;
        private string showintaskbarValue2;
        private string showintaskbarTempvalue;
        private string showintaskbarTempvalue2;
        private string elsDirTemp;
        private System.Windows.Forms.Timer settingsTmr;
        private System.Windows.Forms.Timer packingTmr;
        private System.Windows.Forms.Timer unpackingTmr;
        private System.Windows.Forms.Timer testModsTmr;
        private System.Windows.Forms.Timer launcherTmr;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem packToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unpackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSep1;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainControl"/> class.
        /// </summary>
        public MainControl() => this.InitializeComponent();

        /// <summary>
        /// Event that the control fires that Closes the Form it is on.
        /// </summary>
        public event System.EventHandler CloseForm;

        /// <summary>
        /// Event that the control fires that tells the Form that the Tray Icon name changed.
        /// </summary>
        public event System.EventHandler TrayNameChange;

        /// <summary>
        /// Event that the control fires when the tray icon is clicked by the mouse.
        /// </summary>
        public event System.EventHandler<System.Windows.Forms.MouseEventArgs> TrayClick;

        /// <summary>
        /// Event that is Fired that Allows the Form to open up it's Settings Form.
        /// </summary>
        public event System.EventHandler ConfigForm;

        /// <summary>
        /// Event that is Fired that Allows the Form it is on to open up it's About Form.
        /// </summary>
        public event System.EventHandler AboutForm;

        /// <summary>
        /// Event that is Fired when the Tray and form Icon needs changed.
        /// </summary>
        public event System.EventHandler TrayIconChange;

        /// <summary>
        /// Gets or sets a value indicating whether the
        /// program should close or skip closing.
        /// </summary>
        public static bool Closable { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to
        /// properly close the loop that reads settings
        /// that makes the control work properly.
        /// </summary>
        public bool End_settings_loop { get; set; } = false;

        /// <summary>
        /// Gets the tray Icon.
        /// </summary>
        public System.Windows.Forms.NotifyIcon NotifyIcon1 { get; private set; }

        /// <summary>
        /// Gets if the Els_kom window can be closed or not.
        /// </summary>
        /// <returns>If Els_kom is ready to close or not.</returns>
        public bool AbleToClose() => Classes.ExecutionManager.GetRunningElsword() ||
                Classes.ExecutionManager.GetRunningElswordDirectly() ||
                Classes.KOMManager.GetPackingState() ||
                Classes.KOMManager.GetUnpackingState()
                ? false
                : true;

        /// <summary>
        /// Initializes the MainControl's constants.
        /// </summary>
        public void LoadControl()
        {
            this.MakeTrayIcon();
            var closing = false;
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\koms"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\koms");
            }

            if (Classes.ExecutionManager.IsElsKomRunning() == true)
            {
                Classes.MessageManager.ShowError("Sorry, Only 1 Instance is allowed at a time.", "Error!");
                closing = true;
            }
            else
            {
                Classes.SettingsFile.Settingsxml = new Classes.XMLObject(Classes.SettingsFile.Path, "<Settings></Settings>");
                this.elsDir = Classes.SettingsFile.Settingsxml?.Read("ElsDir");
                if (this.elsDir.Length < 1)
                {
                    Classes.MessageManager.ShowInfo("Welcome to Els_kom." + System.Environment.NewLine + "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + System.Environment.NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!");
                    this.ConfigForm?.Invoke(this, new System.EventArgs());
                }

                var komplugins = Classes.GenericPluginLoader<Interfaces.IKomPlugin>.LoadPlugins("plugins");
                Classes.KOMManager.Komplugins.AddRange(komplugins);
                var callbackplugins = Classes.GenericPluginLoader<Interfaces.ICallbackPlugin>.LoadPlugins("plugins");
                Classes.ExecutionManager.Callbackplugins.AddRange(callbackplugins);
                if (!Classes.Git.IsMaster)
                {
                    Classes.MessageManager.ShowInfo("This branch is not the master branch, meaning this is a feature branch to test changes. When finished please pull request them for the possibility of them getting merged into master.", "Info!");
                }

                if (Classes.Git.IsDirty)
                {
                    var resp = System.Windows.Forms.MessageBox.Show("This build was compiled with Uncommitted changes. As a result, this build might be unstable. Are you sure you want to run this build to test some changes to the code?", "Info!", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                    if (resp == System.Windows.Forms.DialogResult.No)
                    {
                        closing = true;
                    }
                }
            }

            if (!closing)
            {
                if (this.components == null)
                {
                    this.components = new System.ComponentModel.Container();
                }

                this.settingsTmr = new System.Windows.Forms.Timer(this.components)
                {
                    Enabled = true,
                    Interval = 1
                };
                this.packingTmr = new System.Windows.Forms.Timer(this.components)
                {
                    Enabled = false,
                    Interval = 1
                };
                this.packingTmr.Tick += new System.EventHandler(this.Packing);
                this.unpackingTmr = new System.Windows.Forms.Timer(this.components)
                {
                    Enabled = false,
                    Interval = 1
                };
                this.unpackingTmr.Tick += new System.EventHandler(this.Unpacking);
                this.testModsTmr = new System.Windows.Forms.Timer(this.components)
                {
                    Enabled = false,
                    Interval = 1
                };
                this.testModsTmr.Tick += new System.EventHandler(this.TestMods2);
                this.launcherTmr = new System.Windows.Forms.Timer(this.components)
                {
                    Enabled = false,
                    Interval = 1
                };
                this.launcherTmr.Tick += new System.EventHandler(this.Launcher);
                this.NotifyIcon1.Icon = this.FindForm().Icon;
                this.NotifyIcon1.Text = this.FindForm().Text;
                this.NotifyIcon1.Visible = true;
                this.FindForm().Show();
            }
            else
            {
                Classes.SettingsFile.Settingsxml?.Dispose();
                this.CloseForm?.Invoke(this, new System.EventArgs());
            }
        }

        /// <summary>
        /// Shows the Version Error message and closes the main form if the checked version is not the same.
        /// </summary>
        /// <returns>If Els_kom should remain open or not.</returns>
        public bool VersionCheck()
        {
            if (typeof(MainControl).Assembly.GetName().Version.ToString() != System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
            {
                Classes.MessageManager.ShowError("Sorry, you cannot use Els_kom.exe from this version with a newer or older Core. Please update the executable as well.", "Error!");
                this.CloseForm?.Invoke(this, new System.EventArgs());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the Syscommand check value.
        /// </summary>
        /// <returns>The Syscommand check value.</returns>
        public int GetSysCommand() => (int)Enums.SYSCOMMANDS.WM_SYSCOMMAND;

        /// <summary>
        /// Gets the Minimize Command check value.
        /// </summary>
        /// <returns>The Minimize Command check value.</returns>
        public int GetMinimizeCommand() => (int)Enums.SYSCOMMANDS.SC_MINIMIZE;

        /// <summary>
        /// Gets the Maximize Command check value.
        /// </summary>
        /// <returns>The Maximize Command check value.</returns>
        public int GetMaximizeCommand() => (int)Enums.SYSCOMMANDS.SC_MAXIMIZE;

        /// <summary>
        /// Gets the Restore Command check value.
        /// </summary>
        /// <returns>The Restore Command check value.</returns>
        public int GetRestoreCommand() => (int)Enums.SYSCOMMANDS.SC_RESTORE;

        private void Command1_Click(object sender, System.EventArgs e)
        {
            this.Label1.Text = string.Empty;
            var tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms)
            {
                Name = "Classes.KOMManager.PackKoms"
            };
            tr2.Start();
            this.packingTmr.Enabled = true;
        }

        private void Command1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => this.Label1.Text = "This option uses plugins to Pack koms.";

        private void Command2_Click(object sender, System.EventArgs e)
        {
            this.Label1.Text = string.Empty;
            var tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms)
            {
                Name = "Classes.KOMManager.UnpackKoms"
            };
            tr1.Start();
            this.unpackingTmr.Enabled = true;
        }

        private void Command2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => this.Label1.Text = "This option uses plugins to Unpack koms.";

        private void Command3_Click(object sender, System.EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.AboutForm?.Invoke(this, new System.EventArgs());
        }

        private void Command3_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => this.Label1.Text = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed.";

        private void Command4_Click(object sender, System.EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.FindForm().WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.TestMods();
        }

        private void Command4_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => this.Label1.Text = "Test the mods you made.";

        private void Command5_Click(object sender, System.EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.FindForm().WindowState = System.Windows.Forms.FormWindowState.Minimized;
            var tr4 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordLauncher)
            {
                Name = "Classes.ExecutionManager.RunElswordLauncher"
            };
            tr4.Start();
            this.launcherTmr.Enabled = true;
        }

        private void Command5_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => this.Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens (you might have to remake some mods for some files).";

        private void Command6_Click(object sender, System.EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.ConfigForm?.Invoke(this, new System.EventArgs());
        }

        private void Command6_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => this.Label1.Text = "Shows the Settings Window. Here you can easily change the Settings to Els_kom.";

        private void Label1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => this.Label1.Text = string.Empty;

        private void ExitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var cancel = false;
            if (Classes.ExecutionManager.GetRunningElsword() || Classes.ExecutionManager.GetRunningElswordDirectly() || Classes.KOMManager.GetPackingState() || Classes.KOMManager.GetUnpackingState())
            {
                cancel = true;
                Classes.MessageManager.ShowInfo("Cannot close Els_kom while packing, unpacking, testing mods, or updating the game.", "Info!");
            }

            if (!cancel)
            {
                this.End_settings_loop = true;
                Classes.SettingsFile.Settingsxml?.Dispose();
                this.CloseForm?.Invoke(this, new System.EventArgs());
            }
        }

        private void LauncherToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.FindForm().WindowState = System.Windows.Forms.FormWindowState.Minimized;
            var tr4 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordLauncher)
            {
                Name = "Classes.ExecutionManager.RunElswordLauncher"
            };
            tr4.Start();
            this.launcherTmr.Enabled = true;
        }

        private void UnpackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms)
            {
                Name = "Classes.KOMManager.UnpackKoms"
            };
            tr1.Start();
            this.unpackingTmr.Enabled = true;
        }

        private void TestModsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.FindForm().WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.TestMods();
        }

        private void PackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms)
            {
                Name = "Classes.KOMManager.PackKoms"
            };
            tr2.Start();
            this.packingTmr.Enabled = true;
        }

        private void NotifyIcon1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e) => this.TrayClick?.Invoke(this, e);

        private void MainControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => this.Label1.Text = string.Empty;

        // Handles Packing on the Main Form.
        private void Packing(object sender, System.EventArgs e)
        {
            if (Classes.KOMManager.GetPackingState())
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

                if (!string.Equals(this.NotifyIcon1.Text, this.Label2.Text))
                {
                    this.NotifyIcon1.Text = this.Label2.Text;
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
                this.TrayNameChange?.Invoke(this, new System.EventArgs());
                this.packingTmr.Enabled = false;
            }
        }

        // Handles Unpacking on the Main Form.
        private void Unpacking(object sender, System.EventArgs e)
        {
            if (Classes.KOMManager.GetUnpackingState())
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

                if (!string.Equals(this.NotifyIcon1.Text, this.Label2.Text))
                {
                    this.NotifyIcon1.Text = this.Label2.Text;
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
                this.TrayNameChange?.Invoke(this, new System.EventArgs());
                this.unpackingTmr.Enabled = false;
            }
        }

        /// <summary>
        /// Handles Testing Mods on the Main Form (before Elsword is executed).
        /// </summary>
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
            var di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
            foreach (var fi in di.GetFiles("*.kom"))
            {
                var kom_file = fi.Name;

                // do not copy kom files that are in the koms directory but cannot be found to copy from taget directory to the backup directory to restore later.
                Classes.KOMManager.CopyKomFiles(kom_file, System.Windows.Forms.Application.StartupPath + "\\koms\\", this.elsDir + "\\data");
            }

            var tr3 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordDirectly)
            {
                Name = "Classes.ExecutionManager.RunElswordDirectly"
            };
            tr3.Start();
            this.testModsTmr.Enabled = true;
        }

        // Handles Testing Mods on the Main Form (when Elsword (the game window) closes).
        private void TestMods2(object sender, System.EventArgs e)
        {
            var executing = Classes.ExecutionManager.GetExecutingElsword();
            if (!executing)
            {
                if (Classes.ExecutionManager.GetRunningElswordDirectly())
                {
                    Classes.ExecutionManager.DeployCallBack();
                    if (string.Equals(this.Label2.Text, string.Empty))
                    {
                        this.Label2.Text = "Testing Mods...";
                    }
                }
                else
                {
                    var di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
                    foreach (var fi in di.GetFiles("*.kom"))
                    {
                        var kom_file = fi.Name;
                        Classes.KOMManager.MoveOriginalKomFilesBack(kom_file, this.elsDir + "\\data\\backup", this.elsDir + "\\data");
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
                    this.FindForm().WindowState = System.Windows.Forms.FormWindowState.Normal;
                    this.testModsTmr.Enabled = false;
                }
            }
        }

        // Handles Updating the Game but disables the controls while it is updating to avoid unpacking,
        // packing, and testing mods.
        private void Launcher(object sender, System.EventArgs e)
        {
            if (!Classes.ExecutionManager.GetExecutingElsword())
            {
                if (Classes.ExecutionManager.GetRunningElsword())
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
                    this.FindForm().WindowState = System.Windows.Forms.FormWindowState.Normal;
                    this.launcherTmr.Enabled = false;
                }
            }
        }

        private void CheckSettings(object sender, System.EventArgs e)
        {
            if (this.End_settings_loop)
            {
                this.settingsTmr.Enabled = false;
            }
            else
            {
                if (this.AbleToClose())
                {
                    Classes.SettingsFile.Settingsxml?.ReopenFile();
                    this.showintaskbarTempvalue = Classes.SettingsFile.Settingsxml?.Read("IconWhileElsNotRunning");
                    this.showintaskbarTempvalue2 = Classes.SettingsFile.Settingsxml?.Read("IconWhileElsRunning");
                    this.elsDirTemp = Classes.SettingsFile.Settingsxml?.Read("ElsDir");
                    this.TrayIconChange?.Invoke(this, new System.EventArgs());
                    if (!string.Equals(this.elsDir, this.elsDirTemp))
                    {
                        this.elsDir = this.elsDirTemp;
                    }

                    if (!Classes.ExecutionManager.GetRunningElswordDirectly())
                    {
                        if (this.showintaskbarValue != this.showintaskbarTempvalue)
                        {
                            this.showintaskbarValue = this.showintaskbarTempvalue;
                        }

                        if (this.showintaskbarValue.Equals("0"))
                        {
                            // Taskbar only!!!
                            this.NotifyIcon1.Visible = false;
                            this.FindForm().ShowInTaskbar = true;
                        }

                        if (this.showintaskbarValue.Equals("1"))
                        {
                            // Tray only!!!
                            this.NotifyIcon1.Visible = true;
                            this.FindForm().ShowInTaskbar = false;
                        }

                        if (this.showintaskbarValue.Equals("2"))
                        {
                            // Both!!!
                            this.NotifyIcon1.Visible = true;
                            this.FindForm().ShowInTaskbar = true;
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
                            this.NotifyIcon1.Visible = false;
                            this.FindForm().ShowInTaskbar = true;
                        }

                        if (this.showintaskbarValue2.Equals("1"))
                        {
                            // Tray only!!!
                            this.NotifyIcon1.Visible = true;
                            this.FindForm().ShowInTaskbar = false;
                        }

                        if (this.showintaskbarValue2.Equals("2"))
                        {
                            // Both!!!
                            this.NotifyIcon1.Visible = true;
                            this.FindForm().ShowInTaskbar = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Makes the tray icon.
        /// </summary>
        private void MakeTrayIcon()
        {
            this.packToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "PackToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Pack"
            };
            this.packToolStripMenuItem.Click += new System.EventHandler(this.PackToolStripMenuItem_Click);
            this.unpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "UnpackToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Unpack"
            };
            this.unpackToolStripMenuItem.Click += new System.EventHandler(this.UnpackToolStripMenuItem_Click);
            this.toolStripMenuSep1 = new System.Windows.Forms.ToolStripSeparator
            {
                Name = "ToolStripMenuItem3",
                Size = new System.Drawing.Size(126, 6)
            };
            this.testModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "TestModsToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Test Mods"
            };
            this.testModsToolStripMenuItem.Click += new System.EventHandler(this.TestModsToolStripMenuItem_Click);
            this.launcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "LauncherToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Launcher"
            };
            this.launcherToolStripMenuItem.Click += new System.EventHandler(this.LauncherToolStripMenuItem_Click);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "ExitToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Exit"
            };
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.packToolStripMenuItem, this.unpackToolStripMenuItem, this.testModsToolStripMenuItem,
                this.launcherToolStripMenuItem, this.toolStripMenuSep1, this.exitToolStripMenuItem
            });
            this.contextMenuStrip1.Name = "ContextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 154);
            this.contextMenuStrip1.ResumeLayout(false);
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components)
            {
                ContextMenuStrip = this.contextMenuStrip1,
                Visible = false
            };
            this.NotifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseClick);
        }
    }
}
