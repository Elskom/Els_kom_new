// Copyright (c) 2014-2018, Els_kom org.
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
            this.aboutControl1 = (new Els_kom_Core.Controls.AboutControl());
            this.SuspendLayout();
            // 
            // aboutControl1
            // 
            this.aboutControl1.Location = new System.Drawing.Point(0, 0);
            this.aboutControl1.Name = "aboutControl1";
            this.aboutControl1.picIcon.Image = global::Els_kom.Icons.FormImage;
            this.aboutControl1.Size = new System.Drawing.Size(382, 220);
            this.aboutControl1.TabIndex = 13;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6, 14);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AcceptButton = this.aboutControl1.CmdOK;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(382, 227);
            this.CancelButton = this.aboutControl1.CmdOK;
            this.Controls.Add(this.aboutControl1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = (new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)0));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(3, 25);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Icon = global::Els_kom.Icons.FormIcon;
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AboutForm_FormClosing);
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Els_kom_Core.Controls.AboutControl  aboutControl1;
        #endregion
    }
}
