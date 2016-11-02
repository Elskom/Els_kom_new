using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Els_kom
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		public enum ShowCommands
		{
			SW_HIDE = 0,
			SW_SHOWNORMAL = 1,
			SW_NORMAL = 1,
			SW_SHOWMINIMIZED = 2,
			SW_SHOWMAXIMIZED = 3,
			SW_MAXIMIZE = 3,
			SW_SHOWNOACTIVATE = 4,
			SW_SHOW = 5,
			SW_MINIMIZE = 6,
			SW_SHOWMINNOACTIVE = 7,
			SW_SHOWNA = 8,
			SW_RESTORE = 9,
			SW_SHOWDEFAULT = 10,
			SW_FORCEMINIMIZE = 11,
			SW_MAX = 11
		}

		[DllImport("shell32.dll")]
		static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);

		public bool OpenBrowser(string URL)
		{
			int res;
			if (URL.IndexOf("http://") == -1)
			{
				URL = "http://" + URL;
			}
			res = (int)ShellExecute((System.IntPtr)0, "open", URL, null, null, ShowCommands.SW_NORMAL);
			return (res > 32);
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			Label1.Text = "1";
			this.Icon = Els_kom_Core.Properties.Resources.els_kom_icon;
			picIcon.Image = Els_kom_Core.Properties.Resources.els_kom;
			Picture1.Image = Els_kom_Core.Properties.Resources.bmp100;
			Picture2.Image = Els_kom_Core.Properties.Resources.bmp101;
		}

		private void Form2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Picture1.Visible = true;
			Picture2.Visible = false;
		}

		private void Picture1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Picture1.Visible = false;
			Picture2.Visible = true;
		}

		private void Picture2_Click(object sender, EventArgs e)
		{
			Picture1.Visible = true;
			Picture2.Visible = false;
			OpenBrowser("www.elsword.to/forum/index.php?/topic/51000-updated-els-kom-v1494-working-as-of-8-10-16/");
		}

		private void lblDisclaimer_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Picture1.Visible = true;
			Picture2.Visible = false;
		}

		private void Form2_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			Label1.Text = "0";
		}

		private void Form2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(Pens.Gray, 0, 151, this.Width, 151);
			e.Graphics.DrawLine(Pens.White, 0, 152, this.Width, 152);
		}
	}
}