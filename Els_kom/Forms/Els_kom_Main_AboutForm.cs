// Copyright (c) 2014-2019, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System;
    using System.Windows.Forms;

    internal partial class AboutForm : Form
    {
        internal AboutForm() => this.InitializeComponent();

        internal static string Label1 { get; private set; } = "0";

        private void AboutForm_Load(object sender, EventArgs e) => Label1 = "1";

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e) => Label1 = "0";
    }
}
