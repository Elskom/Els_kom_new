// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Plugins");
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.RadioButton6 = new System.Windows.Forms.RadioButton();
            this.RadioButton5 = new System.Windows.Forms.RadioButton();
            this.RadioButton4 = new System.Windows.Forms.RadioButton();
            this.RadioButton3 = new System.Windows.Forms.RadioButton();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.SuspendLayout();
            //
            // Panel1
            //
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Button1);
            this.Panel1.Controls.Add(this.TextBox1);
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(413, 46);
            this.Panel1.TabIndex = 4;
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
            this.Button2.Location = new System.Drawing.Point(432, 134);
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
            treeNode1.Name = "Node0";
            treeNode1.Text = "General";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Plugins";
            this.TreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.TreeView1.Scrollable = false;
            this.TreeView1.Size = new System.Drawing.Size(96, 127);
            this.TreeView1.TabIndex = 6;
            this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            //
            // Panel2
            //
            this.Panel2.Controls.Add(this.Panel4);
            this.Panel2.Controls.Add(this.RadioButton3);
            this.Panel2.Controls.Add(this.RadioButton2);
            this.Panel2.Controls.Add(this.RadioButton1);
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Location = new System.Drawing.Point(0, 51);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(413, 74);
            this.Panel2.TabIndex = 7;
            //
            // Panel4
            //
            this.Panel4.Controls.Add(this.RadioButton6);
            this.Panel4.Controls.Add(this.RadioButton5);
            this.Panel4.Controls.Add(this.RadioButton4);
            this.Panel4.Location = new System.Drawing.Point(144, 34);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(198, 29);
            this.Panel4.TabIndex = 6;
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
            // Panel3
            //
            this.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel3.Controls.Add(this.Panel1);
            this.Panel3.Controls.Add(this.Panel2);
            this.Panel3.Location = new System.Drawing.Point(95, 0);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(415, 127);
            this.Panel3.TabIndex = 6;
            //
            // Panel5
            //
            this.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel5.Controls.Add(this.button3);
            this.Panel5.Controls.Add(this.listView1);
            this.Panel5.Location = new System.Drawing.Point(95, 0);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(415, 127);
            this.Panel5.TabIndex = 6;
            this.Panel5.Visible = false;
            //
            // button3
            //
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(325, 95);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Settings";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            //
            // listView1
            //
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(7, 7);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(396, 83);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            //
            // columnHeader1
            //
            this.columnHeader1.Text = "Plugin";
            this.columnHeader1.Width = 395;
            //
            // SettingsControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TreeView1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.Panel5);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(510, 160);
            this.Load += new System.EventHandler(this.SettingsControl_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button button3;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.TreeView TreeView1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.Panel Panel5;
        internal System.Windows.Forms.RadioButton RadioButton1;
        internal System.Windows.Forms.RadioButton RadioButton2;
        internal System.Windows.Forms.RadioButton RadioButton3;
        internal System.Windows.Forms.RadioButton RadioButton4;
        internal System.Windows.Forms.RadioButton RadioButton5;
        internal System.Windows.Forms.RadioButton RadioButton6;
        internal System.Windows.Forms.ListView listView1;
        internal System.Windows.Forms.ColumnHeader columnHeader1;

        #endregion

    }
}
