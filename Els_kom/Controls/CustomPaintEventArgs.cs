// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Controls
{
    using System;
    using System.Drawing;

    // for use in the NCPaint event handler
    internal class CustomPaintEventArgs : EventArgs
    {
        internal CustomPaintEventArgs(Graphics g, Size paintArea)
        {
            this.Graphics = g;
            this.PaintArea = paintArea;
        }

        public Graphics Graphics { get; }

        public Size PaintArea { get; set; }
    }
}
