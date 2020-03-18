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
            this.themedLine1 = new Els_kom.Controls.ThemedLine();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // Text1
            // 
            this.Text1.AcceptsReturn = true;
            this.Text1.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.Text1, "Text1");
            this.Text1.Name = "Text1";
            this.Text1.ReadOnly = true;
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.Transparent;
            this.picIcon.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.picIcon, "picIcon");
            this.picIcon.Name = "picIcon";
            this.picIcon.TabStop = false;
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.lblDisclaimer, "lblDisclaimer");
            this.lblDisclaimer.Name = "lblDisclaimer";
            // 
            // lblTitle
            // 
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // lblDescription
            // 
            this.lblDescription.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // CmdOK
            // 
            this.CmdOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.CmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.CmdOK, "CmdOK");
            this.CmdOK.Name = "CmdOK";
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
            // themedLine1
            // 
            resources.ApplyResources(this.themedLine1, "themedLine1");
            this.themedLine1.Name = "themedLine1";
            // 
            // AboutForm
            // 
            this.AcceptButton = this.CmdOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.CmdOK;
            this.Controls.Add(this.themedLine1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.Text1);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.lblDisclaimer);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.CmdOK);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AboutForm_FormClosing);
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button/*Els_kom.Controls.ThemedButton*/ CmdOK;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label/*Els_kom.Controls.ThemedLabel*/ lblDescription;
        private System.Windows.Forms.Label/*Els_kom.Controls.ThemedLabel*/ lblTitle;
        private System.Windows.Forms.TextBox/*Els_kom.Controls.ThemedTextBox*/ Text1;
        private System.Windows.Forms.Label/*Els_kom.Controls.ThemedLabel*/ lblDisclaimer;
        private Els_kom.Controls.ThemedLine themedLine1;
        #endregion
    }
}
