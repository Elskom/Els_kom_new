namespace callbacktest_plugin
{
    partial class CallbacktestForm
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
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // checkBox1
            //
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(6, 6);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(15, 14);
            this.CheckBox1.TabIndex = 0;
            this.CheckBox1.UseVisualStyleBackColor = true;
            this.CheckBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            //
            // label1
            //
            this.Label1.Location = new System.Drawing.Point(23, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(118, 14);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Display Test Messages";
            //
            // CallbacktestForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 27);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.CheckBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = Els_kom_Core.Classes.Icons.FormIcon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CallbacktestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Callback Test Plugin Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CallbacktestForm_FormClosing);
            this.Load += new System.EventHandler(this.CallbacktestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CheckBox1;
        private System.Windows.Forms.Label Label1;
    }
}
