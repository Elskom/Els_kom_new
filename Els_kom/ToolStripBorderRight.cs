// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System.Drawing;
    using System.Windows.Forms;

    public class ToolStripBorderRight : ToolStrip
    {
        public bool DrawCustomBorder { get; set; } = true;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.DrawCustomBorder)
            {
                using (var pen = new Pen(ProfessionalColors.SeparatorDark))
                {
                    e.Graphics.DrawLine(pen, new Point(this.ClientSize.Width - 1, 0), new Point(this.ClientSize.Width - 1, this.ClientSize.Height - 1));
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
            => base.OnPaintBackground(e);
    }
}
