// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

// If the thread control invoke delegates was not present to change
// the controls on the main thread The Visual Studio Debugger when
// Running Els_kom in Debug mode to Debug plugins will yell at you
// throwing an System.InvalidOperationException exception and
// saying 'Cross-thread operation not valid: Control
// '%s' accessed from a thread other than the thread it was
// created on.'.

namespace Els_kom_Core.Controls
{
    /// <summary>
    /// MainControl control for Els_kom's Main form.
    /// </summary>
    public partial class MainControl : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// MainControl constructor.
        /// </summary>
        public MainControl()
        {
            InitializeComponent();
        }

        private string ElsDir;
        private string showintaskbar_value;
        private string showintaskbar_value2;
        private string showintaskbar_tempvalue;
        private string showintaskbar_tempvalue2;
        private string ElsDir_temp;
        /// <summary>
        /// Allows this control to properly close the loop that reads
        /// settings that makes it work properly.
        /// </summary>
        public bool end_settings_loop = false;
        /// <summary>
        /// Tray Icon.
        /// </summary>
        public System.Windows.Forms.NotifyIcon NotifyIcon1;

        internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem PackToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem UnpackToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem TestModsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem LauncherToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripMenuSep1;

        /// <summary>
        /// Event that the control fires that Minimizes the Form it is on.
        /// </summary>
        public event System.EventHandler MinimizeForm;
        /// <summary>
        /// Event that the control fires that Closes the Form it is on.
        /// </summary>
        public event System.EventHandler CloseForm;
        /// <summary>
        /// Event that the control fires that tells the Form that the Tray Icon name changed.
        /// </summary>
        public event System.EventHandler TrayNameChange;
        /// <summary>
        /// Event that the control fires that Shows the Form it is on in the Taskbar.
        /// </summary>
        public event System.EventHandler<Classes.ShowTaskbarEvent> TaskbarShow;
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
        /// Event that is Fired that Allows the Form to show.
        /// </summary>
        public event System.EventHandler ShowForm;

