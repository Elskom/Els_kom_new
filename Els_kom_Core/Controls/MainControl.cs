using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Els_kom_Core.Controls
{
    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();
        }

        string ElsDir;
        public string showintaskbar_value;
        public Classes.INIObject settingsini;
        public bool x2bool;
        public string showintaskbar_value2;
        public string showintaskbar_tempvalue;
        public string showintaskbar_tempvalue2;

        // events.
        public event EventHandler MinimizeForm;
        public event EventHandler CloseForm;
        public event EventHandler TrayNameChange;
        public event EventHandler<Classes.ShowTaskbarEvent> TaskbarShow;
        public event EventHandler<MouseEventArgs> TrayClick;
        public event EventHandler ConfigForm;
        public event EventHandler ConfigForm2;
        public event EventHandler AboutForm;

        void Command1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms);
            tr2.Start();
            Timer2.Enabled = true;
        }

        void Command1_MouseMove(object sender, MouseEventArgs e)
        {
            Label1.Text = "This uses kompact_new.exe to Pack koms.";
        }

        void Command2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms);
            tr1.Start();
            Timer1.Enabled = true;
        }

        void Command2_MouseMove(object sender, MouseEventArgs e)
        {
            Label1.Text = "This uses komextract_new.exe to Unpack koms.";
        }

        void Command3_Click(object sender, EventArgs e)
        {
            AboutForm?.Invoke(this, new EventArgs());
        }

        void Command3_MouseMove(object sender, MouseEventArgs e)
        {
            Label1.Text = "Shows the About Window. Here you will see things like the version as well as a link to go to the topic to update Els_kom if needed.";
        }

        void Command4_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\Test_Mods.exe"))
            {
                Label1.Text = "";
                MinimizeForm?.Invoke(this, new EventArgs());
                Timer3.Enabled = true;
            }
            else
            {
                MessageBox.Show("Can't find '" + Application.StartupPath + "\\Test_Mods.exe'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void Command4_MouseMove(object sender, MouseEventArgs e)
        {
            Label1.Text = "Test the mods you made.";
        }

        void Command5_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\Launcher.exe"))
            {
                Label1.Text = "";
                MinimizeForm?.Invoke(this, new EventArgs());
                Classes.Process.Shell(Application.StartupPath + "\\Launcher.exe", null, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, Application.StartupPath, false);
            }
            else
            {
                MessageBox.Show("Can't find '" + Application.StartupPath + "\\Launcher.exe'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void Command5_MouseMove(object sender, MouseEventArgs e)
        {
            Label1.Text = "Run the Launcher to Elsword to Update the client for when a server Maintenance happens. (you might have to remake some mods for some files)";
        }

        void Label1_MouseMove(object sender, MouseEventArgs e)
        {
            Label1.Text = "";
        }

        void Timer1_Tick(object sender, EventArgs e)
        {
            if (Classes.KOMManager.is_unpacking)
            {
                Timer6.Enabled = false;
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
                Timer6.Enabled = true;
                Command1.Enabled = true;
                Command2.Enabled = true;
                Command4.Enabled = true;
                Command5.Enabled = true;
                PackToolStripMenuItem.Enabled = true;
                UnpackToolStripMenuItem.Enabled = true;
                TestModsToolStripMenuItem.Enabled = true;
                LauncherToolStripMenuItem.Enabled = true;
                Label2.Text = "";
                TrayNameChange?.Invoke(this, new EventArgs());
            }
        }

        void Timer2_Tick(object sender, EventArgs e)
        {
            if (Classes.KOMManager.is_packing)
            {
                Timer6.Enabled = false;
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
                Timer6.Enabled = true;
                Command1.Enabled = true;
                Command2.Enabled = true;
                Command4.Enabled = true;
                Command5.Enabled = true;
                PackToolStripMenuItem.Enabled = true;
                UnpackToolStripMenuItem.Enabled = true;
                TestModsToolStripMenuItem.Enabled = true;
                LauncherToolStripMenuItem.Enabled = true;
                Label2.Text = "";
                TrayNameChange?.Invoke(this, new EventArgs());
            }
        }

        void Timer3_Tick(object sender, EventArgs e)
        {
            Timer6.Enabled = false;
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
            Classes.Process.Shell(Application.StartupPath + "\\Test_Mods.exe", null, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, Application.StartupPath, false);
            Timer4.Interval = 1;
            Timer4.Enabled = true;
        }

        void Timer4_Tick(object sender, EventArgs e)
        {
            x2bool = Classes.Process.IsX2Running();
            if (x2bool)
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
                Timer6.Enabled = true;
                Timer4.Enabled = false;
            }
        }

        /*
			This timer is required for Reading The Settings for the 2 icon Events for when Elsword is running or not.
			What this should do is make it actually work and read in a timely manner using global variables to compare the 2 values if not the same change
			the used global variable and set it accordingly.
		*/
        void Timer5_Tick(object sender, EventArgs e)
        {
            showintaskbar_tempvalue = settingsini.Read("Settings.ini", "IconWhileElsNotRunning");
            showintaskbar_tempvalue2 = settingsini.Read("Settings.ini", "IconWhileElsRunning");
            x2bool = Classes.Process.IsX2Running();
            if (!x2bool)
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

        void Timer6_Tick(object sender, EventArgs e)
        {
            bool checkiflauncherstubexists = false;
            bool checkiftestmodsstubexists = false;

            if (!checkiflauncherstubexists)
            {
                checkiflauncherstubexists = true;
            }
            if (!checkiftestmodsstubexists)
            {
                checkiftestmodsstubexists = true;
            }
            if (checkiflauncherstubexists)
            {
                if (System.IO.File.Exists(Application.StartupPath + "\\Launcher.exe"))
                {
                    LauncherToolStripMenuItem.Enabled = true;
                    Command5.Enabled = true;
                    checkiflauncherstubexists = false;
                }
                else
                {
                    LauncherToolStripMenuItem.Enabled = false;
                    Command5.Enabled = false;
                    checkiflauncherstubexists = false;
                }
            }
            if (checkiftestmodsstubexists)
            {
                if (System.IO.File.Exists(Application.StartupPath + "\\Test_Mods.exe"))
                {
                    TestModsToolStripMenuItem.Enabled = true;
                    Command4.Enabled = true;
                    checkiftestmodsstubexists = false;
                }
                else
                {
                    TestModsToolStripMenuItem.Enabled = false;
                    Command4.Enabled = false;
                    checkiftestmodsstubexists = false;
                }
            }
        }

        void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            Timer2.Enabled = false;
            Timer6.Enabled = false;
            timer5.Enabled = false;
            CloseForm?.Invoke(this, new EventArgs());
        }

        void LauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\Launcher.exe"))
            {
                MinimizeForm?.Invoke(this, new EventArgs());
                Classes.Process.Shell(Application.StartupPath + "\\Launcher.exe", null, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, Application.StartupPath, false);
            }
            else
            {
                MessageBox.Show("Can't find '" + Application.StartupPath + "\\Launcher.exe'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void UnpackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread tr1 = new System.Threading.Thread(Classes.KOMManager.UnpackKoms);
            tr1.Start();
            Timer1.Enabled = true;
        }

        void TestModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\Test_Mods.exe"))
            {
                MinimizeForm?.Invoke(this, new EventArgs());
                Timer3.Enabled = true;
            }
            else
            {
                MessageBox.Show("Can't find '" + Application.StartupPath + "\\Test_Mods.exe'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void PackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread tr2 = new System.Threading.Thread(Classes.KOMManager.PackKoms);
            tr2.Start();
            Timer2.Enabled = true;
        }

        void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm?.Invoke(this, new EventArgs());
        }

        void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            TrayClick?.Invoke(this, e);
        }

        private void MainControl_MouseMove(object sender, MouseEventArgs e)
        {
            Label1.Text = "";
        }

        public void LoadControl()
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\Settings.ini"))
            {
                settingsini = new Classes.INIObject(Application.StartupPath + "\\Settings.ini");
                ElsDir = settingsini.Read("Settings.ini", "ElsDir");
                if (ElsDir.Length > 0)
                {
                    //The Setting actually exists and is not a empty String so we do not need to open the dialog again.
                }
                else
                {
                    ConfigForm2?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                ConfigForm2?.Invoke(this, new EventArgs());
            }
            timer5.Enabled = true;
        }
    }
}
