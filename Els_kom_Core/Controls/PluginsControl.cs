// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    /// <summary>
    /// PluginsControl control for Els_kom's Plugins installer/updater form.
    /// </summary>
    public partial class PluginsControl : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// PluginsControl constructor.
        /// </summary>
        public PluginsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Parrent Form that the control is on.
        /// </summary>
        public new System.Windows.Forms.Form ParentForm;
    }
}
