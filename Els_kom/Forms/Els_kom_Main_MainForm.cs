using System;
using System.Windows.Forms;


namespace Els_kom.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public Form aboutfrm;
        public Form settingsfrm;

        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.Category("Behavior")]
        [System.ComponentModel.Description("Specifies whether to allow the window to minimize when the minimize button and command are enabled.")]
        [System.ComponentModel.DefaultValue(true)]
        /* Allows the user to enable or disable their event
		 * handler at will if they only want it to sometimes
		 * fire. */
        public bool Enablehandlers { get; set; }
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MAXIMIZE = 0xf030;
        private const int SC_MINIMIZE = 0xf020;
        private const int SC_RESTORE = 0xf120;

        //void Command1_Click(object sender, EventArgs e)
        //{
        //    System.Threading.Thread tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms);
        //    tr2.Start();
        //    Timer2.Enabled = true;
        //}

        //void Command1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Label1.Text = "This uses kompact_new.exe to Pack koms.";
        //}

        //void Command2_Click(object sender, EventArgs e)
        //{
        //    System.Threading.Thread tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms);
        //    tr1.Start();
        //    Timer1.Enabled = true;
        //}

        //void Command2_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Label1.Text = "This uses komextract_new.exe to Unpack koms.";
        //}

        //void Command3_Click(object sender, EventArgs e)
        //{
        //    aboutfrm = new AboutForm();
        //    aboutfrm.ShowDialog();
        //}

        //void Command3_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Label1.Text = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed.";
        //}

        //void Command4_Click(object sender, EventArgs e)
        //{
        //    if (System.IO.File.Exists(Application.StartupPath + "\\Test_Mods.exe"))
        //    {
        //        Label1.Text = "";
        //        this.WindowState = FormWindowState.Minimized;
        //        Timer3.Enabled = true;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Can't find '" + Application.StartupPath + "\\Test_Mods.exe'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        //void Command4_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Label1.Text = "Test the mods you made.";
        //}

        //void Command5_Click(object sender, EventArgs e)
        //{
        //    if (System.IO.File.Exists(Application.StartupPath + "\\Launcher.exe"))
        //    {
        //        Label1.Text = "";
        //        this.WindowState = FormWindowState.Minimized;
        //        Classes.Process.Shell(Application.StartupPath + "\\Launcher.exe", null, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, Application.StartupPath, false);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Can't find '" + Application.StartupPath + "\\Launcher.exe'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        //void Command5_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens. (you might have to remake some mods for some files)";
        //}

        void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.Maincontrol1.NotifyIcon1.Visible = false;
            this.ShowInTaskbar = false;
            bool previnstance;
            this.Icon = Els_kom_Core.Properties.Resources.els_kom_icon;
            this.Maincontrol1.NotifyIcon1.Icon = this.Icon;
            this.Maincontrol1.NotifyIcon1.Text = this.Text;
            previnstance = Els_kom_Core.Classes.Process.IsElsKomRunning();
            if (Els_kom_Core.Classes.Version.version != "1.4.9.7") {
                MessageBox.Show("Sorry, you cannot use Els_kom.exe from version the last version with the newer Core. Please update the executable as well.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            if (previnstance == true)
            {
                MessageBox.Show("Sorry, Only 1 Instance is allowed at a time.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                this.Maincontrol1.LoadControl();
                this.Show();
            }
        }

        //void MainForm_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Label1.Text = "";
        //}

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool Cancel = e.Cancel;
            // CloseReason UnloadMode = e.CloseReason; <-- Removed because not used.
            e.Cancel = Cancel;
        }

        void MainForm_MouseLeave(object sender, EventArgs e)
        {
            this.Maincontrol1.Label1.Text = "";
        }

        //void Label1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Label1.Text = "";
        //}

        //void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (AboutForm.Label1 != null && AboutForm.Label1.Text == "1")
		//	{
        //        //I have to Sadly disable left button on the Notify Icon to prevent a bug with AboutForm Randomly Unloading or not reshowing.
        //    }
		//	else
		//	{
		//		if (e.Button == MouseButtons.Left)
		//		{
        //            if (this.ShowInTaskbar == true)
        //            {
        //                if (this.WindowState == FormWindowState.Minimized)
        //                {
        //                    this.WindowState = FormWindowState.Normal;
        //                    this.Activate();
        //                }
        //                else
        //                {
        //                    this.WindowState = FormWindowState.Minimized;
        //                }
        //            }
        //            else if (NotifyIcon1.Visible == true)
        //            {
        //                if (this.WindowState == FormWindowState.Minimized)
        //                {
        //                    this.WindowState = FormWindowState.Normal;
        //                    this.Show();
        //                }
        //                else
        //                {
        //                    this.Hide();
        //                    this.WindowState = FormWindowState.Minimized;
        //                }
        //            }
		//	    }
		//	}
		//}

		//void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//	Timer1.Enabled = false;
		//	Timer2.Enabled = false;
		//	Timer6.Enabled = false;
		//	timer5.Enabled = false;
        //    if (aboutfrm != null) {
        //        aboutfrm.Close();
        //    }
        //    if (settingsfrm != null) {
        //        settingsfrm.Close();
        //    }
		//	this.Close();
		//}

		//void LauncherToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//	Label1.Text = "";
		//	if (System.IO.File.Exists(Application.StartupPath + "\\Launcher.exe"))
		//	{
		//		this.WindowState = FormWindowState.Minimized;
		//		Classes.Process.Shell(Application.StartupPath + "\\Launcher.exe", null, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, Application.StartupPath, false);
		//	}
		//	else
		//	{
		//		MessageBox.Show("Can't find '" + Application.StartupPath + "\\Launcher.exe'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		//	}
		//}

		//void UnpackToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//	System.Threading.Thread tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms);
		//	tr1.Start();
		//	Timer1.Enabled = true;
		//}

		//void TestModsToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//	Label1.Text = "";
		//	if (System.IO.File.Exists(Application.StartupPath + "\\Test_Mods.exe"))
		//	{
		//		this.WindowState = FormWindowState.Minimized;
		//		Timer3.Enabled = true;
		//	}
		//	else
		//	{
		//		MessageBox.Show("Can't find '" + Application.StartupPath + "\\Test_Mods.exe'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		//	}
		//}

		//void PackToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//	System.Threading.Thread tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms);
		//	tr2.Start();
		//	Timer2.Enabled = true;
		//}

		//void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//	settingsfrm = new SettingsForm();
		//	settingsfrm.ShowDialog();
		//}

        //void Timer1_Tick(object sender, EventArgs e)
        //{
        //    if (Classes.KOMManager.is_unpacking)
        //    {
        //        Timer6.Enabled = false;
        //        Command1.Enabled = false;
        //        Command2.Enabled = false;
        //        Command4.Enabled = false;
        //        Command5.Enabled = false;
        //        PackToolStripMenuItem.Enabled = false;
        //        UnpackToolStripMenuItem.Enabled = false;
        //        TestModsToolStripMenuItem.Enabled = false;
        //        LauncherToolStripMenuItem.Enabled = false;
        //        Label2.Text = "Unpacking...";
        //        NotifyIcon1.Text = Label2.Text;
        //    }
        //    else
        //    {
        //        Timer1.Enabled = false;
        //        Timer6.Enabled = true;
        //        Command1.Enabled = true;
        //        Command2.Enabled = true;
        //        Command4.Enabled = true;
        //        Command5.Enabled = true;
        //        PackToolStripMenuItem.Enabled = true;
        //        UnpackToolStripMenuItem.Enabled = true;
        //        TestModsToolStripMenuItem.Enabled = true;
        //        LauncherToolStripMenuItem.Enabled = true;
        //        Label2.Text = "";
        //        NotifyIcon1.Text = this.Text;
        //    }
        //}

        //void Timer2_Tick(object sender, EventArgs e)
        //{
        //    if (Classes.KOMManager.is_packing)
        //    {
        //        Timer6.Enabled = false;
        //        Command1.Enabled = false;
        //        Command2.Enabled = false;
        //        Command4.Enabled = false;
        //        Command5.Enabled = false;
        //        PackToolStripMenuItem.Enabled = false;
        //        UnpackToolStripMenuItem.Enabled = false;
        //        TestModsToolStripMenuItem.Enabled = false;
        //        LauncherToolStripMenuItem.Enabled = false;
        //        Label2.Text = "Packing...";
        //        NotifyIcon1.Text = Label2.Text;
        //    }
        //    else
        //    {
        //        Timer2.Enabled = false;
        //        Timer6.Enabled = true;
        //        Command1.Enabled = true;
        //        Command2.Enabled = true;
        //        Command4.Enabled = true;
        //        Command5.Enabled = true;
        //        PackToolStripMenuItem.Enabled = true;
        //        UnpackToolStripMenuItem.Enabled = true;
        //        TestModsToolStripMenuItem.Enabled = true;
        //        LauncherToolStripMenuItem.Enabled = true;
        //        Label2.Text = "";
        //        NotifyIcon1.Text = this.Text;
        //    }
        //}

        //void Timer3_Tick(object sender, EventArgs e)
		//{
		//	Timer6.Enabled = false;
		//	Timer3.Enabled = false;
		//	Command1.Enabled = false;
		//	Command2.Enabled = false;
		//	Command4.Enabled = false;
		//	Command5.Enabled = false;
		//	PackToolStripMenuItem.Enabled = false;
		//	UnpackToolStripMenuItem.Enabled = false;
		//	TestModsToolStripMenuItem.Enabled = false;
		//	LauncherToolStripMenuItem.Enabled = false;
		//	// TODO: Copy All KOM Files on this part.
		//	Classes.Process.Shell(Application.StartupPath + "\\Test_Mods.exe", null, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, Application.StartupPath, false);
		//	Timer4.Interval = 1;
		//	Timer4.Enabled = true;
		//}

		//void Timer4_Tick(object sender, EventArgs e)
		//{
		//	x2bool = Classes.Process.IsX2Running();
		//	if (x2bool)
		//	{
		//		Label2.Text = "Testing Mods...";
		//	}
		//	else
		//	{
		//		Command1.Enabled = true;
		//		Command2.Enabled = true;
		//		Command4.Enabled = true;
		//		Command5.Enabled = true;
		//		PackToolStripMenuItem.Enabled = true;
		//		UnpackToolStripMenuItem.Enabled = true;
		//		TestModsToolStripMenuItem.Enabled = true;
		//		LauncherToolStripMenuItem.Enabled = true;
		//		Label2.Text = "";
		//		Timer6.Enabled = true;
		//		Timer4.Enabled = false;
		//	}
		//}

		/*
			This timer is required for Reading The Settings for the 2 icon Events for when Elsword is running or not.
			What this should do is make it actually work and read in a timely manner using global variables to compare the 2 values if not the same change
			the used global variable and set it accordingly.
		*/
		//void Timer5_Tick(object sender, EventArgs e)
		//{
		//	showintaskbar_tempvalue = settingsini.Read("Settings.ini", "IconWhileElsNotRunning");
		//	showintaskbar_tempvalue2 = settingsini.Read("Settings.ini", "IconWhileElsRunning");
		//	x2bool = Classes.Process.IsX2Running();
		//	if (!x2bool)
		//	{
		//		if (showintaskbar_value != showintaskbar_tempvalue)
		//		{
		//			showintaskbar_value = showintaskbar_tempvalue;
		//		}
		//		if (showintaskbar_value2 != showintaskbar_tempvalue2)
		//		{
		//			showintaskbar_value2 = showintaskbar_tempvalue2;
		//		}
		//		if (showintaskbar_value == "0") // Taskbar only!!!
		//		{
		//			this.NotifyIcon1.Visible = false;
		//			this.ShowInTaskbar = true;
		//		}
		//		if (showintaskbar_value == "1") // Tray only!!!
		//		{
		//			NotifyIcon1.Visible = true;
		//			this.ShowInTaskbar = false;
		//		}
		//		if (showintaskbar_value == "2") // Both!!!
		//		{
		//			NotifyIcon1.Visible = true;
		//			this.ShowInTaskbar = true;
		//		}
		//	}
		//	else
		//	{
		//		if (showintaskbar_value != showintaskbar_tempvalue)
		//		{
		//			showintaskbar_value = showintaskbar_tempvalue;
		//		}
		//		if (showintaskbar_value2 != showintaskbar_tempvalue2)
		//		{
		//			showintaskbar_value2 = showintaskbar_tempvalue2;
		//		}
		//		if (showintaskbar_value2 == "0") // Taskbar only!!!
		//		{
		//			NotifyIcon1.Visible = false;
		//			this.ShowInTaskbar = true;
		//		}
		//		if (showintaskbar_value2 == "1") // Tray only!!!
		//		{
		//			NotifyIcon1.Visible = true;
		//			this.ShowInTaskbar = false;
		//		}
		//		if (showintaskbar_value2 == "2") // Both!!!
		//		{
		//			NotifyIcon1.Visible = true;
		//			this.ShowInTaskbar = true;
		//		}
		//	}
		//	if (!this.ShowInTaskbar) {
        //        this.Enablehandlers = true;
		//	} else {
        //        this.Enablehandlers = false;
		//	}
		//}

        //void Timer6_Tick(object sender, EventArgs e)
        //{
        //    bool checkiflauncherstubexists = false;
        //    bool checkiftestmodsstubexists = false;

        //    if (this.WindowState == FormWindowState.Normal)
        //    {
        //        this.Height = 184;
        //        this.Width = 320;
        //    }
        //    if (!checkiflauncherstubexists)
        //    {
        //        checkiflauncherstubexists = true;
        //    }
        //    if (!checkiftestmodsstubexists)
        //    {
        //        checkiftestmodsstubexists = true;
        //    }
        //    if (checkiflauncherstubexists)
        //    {
        //        if (System.IO.File.Exists(Application.StartupPath + "\\Launcher.exe"))
        //        {
        //            LauncherToolStripMenuItem.Enabled = true;
        //            Command5.Enabled = true;
        //            checkiflauncherstubexists = false;
        //        }
        //        else
        //        {
        //            LauncherToolStripMenuItem.Enabled = false;
        //            Command5.Enabled = false;
        //            checkiflauncherstubexists = false;
        //        }
        //    }
        //    if (checkiftestmodsstubexists)
        //    {
        //        if (System.IO.File.Exists(Application.StartupPath + "\\Test_Mods.exe"))
        //        {
        //            TestModsToolStripMenuItem.Enabled = true;
        //            Command4.Enabled = true;
        //            checkiftestmodsstubexists = false;
        //        }
        //        else
        //        {
        //            TestModsToolStripMenuItem.Enabled = false;
        //            Command4.Enabled = false;
        //            checkiftestmodsstubexists = false;
        //        }
        //    }
        //}

        protected override void WndProc(ref Message m)
        {
            if (Enablehandlers && m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == SC_MINIMIZE)
                {
                    this.Hide();
                    m.Result = IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() == SC_MAXIMIZE)
                {
                    this.Show();
                    this.Activate();
                    m.Result = IntPtr.Zero;
                    return;
                }
                else if (m.WParam.ToInt32() == SC_RESTORE)
                {
                    this.Show();
                    this.Activate();
                    m.Result = IntPtr.Zero;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void Maincontrol1_CloseForm(object sender, EventArgs e)
        {
            if (aboutfrm != null)
            {
                aboutfrm.Close();
            }
            if (settingsfrm != null)
            {
                settingsfrm.Close();
            }
            this.Close();
        }

        private void Maincontrol1_MinimizeForm(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Maincontrol1_TaskbarShow(object sender, Els_kom_Core.Classes.ShowTaskbarEvent e)
        {
            if (e.value == "0") // Taskbar only!!!
            {
                this.Maincontrol1.NotifyIcon1.Visible = false;
                this.ShowInTaskbar = true;
            }
            if (e.value == "1") // Tray only!!!
            {
                this.Maincontrol1.NotifyIcon1.Visible = true;
                this.ShowInTaskbar = false;
            }
            if (e.value == "2") // Both!!!
            {
                this.Maincontrol1.NotifyIcon1.Visible = true;
                this.ShowInTaskbar = true;
            }
            if (!this.ShowInTaskbar)
            {
                this.Enablehandlers = true;
            }
            else
            {
                this.Enablehandlers = false;
            }
        }

        private void Maincontrol1_TrayNameChange(object sender, EventArgs e)
        {
            this.Maincontrol1.NotifyIcon1.Text = this.Text;
        }

        private void Maincontrol1_TrayClick(object sender, MouseEventArgs e)
        {
            if (AboutForm.Label1 != null && AboutForm.Label1.Text == "1")
            {
                //I have to Sadly disable left button on the Notify Icon to prevent a bug with AboutForm Randomly Unloading or not reshowing.
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (this.ShowInTaskbar == true)
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
                    else if (this.Maincontrol1.NotifyIcon1.Visible == true)
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

        private void Maincontrol1_AboutForm(object sender, EventArgs e)
        {
            aboutfrm = new AboutForm();
            aboutfrm.ShowDialog();
        }

        private void Maincontrol1_ConfigForm(object sender, EventArgs e)
        {
            settingsfrm = new SettingsForm();
            settingsfrm.ShowDialog();
        }

        private void Maincontrol1_ConfigForm2(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to Els_kom." + Environment.NewLine + "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + Environment.NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            settingsfrm = new SettingsForm();
            settingsfrm.ShowDialog();
        }
    }
}
