// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Controls
{
    using Els_kom.Enums;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    // using NativeInterface;
    internal class ThemedForm : /*Custom*/Form
    {
        private bool minimizeHover = false;
        private bool maximizeHover = false;
        private bool closeHover = false;
        private bool helpHover = false;
        private bool active = false;

        internal ThemedForm()
        {
            this.InitializeComponent();
            this.MinimizeHitbox = new Rectangle(this.Size.Width - 126, 1, 45, 29);
            this.MaximizeHitbox = new Rectangle(this.Size.Width - 86, 1, 45, 29);
            this.CloseHitbox = new Rectangle(this.Size.Width - 42, 1, 45, 29);
            this.HelpHitbox = new Rectangle(this.Size.Width - 84, 1, 45, 29);
            this.Paint += this.ThemedForm_Paint;
            this.Load += this.ThemedForm_Load;
            this.MouseDown += this.ThemedForm_MouseDown;
            this.MouseMove += this.ThemedForm_MouseMove;
            this.Activated += this.ThemedForm_Activated;
            this.Deactivate += this.ThemedForm_Deactivate;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Localizable(false)]
        public new Size Size
        {
            get => base.Size;
            set
            {
                // this._size = value;

                // the size of the Windows 10 Window frame.
                Size tmp = default;
                tmp.Width += value.Width + 2;
                tmp.Height += value.Height + 32;
                base.Size = tmp;
            }
        }

        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Size ClientSize
        {
            get => base.ClientSize;
            set
            {
                // this._size = value;

                // the size of the Windows 10 Window frame.
                Size tmp = default;
                tmp.Width += value.Width + 2;
                tmp.Height += value.Height + 32;
                base.ClientSize = tmp;
            }
        }

        protected override Size DefaultSize => new Size(300, 300);

        private Rectangle MinimizeHitbox { get; }

        private Rectangle MaximizeHitbox { get; }

        private Rectangle CloseHitbox { get; }

        private Rectangle HelpHitbox { get; }

        /*
        protected override void OnNonClientPaint(Graphics g, Size paintArea)
        {
            base.OnNonClientPaint(g, paintArea);

            // points to be used in linear gradient brush
            var p1 = new Point(0, 0);
            var p2 = new Point(paintArea.Width, 0);

            // var c1 = this.IsWindowActive ? Color.Blue : Color.SkyBlue;
            // var c2 = this.IsWindowActive ? Color.LightBlue : Color.LightSkyBlue;

            using (var brush = new LinearGradientBrush(p1, p2, ShareXResources.Theme.BackgroundColor, ShareXResources.Theme.BackgroundColor))
            {
                g.FillRectangle(brush, 0, 0, paintArea.Width, paintArea.Height);
            }

            TextRenderer.DrawText(g, this.Text, SystemFonts.CaptionFont, new Rectangle(10, 0, 256, 40),
                Color.White, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        protected override void OnPaint(PaintEventArgs e)
            => base.OnPaint(e);

        protected override void OnLoad(EventArgs e)
            => base.OnLoad(e);
        */

        private void ThemedForm_Paint(object sender, PaintEventArgs e)
        {
            var fillRect = new Rectangle(1, 1, this.Size.Width - 2, 30);
            using (var pen = new Pen(ShareXResources.Theme.BorderColor))
            using (var pen2 = new Pen(ShareXResources.Theme.TextColor))
            using (var brush = new LinearGradientBrush(fillRect, ShareXResources.Theme.LightBackgroundColor, ShareXResources.Theme.LightBackgroundColor, LinearGradientMode.Vertical))
            using (var brush2 = new LinearGradientBrush(fillRect, ShareXResources.Theme.DarkBackgroundColor, ShareXResources.Theme.DarkBackgroundColor, LinearGradientMode.Vertical))
            using (var brush3 = new LinearGradientBrush(this.MinimizeHitbox, ShareXResources.Theme.MenuHighlightColor, ShareXResources.Theme.MenuHighlightColor, LinearGradientMode.Vertical))
            using (var brush4 = new LinearGradientBrush(this.MaximizeHitbox, ShareXResources.Theme.MenuHighlightColor, ShareXResources.Theme.MenuHighlightColor, LinearGradientMode.Vertical))
            using (var brush5 = new LinearGradientBrush(this.CloseHitbox, ShareXResources.Theme.MenuHighlightColor, ShareXResources.Theme.MenuHighlightColor, LinearGradientMode.Vertical))
            using (var brush6 = new LinearGradientBrush(this.HelpHitbox, ShareXResources.Theme.MenuHighlightColor, ShareXResources.Theme.MenuHighlightColor, LinearGradientMode.Vertical))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.Size.Width - 1, this.Size.Height - 1);
                if (Equals(ActiveForm, this) && this.active)
                {
                    e.Graphics.FillRectangle(brush2, fillRect);
                }
                else
                {
                    e.Graphics.FillRectangle(brush, fillRect);
                }

                if (this.ControlBox)
                {
                    // draw the art work for the minimize, maximize, close, and help buttons.
                    if (this.MaximizeBox)
                    {
                        if (this.MinimizeBox)
                        {
                            if (this.minimizeHover)
                            {
                                e.Graphics.FillRectangle(brush3, this.MinimizeHitbox);
                            }

                            e.Graphics.DrawLine(pen2, 187, 16, 196, 16);
                        }
                        else
                        {
                            e.Graphics.DrawLine(Pens.LightGray, 187, 16, 196, 16);
                        }

                        if (this.maximizeHover)
                        {
                            e.Graphics.FillRectangle(brush4, this.MaximizeHitbox);
                        }

                        // close box is here. Sadly no check for this though.
                        if (this.closeHover)
                        {
                            e.Graphics.FillRectangle(brush5, this.CloseHitbox);
                        }

                        // TODO: Draw Close Button image.
                    }
                    else
                    {
                        if (this.MinimizeBox)
                        {
                            if (this.minimizeHover)
                            {
                                e.Graphics.FillRectangle(brush3, this.MinimizeHitbox);
                            }

                            e.Graphics.DrawLine(pen2, 187, 16, 196, 16);
                        }
                        else if (this.HelpButton)
                        {
                            if (this.helpHover)
                            {
                                e.Graphics.FillRectangle(brush6, this.HelpHitbox);
                            }

                            // TODO: Draw Help Button image.
                        }

                        // close box is here. Sadly no check for this though.
                        if (this.closeHover)
                        {
                            e.Graphics.FillRectangle(brush5, this.CloseHitbox);
                        }

                        // TODO: Draw Close Button image.
                    }
                }
            }

            if (this.ControlBox)
            {
                // icons on Windows 10 seems to always be drawn at the Location(9 (x), 7 (y))
                // and is always 16x16 in pixels at least on FixedSingle frames.
                e.Graphics.DrawIcon(this.Icon, new Rectangle(9, 7, 16, 16));

                // window text seems to always be at Location(30 (x), 17, (y))
                // at least on FixedSingle frames.
                TextRenderer.DrawText(
                    e.Graphics,
                    this.Text,
                    SystemFonts.CaptionFont,
                    new Point(30, 17),
                    ShareXResources.Theme.TextColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
            else
            {
                // todo: get where the text is located at when ControlBox is disabled.
            }

            /*
            var val = 2;
            var result = NativeMethods.DwmSetWindowAttribute(this.Handle, (int)NativeMethods.DwmWindowAttribute.DWMWA_NCRENDERING_POLICY, ref val, sizeof(int));
            if (result == 0)
            {
                var m = new NativeMethods.MARGINS { bottomHeight = 1, leftWidth = 1, rightWidth = 1, topHeight = 31 };
                var result2 = NativeMethods.DwmExtendFrameIntoClientArea(this.Handle, ref m);
                if (result2 == 0)
                {
                    // call to extend frame into client are failed.
                }
            }
            else
            {
                // call to extend frame into client are failed.
            }
            */

            // var g = e.Graphics;
            // var fillRect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            // if (fillRect.Width > 0 && fillRect.Height > 0)
            // {
            //     using (var brush = new LinearGradientBrush(fillRect, Color.FromArgb(80, 80, 80), Color.FromArgb(40, 40, 40), LinearGradientMode.Vertical))
            //     {
            //         g.FillRectangle(brush, fillRect);
            //     }
            // }
            // throw new NotImplementedException();
        }

        private void ThemedForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            foreach (Control control in this.Controls)
            {
                var tmp = new Point(control.Location.X, control.Location.Y);
                tmp.X += 1;
                tmp.Y += 31;
                control.Location = tmp;
            }

            if (!this.DesignMode)
            {
                ShareXResources.Theme = ShareXTheme.GetPresets()[0];
                ShareXResources.ApplyTheme(this);
            }

            // throw new NotImplementedException();
        }

        private void ThemedForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.ControlBox)
            {
                // todo: do what the buttons originally did.
                if (this.MinimizeHitbox.Contains(e.Location) && this.MinimizeBox)
                {
                    _ = NativeMethods.SendMessage(this.Handle, (uint)WindowsMessages.SYSCOMMAND, (int)SYSCOMMANDS.SC_MINIMIZE, 0);
                }
                else if (this.MaximizeHitbox.Contains(e.Location) && this.MaximizeBox)
                {
                    _ = NativeMethods.SendMessage(this.Handle, (uint)WindowsMessages.SYSCOMMAND, (int)SYSCOMMANDS.SC_MAXIMIZE, 0);
                }
                else if (this.HelpHitbox.Contains(e.Location) && this.HelpButton)
                {
                    _ = NativeMethods.SendMessage(this.Handle, (uint)WindowsMessages.SYSCOMMAND, (int)SYSCOMMANDS.SC_CONTEXTHELP, 0);
                }
                else if (this.CloseHitbox.Contains(e.Location))
                {
                    _ = NativeMethods.SendMessage(this.Handle, (uint)WindowsMessages.SYSCOMMAND, (int)SYSCOMMANDS.SC_CLOSE, 0);
                }
            }
        }

        private void ThemedForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.ControlBox)
            {
                this.minimizeHover = this.MinimizeHitbox.Contains(e.Location) && this.MinimizeBox;
                this.maximizeHover = this.MaximizeHitbox.Contains(e.Location) && this.MaximizeBox;
                this.helpHover = this.HelpHitbox.Contains(e.Location) && this.HelpButton;
                this.closeHover = this.CloseHitbox.Contains(e.Location);
            }
        }

        private void ThemedForm_Activated(object sender, EventArgs e)
            => this.active = true;

        private void ThemedForm_Deactivate(object sender, EventArgs e)
            => this.active = false;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ThemedForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ThemedForm";
            this.ResumeLayout(false);

        }
    }
}
