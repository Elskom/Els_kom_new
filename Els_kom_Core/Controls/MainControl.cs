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
        public MainControl() => InitializeComponent();

        private string ElsDir;
        private string showintaskbar_value;
        private string showintaskbar_value2;
        private string showintaskbar_tempvalue;
        private string showintaskbar_tempvalue2;
        private string ElsDir_temp;
        private string iconVal;
        private System.Windows.Forms.Timer SettingsTmr;
        private System.Windows.Forms.Timer PackingTmr;
        private System.Windows.Forms.Timer UnpackingTmr;
        private System.Windows.Forms.Timer TestModsTmr;
        private System.Windows.Forms.Timer LauncherTmr;
        /// <summary>
        /// Allows this control to properly close the loop that reads
        /// settings that makes it work properly.
        /// </summary>
        public bool end_settings_loop = false;
        /// <summary>
        /// Determines if the program should close
        /// or skip closing.
        /// </summary>
        public static bool _closable = false;
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
            var tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms)
            {
                Name = "Classes.KOMManager.PackKoms"
            };
            tr2.Start();
            PackingTmr.Enabled = true;
        }

        private void Command1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => Label1.Text = "This option uses plugins to Pack koms.";

        private void Command2_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            var tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms)
            {
                Name = "Classes.KOMManager.UnpackKoms"
            };
            tr1.Start();
            UnpackingTmr.Enabled = true;
        }

        private void Command2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => Label1.Text = "This option uses plugins to Unpack koms.";

        private void Command3_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            AboutForm?.Invoke(this, new System.EventArgs());
        }

        private void Command3_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => Label1.Text = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed.";

        private void Command4_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            TestMods();
        }

        private void Command4_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => Label1.Text = "Test the mods you made.";

        private void Command5_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            var tr4 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordLauncher)
            {
                Name = "Classes.ExecutionManager.RunElswordLauncher"
            };
            tr4.Start();
            LauncherTmr.Enabled = true;
        }

        private void Command5_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens (you might have to remake some mods for some files).";

        private void Command6_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            ConfigForm?.Invoke(this, new System.EventArgs());
        }

        private void Command6_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => Label1.Text = "Shows the Settings Window. Here you can easily change the Settings to Els_kom.";

        private void Label1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => Label1.Text = "";

        private void ExitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var Cancel = false;
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
            var tr4 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordLauncher)
            {
                Name = "Classes.ExecutionManager.RunElswordLauncher"
            };
            tr4.Start();
            LauncherTmr.Enabled = true;
        }

        private void UnpackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms)
            {
                Name = "Classes.KOMManager.UnpackKoms"
            };
            tr1.Start();
            UnpackingTmr.Enabled = true;
        }

        private void TestModsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            TestMods();
        }

        private void PackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms)
            {
                Name = "Classes.KOMManager.PackKoms"
            };
            tr2.Start();
            PackingTmr.Enabled = true;
        }

        private void NotifyIcon1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e) => TrayClick?.Invoke(this, e);

        private void MainControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) => Label1.Text = "";

        /// <summary>
        /// Gets if the Els_kom window can be closed or not.
        /// </summary>
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
            MakeTrayIcon();
            var Closing = false;
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\koms"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\koms");
            }
            if (Classes.ExecutionManager.IsElsKomRunning() == true)
            {
                Classes.MessageManager.ShowError("Sorry, Only 1 Instance is allowed at a time.", "Error!");
                Closing = true;
            }
            else
            {
                Classes.SettingsFile.Settingsxml = new Classes.XMLObject(Classes.SettingsFile.Path, "<Settings></Settings>");
                ElsDir = Classes.SettingsFile.Settingsxml.Read("ElsDir");
                var tmpiconVal = Classes.SettingsFile.Settingsxml.Read("WindowIcon");
                iconVal = string.IsNullOrEmpty(tmpiconVal) ? "0" : tmpiconVal;
                if (ElsDir.Length < 1)
                {
                    Classes.MessageManager.ShowInfo("Welcome to Els_kom." + System.Environment.NewLine + "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + System.Environment.NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!");
                    ConfigForm?.Invoke(this, new System.EventArgs());
                }
                var _komplugins = Classes.GenericPluginLoader<interfaces.IKomPlugin>.LoadPlugins("plugins");
                Classes.KOMManager.komplugins = new System.Collections.Generic.List<interfaces.IKomPlugin>();
                foreach (var komplugin in _komplugins)
                {
                    Classes.KOMManager.komplugins.Add(komplugin);
                }
                var _callbackplugins = Classes.GenericPluginLoader<interfaces.ICallbackPlugin>.LoadPlugins("plugins");
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
                    var resp = System.Windows.Forms.MessageBox.Show("This build was compiled with Uncommitted changes. As a result, this build might be unstable. Are you sure you want to run this build to test some changes to the code?", "Info!", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                    if (resp == System.Windows.Forms.DialogResult.No)
                    {
                        Closing = true;
                    }
                }
            }
            if (!Closing)
            {
                if (components == null)
                {
                    components = new System.ComponentModel.Container();
                }
                SettingsTmr = new System.Windows.Forms.Timer(components)
                {
                    Enabled = true,
                    Interval = 1
                };
                PackingTmr = new System.Windows.Forms.Timer(components)
                {
                    Enabled = false,
                    Interval = 1
                };
                PackingTmr.Tick += new System.EventHandler(Packing);
                UnpackingTmr = new System.Windows.Forms.Timer(components)
                {
                    Enabled = false,
                    Interval = 1
                };
                UnpackingTmr.Tick += new System.EventHandler(Unpacking);
                TestModsTmr = new System.Windows.Forms.Timer(components)
                {
                    Enabled = false,
                    Interval = 1
                };
                TestModsTmr.Tick += new System.EventHandler(TestMods2);
                LauncherTmr = new System.Windows.Forms.Timer(components)
                {
                    Enabled = false,
                    Interval = 1
                };
                LauncherTmr.Tick += new System.EventHandler(Launcher);
                ShowForm?.Invoke(this, new System.EventArgs());
            }
            else
            {
                Classes.SettingsFile.Settingsxml?.Dispose();
                CloseForm?.Invoke(this, new System.EventArgs());
            }
        }

        /// <summary>
        /// Shows the Version Error message and closes the main form if the checked version is not the same.
        /// </summary>
        public bool VersionCheck()
        {
            if (typeof(MainControl).Assembly.GetName().Version.ToString() != System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
            {
                Classes.MessageManager.ShowError("Sorry, you cannot use Els_kom.exe from this version with a newer or older Core. Please update the executable as well.", "Error!");
                CloseForm?.Invoke(this, new System.EventArgs());
                return false;
            }
            return true;
        }

        /// <summary>
        /// Handles Packing on the Main Form.
        /// </summary>
        private void Packing(object sender, System.EventArgs e)
        {
            if (Classes.KOMManager.GetPackingState())
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
            }
            else
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
                PackingTmr.Enabled = false;
            }
        }

        /// <summary>
        /// Handles Unpacking on the Main Form.
        /// </summary>
        private void Unpacking(object sender, System.EventArgs e)
        {
            if (Classes.KOMManager.GetUnpackingState())
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
            }
            else
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
                UnpackingTmr.Enabled = false;
            }
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
            var di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
            foreach (var fi in di.GetFiles("*.kom"))
            {
                var _kom_file = fi.Name;
                // do not copy kom files that are in the koms directory but cannot be found to copy from taget directory to the backup directory to restore later.
                Classes.KOMManager.CopyKomFiles(_kom_file, System.Windows.Forms.Application.StartupPath + "\\koms\\", ElsDir + "\\data");
            }
            var tr3 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordDirectly)
            {
                Name = "Classes.ExecutionManager.RunElswordDirectly"
            };
            tr3.Start();
            TestModsTmr.Enabled = true;
        }

        /// <summary>
        /// Handles Testing Mods on the Main Form (when Elsword (the game window) closes).
        /// </summary>
        private void TestMods2(object sender, System.EventArgs e)
        {
            var executing = Classes.ExecutionManager.GetExecutingElsword();
            if (!executing)
            {
                if (Classes.ExecutionManager.GetRunningElswordDirectly())
                {
                    Classes.ExecutionManager.DeployCallBack();
                    if (string.Equals(Label2.Text, string.Empty))
                    {
                        Label2.Text = "Testing Mods...";
                    }
                }
                else
                {
                    var di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
                    foreach (var fi in di.GetFiles("*.kom"))
                    {
                        var _kom_file = fi.Name;
                        Classes.KOMManager.MoveOriginalKomFilesBack(_kom_file, ElsDir + "\\data\\backup", ElsDir + "\\data");
                    }
                    Command1.Enabled = true;
                    Command2.Enabled = true;
                    Command4.Enabled = true;
                    Command5.Enabled = true;
                    PackToolStripMenuItem.Enabled = true;
                    UnpackToolStripMenuItem.Enabled = true;
                    TestModsToolStripMenuItem.Enabled = true;
                    LauncherToolStripMenuItem.Enabled = true;
                    Label2.Text = "";
                    TestModsTmr.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Handles Updating the Game but disables the controls while it is updating to avoid unpacking,
        /// packing, and testing mods.
        /// </summary>
        private void Launcher(object sender, System.EventArgs e)
        {
            if (!Classes.ExecutionManager.GetExecutingElsword())
            {
                if (Classes.ExecutionManager.GetRunningElsword())
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
                }
                else
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
                    LauncherTmr.Enabled = false;
                }
            }
        }

        private void CheckSettings(object sender, System.EventArgs e)
        {
            if (end_settings_loop)
            {
                SettingsTmr.Enabled = false;
            }
            else
            {
                if (AbleToClose())
                {
                    Classes.SettingsFile.Settingsxml.ReopenFile();
                    showintaskbar_tempvalue = Classes.SettingsFile.Settingsxml.Read("IconWhileElsNotRunning");
                    showintaskbar_tempvalue2 = Classes.SettingsFile.Settingsxml.Read("IconWhileElsRunning");
                    ElsDir_temp = Classes.SettingsFile.Settingsxml.Read("ElsDir");
                    var tmpiconVal = Classes.SettingsFile.Settingsxml.Read("WindowIcon");
                    if (!iconVal.Equals(tmpiconVal))
                    {
                        iconVal = tmpiconVal;
                        // this seem to not update the form icon at runtime...
                        FindForm().Icon = Classes.Icons.FormIcon;
                    }
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
                        TaskbarShow?.Invoke(this, new Classes.ShowTaskbarEvent(showintaskbar_value));
                    }
                    else
                    {
                        if (showintaskbar_value2 != showintaskbar_tempvalue2)
                        {
                            showintaskbar_value2 = showintaskbar_tempvalue2;
                        }
                        TaskbarShow?.Invoke(this, new Classes.ShowTaskbarEvent(showintaskbar_value));
                    }
                }
            }
        }

        /// <summary>
        /// Makes the tray icon.
        /// </summary>
        private void MakeTrayIcon()
        {
            PackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "PackToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Pack"
            };
            PackToolStripMenuItem.Click += new System.EventHandler(PackToolStripMenuItem_Click);
            UnpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "UnpackToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Unpack"
            };
            UnpackToolStripMenuItem.Click += new System.EventHandler(UnpackToolStripMenuItem_Click);
            ToolStripMenuSep1 = new System.Windows.Forms.ToolStripSeparator
            {
                Name = "ToolStripMenuItem3",
                Size = new System.Drawing.Size(126, 6)
            };
            TestModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "TestModsToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Test Mods"
            };
            TestModsToolStripMenuItem.Click += new System.EventHandler(TestModsToolStripMenuItem_Click);
            LauncherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "LauncherToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Launcher"
            };
            LauncherToolStripMenuItem.Click += new System.EventHandler(LauncherToolStripMenuItem_Click);
            ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem
            {
                Name = "ExitToolStripMenuItem",
                Size = new System.Drawing.Size(129, 22),
                Text = "Exit"
            };
            ExitToolStripMenuItem.Click += new System.EventHandler(ExitToolStripMenuItem_Click);
            ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            ContextMenuStrip1.SuspendLayout();
            ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                PackToolStripMenuItem, UnpackToolStripMenuItem, TestModsToolStripMenuItem,
                LauncherToolStripMenuItem, ToolStripMenuSep1, ExitToolStripMenuItem});
            ContextMenuStrip1.Name = "ContextMenuStrip1";
            ContextMenuStrip1.Size = new System.Drawing.Size(130, 154);
            ContextMenuStrip1.ResumeLayout(false);
            NotifyIcon1 = new System.Windows.Forms.NotifyIcon(components)
            {
                ContextMenuStrip = ContextMenuStrip1,
                Visible = false
            };
            NotifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon1_MouseClick);
        }

        /// <summary>
        /// Gets the Syscommand check value.
        /// </summary>
        public int GetSysCommand() => (int)Enums.SYSCOMMANDS.WM_SYSCOMMAND;

        /// <summary>
        /// Gets the Minimize Command check value.
        /// </summary>
        public int GetMinimizeCommand() => (int)Enums.SYSCOMMANDS.SC_MINIMIZE;

        /// <summary>
        /// Gets the Maximize Command check value.
        /// </summary>
        public int GetMaximizeCommand() => (int)Enums.SYSCOMMANDS.SC_MAXIMIZE;

        /// <summary>
        /// Gets the Restore Command check value.
        /// </summary>
        public int GetRestoreCommand() => (int)Enums.SYSCOMMANDS.SC_RESTORE;
    }
}
