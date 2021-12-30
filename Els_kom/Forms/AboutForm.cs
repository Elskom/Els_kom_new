// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System.Diagnostics;
    using Els_kom.Controls;

    internal partial class AboutForm : /*Form*/ThemedForm
    {
        internal AboutForm()
            => this.InitializeComponent();

        internal static int Label1 { get; private set; }

        private void AboutForm_Load(object sender, EventArgs e)
            => Label1 = 1;

        private void CmdOK_Click(object sender, EventArgs e)
            => this.Close();

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
            => Label1 = 0;

        private void LinkLabel1_MouseEnter(object sender, EventArgs e)
            => this.linkLabel1.LinkColor = Color.Yellow;

        private void LinkLabel1_MouseLeave(object sender, EventArgs e)
            => this.linkLabel1.LinkColor = Color.Blue;

        private void LinkLabel1_Click(object sender, EventArgs e)
        {
            var sInfo = new ProcessStartInfo("https://www.elsword.to/forum/index.php?/topic/51000-updated-els-kom-v1494-working-as-of-8-10-16/");
            using var proc = Process.Start(sInfo);

            // wait for exit.
            proc?.WaitForExit();
        }
    }
}
