using System.Windows.Forms;

/*
	This form Integrates a Settings Interface to Els_kom that is also read into the Stubs.

	Note: A Few settings still needs implimented into the main form of Els_kom itself and will be fully functional in v1.4.9.6 Release.

	Also a few people might be Wondering "Why did you change from VB.Net to C#?" The answer is the end lines of Code. I get so used to the ";"'s in C++ that I would forget VB's syntax.
		In that case C# is the solution for that.
*/

namespace Els_kom_Core.Forms.Els_kom_Main
{
	public partial class SettingsForm : Form
	{
		public SettingsForm()
		{
			InitializeComponent();
		}

		string curvalue;
		string curvalue2;
		string curvalue3;

		void Button1_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog1.ShowDialog();
			Timer1.Enabled = true;
		}

		void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Classes.INIObject settingsini = new Classes.INIObject(Application.StartupPath + "\\Settings.ini");
			if (!ReferenceEquals(TextBox1.Text, curvalue3))
			{
				if (TextBox1.Text.Length > 0)
				{
					settingsini.Write("Settings.ini", "ElsDir", TextBox1.Text);
				}
				else
				{
					MessageBox.Show("You Should Set a Working Elsword Directory.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			if (!ReferenceEquals(Label4.Text, curvalue))
			{
				if (Label5.Text == "...")
				{
					settingsini.Write("Settings.ini", "IconWhileElsNotRunning", "2");
				}
				else
				{
					settingsini.Write("Settings.ini", "IconWhileElsNotRunning", Label4.Text);
				}
			}
			if (!ReferenceEquals(Label5.Text, curvalue2))
			{
				if (Label5.Text == "...")
				{
					settingsini.Write("Settings.ini", "IconWhileElsRunning", "1");
				}
				else
				{
					settingsini.Write("Settings.ini", "IconWhileElsRunning", Label5.Text);
				}
			}
		}

		void SettingsForm_Load(object sender, System.EventArgs e)
		{
			this.Icon = Properties.Resources.els_kom_icon;
			if (System.IO.File.Exists(Application.StartupPath + "\\Settings.ini"))
			{
				Classes.INIObject settingsini = new Els_kom_Core.Classes.INIObject(Application.StartupPath + "\\Settings.ini");
				curvalue3 = settingsini.Read("Settings.ini", "ElsDir");
				curvalue = settingsini.Read("Settings.ini", "IconWhileElsNotRunning");
				curvalue2 = settingsini.Read("Settings.ini", "IconWhileElsRunning");
				TextBox1.Text = curvalue3;
				Label4.Text = curvalue;
				Label5.Text = curvalue2;
				if (Label4.Text == "0")
				{
					RadioButton1.Checked = true;
				}
				else if (Label4.Text == "1")
				{
					RadioButton2.Checked = true;
				}
				else if (Label4.Text == "2")
				{
					RadioButton3.Checked = true;
				}
				else if (Label4.Text == "...")
				{
					RadioButton3.Checked = true;
				}
				if (Label5.Text == "0")
				{
					RadioButton4.Checked = true;
				}
				else if (Label5.Text == "1")
				{
					RadioButton5.Checked = true;
				}
				else if (Label5.Text == "2")
				{
					RadioButton6.Checked = true;
				}
				else if (Label5.Text == "...")
				{
					RadioButton5.Checked = true;
				}
				TreeView1.SelectedNode = TreeView1.Nodes[0];
			}
		}

		void Button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		void Timer1_Tick(object sender, System.EventArgs e)
		{
			if (FolderBrowserDialog1.SelectedPath.Length > 0)
			{
				TextBox1.Text = FolderBrowserDialog1.SelectedPath;
				Timer1.Enabled = false;
			}
		}

		void RadioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton1.Checked)
			{
				Label4.Text = "0";
				if (RadioButton2.Checked)
				{
					RadioButton2.Checked = false;
				}
				if (RadioButton3.Checked)
				{
					RadioButton3.Checked = false;
				}
			}
		}

		void RadioButton2_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton2.Checked)
			{
				Label4.Text = "1";
				if (RadioButton1.Checked)
				{
					RadioButton1.Checked = false;
				}
				if (RadioButton3.Checked)
				{
					RadioButton3.Checked = false;
				}
			}
		}

		void RadioButton3_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton3.Checked)
			{
				Label4.Text = "2";
				if (RadioButton1.Checked)
				{
					RadioButton1.Checked = false;
				}
				if (RadioButton2.Checked)
				{
					RadioButton2.Checked = false;
				}
			}
		}

		void RadioButton4_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton4.Checked)
			{
				Label5.Text = "0";
				if (RadioButton5.Checked)
				{
					RadioButton5.Checked = false;
				}
				if (RadioButton6.Checked)
				{
					RadioButton6.Checked = false;
				}
			}
		}

		void RadioButton5_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton5.Checked)
			{
				Label5.Text = "1";
				if (RadioButton4.Checked)
				{
					RadioButton4.Checked = false;
				}
				if (RadioButton6.Checked)
				{
					RadioButton6.Checked = false;
				}
			}
		}

		void RadioButton6_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton6.Checked)
			{
				Label5.Text = "2";
				if (RadioButton4.Checked)
				{
					RadioButton4.Checked = false;
				}
				if (RadioButton5.Checked)
				{
					RadioButton5.Checked = false;
				}
			}
		}

		void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (TreeView1.SelectedNode.Index == 0)
			{
				Panel1.Visible = true;
				Panel2.Visible = false;
				TreeView1.Focus();
			}
			else
			{
				Panel1.Visible = false;
				Panel2.Visible = true;
				TreeView1.Focus();
			}
		}
	}
}
