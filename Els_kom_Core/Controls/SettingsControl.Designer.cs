namespace Els_kom_Core.Controls
{
    partial class SettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Game Folder");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Icon Settings");
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.RadioButton6 = new System.Windows.Forms.RadioButton();
            this.RadioButton5 = new System.Windows.Forms.RadioButton();
            this.RadioButton4 = new System.Windows.Forms.RadioButton();
            this.RadioButton3 = new System.Windows.Forms.RadioButton();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // FolderBrowserDialog1
            // 
            this.FolderBrowserDialog1.Description = "Select the Folder that Your Elsword Install is in (Must be the one that either el" +
    "sword.exe or voidels.exe is in).";
            this.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.FolderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // Timer1
            // 
            this.Timer1.Interval = 1;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Button1);
            this.Panel1.Controls.Add(this.TextBox1);
            this.Panel1.Location = new System.Drawing.Point(96, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(414, 46);
            this.Panel1.TabIndex = 4;
            this.Panel1.Visible = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(6, 14);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(92, 13);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Elsword Directory:";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(348, 11);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(54, 19);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Browse";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(100, 11);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(242, 20);
            this.TextBox1.TabIndex = 0;
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(432, 83);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(79, 21);
            this.Button2.TabIndex = 5;
            this.Button2.Text = "Ok";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // TreeView1
            // 
            this.TreeView1.Location = new System.Drawing.Point(0, 0);
            this.TreeView1.Name = "TreeView1";
            treeNode21.Name = "Node0";
            treeNode21.Text = "Game Folder";
            treeNode22.Name = "Node1";
            treeNode22.Text = "Icon Settings";
            this.TreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode22});
            this.TreeView1.Scrollable = false;
            this.TreeView1.ShowLines = false;
            this.TreeView1.ShowPlusMinus = false;
            this.TreeView1.ShowRootLines = false;
            this.TreeView1.Size = new System.Drawing.Size(96, 74);
            this.TreeView1.TabIndex = 6;
            this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Panel3);
            this.Panel2.Controls.Add(this.RadioButton3);
            this.Panel2.Controls.Add(this.RadioButton2);
            this.Panel2.Controls.Add(this.RadioButton1);
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Location = new System.Drawing.Point(96, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(414, 74);
            this.Panel2.TabIndex = 7;
            this.Panel2.Visible = false;
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.RadioButton6);
            this.Panel3.Controls.Add(this.RadioButton5);
            this.Panel3.Controls.Add(this.RadioButton4);
            this.Panel3.Location = new System.Drawing.Point(144, 34);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(198, 29);
            this.Panel3.TabIndex = 6;
            // 
            // RadioButton6
            // 
            this.RadioButton6.AutoSize = true;
            this.RadioButton6.Location = new System.Drawing.Point(142, 6);
            this.RadioButton6.Name = "RadioButton6";
            this.RadioButton6.Size = new System.Drawing.Size(47, 17);
            this.RadioButton6.TabIndex = 2;
            this.RadioButton6.TabStop = true;
            this.RadioButton6.Text = "Both";
            this.RadioButton6.UseVisualStyleBackColor = true;
            this.RadioButton6.CheckedChanged += new System.EventHandler(this.RadioButton6_CheckedChanged);
            // 
            // RadioButton5
            // 
            this.RadioButton5.AutoSize = true;
            this.RadioButton5.Location = new System.Drawing.Point(80, 6);
            this.RadioButton5.Name = "RadioButton5";
            this.RadioButton5.Size = new System.Drawing.Size(46, 17);
            this.RadioButton5.TabIndex = 1;
            this.RadioButton5.TabStop = true;
            this.RadioButton5.Text = "Tray";
            this.RadioButton5.UseVisualStyleBackColor = true;
            this.RadioButton5.CheckedChanged += new System.EventHandler(this.RadioButton5_CheckedChanged);
            // 
            // RadioButton4
            // 
            this.RadioButton4.AutoSize = true;
            this.RadioButton4.Location = new System.Drawing.Point(3, 6);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(64, 17);
            this.RadioButton4.TabIndex = 0;
            this.RadioButton4.TabStop = true;
            this.RadioButton4.Text = "Taskbar";
            this.RadioButton4.UseVisualStyleBackColor = true;
            this.RadioButton4.CheckedChanged += new System.EventHandler(this.RadioButton4_CheckedChanged);
            // 
            // RadioButton3
            // 
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.Location = new System.Drawing.Point(303, 12);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(47, 17);
            this.RadioButton3.TabIndex = 5;
            this.RadioButton3.TabStop = true;
            this.RadioButton3.Text = "Both";
            this.RadioButton3.UseVisualStyleBackColor = true;
            this.RadioButton3.CheckedChanged += new System.EventHandler(this.RadioButton3_CheckedChanged);
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Location = new System.Drawing.Point(240, 12);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(46, 17);
            this.RadioButton2.TabIndex = 4;
            this.RadioButton2.TabStop = true;
            this.RadioButton2.Text = "Tray";
            this.RadioButton2.UseVisualStyleBackColor = true;
            this.RadioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Location = new System.Drawing.Point(163, 12);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(64, 17);
            this.RadioButton1.TabIndex = 3;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "Taskbar";
            this.RadioButton1.UseVisualStyleBackColor = true;
            this.RadioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(8, 42);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(130, 13);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "While Elsword is Running:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(7, 14);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(148, 13);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "While Elsword is not Running:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(110, 94);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(16, 13);
            this.Label4.TabIndex = 8;
            this.Label4.Text = "...";
            this.Label4.Visible = false;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(164, 93);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(16, 13);
            this.Label5.TabIndex = 9;
            this.Label5.Text = "...";
            this.Label5.Visible = false;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.TreeView1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Button2);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(510, 106);
            this.Load += new System.EventHandler(this.SettingsControl_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
        internal System.Windows.Forms.Timer Timer1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.TreeView TreeView1;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.RadioButton RadioButton3;
        internal System.Windows.Forms.RadioButton RadioButton2;
        internal System.Windows.Forms.RadioButton RadioButton1;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.RadioButton RadioButton6;
        internal System.Windows.Forms.RadioButton RadioButton5;
        internal System.Windows.Forms.RadioButton RadioButton4;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        #endregion

    }
}
