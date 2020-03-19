// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System.Drawing;

    internal static class Extensions
    {
        public static Rectangle LocationOffset(this Rectangle rect, int x, int y)
            => new Rectangle(rect.X + x, rect.Y + y, rect.Width, rect.Height);

        public static Rectangle SizeOffset(this Rectangle rect, int width, int height)
            => new Rectangle(rect.X, rect.Y, rect.Width + width, rect.Height + height);
    }
}
