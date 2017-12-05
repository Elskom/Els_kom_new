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

        string curvalue;
        string curvalue2;
        string curvalue3;
        /// <summary>
        /// Event that the control fires that Closes the Form it is on.
        /// </summary>
        public event System.EventHandler CloseForm;

        /// <summary>
        /// Saves the Settings that changed in this Control's buffers.
        /// </summary>
        public void SaveSettings()
        {
            Classes.INIObject settingsini = new Classes.INIObject(System.Windows.Forms.Application.StartupPath + "\\Settings.ini");
            if (!ReferenceEquals(TextBox1.Text, curvalue3))
            {
                if (TextBox1.Text.Length > 0)
                {
                    settingsini.Write("Settings.ini", "ElsDir", TextBox1.Text);
                }
                else
                {
                    Classes.MessageManager.ShowWarning("You Should Set a Working Elsword Directory.", "Warning!");
                }
            }
            if (!ReferenceEquals(Label4.Text, curvalue))
            {
                if (Label5.Text == "...")
                {
                    settingsini.Write("Settings.ini", "IconWhileElsNotRunning", "2");
                }
                else
                {
                    settingsini.Write("Settings.ini", "IconWhileElsNotRunning", Label4.Text);
                }
            }
            if (!ReferenceEquals(Label5.Text, curvalue2))
            {
                if (Label5.Text == "...")
                {
                    settingsini.Write("Settings.ini", "IconWhileElsRunning", "1");
                }
                else
                {
                    settingsini.Write("Settings.ini", "IconWhileElsRunning", Label5.Text);
                }
            }
        }

        void Button1_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog1.ShowDialog();
            Timer1.Enabled = true;
        }

        void Button2_Click(object sender, System.EventArgs e)
        {
            CloseForm?.Invoke(this, new System.EventArgs());
        }

        void Timer1_Tick(object sender, System.EventArgs e)
        {
            if (FolderBrowserDialog1.SelectedPath.Length > 0)
            {
                TextBox1.Text = FolderBrowserDialog1.SelectedPath;
                Timer1.Enabled = false;
            }
        }

        void RadioButton1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (RadioButton1.Checked)
            {
                Label4.Text = "0";
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
                Label4.Text = "1";
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
                Label4.Text = "2";
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
                Label5.Text = "0";
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
                Label5.Text = "1";
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
                Label5.Text = "2";
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
                Panel1.Visible = true;
                Panel2.Visible = false;
                TreeView1.Focus();
            }
            else if (TreeView1.SelectedNode.Index == 1)
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                TreeView1.Focus();
            }
        }

        private void SettingsControl_Load(object sender, System.EventArgs e)
        {
            if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\Settings.ini"))
            {
                Classes.INIObject settingsini = new Classes.INIObject(System.Windows.Forms.Application.StartupPath + "\\Settings.ini");
                curvalue3 = settingsini.Read("Settings.ini", "ElsDir");
                curvalue = settingsini.Read("Settings.ini", "IconWhileElsNotRunning");
                curvalue2 = settingsini.Read("Settings.ini", "IconWhileElsRunning");
                TextBox1.Text = curvalue3;
                Label4.Text = curvalue;
                Label5.Text = curvalue2;
                if (Label4.Text == "0")
                {
                    RadioButton1.Checked = true;
                }
                else if (Label4.Text == "1")
                {
                    RadioButton2.Checked = true;
                }
                else if (Label4.Text == "2")
                {
                    RadioButton3.Checked = true;
                }
                else if (Label4.Text == "...")
                {
                    RadioButton3.Checked = true;
                }
                if (Label5.Text == "0")
                {
                    RadioButton4.Checked = true;
                }
                else if (Label5.Text == "1")
                {
                    RadioButton5.Checked = true;
                }
                else if (Label5.Text == "2")
                {
                    RadioButton6.Checked = true;
                }
                else if (Label5.Text == "...")
                {
                    RadioButton5.Checked = true;
                }
                TreeView1.SelectedNode = TreeView1.Nodes[0];
            }
        }
    }
}
