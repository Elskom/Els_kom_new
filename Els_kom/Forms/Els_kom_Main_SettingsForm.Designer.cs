// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    partial class SettingsForm
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
            this.SettingsControl1 = new Els_kom_Core.Controls.SettingsControl();
            this.SuspendLayout();
            // 
            // SettingsControl1
            // 
            this.SettingsControl1.Location = new System.Drawing.Point(12, 10);
            this.SettingsControl1.Name = "SettingsControl1";
            this.SettingsControl1.Size = new System.Drawing.Size(510, 157);
            this.SettingsControl1.TabIndex = 10;
            this.SettingsControl1.OpenPluginsForm += new System.EventHandler(this.SettingsControl1_OpenPluginsForm);
            this.SettingsControl1.OpenPluginsSettings += new System.EventHandler(this.SettingsControl1_OpenPluginsSettings);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 177);
            this.Controls.Add(this.SettingsControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = Icons.FormIcon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);

        }

        private Els_kom_Core.Controls.SettingsControl SettingsControl1;
        #endregion

    }
}
