using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Els_kom.Forms
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
		}

		void cmdOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		void AboutForm_Load(object sender, EventArgs e)
		{
			Label1.Text = "1";
			this.Icon = Els_kom_Core.Properties.Resources.els_kom_icon;
		}

		void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Label1.Text = "0";
		}
	}
}
