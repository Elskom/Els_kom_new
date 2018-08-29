// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System;
    using System.Windows.Forms;

    internal partial class PluginsForm : Form
    {
        internal PluginsForm() => this.InitializeComponent();

        private void PluginsForm_Load(object sender, EventArgs e) => this.PluginsControl1.InitControl();
    }
}