        private void Command1_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            System.Threading.Thread tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms)
            {
                Name = "Classes.KOMManager.PackKoms"
            };
            tr2.Start();
            System.Threading.Thread tr3 = new System.Threading.Thread(Packing)
            {
                Name = "Packing"
            };
            tr3.Start();
        }

        private void Command1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "This option uses plugins to Pack koms.";
        }

        private void Command2_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            System.Threading.Thread tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms)
            {
                Name = "Classes.KOMManager.UnpackKoms"
            };
            tr1.Start();
            System.Threading.Thread tr2 = new System.Threading.Thread(Unpacking)
            {
                Name = "Unpacking"
            };
            tr2.Start();
        }

        private void Command2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "This option uses plugins to Unpack koms.";
        }

        private void Command3_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            AboutForm?.Invoke(this, new System.EventArgs());
        }

        private void Command3_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed.";
        }

        private void Command4_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            TestMods();
        }

        private void Command4_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "Test the mods you made.";
        }

        private void Command5_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            System.Threading.Thread tr4 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordLauncher)
            {
                Name = "Classes.ExecutionManager.RunElswordLauncher"
            };
            tr4.Start();
            System.Threading.Thread tr5 = new System.Threading.Thread(Launcher)
            {
                Name = "Launcher"
            };
            tr5.Start();
        }

        private void Command5_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens. (you might have to remake some mods for some files)";
        }

        private void Command6_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            ConfigForm?.Invoke(this, new System.EventArgs());
        }

        private void Command6_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "Shows the Settings Window. Here you can easily change the Settings to Els_kom.";
        }

        private void Label1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "";
        }

        private void ExitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            bool Cancel = false;
            if (Classes.ExecutionManager.GetRunningElsword() || Classes.ExecutionManager.GetRunningElswordDirectly() || Classes.KOMManager.GetPackingState() || Classes.KOMManager.GetUnpackingState())
            {
                Cancel = true;
                Classes.MessageManager.ShowInfo("Cannot close Els_kom while packing, unpacking, testing mods, or updating the game.", "Info!");
            }
            if (!Cancel)
            {
                end_settings_loop = true;
                Classes.SettingsFile.Settingsxml.Dispose();
                CloseForm?.Invoke(this, new System.EventArgs());
            }
        }

        private void LauncherToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            System.Threading.Thread tr4 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordLauncher)
            {
                Name = "Classes.ExecutionManager.RunElswordLauncher"
            };
            tr4.Start();
            System.Threading.Thread tr5 = new System.Threading.Thread(Launcher)
            {
                Name = "Launcher"
            };
            tr5.Start();
        }

        private void UnpackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            System.Threading.Thread tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms)
            {
                Name = "Classes.KOMManager.UnpackKoms"
            };
            tr1.Start();
            System.Threading.Thread tr2 = new System.Threading.Thread(Unpacking)
            {
                Name = "Unpacking"
            };
            tr2.Start();
        }

        private void TestModsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            TestMods();
        }

        private void PackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            System.Threading.Thread tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms)
            {
                Name = "Classes.KOMManager.PackKoms"
            };
            tr2.Start();
            System.Threading.Thread tr3 = new System.Threading.Thread(Packing)
            {
                Name = "Packing"
            };
            tr3.Start();
        }

        private void NotifyIcon1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TrayClick?.Invoke(this, e);
        }

        private void MainControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "";
        }

        /// <summary>
        /// Initializes the MainControl's constants.
        /// </summary>
        public void LoadControl()
        {
            MakeTrayIcon();
            bool Closing = false;
            if (Classes.ExecutionManager.IsElsKomRunning() == true)
            {
                Classes.MessageManager.ShowError("Sorry, Only 1 Instance is allowed at a time.", "Error!");
                Closing = true;
            }
            else
            {
                if (System.IO.File.Exists(Classes.SettingsFile.Path))
                {
                    Classes.SettingsFile.Settingsxml = new Classes.XMLObject(Classes.SettingsFile.Path, "<Settings></Settings>");
                    ElsDir = Classes.SettingsFile.Settingsxml.Read("ElsDir");
                    if (ElsDir.Length < 1)
                    {
                        Classes.MessageManager.ShowInfo("Welcome to Els_kom." + System.Environment.NewLine + "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + System.Environment.NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!");
                        ConfigForm?.Invoke(this, new System.EventArgs());
                    }
                }
                else
                {
                    Classes.MessageManager.ShowInfo("Welcome to Els_kom." + System.Environment.NewLine + "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + System.Environment.NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!");
                    ConfigForm?.Invoke(this, new System.EventArgs());
                }
                System.Collections.Generic.ICollection<interfaces.IKomPlugin> _komplugins = Classes.GenericPluginLoader<interfaces.IKomPlugin>.LoadPlugins("plugins");
                Classes.KOMManager.komplugins = new System.Collections.Generic.List<interfaces.IKomPlugin>();
                foreach (var komplugin in _komplugins)
                {
                    Classes.KOMManager.komplugins.Add(komplugin);
                }
                System.Collections.Generic.ICollection<interfaces.ICallbackPlugin> _callbackplugins = Classes.GenericPluginLoader<interfaces.ICallbackPlugin>.LoadPlugins("plugins");
                Classes.ExecutionManager.callbackplugins = new System.Collections.Generic.List<interfaces.ICallbackPlugin>();
                foreach (var callbackplugins in _callbackplugins)
                {
                    Classes.ExecutionManager.callbackplugins.Add(callbackplugins);
                }
                if (!Classes.Git.IsMaster)
                {
                    Classes.MessageManager.ShowInfo("This branch is not the master branch, meaning this is a feature branch to test changes. When finished please pull request them for the possibility of them getting merged into master.", "Info!");
                }
                if (Classes.Git.IsDirty)
                {
                    System.Windows.Forms.DialogResult resp = System.Windows.Forms.MessageBox.Show("This build was compiled with Uncommitted changes. As a result, this build might be unstable. Are you sure you want to run this build to test some changes to the code?", "Info!", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                    if (resp == System.Windows.Forms.DialogResult.No)
                    {
                        Closing = true;
                    }
                }
            }
            if (!Closing)
            {
                System.Threading.Thread tr1 = new System.Threading.Thread(CheckSettings)
                {
                    Name = "CheckSettings"
                };
                tr1.Start();
                ShowForm?.Invoke(this, new System.EventArgs());
            }
            else
            {
                Classes.SettingsFile.Settingsxml.Dispose();
                CloseForm?.Invoke(this, new System.EventArgs());
            }
        }

        /// <summary>
        /// Shows the Version Error message and closes the main form if the checked version is not the same.
        /// </summary>
        public bool VersionCheck(string version)
        {
            if (Classes.Version.version != version)
            {
                Classes.MessageManager.ShowError("Sorry, you cannot use Els_kom.exe from this version with a newer or older Core. Please update the executable as well.", "Error!");
                Classes.SettingsFile.Settingsxml.Dispose();
                CloseForm?.Invoke(this, new System.EventArgs());
                return false;
            }
            return true;
        }

        /// <summary>
        /// Handles Packing on the Main Form.
        /// </summary>
        private void Packing()
        {
            while (Classes.KOMManager.GetPackingState())
            {
                Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    if (Command1.Enabled)
                    {
                        Command1.Enabled = false;
                    }
                    if (Command2.Enabled)
                    {
                        Command2.Enabled = false;
                    }
                    if (Command4.Enabled)
                    {
                        Command4.Enabled = false;
                    }
                    if (Command5.Enabled)
                    {
                        Command5.Enabled = false;
                    }
                    if (PackToolStripMenuItem.Enabled)
                    {
                        PackToolStripMenuItem.Enabled = false;
                    }
                    if (UnpackToolStripMenuItem.Enabled)
                    {
                        UnpackToolStripMenuItem.Enabled = false;
                    }
                    if (TestModsToolStripMenuItem.Enabled)
                    {
                        TestModsToolStripMenuItem.Enabled = false;
                    }
                    if (LauncherToolStripMenuItem.Enabled)
                    {
                        LauncherToolStripMenuItem.Enabled = false;
                    }
                    if (string.Equals(Label2.Text, string.Empty))
                    {
                        Label2.Text = "Packing...";
                    }
                    if (!string.Equals(NotifyIcon1.Text, Label2.Text))
                    {
                        NotifyIcon1.Text = Label2.Text;
                    }
                });
            }
            Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                Command1.Enabled = true;
                Command2.Enabled = true;
                Command4.Enabled = true;
                Command5.Enabled = true;
                PackToolStripMenuItem.Enabled = true;
                UnpackToolStripMenuItem.Enabled = true;
                TestModsToolStripMenuItem.Enabled = true;
                LauncherToolStripMenuItem.Enabled = true;
                Label2.Text = "";
                TrayNameChange?.Invoke(this, new System.EventArgs());
            });
        }

        /// <summary>
        /// Handles Unpacking on the Main Form.
        /// </summary>
        private void Unpacking()
        {
            while (Classes.KOMManager.GetUnpackingState())
            {
                Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    if (Command1.Enabled)
                    {
                        Command1.Enabled = false;
                    }
                    if (Command2.Enabled)
                    {
                        Command2.Enabled = false;
                    }
                    if (Command4.Enabled)
                    {
                        Command4.Enabled = false;
                    }
                    if (Command5.Enabled)
                    {
                        Command5.Enabled = false;
                    }
                    if (PackToolStripMenuItem.Enabled)
                    {
                        PackToolStripMenuItem.Enabled = false;
                    }
                    if (UnpackToolStripMenuItem.Enabled)
                    {
                        UnpackToolStripMenuItem.Enabled = false;
                    }
                    if (TestModsToolStripMenuItem.Enabled)
                    {
                        TestModsToolStripMenuItem.Enabled = false;
                    }
                    if (LauncherToolStripMenuItem.Enabled)
                    {
                        LauncherToolStripMenuItem.Enabled = false;
                    }
                    if (string.Equals(Label2.Text, string.Empty))
                    {
                        Label2.Text = "Unpacking...";
                    }
                    if (!string.Equals(NotifyIcon1.Text, Label2.Text))
                    {
                        NotifyIcon1.Text = Label2.Text;
                    }
                });
            }
            Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                Command1.Enabled = true;
                Command2.Enabled = true;
                Command4.Enabled = true;
                Command5.Enabled = true;
                PackToolStripMenuItem.Enabled = true;
                UnpackToolStripMenuItem.Enabled = true;
                TestModsToolStripMenuItem.Enabled = true;
                LauncherToolStripMenuItem.Enabled = true;
                Label2.Text = "";
                TrayNameChange?.Invoke(this, new System.EventArgs());
            });
        }

        /// <summary>
        /// Handles Testing Mods on the Main Form (before Elsword is executed).
        /// </summary>
        private void TestMods()
        {
            Command1.Enabled = false;
            Command2.Enabled = false;
            Command4.Enabled = false;
            Command5.Enabled = false;
            PackToolStripMenuItem.Enabled = false;
            UnpackToolStripMenuItem.Enabled = false;
            TestModsToolStripMenuItem.Enabled = false;
            LauncherToolStripMenuItem.Enabled = false;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
            foreach (var fi in di.GetFiles("*.kom"))
            {
                string _kom_file = fi.Name;
                // do not copy kom files that are in the koms directory but cannot be found to copy from taget directory to the backup directory to restore later.
                Classes.KOMManager.CopyKomFiles(_kom_file, System.Windows.Forms.Application.StartupPath + "\\koms\\", ElsDir + "\\data");
            }
            System.Threading.Thread tr3 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordDirectly)
            {
                Name = "Classes.ExecutionManager.RunElswordDirectly"
            };
            tr3.Start();
            // TODO: in v1.5.0.0 call DeployCallBack in a new thread right before TestMods2!!!
            System.Threading.Thread tr4 = new System.Threading.Thread(TestMods2)
            {
                Name = "TestMods2"
            };
            tr4.Start();
        }

        /// <summary>
        /// Handles Testing Mods on the Main Form (when Elsword (the game window) closes).
        /// </summary>
        private void TestMods2()
        {
            while (Classes.ExecutionManager.GetExecutingElsword())
            {
            }
            while (Classes.ExecutionManager.GetRunningElswordDirectly())
            {
                if (string.Equals(Label2.Text, string.Empty))
                {
                    Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        Label2.Text = "Testing Mods...";
                    });
                }
            }
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
            foreach (var fi in di.GetFiles("*.kom"))
            {
                string _kom_file = fi.Name;
                Classes.KOMManager.MoveOriginalKomFilesBack(_kom_file, ElsDir + "\\data\\backup", ElsDir + "\\data");
            }
            Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                Command1.Enabled = true;
                Command2.Enabled = true;
                Command4.Enabled = true;
                Command5.Enabled = true;
                PackToolStripMenuItem.Enabled = true;
                UnpackToolStripMenuItem.Enabled = true;
                TestModsToolStripMenuItem.Enabled = true;
                LauncherToolStripMenuItem.Enabled = true;
                Label2.Text = "";
            });
        }

        /// <summary>
        /// Handles Updating the Game but disables the controls while it is updating to avoid unpacking,
        /// packing, and testing mods.
        /// </summary>
        private void Launcher()
        {
            while (Classes.ExecutionManager.GetExecutingElsword())
            {
            }
            while (Classes.ExecutionManager.GetRunningElsword())
            {
                Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    if (Command1.Enabled)
                    {
                        Command1.Enabled = false;
                    }
                    if (Command2.Enabled)
                    {
                        Command2.Enabled = false;
                    }
                    if (Command4.Enabled)
                    {
                        Command4.Enabled = false;
                    }
                    if (Command5.Enabled)
                    {
                        Command5.Enabled = false;
                    }
                    if (PackToolStripMenuItem.Enabled)
                    {
                        PackToolStripMenuItem.Enabled = false;
                    }
                    if (UnpackToolStripMenuItem.Enabled)
                    {
                        UnpackToolStripMenuItem.Enabled = false;
                    }
                    if (TestModsToolStripMenuItem.Enabled)
                    {
                        TestModsToolStripMenuItem.Enabled = false;
                    }
                    if (LauncherToolStripMenuItem.Enabled)
                    {
                        LauncherToolStripMenuItem.Enabled = false;
                    }
                    if (string.Equals(Label2.Text, string.Empty))
                    {
                        Label2.Text = "Updating...";
                    }
                });
            }
            Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                Command1.Enabled = true;
                Command2.Enabled = true;
                Command4.Enabled = true;
                Command5.Enabled = true;
                PackToolStripMenuItem.Enabled = true;
                UnpackToolStripMenuItem.Enabled = true;
                TestModsToolStripMenuItem.Enabled = true;
                LauncherToolStripMenuItem.Enabled = true;
                Label2.Text = "";
            });
        }

        /// <summary>
        /// Ensures the settings that the Main Form reads is always up to date.
        /// </summary>
        private void CheckSettings()
        {
            while (true)
            {
                if (end_settings_loop)
                {
                    break;
                }
                else
                {
                    Classes.SettingsFile.Settingsxml.ReopenFile();
                    showintaskbar_tempvalue = Classes.SettingsFile.Settingsxml.Read("IconWhileElsNotRunning");
                    showintaskbar_tempvalue2 = Classes.SettingsFile.Settingsxml.Read("IconWhileElsRunning");
                    ElsDir_temp = Classes.SettingsFile.Settingsxml.Read("ElsDir");
                    if (!string.Equals(ElsDir, ElsDir_temp))
                    {
                        ElsDir = ElsDir_temp;
                    }
                    if (!Classes.ExecutionManager.GetRunningElswordDirectly())
                    {
                        if (showintaskbar_value != showintaskbar_tempvalue)
                        {
                            showintaskbar_value = showintaskbar_tempvalue;
                        }
                        try
                        {
                            Invoke((System.Windows.Forms.MethodInvoker)delegate
                            {
                                TaskbarShow?.Invoke(this, new Classes.ShowTaskbarEvent(showintaskbar_value));
                            });
                        }
                        catch (System.ComponentModel.InvalidAsynchronousStateException)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (showintaskbar_value2 != showintaskbar_tempvalue2)
                        {
                            showintaskbar_value2 = showintaskbar_tempvalue2;
                        }
                        try
                        {
                            Invoke((System.Windows.Forms.MethodInvoker)delegate
                            {
                                TaskbarShow?.Invoke(this, new Classes.ShowTaskbarEvent(showintaskbar_value));
                            });
                        }
                        catch (System.ComponentModel.InvalidAsynchronousStateException)
                        {
                            break;
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
            this.PackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "PackToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Pack"
            };
            this.PackToolStripMenuItem.Click += new System.EventHandler(this.PackToolStripMenuItem_Click);
            this.UnpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "UnpackToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Unpack"
            };
            this.UnpackToolStripMenuItem.Click += new System.EventHandler(this.UnpackToolStripMenuItem_Click);
            this.ToolStripMenuSep1 = new System.Windows.Forms.ToolStripSeparator
            {
                Name = "ToolStripMenuItem3",
                Size = new System.Drawing.Size(126, 6)
            };
            this.TestModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "TestModsToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Test Mods"
            };
            this.TestModsToolStripMenuItem.Click += new System.EventHandler(this.TestModsToolStripMenuItem_Click);
            this.LauncherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "LauncherToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Launcher"
            };
            this.LauncherToolStripMenuItem.Click += new System.EventHandler(this.LauncherToolStripMenuItem_Click);
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "ExitToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Exit"
            };
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuStrip1.SuspendLayout();
            this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.PackToolStripMenuItem, this.UnpackToolStripMenuItem, this.TestModsToolStripMenuItem,
                this.LauncherToolStripMenuItem, this.ToolStripMenuSep1, this.ExitToolStripMenuItem});
            this.ContextMenuStrip1.Name = "ContextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(130, 154);
            this.ContextMenuStrip1.ResumeLayout(false);
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components)
            {
                ContextMenuStrip = this.ContextMenuStrip1,
                Visible = false
            };
            this.NotifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseClick);
        }
    }
}
