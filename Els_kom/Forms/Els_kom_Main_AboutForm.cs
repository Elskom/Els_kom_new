// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    internal partial class AboutForm : System.Windows.Forms.Form
    {
        internal static string Label1 = "0";
        internal AboutForm() => InitializeComponent();
        private void AboutForm_Load(object sender, System.EventArgs e) => Label1 = "1";
        private void AboutForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) => Label1 = "0";
    }
}
