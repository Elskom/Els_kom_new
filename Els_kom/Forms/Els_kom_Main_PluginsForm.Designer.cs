// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    partial class PluginsForm
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
            this.PluginsControl1 = new Els_kom_Core.Controls.PluginsControl();
            this.SuspendLayout();
            // 
            // pluginsControl1
            // 
            this.PluginsControl1.Location = new System.Drawing.Point(0, 0);
            this.PluginsControl1.Name = "PluginsControl1";
            this.PluginsControl1.Size = new System.Drawing.Size(359, 249);
            this.PluginsControl1.TabIndex = 0;
            // 
            // PluginsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 249);
            this.Controls.Add(this.PluginsControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Els_kom.Icons.FormIcon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plugins Installer, Updater, and Uninstaller";
            this.Load += new System.EventHandler(this.PluginsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Els_kom_Core.Controls.PluginsControl PluginsControl1;
    }
}