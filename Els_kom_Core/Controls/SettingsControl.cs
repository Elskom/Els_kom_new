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
        public SettingsControl() => InitializeComponent();

        private string curvalue;
        private string curvalue2;
        private string curvalue3;
        private int curvalue4;
        private int curvalue5;
        private string Label4 = "...";
        private string Label5 = "...";

        /// <summary>
        /// Plugins Installer/Updating form opening event.
        /// </summary>
        public event System.EventHandler OpenPluginsForm;

        /// <summary>
        /// Saves the Settings that changed in this Control's buffers.
        /// </summary>
        public void SaveSettings()
        {
            Classes.SettingsFile.Settingsxml.ReopenFile();
            curvalue3 = Classes.SettingsFile.Settingsxml.Read("ElsDir");
            curvalue = Classes.SettingsFile.Settingsxml.Read("IconWhileElsNotRunning");
            curvalue2 = Classes.SettingsFile.Settingsxml.Read("IconWhileElsRunning");
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
            Classes.SettingsFile.Settingsxml.Write("LoadPDB", curvalue4.ToString());
            Classes.SettingsFile.Settingsxml.Write("SaveToZip", curvalue5.ToString());
            // TODO: Save Configured Plugin Sources URL's in ListView2.
            Classes.SettingsFile.Settingsxml.Save();
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            var FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Select the Folder that Your Elsword Install is in (Must be the one that either elsword.exe or voidels.exe is in).",
                RootFolder = System.Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = false
            };
            var res = FolderBrowserDialog1.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                if (FolderBrowserDialog1.SelectedPath.Length > 0)
                {
                    TextBox1.Text = FolderBrowserDialog1.SelectedPath;
                }
            }
        }

        private void Button2_Click(object sender, System.EventArgs e) => FindForm()?.Close();

        private void RadioButton1_CheckedChanged(object sender, System.EventArgs e)
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

        private void RadioButton2_CheckedChanged(object sender, System.EventArgs e)
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

        private void RadioButton3_CheckedChanged(object sender, System.EventArgs e)
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

        private void RadioButton4_CheckedChanged(object sender, System.EventArgs e)
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

        private void RadioButton5_CheckedChanged(object sender, System.EventArgs e)
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

        private void RadioButton6_CheckedChanged(object sender, System.EventArgs e)
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

        private void TreeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (TreeView1.SelectedNode.Index == 0)
            {
                Panel3.Visible = true;
                Panel5.Visible = false;
                Panel6.Visible = false;
                TreeView1.Focus();
            }
            else if (TreeView1.SelectedNode.Index == 1)
            {
                Panel3.Visible = false;
                Panel5.Visible = true;
                Panel6.Visible = false;
                TreeView1.Focus();
            }
            else if (TreeView1.SelectedNode.Index == 2)
            {
                Panel3.Visible = false;
                Panel5.Visible = false;
                Panel6.Visible = true;
                TreeView1.Focus();
            }
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
            Classes.SettingsFile.Settingsxml.ReopenFile();
            curvalue3 = Classes.SettingsFile.Settingsxml.Read("ElsDir");
            curvalue = Classes.SettingsFile.Settingsxml.Read("IconWhileElsNotRunning");
            curvalue2 = Classes.SettingsFile.Settingsxml.Read("IconWhileElsRunning");
            int.TryParse(Classes.SettingsFile.Settingsxml.Read("LoadPDB"), out curvalue4);
            int.TryParse(Classes.SettingsFile.Settingsxml.Read("SaveToZip"), out curvalue5);
            // TODO: Load Configured Plugin Sources URL's to ListView2.
            TextBox1.Text = curvalue3;
            // set these to the values read above only if they are not empty.
            Label4 = string.IsNullOrEmpty(curvalue) ? Label4 : curvalue;
            Label5 = string.IsNullOrEmpty(curvalue2) ? Label5 : curvalue2;
            CheckBox1.Checked = System.Convert.ToBoolean(curvalue5);
            CheckBox2.Checked = System.Convert.ToBoolean(curvalue4);
            var Entries = new System.Collections.Generic.List<System.Windows.Forms.ListViewItem>();
            foreach (var callbackplugin in Classes.ExecutionManager.callbackplugins)
            {
                Entries.Add(new System.Windows.Forms.ListViewItem(new string[] {
                    callbackplugin.PluginName }, -1));
            }
            foreach (var komplugin in Classes.KOMManager.komplugins)
            {
                Entries.Add(new System.Windows.Forms.ListViewItem(new string[] {
                    komplugin.PluginName }, -1));
            }
            if (Entries.Count > 3)
            {
                ColumnHeader1.Width -= 17;
            }
            ListView1.Items.AddRange(Entries.ToArray());
            SetRadios();
            TreeView1.SelectedNode = TreeView1.Nodes[0];
        }

        private void SettingsControl_Load(object sender, System.EventArgs e)
        {
            SetRadios();
            TreeView1.SelectedNode = TreeView1.Nodes[0];
        }

        private void ListView1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            for (var i = 0; i < ListView1.SelectedItems.Count; i++)
            {
                var selitem = ListView1.SelectedItems[i];
                var found = false;
                foreach (var callbackplugin in Classes.ExecutionManager.callbackplugins)
                {
                    if (callbackplugin.PluginName.Equals(selitem.Text))
                    {
                        if (callbackplugin.SupportsSettings)
                        {
                            found = true;
                        }
                    }
                }
                Button3.Enabled = !found ? false : true;
            }
        }

        private void Button3_Click(object sender, System.EventArgs e)
        {
            for (var i = 0; i < ListView1.SelectedItems.Count; i++)
            {
                var selitem = ListView1.SelectedItems[i];
                foreach (var callbackplugin in Classes.ExecutionManager.callbackplugins)
                {
                    if (callbackplugin.PluginName.Equals(selitem.Text))
                    {
                        var plugsettingfrm = callbackplugin.SettingsWindow;
                        plugsettingfrm.ShowDialog();
                        // ensure disposed.
                        if (!plugsettingfrm.IsDisposed)
                        {
                            plugsettingfrm.Dispose();
                        }
                        plugsettingfrm = null;
                    }
                }
            }
        }

        private void Button5_Click(object sender, System.EventArgs e) => ListView2.Items.Add("Enter plugin source url here.");

        private void Button6_Click(object sender, System.EventArgs e)
        {
            if (ListView2.SelectedItems.Count > 0)
            {
                ListView2.SelectedItems[0].Remove();
            }
        }

        private void Button4_Click(object sender, System.EventArgs e) => OpenPluginsForm?.Invoke(this, new System.EventArgs());

        private void CheckBox1_CheckedChanged(object sender, System.EventArgs e) => curvalue5 = System.Convert.ToInt32(CheckBox1.Checked);

        private void CheckBox2_CheckedChanged(object sender, System.EventArgs e) => curvalue4 = System.Convert.ToInt32(CheckBox2.Checked);

        // pople say use a DataGridView because they cant hack together a solution.
        // well they were too stupid to hack a elegant soluion like this.
        private void ListView2_DoubleClick(object sender, System.EventArgs e)
        {
            // seems to not place the box in the correct location and it shows under the listview...
            var textBox = new System.Windows.Forms.TextBox
            {
                Bounds = ListView2.SelectedItems[0].Bounds,
                Text = ListView2.SelectedItems[0].Text,
                Visible = true
            };
            textBox.Enter += (s, s1) =>
            {
                    // so the user can replace all the text in the listview item.
                    textBox.SelectAll();
            };
            textBox.Leave += (s, e1) =>
            {
                ListView2.SelectedItems[0].Text = textBox.Text;
                // do not forget to dispose this control.
                ListView2.Controls.Remove(textBox);
                textBox.Dispose();
            };
            textBox.KeyDown += (s, s1) =>
            {
                s1.Handled = true;
                if (s1.KeyData == System.Windows.Forms.Keys.Enter)
                {
                    ListView2.SelectedItems[0].Text = textBox.Text;
                    // do not forget to dispose this control.
                    ListView2.Controls.Remove(textBox);
                    textBox.Dispose();
                }
                else if (s1.KeyData == System.Windows.Forms.Keys.Right)
                {
                    // remove selection.
                    textBox.Select(0, 0);
                }
                else if (s1.KeyData == System.Windows.Forms.Keys.Left)
                {
                    // remove selection.
                    textBox.Select(0, 0);
                }
            };
            ListView2.Controls.Add(textBox);
            ActiveControl = textBox;
        }
    }
}
