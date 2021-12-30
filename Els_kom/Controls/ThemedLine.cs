// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Controls;

internal class ThemedLine : Control
{
    /// <devdoc>
    ///     Deriving classes can override this to configure a default size for their control.
    ///     This is more efficient than setting the size in the control's constructor.
    /// </devdoc>
    protected override Size DefaultSize => new(25, 2);

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var pen1 = new Pen(ShareXResources.Theme.SeparatorDarkColor);
        using var pen2 = new Pen(ShareXResources.Theme.SeparatorLightColor);
        e.Graphics.DrawLine(/*Pens.Gray*/pen1, 0, this.Location.X, this.Width, this.Location.X);
        e.Graphics.DrawLine(/*Pens.White*/pen2, 0, this.Location.X + 1, this.Width, this.Location.X + 1);
    }
}
