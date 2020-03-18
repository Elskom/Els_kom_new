// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Controls
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    // using NativeInterface;
    internal class ThemedForm : /*Custom*/Form
    {
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
        */

        protected override void OnPaint(PaintEventArgs e)
        {
            // var g = e.Graphics;
            // var fillRect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            // if (fillRect.Width > 0 && fillRect.Height > 0)
            // {
            //     using (var brush = new LinearGradientBrush(fillRect, Color.FromArgb(80, 80, 80), Color.FromArgb(40, 40, 40), LinearGradientMode.Vertical))
            //     {
            //         g.FillRectangle(brush, fillRect);
            //     }
            // }
            base.OnPaint(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!this.DesignMode)
            {
                ShareXResources.Theme = ShareXTheme.GetPresets()[0];
                ShareXResources.ApplyTheme(this);
            }

            base.OnLoad(e);
        }
    }
}
