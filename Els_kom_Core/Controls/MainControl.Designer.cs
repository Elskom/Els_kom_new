// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Controls
{
    partial class MainControl
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

        /// <summary>
        /// Label that displays the information about the buttons the mouse is hovering on.
        /// </summary>
        public System.Windows.Forms.Label Label1;

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Command6 = new System.Windows.Forms.Button();
            this.Command5 = new System.Windows.Forms.Button();
            this.Command4 = new System.Windows.Forms.Button();
            this.Command3 = new System.Windows.Forms.Button();
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Command2 = new System.Windows.Forms.Button();
            this.Command1 = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Frame1.SuspendLayout();
            this.SuspendLayout();
            //
            // Command6
            //
            this.Command6.BackColor = System.Drawing.Color.Transparent;
            this.Command6.Cursor = System.Windows.Forms.Cursors.Default;
            this.Command6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Command6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Command6.Location = new System.Drawing.Point(8, 74);
            this.Command6.Name = "Command6";
            this.Command6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Command6.Size = new System.Drawing.Size(65, 20);
            this.Command6.TabIndex = 8;
            this.Command6.Text = "Settings";
            this.Command6.UseVisualStyleBackColor = false;
            this.Command6.Click += new System.EventHandler(this.Command6_Click);
            this.Command6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Command6_MouseMove);
            //
            // Command5
            //
            this.Command5.BackColor = System.Drawing.Color.Transparent;
            this.Command5.Cursor = System.Windows.Forms.Cursors.Default;
            this.Command5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Command5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Command5.Location = new System.Drawing.Point(8, 120);
            this.Command5.Name = "Command5";
            this.Command5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Command5.Size = new System.Drawing.Size(65, 20);
            this.Command5.TabIndex = 7;
            this.Command5.Text = "Launcher";
            this.Command5.UseVisualStyleBackColor = false;
            this.Command5.Click += new System.EventHandler(this.Command5_Click);
            this.Command5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Command5_MouseMove);
            //
            // Command4
            //
            this.Command4.BackColor = System.Drawing.Color.Transparent;
            this.Command4.Cursor = System.Windows.Forms.Cursors.Default;
            this.Command4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Command4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Command4.Location = new System.Drawing.Point(8, 97);
            this.Command4.Name = "Command4";
            this.Command4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Command4.Size = new System.Drawing.Size(65, 20);
            this.Command4.TabIndex = 6;
            this.Command4.Text = "Test Mods";
            this.Command4.UseVisualStyleBackColor = false;
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            this.Command4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Command4_MouseMove);
            //
            // Command3
            //
            this.Command3.BackColor = System.Drawing.Color.Transparent;
            this.Command3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Command3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Command3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Command3.Location = new System.Drawing.Point(8, 52);
            this.Command3.Name = "Command3";
            this.Command3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Command3.Size = new System.Drawing.Size(65, 20);
            this.Command3.TabIndex = 5;
            this.Command3.Text = "About";
            this.Command3.UseVisualStyleBackColor = false;
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            this.Command3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Command3_MouseMove);
            //
            // Frame1
            //
            this.Frame1.BackColor = System.Drawing.SystemColors.Control;
            this.Frame1.Controls.Add(this.Label1);
            this.Frame1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Location = new System.Drawing.Point(80, 0);
            this.Frame1.Name = "Frame1";
            this.Frame1.Padding = new System.Windows.Forms.Padding(0);
            this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame1.Size = new System.Drawing.Size(217, 147);
            this.Frame1.TabIndex = 2;
            this.Frame1.TabStop = false;
            //
            // Label1
            //
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(8, 7);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(203, 132);
            this.Label1.TabIndex = 3;
            this.Label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label1_MouseMove);
            //
            // Command2
            //
            this.Command2.BackColor = System.Drawing.Color.Transparent;
            this.Command2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Command2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Command2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Command2.Location = new System.Drawing.Point(8, 30);
            this.Command2.Name = "Command2";
            this.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Command2.Size = new System.Drawing.Size(65, 20);
            this.Command2.TabIndex = 1;
            this.Command2.Text = "Unpack";
            this.Command2.UseVisualStyleBackColor = false;
            this.Command2.Click += new System.EventHandler(this.Command2_Click);
            this.Command2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Command2_MouseMove);
            //
            // Command1
            //
            this.Command1.BackColor = System.Drawing.Color.Transparent;
            this.Command1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Command1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Command1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Command1.Location = new System.Drawing.Point(8, 7);
            this.Command1.Name = "Command1";
            this.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Command1.Size = new System.Drawing.Size(65, 20);
            this.Command1.TabIndex = 0;
            this.Command1.Text = "Pack";
            this.Command1.UseVisualStyleBackColor = false;
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            this.Command1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Command1_MouseMove);
            //
            // Label2
            //
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(0, 139);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(81, 17);
            this.Label2.TabIndex = 4;
            //
            // MainControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Command6);
            this.Controls.Add(this.Command5);
            this.Controls.Add(this.Command4);
            this.Controls.Add(this.Command3);
            this.Controls.Add(this.Frame1);
            this.Controls.Add(this.Command2);
            this.Controls.Add(this.Command1);
            this.Controls.Add(this.Label2);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(304, 156);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainControl_MouseMove);
            this.Frame1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.GroupBox Frame1;
        internal System.Windows.Forms.Button Command1;
        internal System.Windows.Forms.Button Command2;
        internal System.Windows.Forms.Button Command3;
        internal System.Windows.Forms.Button Command4;
        internal System.Windows.Forms.Button Command5;
        internal System.Windows.Forms.Button Command6;
        #endregion
    }
}
