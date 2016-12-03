using System.Windows.Forms;

namespace Els_kom_Core.Forms.Launcher_Stub
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		public string ElsDir;

		void Form1_Load(object sender, System.EventArgs e)
		{
			if (System.IO.File.Exists(Application.StartupPath + "\\Settings.ini"))
			{
				Classes.INIObject settingsini = new Classes.INIObject(Application.StartupPath + "\\Settings.ini");
				ElsDir = settingsini.Read("Settings.ini", "ElsDir");
				if (ElsDir.Length > 0)
				{
					if (System.IO.File.Exists(ElsDir + "\\voidels.exe"))
					{
						Classes.Process.Shell(ElsDir + "\\voidels.exe", "", false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir, true);
						this.Close();
					}
					else
					{
						if (System.IO.File.Exists(ElsDir + "\\elsword.exe"))
						{
							Classes.Process.Shell(ElsDir + "\\elsword.exe", "", false, false, false, System.Diagnostics.ProcessWindowStyle.Normal, ElsDir, true);
							this.Close();
						}
						else
						{
							MessageBox.Show("Can't find '" + ElsDir + "\\elsword.exe'. Make sure the File Exists and try to update Elsword Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
							this.Close();
						}
					}
				}
				else
				{
					MessageBox.Show("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to update Elsword Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Close();
				}
			}
			else
			{
				MessageBox.Show("The Elsword Directory Setting is not set. Make sure to Set your Elsword Directory Setting and try to update Elsword Again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}
	}
}
