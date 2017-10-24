
using System.Windows.Forms;
using Els_kom_Core.Controls;

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
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		#region "Windows Form Designer generated code "
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.Maincontrol1 = new Els_kom_Core.Controls.MainControl();
            this.SuspendLayout();
            // 
            // Maincontrol1
            // 
            this.Maincontrol1.Location = new System.Drawing.Point(0, 0);
            this.Maincontrol1.Name = "Maincontrol1";
            this.Maincontrol1.Size = new System.Drawing.Size(304, 146);
            this.Maincontrol1.TabIndex = 8;
            this.Maincontrol1.MinimizeForm += new System.EventHandler(this.Maincontrol1_MinimizeForm);
            this.Maincontrol1.CloseForm += new System.EventHandler(this.Maincontrol1_CloseForm);
            this.Maincontrol1.TrayNameChange += new System.EventHandler(this.Maincontrol1_TrayNameChange);
            this.Maincontrol1.TaskbarShow += new System.EventHandler<Els_kom_Core.Classes.ShowTaskbarEvent>(this.Maincontrol1_TaskbarShow);
            this.Maincontrol1.TrayClick += new System.EventHandler<MouseEventArgs>(this.Maincontrol1_TrayClick);
            this.Maincontrol1.ConfigForm += new System.EventHandler(this.Maincontrol1_ConfigForm);
            this.Maincontrol1.ConfigForm2 += new System.EventHandler(this.Maincontrol1_ConfigForm2);
            this.Maincontrol1.AboutForm += new System.EventHandler(this.Maincontrol1_AboutForm);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(304, 146);
            this.Controls.Add(this.Maincontrol1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(3, 23);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Els_kom v1.4.9.7";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseLeave += new System.EventHandler(this.MainForm_MouseLeave);
            this.ResumeLayout(false);

		}

		#endregion

        private Els_kom_Core.Controls.MainControl Maincontrol1;
    }
}
