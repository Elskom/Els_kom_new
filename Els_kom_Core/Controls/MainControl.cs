// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using Els_kom_Core.Classes;
    using Els_kom_Core.Enums;
    using Els_kom_Core.Interfaces;

    /// <summary>
    /// MainControl control for Els_kom's Main form.
    /// </summary>
    public partial class MainControl : UserControl
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
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem packToolStripMenuItem;
        private ToolStripMenuItem unpackToolStripMenuItem;
        private ToolStripMenuItem testModsToolStripMenuItem;
        private ToolStripMenuItem launcherToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripSeparator toolStripMenuSep1;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainControl"/> class.
        /// </summary>
        public MainControl() => this.InitializeComponent();

        /// <summary>
        /// Event that the control fires that Closes the Form it is on.
        /// </summary>
        public event EventHandler CloseForm;

        /// <summary>
        /// Event that the control fires that tells the Form that the Tray Icon name changed.
        /// </summary>
        public event EventHandler TrayNameChange;

        /// <summary>
        /// Event that the control fires when the tray icon is clicked by the mouse.
        /// </summary>
        public event EventHandler<MouseEventArgs> TrayClick;

        /// <summary>
        /// Event that is Fired that Allows the Form to open up it's Settings Form.
        /// </summary>
        public event EventHandler ConfigForm;

        /// <summary>
        /// Event that is Fired that Allows the Form it is on to open up it's About Form.
        /// </summary>
        public event EventHandler AboutForm;

        /// <summary>
        /// Event that is Fired when the Tray and form Icon needs changed.
        /// </summary>
        public event EventHandler TrayIconChange;

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
        public NotifyIcon NotifyIcon1 { get; private set; }

        private bool Enablehandlers { get; set; }

        /// <summary>
        /// Gets if the Els_kom window can be closed or not.
        /// </summary>
        /// <returns>If Els_kom is ready to close or not.</returns>
        public bool AbleToClose() => ExecutionManager.RunningElsword ||
                ExecutionManager.RunningElswordDirectly ||
                KOMManager.PackingState ||
                KOMManager.UnpackingState ? false
                : true;

        /// <summary>
        /// Initializes the MainControl's constants.
        /// </summary>
        public void LoadControl()
        {
            this.MakeTrayIcon();
            var closing = false;
            if (!Directory.Exists(Application.StartupPath + "\\koms"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\koms");
            }

            if (ExecutionManager.IsElsKomRunning() == true)
            {
                MessageManager.ShowError("Sorry, Only 1 Instance is allowed at a time.", "Error!");
                closing = true;
            }
            else
            {
                SettingsFile.Settingsxml = new XMLObject(SettingsFile.Path, "<Settings></Settings>");
                this.elsDir = SettingsFile.Settingsxml?.Read("ElsDir");
                if (this.elsDir.Length < 1)
                {
                    MessageManager.ShowInfo("Welcome to Els_kom." + Environment.NewLine + "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + Environment.NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!");
                    this.ConfigForm?.Invoke(this, new EventArgs());
                }

                var komplugins = GenericPluginLoader<IKomPlugin>.LoadPlugins("plugins");
                KOMManager.Komplugins.AddRange(komplugins);
                var callbackplugins = GenericPluginLoader<ICallbackPlugin>.LoadPlugins("plugins");
                ExecutionManager.Callbackplugins.AddRange(callbackplugins);
                if (!Git.IsMaster)
                {
                    MessageManager.ShowInfo("This branch is not the master branch, meaning this is a feature branch to test changes. When finished please pull request them for the possibility of them getting merged into master.", "Info!");
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
                if (this.components == null)
                {
                    this.components = new Container();
                }

                this.settingsTmr = new System.Windows.Forms.Timer(this.components)
                {
                    Enabled = true,
                    Interval = 1
                };
                this.settingsTmr.Tick += new EventHandler(this.CheckSettings);
                this.packingTmr = new System.Windows.Forms.Timer(this.components)
                {
                    Enabled = false,
                    Interval = 1
                };
                this.packingTmr.Tick += new EventHandler(this.Packing);
                this.unpackingTmr = new System.Windows.Forms.Timer(this.components)
                {
                    Enabled = false,
                    Interval = 1
                };
                this.unpackingTmr.Tick += new EventHandler(this.Unpacking);
                this.testModsTmr = new System.Windows.Forms.Timer(this.components)
                {
                    Enabled = false,
                    Interval = 1
                };
                this.testModsTmr.Tick += new EventHandler(this.TestMods2);
                this.launcherTmr = new System.Windows.Forms.Timer(this.components)
                {
                    Enabled = false,
                    Interval = 1
                };
                this.launcherTmr.Tick += new EventHandler(this.Launcher);
                this.NotifyIcon1.Icon = this.FindForm().Icon;
                this.NotifyIcon1.Text = this.FindForm().Text;
                this.NotifyIcon1.Visible = true;
                this.FindForm().Show();
            }
            else
            {
                SettingsFile.Settingsxml?.Dispose();
                this.CloseForm?.Invoke(this, new EventArgs());
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
                MessageManager.ShowError("Sorry, you cannot use Els_kom.exe from this version with a newer or older Core. Please update the executable as well.", "Error!");
                this.CloseForm?.Invoke(this, new EventArgs());
                return false;
            }

            return true;
        }

        /// <summary>Processes Windows messages.</summary>
        /// <param name="m">The Windows <see cref="Message"/> to process.</param>
        protected override void WndProc(ref Message m)
        {
            this.Enablehandlers = !this.FindForm().ShowInTaskbar ? true : false;
            if (this.Enablehandlers && m.Msg == (int)SYSCOMMANDS.WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == (int)SYSCOMMANDS.SC_MINIMIZE)
                {
                    this.FindForm().Hide();
                    m.Result = IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() == (int)SYSCOMMANDS.SC_MAXIMIZE)
                {
                    this.FindForm().Show();
                    this.FindForm().Activate();
                    m.Result = IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() == (int)SYSCOMMANDS.SC_RESTORE)
                {
                    this.FindForm().Show();
                    this.FindForm().Activate();
                    m.Result = IntPtr.Zero;
                    return;
                }
            }

            base.WndProc(ref m);
        }

        private void Command1_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            var tr2 = new Thread(KOMManager.PackKoms)
            {
                Name = "Classes.KOMManager.PackKoms"
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
                Name = "Classes.KOMManager.UnpackKoms"
            };
            tr1.Start();
            this.unpackingTmr.Enabled = true;
        }

        private void Command2_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = "This option uses plugins to Unpack koms.";

        private void Command3_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.AboutForm?.Invoke(this, new EventArgs());
        }

        private void Command3_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed.";

        private void Command4_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.FindForm().Hide();
            this.FindForm().WindowState = FormWindowState.Minimized;
            this.TestMods();
        }

        private void Command4_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = "Test the mods you made.";

        private void Command5_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.FindForm().Hide();
            this.FindForm().WindowState = FormWindowState.Minimized;
            var tr4 = new Thread(ExecutionManager.RunElswordLauncher)
            {
                Name = "Classes.ExecutionManager.RunElswordLauncher"
            };
            tr4.Start();
            this.launcherTmr.Enabled = true;
        }

        private void Command5_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens (you might have to remake some mods for some files).";

        private void Command6_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.ConfigForm?.Invoke(this, new EventArgs());
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
                MessageManager.ShowInfo("Cannot close Els_kom while packing, unpacking, testing mods, or updating the game.", "Info!");
            }

            if (!cancel)
            {
                this.End_settings_loop = true;
                SettingsFile.Settingsxml?.Dispose();
                this.CloseForm?.Invoke(this, new EventArgs());
            }
        }

        private void LauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.FindForm().Hide();
            this.FindForm().WindowState = FormWindowState.Minimized;
            var tr4 = new Thread(ExecutionManager.RunElswordLauncher)
            {
                Name = "Classes.ExecutionManager.RunElswordLauncher"
            };
            tr4.Start();
            this.launcherTmr.Enabled = true;
        }

        private void UnpackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tr1 = new Thread(KOMManager.UnpackKoms)
            {
                Name = "Classes.KOMManager.UnpackKoms"
            };
            tr1.Start();
            this.unpackingTmr.Enabled = true;
        }

        private void TestModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Label1.Text = string.Empty;
            this.FindForm().Hide();
            this.FindForm().WindowState = FormWindowState.Minimized;
            this.TestMods();
        }

        private void PackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tr2 = new Thread(KOMManager.PackKoms)
            {
                Name = "Classes.KOMManager.PackKoms"
            };
            tr2.Start();
            this.packingTmr.Enabled = true;
        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e) => this.TrayClick?.Invoke(this, e);

        private void MainControl_MouseMove(object sender, MouseEventArgs e) => this.Label1.Text = string.Empty;

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
                this.TrayNameChange?.Invoke(this, new EventArgs());
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
                this.TrayNameChange?.Invoke(this, new EventArgs());
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
            var di = new DirectoryInfo(Application.StartupPath + "\\koms");
            foreach (var fi in di.GetFiles("*.kom"))
            {
                var kom_file = fi.Name;

                // do not copy kom files that are in the koms directory but cannot be found to copy from taget directory to the backup directory to restore later.
                KOMManager.CopyKomFiles(kom_file, Application.StartupPath + "\\koms\\", this.elsDir + "\\data");
            }

            var tr3 = new Thread(ExecutionManager.RunElswordDirectly)
            {
                Name = "Classes.ExecutionManager.RunElswordDirectly"
            };
            tr3.Start();
            this.testModsTmr.Enabled = true;
        }

        // Handles Testing Mods on the Main Form (when Elsword (the game window) closes).
        private void TestMods2(object sender, EventArgs e)
        {
            var executing = ExecutionManager.ExecutingElsword;
            if (!executing)
            {
                if (ExecutionManager.RunningElswordDirectly)
                {
                    ExecutionManager.DeployCallBack();
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
                    this.FindForm().WindowState = FormWindowState.Normal;
                    this.FindForm().Show();
                    this.testModsTmr.Enabled = false;
                }
            }
        }

        // Handles Updating the Game but disables the controls while it is updating to avoid unpacking,
        // packing, and testing mods.
        private void Launcher(object sender, EventArgs e)
        {
            if (!ExecutionManager.ExecutingElsword)
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
                    this.FindForm().WindowState = FormWindowState.Normal;
                    this.FindForm().Show();
                    this.launcherTmr.Enabled = false;
                }
            }
        }

        private void CheckSettings(object sender, EventArgs e)
        {
            if (this.End_settings_loop)
            {
                this.settingsTmr.Enabled = false;
            }
            else
            {
                if (this.AbleToClose())
                {
                    SettingsFile.Settingsxml?.ReopenFile();
                    this.showintaskbarTempvalue = SettingsFile.Settingsxml?.Read("IconWhileElsNotRunning");
                    this.showintaskbarTempvalue2 = SettingsFile.Settingsxml?.Read("IconWhileElsRunning");
                    this.elsDirTemp = SettingsFile.Settingsxml?.Read("ElsDir");
                    this.TrayIconChange?.Invoke(this, new EventArgs());
                    if (!string.Equals(this.elsDir, this.elsDirTemp))
                    {
                        this.elsDir = this.elsDirTemp;
                    }

                    if (!ExecutionManager.RunningElswordDirectly)
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
            this.packToolStripMenuItem = new ToolStripMenuItem
            {
                Name = "PackToolStripMenuItem",
                Size = new Size(129, 22),
                Text = "Pack"
            };
            this.packToolStripMenuItem.Click += new EventHandler(this.PackToolStripMenuItem_Click);
            this.unpackToolStripMenuItem = new ToolStripMenuItem
            {
                Name = "UnpackToolStripMenuItem",
                Size = new Size(129, 22),
                Text = "Unpack"
            };
            this.unpackToolStripMenuItem.Click += new EventHandler(this.UnpackToolStripMenuItem_Click);
            this.toolStripMenuSep1 = new ToolStripSeparator
            {
                Name = "ToolStripMenuItem3",
                Size = new Size(126, 6)
            };
            this.testModsToolStripMenuItem = new ToolStripMenuItem
            {
                Name = "TestModsToolStripMenuItem",
                Size = new Size(129, 22),
                Text = "Test Mods"
            };
            this.testModsToolStripMenuItem.Click += new EventHandler(this.TestModsToolStripMenuItem_Click);
            this.launcherToolStripMenuItem = new ToolStripMenuItem
            {
                Name = "LauncherToolStripMenuItem",
                Size = new Size(129, 22),
                Text = "Launcher"
            };
            this.launcherToolStripMenuItem.Click += new EventHandler(this.LauncherToolStripMenuItem_Click);
            this.exitToolStripMenuItem = new ToolStripMenuItem
            {
                Name = "ExitToolStripMenuItem",
                Size = new Size(129, 22),
                Text = "Exit"
            };
            this.exitToolStripMenuItem.Click += new EventHandler(this.ExitToolStripMenuItem_Click);
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
            {
                this.packToolStripMenuItem, this.unpackToolStripMenuItem, this.testModsToolStripMenuItem,
                this.launcherToolStripMenuItem, this.toolStripMenuSep1, this.exitToolStripMenuItem
            });
            this.contextMenuStrip1.Name = "ContextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(130, 154);
            this.contextMenuStrip1.ResumeLayout(false);
            this.NotifyIcon1 = new NotifyIcon(this.components)
            {
                ContextMenuStrip = this.contextMenuStrip1,
                Visible = false
            };
            this.NotifyIcon1.MouseClick += new MouseEventHandler(this.NotifyIcon1_MouseClick);
        }
    }
}
