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
        private readonly ListViewColumnSorter lvwColumnSorter;
        private int lineIndex = -1;
        private int lastLineIndex = -1;
        private ListViewItem dragOverItem;

        public ThemedListView()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage, true);
            this.AutoFillColumn = false;
            this.AutoFillColumnIndex = -1;
            this.AllowColumnSort = false;
            this.FullRowSelect = true;
            this.View = View.Details;
            this.lvwColumnSorter = new ListViewColumnSorter();
            this.ListViewItemSorter = this.lvwColumnSorter;
        }

        public delegate void ListViewItemMovedEventHandler(object sender, int oldIndex, int newIndex);

        public event ListViewItemMovedEventHandler ItemMoved;

        [DefaultValue(false)]
        public bool AutoFillColumn { get; set; }

        [DefaultValue(-1)]
        public int AutoFillColumnIndex { get; set; }

        [DefaultValue(false)]
        public bool AllowColumnSort { get; set; }

        // Note: AllowDrag also need to be true.
        [DefaultValue(false)]
        public bool AllowItemDrag { get; set; }

        [DefaultValue(false)]
        public bool DisableDeselect { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get => this.SelectedIndices.Count > 0 ? this.SelectedIndices[0] : -1;

            set
            {
                this.UnselectAll();
                if (value > -1)
                {
                    this.Items[value].Selected = true;
                }
            }
        }

        public void FillColumn(int index)
        {
            if (this.View == View.Details && this.Columns.Count > 0 && index >= -1 && index < this.Columns.Count)
            {
                if (index == -1)
                {
                    index = this.Columns.Count - 1;
                }

                var width = 0;
                for (var i = 0; i < this.Columns.Count; i++)
                {
                    if (i != index)
                    {
                        width += this.Columns[i].Width;
                    }
                }

                var columnWidth = this.ClientSize.Width - width;
                if (columnWidth > 0 && this.Columns[index].Width != columnWidth)
                {
                    this.Columns[index].Width = columnWidth;
                }
            }
        }

        public void FillLastColumn()
            => this.FillColumn(-1);

        public void Select(int index)
        {
            if (this.Items.Count > 0 && index > -1 && index < this.Items.Count)
            {
                this.SelectedIndex = index;
            }
        }

        public void SelectLast()
        {
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = this.Items.Count - 1;
            }
        }

        public void SelectSingle(ListViewItem lvi)
        {
            this.UnselectAll();

            if (lvi != null)
            {
                lvi.Selected = true;
            }
        }

        public void UnselectAll()
        {
            foreach (ListViewItem lvi in this.SelectedItems)
            {
                lvi.Selected = false;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.MultiSelect && e.Control && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem lvi in this.Items)
                {
                    lvi.Selected = true;
                }
            }

            base.OnKeyDown(e);
        }

        [DebuggerStepThrough]
        protected override void WndProc(ref Message m)
        {
            if (this.AutoFillColumn && m.Msg == (int)WindowsMessages.PAINT && !this.DesignMode)
            {
                this.FillColumn(this.AutoFillColumnIndex);
            }

            if (m.Msg == (int)WindowsMessages.ERASEBKGND)
            {
                return;
            }

            if (this.DisableDeselect && m.Msg >= (int)WindowsMessages.LBUTTONDOWN && m.Msg <= (int)WindowsMessages.MBUTTONDBLCLK)
            {
                var pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                var hit = this.HitTest(pos);
                switch (hit.Location)
                {
                    case ListViewHitTestLocations.AboveClientArea:
                    case ListViewHitTestLocations.BelowClientArea:
                    case ListViewHitTestLocations.LeftOfClientArea:
                    case ListViewHitTestLocations.RightOfClientArea:
                    case ListViewHitTestLocations.None:
                        return;
                }
            }

            base.WndProc(ref m);
            if (m.Msg == (int)WindowsMessages.PAINT && this.lineIndex >= 0)
            {
                var rc = this.Items[this.lineIndex < this.Items.Count ? this.lineIndex : this.lineIndex - 1].GetBounds(ItemBoundsPortion.Entire);
                this.DrawInsertionLine(rc.Left, rc.Right, this.lineIndex < this.Items.Count ? rc.Top : rc.Bottom);
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);
            if (this.AllowDrop && this.AllowItemDrag && e.Button == MouseButtons.Left)
            {
                this.DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            var lvi = drgevent.Data.GetData(typeof(ListViewItem)) as ListViewItem;
            if (lvi != null && lvi.ListView == this)
            {
                drgevent.Effect = DragDropEffects.Move;
                var cp = this.PointToClient(new Point(drgevent.X, drgevent.Y));
                this.dragOverItem = this.GetItemAt(cp.X, cp.Y);
                this.lineIndex = this.dragOverItem != null ? this.dragOverItem.Index : this.Items.Count;
                if (this.lineIndex != this.lastLineIndex)
                {
                    this.Invalidate();
                }

                this.lastLineIndex = this.lineIndex;
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);
            var lvi = drgevent.Data.GetData(typeof(ListViewItem)) as ListViewItem;
            if (lvi != null && lvi.ListView == this && lvi != this.dragOverItem)
            {
                var oldIndex = lvi.Index;
                int newIndex;
                if (this.dragOverItem != null)
                {
                    newIndex = this.dragOverItem.Index;
                    if (newIndex > oldIndex)
                    {
                        newIndex--;
                    }
                }
                else
                {
                    newIndex = this.Items.Count - 1;
                }

                this.Items.RemoveAt(oldIndex);
                this.Items.Insert(newIndex, lvi);
                this.OnItemMoved(oldIndex, newIndex);
            }

            this.lineIndex = this.lastLineIndex = -1;
            this.Invalidate();
        }

        protected void OnItemMoved(int oldIndex, int newIndex)
            => this.ItemMoved?.Invoke(this, oldIndex, newIndex);

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            this.lineIndex = this.lastLineIndex = -1;
            this.Invalidate();
        }

        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            base.OnColumnClick(e);
            if (this.AllowColumnSort)
            {
                if (e.Column == this.lvwColumnSorter.SortColumn)
                {
                    this.lvwColumnSorter.Order = this.lvwColumnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
                }
                else
                {
                    this.lvwColumnSorter.SortColumn = e.Column;
                    this.lvwColumnSorter.Order = SortOrder.Ascending;
                }

                // if the column is tagged as a DateTime, then sort by date
                this.lvwColumnSorter.SortByDate = this.Columns[e.Column].Tag is DateTime;
                Cursor.Current = Cursors.WaitCursor;
                this.Sort();
                Cursor.Current = Cursors.Default;
            }
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);
            foreach (ColumnHeader column in this.Columns)
            {
                column.Width = (int)Math.Round(column.Width * factor.Width);
            }
        }

        private void DrawInsertionLine(int left, int right, int y)
        {
            using (var g = this.CreateGraphics())
            {
                g.DrawLine(SystemPens.HotTrack, left, y, right - 1, y);
                var leftTriangle = new Point[] { new Point(left, y - 4), new Point(left + 7, y), new Point(left, y + 4) };
                g.FillPolygon(SystemBrushes.HotTrack, leftTriangle);
                var rightTriangle = new Point[] { new Point(right, y - 4), new Point(right - 8, y), new Point(right, y + 4) };
                g.FillPolygon(SystemBrushes.HotTrack, rightTriangle);
            }
        }
    }
}
