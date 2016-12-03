using System;
using System.Windows.Forms;

namespace Els_kom_Core.Forms.Els_kom_main
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			this.SizeChanged += new EventHandler(Form1_SizeEventHandler);
		}

		string ElsDir;

		public bool bypasswindowbug;
		public Form aboutfrm;
		public Form settingsfrm;
		public string showintaskbar_value;
		public Classes.INIObject settingsini;
		public bool x2bool;
		public string showintaskbar_value2;
		public string showintaskbar_tempvalue;
		public string showintaskbar_tempvalue2;

		void Command1_Click(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(Application.StartupPath + "\\pack.bat"))
			{
				System.IO.File.Create(Application.StartupPath + "\\packing.pack").Close();
				Classes.Process.Shell(Application.StartupPath + "\\pack.bat", null, false, false, true, System.Diagnostics.ProcessWindowStyle.Hidden, Application.StartupPath, false);
				Timer2.Enabled = true;
			}
			else
			{
				MessageBox.Show("Can't find 'pack.bat'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		void Command1_MouseMove(object sender, MouseEventArgs e)
		{
			Label1.Text = "This uses kompact_new.exe to Pack koms.";
		}

		void Command2_Click(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(Application.StartupPath + "\\unpack.bat"))
			{
				System.IO.File.Create(Application.StartupPath + "\\unpacking.unpack").Close();
				Classes.Process.Shell(Application.StartupPath + "\\unpack.bat", null, false, false, true, System.Diagnostics.ProcessWindowStyle.Hidden, Application.StartupPath, false);
				Timer1.Enabled = true;
			}
			else
			{
				MessageBox.Show("Can't find 'unpack.bat'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		void Command2_MouseMove(object sender, MouseEventArgs e)
		{
			Label1.Text = "This uses komextract_new.exe to Unpack koms.";
		}

		void Command3_Click(object sender, EventArgs e)
		{
			aboutfrm = new Form2();
			aboutfrm.ShowDialog();
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
				this.WindowState = FormWindowState.Minimized;
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
				this.WindowState = FormWindowState.Minimized;
				Els_kom_Core.Classes.Process.Shell(Application.StartupPath + "\\Launcher.exe", null, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, Application.StartupPath, false);
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

		void Form1_Load(object sender, EventArgs e)
		{
			NotifyIcon1.Visible = false;
			this.ShowInTaskbar = false;
			bool previnstance;
			this.Icon = Properties.Resources.els_kom_icon;
			NotifyIcon1.Icon = this.Icon;
			NotifyIcon1.Text = this.Text;
			previnstance = Classes.Process.IsElsKomRunning();
			if (previnstance == true)
			{
				MessageBox.Show("Sorry, Only 1 Instance is allowed at a time.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.Close();
			}
			else
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
						MessageBox.Show("Welcome to Els_kom." + Environment.NewLine + "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + Environment.NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!", MessageBoxButtons.OK, MessageBoxIcon.Information);
						settingsfrm = new Form3();
						settingsfrm.ShowDialog();
					}
				}
				else
				{
					MessageBox.Show("Welcome to Els_kom." + Environment.NewLine + "Now your fist step is to Configure Els_kom to the path that you have installed Elsword to and then you can Use the test Mods and the executing of the Launcher features. It will only take less than 1~3 minutes tops." + Environment.NewLine + "Also if you encounter any bugs or other things take a look at the Issue Tracker.", "Welcome!", MessageBoxButtons.OK, MessageBoxIcon.Information);
					settingsfrm = new Form3();
					settingsfrm.ShowDialog();
				}
				timer5.Enabled = true;
			}
		}

		void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			Label1.Text = "";
		}

		void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool Cancel = e.Cancel;
			// CloseReason UnloadMode = e.CloseReason; <-- Removed because not used.
			e.Cancel = Cancel;
		}

		void Timer1_Tick(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(Application.StartupPath + " \\ unpacking.unpack"))
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
				NotifyIcon1.Text = this.Text;
			}
		}

		void Timer2_Tick(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(Application.StartupPath + "\\packing.pack"))
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
				NotifyIcon1.Text = this.Text;
			}
		}

		void Timer6_Tick(object sender, EventArgs e)
		{
			bool checkiflauncherstubexists = false;
			bool checkiftestmodsstubexists = false;

			if (this.WindowState == FormWindowState.Normal)
			{
				this.Height = 184;
				this.Width = 320;
			}
			//Have to disable these specific warnings here because Visual Studio is too stupid to figure out this is needed to avoid constant changing of values that results in a slow UI.
#pragma warning disable S2583
#pragma warning disable S1854
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
#pragma warning restore S1854
#pragma warning restore S2583
		}

		void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (Form2.Label1.Text == "1")
				{
					//I have to Sadly disable left button on the Notify Icon to prevent a bug with Form2 Randomly Unloading or not reshowing.
				}
				else
				{
					if (e.Button == MouseButtons.Left)
					{
						if (!x2bool)
						{
							if (showintaskbar_value == "0")
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
							else if (showintaskbar_value == "1")
							{
								if (this.WindowState == FormWindowState.Minimized)
								{
									this.WindowState = FormWindowState.Normal;
									this.Show();
								}
								else
								{
									this.Hide();
									bypasswindowbug = true;
									this.WindowState = FormWindowState.Minimized;
								}
							}
							else if (showintaskbar_value == "2")
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
						}
						else
						{
							if (showintaskbar_value2 == "0")
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
							else if (showintaskbar_value2 == "1")
							{
								if (this.WindowState == FormWindowState.Minimized)
								{
									this.WindowState = FormWindowState.Normal;
									this.Show();
								}
								else
								{
									this.Hide();
									bypasswindowbug = true;
									this.WindowState = FormWindowState.Minimized;
								}
							}
							else if (showintaskbar_value2 == "2")
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
						}
					}
				}
			}
			catch (System.NullReferenceException)  // Form2 was never opened so lets bypass this NullReferenceException.
			{
				if (e.Button == MouseButtons.Left)
				{
					if (!x2bool)
					{
						if (showintaskbar_value == "0")
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
						else if (showintaskbar_value == "1")
						{
							if (this.WindowState == FormWindowState.Minimized)
							{
								this.WindowState = FormWindowState.Normal;
								this.Show();
							}
							else
							{
								this.Hide();
								bypasswindowbug = true;
								this.WindowState = FormWindowState.Minimized;
							}
						}
						else if (showintaskbar_value == "2")
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
					}
					else
					{
						if (showintaskbar_value2 == "0")
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
						else if (showintaskbar_value2 == "1")
						{
							if (this.WindowState == FormWindowState.Minimized)
							{
								this.WindowState = FormWindowState.Normal;
								this.Show();
							}
							else
							{
								this.Hide();
								bypasswindowbug = true;
								this.WindowState = FormWindowState.Minimized;
							}
						}
						else if (showintaskbar_value2 == "2")
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
					}
				}
			}
		}

		void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Timer1.Enabled = false;
			Timer2.Enabled = false;
			Timer6.Enabled = false;
			timer5.Enabled = false;
			try
			{
				aboutfrm.Close();
			}
			catch (System.NullReferenceException)
			{
				//nothing here.
			}
			try
			{
				settingsfrm.Close();
			}
			catch (System.NullReferenceException)
			{
				//nothing here.
			}
			this.Close();
		}

		void LauncherToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Label1.Text = "";
			if (System.IO.File.Exists(Application.StartupPath + "\\Launcher.exe"))
			{
				this.WindowState = FormWindowState.Minimized;
				Classes.Process.Shell(Application.StartupPath + "\\Launcher.exe", null, false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, Application.StartupPath, false);
			}
			else
			{
				MessageBox.Show("Can't find '" + Application.StartupPath + "\\Launcher.exe'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		void UnpackToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(Application.StartupPath + "\\unpack.bat"))
			{
				System.IO.File.Create(Application.StartupPath + "\\unpacking.unpack").Close();
				Classes.Process.Shell(Application.StartupPath + "\\unpack.bat", null, false, false, true, System.Diagnostics.ProcessWindowStyle.Hidden, Application.StartupPath, false);
				Timer1.Enabled = true;
			}
			else
			{
				MessageBox.Show("Can't find 'unpack.bat'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		void TestModsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Label1.Text = "";
			if (System.IO.File.Exists(Application.StartupPath + "\\Test_Mods.exe"))
			{
				this.WindowState = FormWindowState.Minimized;
				Timer3.Enabled = true;
			}
			else
			{
				MessageBox.Show("Can't find '" + Application.StartupPath + "\\Test_Mods.exe'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		void PackToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(Application.StartupPath + "\\pack.bat"))
			{
				System.IO.File.Create(Application.StartupPath + "\\packing.pack").Close();
				Classes.Process.Shell(Application.StartupPath + "\\pack.bat", null, false, false, true, System.Diagnostics.ProcessWindowStyle.Hidden, Application.StartupPath, false);
				Timer2.Enabled = true;
			}
			else
			{
				MessageBox.Show("Can't find 'pack.bat'.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			settingsfrm = new Form3();
			settingsfrm.ShowDialog();
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

		void Form1_MouseLeave(object sender, EventArgs e)
		{
			Label1.Text = "";
		}

		void Label1_MouseMove(object sender, MouseEventArgs e)
		{
			Label1.Text = "";
		}

		/*
			This timer is required for Reading The Settings for the 2 icon Events for when Elsword is running or not.
			What this should do is make it actually work and read in a timely manner using global variables to compare the 2 values if not the same change
			the used global variable and set it accordingly.
		*/
		void timer5_Tick(object sender, EventArgs e)
		{
			showintaskbar_tempvalue = settingsini.Read("Settings.ini", "IconWhileElsNotRunning");
			showintaskbar_tempvalue2 = settingsini.Read("Settings.ini", "IconWhileElsRunning");
			x2bool = Classes.Process.IsX2Running();
			//STFU Visual Studio you don't know what the fuck you are saying.
#pragma warning disable S1066
#pragma warning disable S3440
			if (!x2bool)
			{
				if (showintaskbar_value != showintaskbar_tempvalue)
				{
					showintaskbar_value = showintaskbar_tempvalue;
				}
				if (showintaskbar_value2 != showintaskbar_tempvalue2)
				{
					showintaskbar_value2 = showintaskbar_tempvalue2;
				}
				if (showintaskbar_value == "0") // Taskbar only!!!
				{
					NotifyIcon1.Visible = false;
					this.ShowInTaskbar = true;
				}
				if (showintaskbar_value == "1") // Tray only!!!
				{
					NotifyIcon1.Visible = true;
					this.ShowInTaskbar = false;
				}
				if (showintaskbar_value == "2") // Both!!!
				{
					NotifyIcon1.Visible = true;
					this.ShowInTaskbar = true;
				}
			}
			else
			{
				if (showintaskbar_value != showintaskbar_tempvalue)
				{
					showintaskbar_value = showintaskbar_tempvalue;
				}
				if (showintaskbar_value2 != showintaskbar_tempvalue2)
				{
					showintaskbar_value2 = showintaskbar_tempvalue2;
				}
				if (showintaskbar_value2 == "0") // Taskbar only!!!
				{
					NotifyIcon1.Visible = false;
					this.ShowInTaskbar = true;
				}
				if (showintaskbar_value2 == "1") // Tray only!!!
				{
					NotifyIcon1.Visible = true;
					this.ShowInTaskbar = false;
				}
				if (showintaskbar_value2 == "2") // Both!!!
				{
					NotifyIcon1.Visible = true;
					this.ShowInTaskbar = true;
				}
			}
#pragma warning restore S3440
#pragma warning restore S1066
		}

		void Form1_SizeEventHandler(object sender, EventArgs e)
		{
			if (!x2bool)
			{
				if (!bypasswindowbug)
				{
					if (showintaskbar_value == "1")
					{
						if (this.WindowState == FormWindowState.Minimized)
						{
							this.Hide();
						}
						if (this.WindowState == FormWindowState.Normal)
						{
							this.Show();
						}
					}
				}
				else
				{
					bypasswindowbug = false;
				}
			}
			else
			{
				if (!bypasswindowbug)
				{
					if (showintaskbar_value2 == "1")
					{
						if (this.WindowState == FormWindowState.Minimized)
						{
							this.Hide();
						}
						if (this.WindowState == FormWindowState.Normal)
						{
							this.Show();
						}
					}
				}
				else
				{
					bypasswindowbug = false;
				}
			}
		}
	}
}
