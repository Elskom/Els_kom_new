// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

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

        /// <summary>
        /// Parrent Form that the control is on.
        /// </summary>
        public new System.Windows.Forms.Form ParentForm;

        void CmdOK_Click(object sender, System.EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void AboutControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(System.Drawing.Pens.Gray, 0, 151, this.Width, 151);
            e.Graphics.DrawLine(System.Drawing.Pens.White, 0, 152, this.Width, 152);
        }

        private void LinkLabel1_MouseEnter(object sender, System.EventArgs e)
        {
            linkLabel1.LinkColor = System.Drawing.Color.Yellow;
        }

        private void LinkLabel1_MouseLeave(object sender, System.EventArgs e)
        {
            linkLabel1.LinkColor = System.Drawing.Color.Blue;
        }

        private void LinkLabel1_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("https://www.elsword.to/forum/index.php?/topic/51000-updated-els-kom-v1494-working-as-of-8-10-16/");
            System.Diagnostics.Process.Start(sInfo);
        }
    }
}
