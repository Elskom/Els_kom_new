// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System.Drawing;
    using System.Windows.Forms;

    internal static class Extensions
    {
        public static Rectangle LocationOffset(this Rectangle rect, int x, int y)
            => new Rectangle(rect.X + x, rect.Y + y, rect.Width, rect.Height);

        public static Rectangle SizeOffset(this Rectangle rect, int width, int height)
            => new Rectangle(rect.X, rect.Y, rect.Width + width, rect.Height + height);

        public static void SupportDarkTheme(this ListView lv)
        {
            if (!lv.OwnerDraw)
            {
                lv.OwnerDraw = true;

                lv.DrawItem += (sender, e) =>
                {
                    e.DrawDefault = true;
                };

                lv.DrawSubItem += (sender, e) =>
                {
                    e.DrawDefault = true;
                };

                lv.DrawColumnHeader += (sender, e) =>
                {
                    // if (ShareXResources.UseDarkTheme)
                    // {
                    using (Brush brush = new SolidBrush(ShareXResources.Theme.BackgroundColor))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }

                    TextRenderer.DrawText(
                        e.Graphics,
                        e.Header.Text,
                        e.Font,
                        e.Bounds.LocationOffset(2, 0).SizeOffset(-4, 0),
                        ShareXResources.Theme.TextColor,
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                    if (e.Bounds.Right < lv.ClientRectangle.Right)
                    {
                        using (var pen = new Pen(Color.FromArgb(22, 26, 31)))
                        using (var pen2 = new Pen(Color.FromArgb(56, 64, 75)))
                        {
                            e.Graphics.DrawLine(pen, e.Bounds.Right - 2, e.Bounds.Top, e.Bounds.Right - 2, e.Bounds.Bottom - 1);
                            e.Graphics.DrawLine(pen2, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
                        }
                    }

                    // }
                    // else
                    // {
                    //     e.DrawDefault = true;
                    // }
                };
            }
        }
    }
}
