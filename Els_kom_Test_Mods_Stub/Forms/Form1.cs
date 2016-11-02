using System.Windows.Forms;

namespace Els_kom_Test_Mods_Stub
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		private string ElsDir;
		private void Form1_Load(object sender, System.EventArgs e)
		{
			if (System.IO.File.Exists(Application.StartupPath + "\\Settings.ini"))
			{
				Els_kom_Core.Classes.INIObject settingsini = new Els_kom_Core.Classes.INIObject(Application.StartupPath + "\\Settings.ini");
				ElsDir = settingsini.Read("Settings.ini", "ElsDir");
				if (ElsDir.Length > 0)
				{
					if (System.IO.File.Exists(ElsDir + "\\data\\x2.exe"))
					{
						Els_kom_Core.Classes.Process.Shell(ElsDir + "\\data\\x2.exe", "pxk19slammsu286nfha02kpqnf729ck", false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir + "\\data\\", true);
						this.Close();
					}
					else
					{
						MessageBox.Show("Can't find '" + ElsDir + "\\data\\x2.exe'. Make sure the File Exists and try to Test your mods Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
						this.Close();
					}
				}
				else
				{
					MessageBox.Show("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to Test your mods Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Close();
				}
			}
			else
			{
				MessageBox.Show("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to Test your mods Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}
	}
}
