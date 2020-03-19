// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Controls
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using Els_kom.Enums;

    internal class ThemedListView : ListView
    {
        private bool gridlines;

        public ThemedListView()
            : base()
        {
            this.OwnerDraw = true;
            this.FillLastColumn();
            this.DrawColumnHeader += this.ThemedListView_DrawColumnHeader;
            this.DrawItem += this.ThemedListView_DrawItem;
            this.DrawSubItem += this.ThemedListView_DrawSubItem;
        }

        public new bool GridLines
        {
            get => false;

            set
            {
                if (this.gridlines != value)
                {
                    this.gridlines = value;
                }
            }
        }

        protected internal void FillLastColumn()
            => this.FillColumn();

        private void ThemedListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

            if (e.Bounds.Right < this.ClientRectangle.Right)
            {
                using (var pen = new Pen(Color.FromArgb(22, 26, 31)))
                using (var pen2 = new Pen(Color.FromArgb(56, 64, 75)))
                {
                    e.Graphics.DrawLine(pen, e.Bounds.Right - 2, e.Bounds.Top, e.Bounds.Right - 2, e.Bounds.Bottom - 1);
                    e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right - 2, e.Bounds.Bottom - 1);
                    e.Graphics.DrawLine(pen2, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
                }
            }

            // }
            // else
            // {
            //     e.DrawDefault = true;
            // }
        }

        private void ThemedListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (this.SelectedItems.Contains(e.Item))
            {
                using (Brush brush = new SolidBrush(ShareXResources.Theme.MenuHighlightColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(ShareXResources.Theme.LightBackgroundColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }

            TextRenderer.DrawText(
                e.Graphics,
                e.Item.Text,
                e.Item.Font,
                e.Bounds.LocationOffset(2, 0).SizeOffset(-4, 0),
                ShareXResources.Theme.TextColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            if (this.gridlines)
            {
                if (e.Bounds.Right < this.ClientRectangle.Right)
                {
                    // using (var pen = new Pen(Color.FromArgb(22, 26, 31)))
                    using (var pen = new Pen(Color.FromArgb(255, 0, 255)))
                    using (var pen2 = new Pen(Color.FromArgb(56, 64, 75)))
                    {
                        e.Graphics.DrawLine(pen, e.Bounds.Right - 2, e.Bounds.Top, e.Bounds.Right - 2, e.Bounds.Bottom - 1);
                        e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 3, e.Bounds.Right - 3, e.Bounds.Bottom - 3);
                        e.Graphics.DrawLine(pen2, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
                    }
                }
            }

            // e.DrawDefault = true;
        }

        private void ThemedListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (this.SelectedItems.Contains(e.Item))
            {
                using (Brush brush = new SolidBrush(ShareXResources.Theme.MenuHighlightColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(ShareXResources.Theme.LightBackgroundColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }

            TextRenderer.DrawText(
                e.Graphics,
                e.SubItem.Text,
                e.SubItem.Font,
                e.Bounds.LocationOffset(2, 0).SizeOffset(-4, 0),
                ShareXResources.Theme.TextColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            if (this.gridlines)
            {
                if (e.SubItem.Bounds.Right < this.ClientRectangle.Right)
                {
                    // using (var pen = new Pen(Color.FromArgb(22, 26, 31)))
                    using (var pen = new Pen(Color.FromArgb(255, 0, 255)))
                    using (var pen2 = new Pen(Color.FromArgb(56, 64, 75)))
                    {
                        e.Graphics.DrawLine(pen, e.Bounds.Right - 2, e.Bounds.Top, e.Bounds.Right - 2, e.Bounds.Bottom - 1);
                        e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 3, e.Bounds.Right - 3, e.Bounds.Bottom - 3);
                        e.Graphics.DrawLine(pen2, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
                    }
                }
            }

            // e.DrawDefault = true;
        }

        private void FillColumn()
        {
            if (this.Columns.Count - 1 >= 0)
            {
                var allHeaderSizes = 0;
                foreach (ColumnHeader columns in this.Columns)
                {
                    allHeaderSizes += columns.Width;
                }

                // attempt to fill the last collumn.
                if (allHeaderSizes < this.ClientRectangle.Right)
                {
                    this.Columns[this.Columns.Count - 1].Width += this.ClientRectangle.Width - allHeaderSizes;
                }
            }
        }
    }
}
