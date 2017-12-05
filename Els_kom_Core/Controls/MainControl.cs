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

        string ElsDir;
        private string showintaskbar_value;
        private Classes.INIObject settingsini;
        private string showintaskbar_value2;
        private string showintaskbar_tempvalue;
        private string showintaskbar_tempvalue2;

        // events.
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
        /// Event that is Fired that Allows the Form it is on to open up it's Settings Form.
        /// </summary>
        public event System.EventHandler ConfigForm2;
        /// <summary>
        /// Event that is Fired that Allows the Form it is on to open up it's About Form.
        /// </summary>
        public event System.EventHandler AboutForm;

        void Command1_Click(object sender, System.EventArgs e)
        {
            System.Threading.Thread tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms);
            tr2.Start();
            Timer2.Enabled = true;
        }

        void Command1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "This uses kompact_new.exe to Pack koms.";
        }

        void Command2_Click(object sender, System.EventArgs e)
        {
            System.Threading.Thread tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms);
            tr1.Start();
            Timer1.Enabled = true;
        }

        void Command2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "This uses komextract_new.exe to Unpack koms.";
        }

        void Command3_Click(object sender, System.EventArgs e)
        {
            AboutForm?.Invoke(this, new System.EventArgs());
        }

        void Command3_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed.";
        }

        void Command4_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            Timer3.Enabled = true;
        }

        void Command4_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "Test the mods you made.";
        }

        void Command5_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            System.Threading.Thread tr4 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordLauncher);
            tr4.Start();
        }

        void Command5_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens. (you might have to remake some mods for some files)";
        }

        void Label1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Label1.Text = "";
        }

        void Timer1_Tick(object sender, System.EventArgs e)
        {
            if (Classes.KOMManager.GetUnpackingState())
            {
                Command1.Enabled = false;
                Command2.Enabled = false;
                Command4.Enabled = false;
                Command5.Enabled = false;
                PackToolStripMenuItem.Enabled = false;
                UnpackToolStripMenuItem.Enabled = false;
                TestModsToolStripMenuItem.Enabled = false;
                LauncherToolStripMenuItem.Enabled = false;
                Label2.Text = "Unpacking...";
                NotifyIcon1.Text = Label2.Text;
            }
            else
            {
                Timer1.Enabled = false;
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
            }
        }

        void Timer2_Tick(object sender, System.EventArgs e)
        {
            if (Classes.KOMManager.GetPackingState())
            {
                Command1.Enabled = false;
                Command2.Enabled = false;
                Command4.Enabled = false;
                Command5.Enabled = false;
                PackToolStripMenuItem.Enabled = false;
                UnpackToolStripMenuItem.Enabled = false;
                TestModsToolStripMenuItem.Enabled = false;
                LauncherToolStripMenuItem.Enabled = false;
                Label2.Text = "Packing...";
                NotifyIcon1.Text = Label2.Text;
            }
            else
            {
                Timer2.Enabled = false;
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
            }
        }

        void Timer3_Tick(object sender, System.EventArgs e)
        {
            Timer3.Enabled = false;
            Command1.Enabled = false;
            Command2.Enabled = false;
            Command4.Enabled = false;
            Command5.Enabled = false;
            PackToolStripMenuItem.Enabled = false;
            UnpackToolStripMenuItem.Enabled = false;
            TestModsToolStripMenuItem.Enabled = false;
            LauncherToolStripMenuItem.Enabled = false;
            // TODO: Copy All KOM Files on this part.
            System.Threading.Thread tr3 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordDirectly);
            tr3.Start();
            Timer4.Interval = 1;
            Timer4.Enabled = true;
        }

        void Timer4_Tick(object sender, System.EventArgs e)
        {
            if (Classes.ExecutionManager.GetRunningElswordDirectly())
            {
                Label2.Text = "Testing Mods...";
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
                Timer4.Enabled = false;
            }
        }

        /*
			This timer is required for Reading The Settings for the 2 icon Events for when Elsword is running or not.
			What this should do is make it actually work and read in a timely manner using global variables to compare the 2 values if not the same change
			the used global variable and set it accordingly.
		*/
        void Timer5_Tick(object sender, System.EventArgs e)
        {
            showintaskbar_tempvalue = settingsini.Read("Settings.ini", "IconWhileElsNotRunning");
            showintaskbar_tempvalue2 = settingsini.Read("Settings.ini", "IconWhileElsRunning");
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
                TaskbarShow?.Invoke(this, new Classes.ShowTaskbarEvent(showintaskbar_value2));
            }
        }

        void ExitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Timer1.Enabled = false;
            Timer2.Enabled = false;
            timer5.Enabled = false;
            CloseForm?.Invoke(this, new System.EventArgs());
        }

        void LauncherToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            System.Threading.Thread tr4 = new System.Threading.Thread(Classes.ExecutionManager.RunElswordLauncher);
            tr4.Start();
        }

        void UnpackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            System.Threading.Thread tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms);
            tr1.Start();
            Timer1.Enabled = true;
        }

        void TestModsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Label1.Text = "";
            MinimizeForm?.Invoke(this, new System.EventArgs());
            Timer3.Enabled = true;
        }

        void PackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            System.Threading.Thread tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms);
            tr2.Start();
            Timer2.Enabled = true;
        }

        void SettingsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ConfigForm?.Invoke(this, new System.EventArgs());
        }

        void NotifyIcon1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
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
            if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\Settings.ini"))
            {
                settingsini = new Classes.INIObject(System.Windows.Forms.Application.StartupPath + "\\Settings.ini");
                ElsDir = settingsini.Read("Settings.ini", "ElsDir");
                if (ElsDir.Length > 0)
                {
                    //The Setting actually exists and is not a empty String so we do not need to open the dialog again.
                }
                else
                {
                    ConfigForm2?.Invoke(this, new System.EventArgs());
                }
            }
            else
            {
                ConfigForm2?.Invoke(this, new System.EventArgs());
            }
            timer5.Enabled = true;
        }
    }
}
