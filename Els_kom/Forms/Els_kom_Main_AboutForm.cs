// Copyright (c) 2014-2019, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    internal partial class AboutForm : Form
    {
        internal AboutForm() => this.InitializeComponent();

        internal static string Label1 { get; private set; } = "0";

        private void AboutForm_Load(object sender, EventArgs e) => Label1 = "1";

        private void CmdOK_Click(object sender, EventArgs e) => this.Close();

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e) => Label1 = "0";

        private void LinkLabel1_MouseEnter(object sender, EventArgs e) => this.linkLabel1.LinkColor = Color.Yellow;

        private void LinkLabel1_MouseLeave(object sender, EventArgs e) => this.linkLabel1.LinkColor = Color.Blue;

        private void LinkLabel1_Click(object sender, EventArgs e)
        {
            var sInfo = new ProcessStartInfo("https://www.elsword.to/forum/index.php?/topic/51000-updated-els-kom-v1494-working-as-of-8-10-16/");
            using (var proc = Process.Start(sInfo))
            {
                // wait for exit.
                proc?.WaitForExit();
            }
        }

        private void AboutForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Gray, 0, 151, this.Width, 151);
            e.Graphics.DrawLine(Pens.White, 0, 152, this.Width, 152);
        }
    }
}
