// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    internal partial class PluginsForm : System.Windows.Forms.Form
    {
        internal PluginsForm() => this.InitializeComponent();

        private void PluginsForm_Load(object sender, System.EventArgs e) => this.PluginsControl1.InitControl();
    }
}
