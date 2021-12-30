// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using Els_kom.Controls;

    internal partial class SettingsForm : /*Form*/ThemedForm
    {
        private int curvalue4;
        private int label4 = -1;
        private int label5 = -1;
        private int label8 = -1;

        internal SettingsForm()
            => this.InitializeComponent();

        internal static int Label9 { get; private set; }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.curvalue4 = SettingsFile.SettingsJson.SaveToZip;
            var sourceEntries = new List<ListViewItem>();
            Array.ForEach(
                SettingsFile.SettingsJson.Sources,
                (x) => sourceEntries.Add(
                    new ListViewItem(
                        new string[]
                        {
                            x,
                        },
                        -1)));
            this.ListView2.Items.AddRange(sourceEntries.ToArray());
            this.TextBox1.Text = SettingsFile.SettingsJson.ElsDir;

            // set these to the values read above only if they are not empty.
            this.label4 = SettingsFile.SettingsJson.IconWhileElsNotRunning;
            this.label5 = SettingsFile.SettingsJson.IconWhileElsRunning;
            this.label8 = SettingsFile.SettingsJson.WindowIcon;
            this.CheckBox1.Checked = Convert.ToBoolean(this.curvalue4);
            var entries = new List<ListViewItem>();
            KOMManager.Callbackplugins.ForEach(
                (x) => entries.Add(
                    new ListViewItem(
                        new string[]
                        {
                            x.PluginName,
                        },
                        -1)));
            KOMManager.Komplugins.ForEach(
                (x) => entries.Add(
                    new ListViewItem(
                        new string[]
                        {
                            x.PluginName,
                        },
                        -1)));
            if (entries.Count > 3)
            {
                this.ColumnHeader1.Width -= 17;
            }

            this.ListView1.Items.AddRange(entries.ToArray());
            switch (this.label4)
            {
                case 0:
                    this.RadioButton1.Checked = true;
                    break;
                case 1:
                    this.RadioButton2.Checked = true;
                    break;
                case 2 or -1:
                    this.RadioButton3.Checked = true;
                    break;
                default:
                    // nothing to do.
                    break;
            }

            switch (this.label5)
            {
                case 0:
                    this.RadioButton4.Checked = true;
                    break;
                case 1 or -1:
                    this.RadioButton5.Checked = true;
                    break;
                case 2:
                    this.RadioButton6.Checked = true;
                    break;
                default:
                    // nothing to do.
                    break;
            }

            switch (this.label8)
            {
                case 0 or -1:
                    this.RadioButton7.Checked = true;
                    break;
                case 1:
                    this.RadioButton8.Checked = true;
                    break;
                case 2:
                    this.RadioButton9.Checked = true;
                    break;
                default:
                    // nothing to do.
                    break;
            }

            this.TreeView1.SelectedNode = this.TreeView1.Nodes[0];
            Label9 = 1;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using var folderBrowserDialog1 = new FolderBrowserDialog
            {
                Description = "Select the Folder that Your Elsword Install is in (Must be the one that either elsword.exe or voidels.exe is in).",
                RootFolder = Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = false,
            };
            var res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK && folderBrowserDialog1.SelectedPath.Length > 0)
            {
                this.TextBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
            => this.Close();

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton1.Checked)
            {
                this.label4 = 0;
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton2.Checked)
            {
                this.label4 = 1;
            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton3.Checked)
            {
                this.label4 = 2;
            }
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton4.Checked)
            {
                this.label5 = 0;
            }
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton5.Checked)
            {
                this.label5 = 1;
            }
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton6.Checked)
            {
                this.label5 = 2;
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.TreeView1.SelectedNode.Index == 0)
            {
                this.Panel3.Visible = true;
                this.Panel5.Visible = false;
                this.Panel6.Visible = false;
                _ = this.TreeView1.Focus();
            }
            else if (this.TreeView1.SelectedNode.Index == 1)
            {
                this.Panel3.Visible = false;
                this.Panel5.Visible = true;
                this.Panel6.Visible = false;
                _ = this.TreeView1.Focus();
            }
            else if (this.TreeView1.SelectedNode.Index == 2)
            {
                this.Panel3.Visible = false;
                this.Panel5.Visible = false;
                this.Panel6.Visible = true;
                _ = this.TreeView1.Focus();
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Label9 = 0;
            if (!string.Equals(this.TextBox1.Text, SettingsFile.SettingsJson.ElsDir, StringComparison.Ordinal))
            {
                if (this.TextBox1.Text.Length > 0)
                {
                    SettingsFile.SettingsJson.ElsDir = this.TextBox1.Text;
                }
                else
                {
                    _ = MessageManager.ShowWarning("You Should Set a Working Elsword Directory.", "Warning!", Convert.ToBoolean(SettingsFile.SettingsJson.UseNotifications));
                }
            }

            if (!this.label4.Equals(SettingsFile.SettingsJson.IconWhileElsNotRunning))
            {
                SettingsFile.SettingsJson.IconWhileElsNotRunning = this.label4 == -1 ? 2 : this.label4;
            }

            if (!this.label5.Equals(SettingsFile.SettingsJson.IconWhileElsRunning))
            {
                SettingsFile.SettingsJson.IconWhileElsRunning = this.label5 == -1 ? 1 : this.label5;
            }

            if (!this.label8.Equals(SettingsFile.SettingsJson.WindowIcon))
            {
                SettingsFile.SettingsJson.WindowIcon = this.label8 == -1 ? 0 : this.label8;
            }

            SettingsFile.SettingsJson.SaveToZip = this.curvalue4;
            var sources = new List<string>();
            for (var i = 0; i < this.ListView2.Items.Count; i++)
            {
                sources.Add(this.ListView2.Items[i].Text);
            }

            // refill the sources from the ones in this form
            SettingsFile.SettingsJson.Sources = sources.ToArray();

            // write to file.
            SettingsFile.SettingsJson.Save();
            sources.Clear();
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < this.ListView1.SelectedItems.Count; i++)
            {
                var selitem = this.ListView1.SelectedItems[i];
                var found = false;
                foreach (var callbackplugin in KOMManager.Callbackplugins)
                {
                    if (callbackplugin.PluginName.Equals(selitem.Text) && callbackplugin.SupportsSettings)
                    {
                        found = true;
                    }
                }

                this.Button3.Enabled = found;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < this.ListView1.SelectedItems.Count; i++)
            {
                var selitem = this.ListView1.SelectedItems[i];
                foreach (var callbackplugin in KOMManager.Callbackplugins.Where(callbackplugin => callbackplugin.PluginName.Equals(selitem.Text)))
                {
                    using var plugsettingfrm = (Form)callbackplugin.SettingsWindow;
                    plugsettingfrm.Icon = Icons.FormIcon;
                    _ = plugsettingfrm.ShowDialog(callbackplugin.ShowModal ? this : null);
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e)
            => this.ListView2.Items.Add("Enter plugin source url here.");

        private void Button6_Click(object sender, EventArgs e)
        {
            if (this.ListView2.SelectedItems.Count > 0)
            {
                this.ListView2.SelectedItems[0].Remove();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            using var pluginsForm = new PluginsForm();
            _ = pluginsForm.ShowDialog();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
            => this.curvalue4 = Convert.ToInt32(this.CheckBox1.Checked);

        // people say use a DataGridView because they cant hack together a solution.
        // well they were too stupid to hack a elegant soluion like this.
        private void ListView2_DoubleClick(object sender, EventArgs e)
        {
            var textBox = new TextBox
            {
                Bounds = this.ListView2.SelectedItems[0].Bounds,
                Text = this.ListView2.SelectedItems[0].Text,
                Visible = true,
            };
            textBox.Enter += (s, s1) =>
            {
                // so the user can replace all the text in the listview item.
                textBox.SelectAll();
            };
            textBox.Leave += (s, e1) =>
            {
                this.ListView2.SelectedItems[0].Text = textBox.Text;

                // do not forget to dispose this control.
                this.ListView2.Controls.Remove(textBox);
                textBox.Dispose();
            };
            textBox.KeyDown += (s, s1) =>
            {
                s1.Handled = true;
                if (s1.KeyData == Keys.Enter)
                {
                    this.ListView2.SelectedItems[0].Text = textBox.Text;

                    // do not forget to dispose this control.
                    this.ListView2.Controls.Remove(textBox);
                    textBox.Dispose();
                }
                else if (s1.KeyData == Keys.Right)
                {
                    // remove selection.
                    textBox.Select(0, 0);
                }
                else if (s1.KeyData == Keys.Left)
                {
                    // remove selection.
                    textBox.Select(0, 0);
                }
            };
            ShareXResources.ApplyDarkThemeToControl(textBox);
            this.ListView2.Controls.Add(textBox);
            this.ActiveControl = textBox;
        }

        private void RadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton7.Checked)
            {
                this.label8 = 0;
            }
        }

        private void RadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton8.Checked)
            {
                this.label8 = 1;
            }
        }

        private void RadioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton9.Checked)
            {
                this.label8 = 2;
            }
        }
    }
}
