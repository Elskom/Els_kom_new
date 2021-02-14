// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Controls
{
    using System.Drawing;
    using System.Windows.Forms;

    internal class ThemedListView : ListView
    {
        private bool gridlines;

        public ThemedListView()
        {
            this.OwnerDraw = true;

            // this.FillLastColumn()
            this.DrawColumnHeader += this.ThemedListView_DrawColumnHeader;
            this.DrawItem += this.ThemedListView_DrawItem;
            this.DrawSubItem += this.ThemedListView_DrawSubItem;
        }

        public new bool GridLines
        {
            get => this.gridlines;

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
                using var pen = new Pen(Color.FromArgb(22, 26, 31));
                using var pen2 = new Pen(Color.FromArgb(56, 64, 75));
                e.Graphics.DrawLine(pen, e.Bounds.Right - 2, e.Bounds.Top, e.Bounds.Right - 2, e.Bounds.Bottom - 1);
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right - 2, e.Bounds.Bottom - 1);
                e.Graphics.DrawLine(pen2, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
            }
        }

        private void ThemedListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            this.FillRectangle(e.Item, e.Bounds, e.Graphics);
            TextRenderer.DrawText(
                e.Graphics,
                e.Item.Text,
                e.Item.Font,
                e.Bounds.LocationOffset(2, 0).SizeOffset(-4, 0),
                ShareXResources.Theme.TextColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            this.DrawGrid(e.Bounds, e.Bounds, e.Graphics);
        }

        private void ThemedListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            this.FillRectangle(e.Item, e.Bounds, e.Graphics);
            TextRenderer.DrawText(
                e.Graphics,
                e.SubItem.Text,
                e.SubItem.Font,
                e.Bounds.LocationOffset(2, 0).SizeOffset(-4, 0),
                ShareXResources.Theme.TextColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            this.DrawGrid(e.SubItem.Bounds, e.Bounds, e.Graphics);
        }

        private void FillRectangle(ListViewItem item, Rectangle rectangle, Graphics graphics)
        {
            if (this.SelectedItems.Contains(item))
            {
                using Brush brush = new SolidBrush(ShareXResources.Theme.MenuHighlightColor);
                graphics.FillRectangle(brush, rectangle);
            }
            else
            {
                using Brush brush = new SolidBrush(ShareXResources.Theme.LightBackgroundColor);
                graphics.FillRectangle(brush, rectangle);
            }
        }

        private void DrawGrid(Rectangle rect1, Rectangle rect2, Graphics graphics)
        {
            if (this.gridlines && rect1.Right < this.ClientRectangle.Right)
            {
                // using (var pen = new Pen(Color.FromArgb(22, 26, 31)))
                using var pen = new Pen(Color.FromArgb(255, 0, 255));
                using var pen2 = new Pen(Color.FromArgb(56, 64, 75));
                graphics.DrawLine(pen, rect2.Right - 2, rect2.Top, rect2.Right - 2, rect2.Bottom - 1);
                graphics.DrawLine(pen, rect2.Left, rect2.Bottom - 3, rect2.Right - 3, rect2.Bottom - 3);
                graphics.DrawLine(pen2, rect2.Right - 1, rect2.Top, rect2.Right - 1, rect2.Bottom - 1);
            }
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
                    this.Columns[^1].Width += this.ClientRectangle.Width - allHeaderSizes;
                }
            }
        }
    }
}
