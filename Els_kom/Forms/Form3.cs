using System.Windows.Forms;

/*
	This form Integrates a Settings Interface to Els_kom that is also read into the Stubs.

	Note: A Few settings still needs implimented into the main form of Els_kom itself and will be fully functional in v1.4.9.6 Release.
	
	Also a few people might be Wondering "Why did you change from VB.Net to C#?" The answer is the end lines of Code. I get so used to the ";"'s in C++ that I would forget VB's syntax.
		In that case C# is the solution for that.
*/

namespace Els_kom
{
	public partial class Form3 : Form
	{
		public Form3()
		{
			InitializeComponent();
		}

		string curvalue;
		string curvalue2;
		string curvalue3;

		private void Button1_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog1.ShowDialog();
			Timer1.Enabled = true;
		}

		private void Form3_FormClosing(object sender, FormClosingEventArgs e)
		{
			Els_kom_Core.Classes.INIObject settingsini = new Els_kom_Core.Classes.INIObject(Application.StartupPath + "\\Settings.ini");
			if (!object.ReferenceEquals(TextBox1.Text, curvalue3))
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
			if (!object.ReferenceEquals(Label4.Text, curvalue))
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
			if (!object.ReferenceEquals(Label5.Text, curvalue2))
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

		private void Form3_Load(object sender, System.EventArgs e)
		{
			this.Icon = Els_kom_Core.Properties.Resources.els_kom_icon;
			if (System.IO.File.Exists(Application.StartupPath + "\\Settings.ini"))
			{
				Els_kom_Core.Classes.INIObject settingsini = new Els_kom_Core.Classes.INIObject(Application.StartupPath + "\\Settings.ini");
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

		private void Button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void Timer1_Tick(object sender, System.EventArgs e)
		{
			if (FolderBrowserDialog1.SelectedPath.Length > 0)
			{
				TextBox1.Text = FolderBrowserDialog1.SelectedPath;
				Timer1.Enabled = false;
			}
		}

		private void RadioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton1.Checked == true)
			{
				Label4.Text = "0";
				if (RadioButton2.Checked == true)
				{
					RadioButton2.Checked = false;
				}
				if (RadioButton3.Checked == true)
				{
					RadioButton3.Checked = false;
				}
			}
		}

		private void RadioButton2_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton2.Checked == true)
			{
				Label4.Text = "1";
				if (RadioButton1.Checked == true)
				{
					RadioButton1.Checked = false;
				}
				if (RadioButton3.Checked == true)
				{
					RadioButton3.Checked = false;
				}
			}
		}

		private void RadioButton3_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton3.Checked == true)
			{
				Label4.Text = "2";
				if (RadioButton1.Checked == true)
				{
					RadioButton1.Checked = false;
				}
				if (RadioButton2.Checked == true)
				{
					RadioButton2.Checked = false;
				}
			}
		}

		private void RadioButton4_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton4.Checked == true)
			{
				Label5.Text = "0";
				if (RadioButton5.Checked == true)
				{
					RadioButton5.Checked = false;
				}
				if (RadioButton6.Checked == true)
				{
					RadioButton6.Checked = false;
				}
			}
		}

		private void RadioButton5_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton5.Checked == true)
			{
				Label5.Text = "1";
				if (RadioButton4.Checked == true)
				{
					RadioButton4.Checked = false;
				}
				if (RadioButton6.Checked == true)
				{
					RadioButton6.Checked = false;
				}
			}
		}

		private void RadioButton6_CheckedChanged(object sender, System.EventArgs e)
		{
			if (RadioButton6.Checked == true)
			{
				Label5.Text = "2";
				if (RadioButton4.Checked == true)
				{
					RadioButton4.Checked = false;
				}
				if (RadioButton5.Checked == true)
				{
					RadioButton5.Checked = false;
				}
			}
		}

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
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
