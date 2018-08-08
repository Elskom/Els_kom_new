// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    internal partial class PluginsForm : System.Windows.Forms.Form
    {
        internal PluginsForm() => InitializeComponent();
        private void PluginsForm_Load(object sender, System.EventArgs e) => PluginsControl1.InitControl();
    }
}
