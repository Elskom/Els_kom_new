// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    partial class MainForm
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
            this.MainControl1 = new Els_kom_Core.Controls.MainControl();
            this.SuspendLayout();
            // 
            // MainControl1
            // 
            this.MainControl1.End_settings_loop = false;
            this.MainControl1.Location = new System.Drawing.Point(0, 0);
            this.MainControl1.Name = "MainControl1";
            this.MainControl1.Size = new System.Drawing.Size(304, 166);
            this.MainControl1.TabIndex = 8;
            this.MainControl1.CloseForm += new System.EventHandler(this.MainControl1_CloseForm);
            this.MainControl1.TrayNameChange += new System.EventHandler(this.MainControl1_TrayNameChange);
            this.MainControl1.TrayClick += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.MainControl1_TrayClick);
            this.MainControl1.ConfigForm += new System.EventHandler(this.MainControl1_ConfigForm);
            this.MainControl1.AboutForm += new System.EventHandler(this.MainControl1_AboutForm);
            this.MainControl1.TrayIconChange += new System.EventHandler(this.MainControl1_TrayIconChange);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(304, 166);
            this.Controls.Add(this.MainControl1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Els_kom.Icons.FormIcon;
            this.Location = new System.Drawing.Point(3, 23);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Els_kom v1.4.9.8";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseLeave += new System.EventHandler(this.MainForm_MouseLeave);
            this.ResumeLayout(false);

        }

        private Els_kom_Core.Controls.MainControl MainControl1;
        #endregion
    }
}
