using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Els_kom_Core.Controls
{
    public partial class AboutControl : UserControl
    {
        public AboutControl()
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

        public event EventHandler CloseForm;

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

        void Picture1_MouseMove(object sender, MouseEventArgs e)
        {
            Picture1.Visible = false;
            Picture2.Visible = true;
        }

        void Picture2_Click(object sender, EventArgs e)
        {
            Picture1.Visible = true;
            Picture2.Visible = false;
            OpenBrowser("www.elsword.to/forum/index.php?/topic/51000-updated-els-kom-v1494-working-as-of-8-10-16/");
        }

        void lblDisclaimer_MouseMove(object sender, MouseEventArgs e)
        {
            Picture1.Visible = true;
            Picture2.Visible = false;
        }

        void cmdOK_Click(object sender, EventArgs e)
        {
            CloseForm?.Invoke(this, new EventArgs());
        }

        private void AboutControl_Load(object sender, EventArgs e)
        {
            Picture1.Image = Properties.Resources.bmp100;
            Picture2.Image = Properties.Resources.bmp101;
        }

        private void AboutControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Gray, 0, 151, this.Width, 151);
            e.Graphics.DrawLine(Pens.White, 0, 152, this.Width, 152);
        }

        private void AboutControl_MouseMove(object sender, MouseEventArgs e)
        {
            Picture1.Visible = true;
            Picture2.Visible = false;
        }
    }
}
