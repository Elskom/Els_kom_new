using System.Windows.Forms;

/*
	This form Integrates a Settings Interface to Els_kom that is also read into the Stubs.

	Note: A Few settings still needs implimented into the main form of Els_kom itself and will be fully functional in v1.4.9.6 Release.

	Also a few people might be Wondering "Why did you change from VB.Net to C#?" The answer is the end lines of Code. I get so used to the ";"'s in C++ that I would forget VB's syntax.
		In that case C# is the solution for that.
*/

namespace Els_kom.Forms
{
	public partial class SettingsForm : Form
	{
		public SettingsForm()
		{
			InitializeComponent();
		}

		void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
            this.settingsControl1.SaveSettings();
		}

		void SettingsForm_Load(object sender, System.EventArgs e)
		{
			this.Icon = Els_kom_Core.Properties.Resources.els_kom_icon;
		}

		void Button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
