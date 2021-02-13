// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System;
    using System.Drawing;
    using System.IO;

    internal static class Extensions
    {
        public static Rectangle LocationOffset(this Rectangle rect, int x, int y)
            => new(rect.X + x, rect.Y + y, rect.Width, rect.Height);

        public static Rectangle SizeOffset(this Rectangle rect, int width, int height)
            => new(rect.X, rect.Y, rect.Width + width, rect.Height + height);

        public static byte[] ToByteArray(this Icon icon)
        {
            if (icon == null)
            {
                return Array.Empty<byte>();
            }

            using (var ms = new MemoryStream())
            {
                icon.Save(ms);
                return ms.ToArray();
            }
        }

        public static string ToHexString(this Color color)
            => $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }
}
