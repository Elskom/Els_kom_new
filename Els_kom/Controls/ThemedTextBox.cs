// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Controls;

using Els_kom;
using TerraFX.Interop.Windows;

internal class ThemedTextBox : TextBox
{
    private readonly Pen borderPen;

    public ThemedTextBox()
    {
        this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
        this.borderPen = new Pen(Color.FromArgb(30, 30, 30));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        base.OnPaint(e);
        this.DrawBackground(g);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.borderPen?.Dispose();
        }

        base.Dispose(disposing);
    }

    private unsafe void DrawBackground(Graphics g)
    {
        MARGINS margins = default;
        _ = Windows.DwmExtendFrameIntoClientArea((HWND)this.Handle, &margins);

        // hopefully this is correct.
        var boarderRect = new Rectangle(0, 0, margins.cxRightWidth, margins.cyBottomHeight);
        if (boarderRect.Width > 0 && boarderRect.Height > 0)
        {
            using var pen1 = new Pen(ShareXResources.Theme.BorderColor);
            g.DrawRectangle(pen1, boarderRect);
        }

#if NEVER_DEFINED
        this.DrawText(g);
#endif
    }

#if NEVER_DEFINED
    private void DrawText(Graphics g)
    {
        var tff = this.TextAlign switch
        {
            HorizontalAlignment.Right => TextFormatFlags.Top | TextFormatFlags.Right,
            HorizontalAlignment.Left => TextFormatFlags.Top | TextFormatFlags.Left,
            HorizontalAlignment.Center => TextFormatFlags.Top | TextFormatFlags.HorizontalCenter,
            _ => TextFormatFlags.Top | TextFormatFlags.Left,
        };

        if (this.WordWrap)
        {
            tff |= TextFormatFlags.WordBreak;
        }

        TextRenderer.DrawText(g, this.Text, this.Font, new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y + 1, this.ClientRectangle.Width, this.ClientRectangle.Height), Color.Black, tff);
        TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, tff);
    }
#endif
}
