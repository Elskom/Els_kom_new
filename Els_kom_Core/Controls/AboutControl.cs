// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// AboutControl control for Els_kom's About form.
    /// </summary>
    public partial class AboutControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutControl"/> class.
        /// </summary>
        public AboutControl() => this.InitializeComponent();

        private void CmdOK_Click(object sender, EventArgs e) => this.FindForm()?.Close();

        private void AboutControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Gray, 0, 151, this.Width, 151);
            e.Graphics.DrawLine(Pens.White, 0, 152, this.Width, 152);
        }

        private void LinkLabel1_MouseEnter(object sender, EventArgs e) => this.linkLabel1.LinkColor = Color.Yellow;

        private void LinkLabel1_MouseLeave(object sender, EventArgs e) => this.linkLabel1.LinkColor = Color.Blue;

        private void LinkLabel1_Click(object sender, EventArgs e)
        {
            var sInfo = new ProcessStartInfo("https://www.elsword.to/forum/index.php?/topic/51000-updated-els-kom-v1494-working-as-of-8-10-16/");
            var proc = Process.Start(sInfo);

            // wait for exit.
            proc.WaitForExit();

            // cleanup proc.
            proc.Dispose();
        }
    }
}
