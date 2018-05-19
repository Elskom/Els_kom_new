// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    /// <summary>
    /// SettingsControl control for Els_kom's Settings form.
    /// </summary>
    public partial class SettingsControl : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// SettingsControl constructor.
        /// </summary>
        public SettingsControl()
        {
            InitializeComponent();
        }

        private string curvalue;
        private string curvalue2;
        private string curvalue3;
        private string Label4 = "...";
        private string Label5 = "...";

        /// <summary>
        /// Parrent Form that the control is on.
        /// </summary>
        public new System.Windows.Forms.Form ParentForm;

        /// <summary>
        /// Saves the Settings that changed in this Control's buffers.
        /// </summary>
        public void SaveSettings()
        {
            Classes.SettingsFile.Settingsxml.ReopenFile();
            if (!string.Equals(TextBox1.Text, curvalue3))
            {
                if (TextBox1.Text.Length > 0)
                {
                    Classes.SettingsFile.Settingsxml.Write("ElsDir", TextBox1.Text);
                }
                else
                {
                    Classes.MessageManager.ShowWarning("You Should Set a Working Elsword Directory.", "Warning!");
                }
            }
            if (!string.Equals(Label4, curvalue))
            {
                if (Label5 == "...")
                {
                    Classes.SettingsFile.Settingsxml.Write("IconWhileElsNotRunning", "2");
                }
                else
                {
                    Classes.SettingsFile.Settingsxml.Write("IconWhileElsNotRunning", Label4);
                }
            }
            if (!string.Equals(Label5, curvalue2))
            {
                if (Label5 == "...")
                {
                    Classes.SettingsFile.Settingsxml.Write("IconWhileElsRunning", "1");
                }
                else
                {
                    Classes.SettingsFile.Settingsxml.Write("IconWhileElsRunning", Label5);
                }
            }
            Classes.SettingsFile.Settingsxml.Save();
        }

        void Button1_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Select the Folder that Your Elsword Install is in (Must be the one that either elsword.exe or voidels.exe is in).",
                RootFolder = System.Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = false
            };
            System.Windows.Forms.DialogResult res = FolderBrowserDialog1.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                if (FolderBrowserDialog1.SelectedPath.Length > 0)
                {
                    TextBox1.Text = FolderBrowserDialog1.SelectedPath;
                }
            }
        }

        void Button2_Click(object sender, System.EventArgs e)
        {
            ParentForm.Close();
        }

        void RadioButton1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (RadioButton1.Checked)
            {
                Label4 = "0";
                if (RadioButton2.Checked)
                {
                    RadioButton2.Checked = false;
                }
                if (RadioButton3.Checked)
                {
                    RadioButton3.Checked = false;
                }
            }
        }

        void RadioButton2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (RadioButton2.Checked)
            {
                Label4 = "1";
                if (RadioButton1.Checked)
                {
                    RadioButton1.Checked = false;
                }
                if (RadioButton3.Checked)
                {
                    RadioButton3.Checked = false;
                }
            }
        }

        void RadioButton3_CheckedChanged(object sender, System.EventArgs e)
        {
            if (RadioButton3.Checked)
            {
                Label4 = "2";
                if (RadioButton1.Checked)
                {
                    RadioButton1.Checked = false;
                }
                if (RadioButton2.Checked)
                {
                    RadioButton2.Checked = false;
                }
            }
        }

        void RadioButton4_CheckedChanged(object sender, System.EventArgs e)
        {
            if (RadioButton4.Checked)
            {
                Label5 = "0";
                if (RadioButton5.Checked)
                {
                    RadioButton5.Checked = false;
                }
                if (RadioButton6.Checked)
                {
                    RadioButton6.Checked = false;
                }
            }
        }

        void RadioButton5_CheckedChanged(object sender, System.EventArgs e)
        {
            if (RadioButton5.Checked)
            {
                Label5 = "1";
                if (RadioButton4.Checked)
                {
                    RadioButton4.Checked = false;
                }
                if (RadioButton6.Checked)
                {
                    RadioButton6.Checked = false;
                }
            }
        }

        void RadioButton6_CheckedChanged(object sender, System.EventArgs e)
        {
            if (RadioButton6.Checked)
            {
                Label5 = "2";
                if (RadioButton4.Checked)
                {
                    RadioButton4.Checked = false;
                }
                if (RadioButton5.Checked)
                {
                    RadioButton5.Checked = false;
                }
            }
        }

        void TreeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (TreeView1.SelectedNode.Index == 0)
            {
                Panel3.Visible = true;
                TreeView1.Focus();
            }
            // TODO: Make sure to check if an plugin panel's node is selected or not.
        }

        private void SetRadios()
        {
            if (Label4 == "0")
            {
                RadioButton1.Checked = true;
            }
            else if (Label4 == "1")
            {
                RadioButton2.Checked = true;
            }
            else if (Label4 == "2")
            {
                RadioButton3.Checked = true;
            }
            else if (Label4 == "...")
            {
                RadioButton3.Checked = true;
            }
            if (Label5 == "0")
            {
                RadioButton4.Checked = true;
            }
            else if (Label5 == "1")
            {
                RadioButton5.Checked = true;
            }
            else if (Label5 == "2")
            {
                RadioButton6.Checked = true;
            }
            else if (Label5 == "...")
            {
                RadioButton5.Checked = true;
            }

        }

        /// <summary>
        /// Initializes the SettingsControl's constants.
        /// </summary>
        public void InitControl()
        {
            if (System.IO.File.Exists(Classes.SettingsFile.Path))
            {
                Classes.SettingsFile.Settingsxml.ReopenFile();
                curvalue3 = Classes.SettingsFile.Settingsxml.Read("ElsDir");
                curvalue = Classes.SettingsFile.Settingsxml.Read("IconWhileElsNotRunning");
                curvalue2 = Classes.SettingsFile.Settingsxml.Read("IconWhileElsRunning");
                TextBox1.Text = curvalue3;
                // set these to the values read above only if they are not empty.
                Label4 = string.IsNullOrEmpty(curvalue) ? Label4 : curvalue;
                Label5 = string.IsNullOrEmpty(curvalue2) ? Label5 : curvalue2;
            }
            SetRadios();
            TreeView1.SelectedNode = TreeView1.Nodes[0];
        }

        private void SettingsControl_Load(object sender, System.EventArgs e)
        {
            SetRadios();
            TreeView1.SelectedNode = TreeView1.Nodes[0];
        }
    }
}
