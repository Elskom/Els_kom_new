// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    internal partial class AboutForm : System.Windows.Forms.Form
    {
        private static string label1 = string.Empty;

        internal AboutForm() => this.InitializeComponent();

        internal static string Label1
        {
            get
            {
                if (label1 == string.Empty)
                {
                    label1 = "0";
                }

                return label1;
            }

            private set => label1 = value;
        }

        private void AboutForm_Load(object sender, System.EventArgs e) => Label1 = "1";

        private void AboutForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) => Label1 = "0";
    }
}
