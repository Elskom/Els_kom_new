// Copyright (c) 2014-2023, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Controls;

using System.ComponentModel;

public class MenuButton : Button
{
    [DefaultValue(null)]
    public ContextMenuStrip? Menu { get; set; }

    [DefaultValue(false)]
    public bool ShowMenuUnderCursor { get; set; }

    protected override void OnMouseDown(MouseEventArgs mevent)
    {
        base.OnMouseDown(mevent);
        if (this.Menu != null && mevent.Button == MouseButtons.Left)
        {
            var menuLocation = this.ShowMenuUnderCursor ? mevent.Location : new Point(0, this.Height);
            this.Menu.Show(this, menuLocation);
        }
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        if (this.Menu != null)
        {
            var arrowX = this.ClientRectangle.Width - 14;
            var arrowY = (this.ClientRectangle.Height / 2) - 1;
            var color = this.Enabled ? this.ForeColor : SystemColors.ControlDark;
            using Brush brush = new SolidBrush(color);
            var arrows = new[] { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
            pevent.Graphics.FillPolygon(brush, arrows);
        }
    }
}
