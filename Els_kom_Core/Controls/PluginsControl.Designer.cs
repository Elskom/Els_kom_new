// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    partial class PluginsControl
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
            this.ListView1 = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label1 = new System.Windows.Forms.Label();
            this.InstallButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.UninstallButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListView1
            // 
            this.ListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3});
            this.ListView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.ListView1.FullRowSelect = true;
            this.ListView1.GridLines = true;
            this.ListView1.HideSelection = false;
            this.ListView1.LabelWrap = false;
            this.ListView1.Location = new System.Drawing.Point(6, 108);
            this.ListView1.MultiSelect = false;
            this.ListView1.Name = "ListView1";
            this.ListView1.Size = new System.Drawing.Size(347, 106);
            this.ListView1.TabIndex = 0;
            this.ListView1.UseCompatibleStateImageBehavior = false;
            this.ListView1.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Plugin Name";
            this.ColumnHeader1.Width = 120;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Current Version";
            this.ColumnHeader2.Width = 96;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Installed Version";
            this.ColumnHeader3.Width = 102;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(3, 7);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(351, 98);
            this.Label1.TabIndex = 1;
            // 
            // InstallButton
            // 
            this.InstallButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.InstallButton.Location = new System.Drawing.Point(127, 221);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(69, 23);
            this.InstallButton.TabIndex = 2;
            this.InstallButton.Text = "Install";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Visible = false;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(279, 221);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // UninstallButton
            // 
            this.UninstallButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.UninstallButton.Location = new System.Drawing.Point(203, 221);
            this.UninstallButton.Name = "UninstallButton";
            this.UninstallButton.Size = new System.Drawing.Size(69, 23);
            this.UninstallButton.TabIndex = 4;
            this.UninstallButton.Text = "Uninstall";
            this.UninstallButton.UseVisualStyleBackColor = true;
            this.UninstallButton.Visible = false;
            this.UninstallButton.Click += new System.EventHandler(this.UninstallButton_Click);
            // 
            // PluginsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UninstallButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.InstallButton);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.ListView1);
            this.Name = "PluginsControl";
            this.Size = new System.Drawing.Size(359, 249);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PluginsControl_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ListView1;
        private System.Windows.Forms.ColumnHeader ColumnHeader1;
        private System.Windows.Forms.ColumnHeader ColumnHeader2;
        private System.Windows.Forms.ColumnHeader ColumnHeader3;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button UninstallButton;
    }
}
