namespace Els_kom_Core.Controls
{
    /// <summary>
    /// AboutControl control for Els_kom's About form.
    /// </summary>
    public partial class AboutControl : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// AboutControl constructor.
        /// </summary>
        public AboutControl()
        {
            InitializeComponent();
        }

        private enum ShowCommands
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

        /// <summary>
        /// Event that the control fires that Closes the Form it is on.
        /// </summary>
        public event System.EventHandler CloseForm;

        [System.Runtime.InteropServices.DllImport("shell32.dll")]
        static extern System.IntPtr ShellExecute(System.IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);

        private bool OpenBrowser(string URL)
        {
            int res;
            if (URL.IndexOf("http://") == -1)
            {
                URL = "http://" + URL;
            }
            res = (int)ShellExecute((System.IntPtr)0, "open", URL, null, null, ShowCommands.SW_NORMAL);
            return (res > 32);
        }

        void Picture1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Picture1.Visible = false;
            Picture2.Visible = true;
        }

        void Picture2_Click(object sender, System.EventArgs e)
        {
            Picture1.Visible = true;
            Picture2.Visible = false;
            OpenBrowser("www.elsword.to/forum/index.php?/topic/51000-updated-els-kom-v1494-working-as-of-8-10-16/");
        }

        void lblDisclaimer_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Picture1.Visible = true;
            Picture2.Visible = false;
        }

        void cmdOK_Click(object sender, System.EventArgs e)
        {
            CloseForm?.Invoke(this, new System.EventArgs());
        }

        private void AboutControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(System.Drawing.Pens.Gray, 0, 151, this.Width, 151);
            e.Graphics.DrawLine(System.Drawing.Pens.White, 0, 152, this.Width, 152);
        }

        private void AboutControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Picture1.Visible = true;
            Picture2.Visible = false;
        }
    }
}
