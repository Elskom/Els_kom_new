// Copyright (c) 2014-2019, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    partial class GameSelectionForm
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
            // do not leak dynamically created buttons.
            foreach (var button in buttons)
            {
                button.Dispose();
            }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameSelectionForm));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            resources.ApplyResources(this.Panel1, "Panel1");
            this.Panel1.Name = "Panel1";
            // 
            // GameSelectionForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.Panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSelectionForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.GameSelectionForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel Panel1;
    }
}
