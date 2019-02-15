// Copyright (c) 2014-2019, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    partial class AboutForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.Text1 = new System.Windows.Forms.TextBox();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.CmdOK = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // Text1
            // 
            this.Text1.AcceptsReturn = true;
            this.Text1.BackColor = System.Drawing.SystemColors.Control;
            this.Text1.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.Text1, "Text1");
            this.Text1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Text1.Name = "Text1";
            this.Text1.ReadOnly = true;
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.Transparent;
            this.picIcon.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.picIcon, "picIcon");
            this.picIcon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.picIcon.Name = "picIcon";
            this.picIcon.Image = global::Els_kom.Icons.FormImage;
            this.picIcon.TabStop = false;
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.BackColor = System.Drawing.Color.Transparent;
            this.lblDisclaimer.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.lblDisclaimer, "lblDisclaimer");
            this.lblDisclaimer.ForeColor = System.Drawing.Color.Black;
            this.lblDisclaimer.Name = "lblDisclaimer";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Name = "lblTitle";
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Name = "lblDescription";
            // 
            // CmdOK
            // 
            this.CmdOK.BackColor = System.Drawing.Color.Transparent;
            this.CmdOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.CmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.CmdOK, "CmdOK");
            this.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CmdOK.Name = "CmdOK";
            this.CmdOK.UseVisualStyleBackColor = false;
            this.CmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Click += new System.EventHandler(this.LinkLabel1_Click);
            this.linkLabel1.MouseEnter += new System.EventHandler(this.LinkLabel1_MouseEnter);
            this.linkLabel1.MouseLeave += new System.EventHandler(this.LinkLabel1_MouseLeave);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.CmdOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.CmdOK;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.Text1);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.lblDisclaimer);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.CmdOK);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Icon = global::Els_kom.Icons.FormIcon;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AboutForm_FormClosing);
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AboutForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button CmdOK;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox Text1;
        private System.Windows.Forms.Label lblDisclaimer;
        #endregion
    }
}
