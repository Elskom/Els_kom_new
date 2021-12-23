// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using TerraFX.Interop.Windows;

    internal class ThemedForm : Form
    {
        /*
        private bool minimizeHover;
        private bool maximizeHover;
        private bool closeHover;
        private bool helpHover;
        private bool minimizeClicked;
        private bool maximizeClicked;
        private bool closeClicked;
        private bool helpClicked;
        private bool active;
        private bool mouseDown;
        */
        private ContextMenuStrip systemMenuStrip;
        private bool active;

        // private bool menuShown;
        /*
        private Point lastLocation;
        private bool menuShown;

        // state bitmaps.
        private Bitmap minimizeBitmap;
        private Bitmap minimizeHoverBitmap;
        private Bitmap minimizeDisabledBitmap;
        private Bitmap maximizeBitmap;
        private Bitmap maximizeHoverBitmap;
        private Bitmap maximizeDisabledBitmap;
        private Bitmap closeBitmap;
        private Bitmap closeHoverBitmap;
        private Bitmap closeClickedBitmap;
        private Bitmap helpBitmap;
        private Bitmap helpHoverBitmap;
        private Bitmap iconBitmap;
        private Icon lastIcon;
        */

        internal ThemedForm()
        {
            this.SuspendLayout();

            // ThemedForm
            this.ClientSize = new Size(300, 300);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "ThemedForm";
            this.ResumeLayout(false);

            // These seems to be the correct locations for the caption buttons in Windows 10.
            // this.MinimizeHitbox = new Rectangle(this.Size.Width - 136, 1, 45, 29);
            // this.MaximizeHitbox = new Rectangle(this.Size.Width - 90, 1, 45, 29);
            // this.CloseHitbox = new Rectangle(this.Size.Width - 44, 1, 45, 29);
            // this.HelpHitbox = new Rectangle(this.Size.Width - 90, 1, 45, 29);
            //
            // these are fixed points never changeing.
            // this.IconHitbox = new Rectangle(9, 7, 16, 16);
            //
            // the caption in the title bar. We need this to set the bit to make form moveable again.
            // this.CaptionHitbox = new Rectangle(0, 0, this.Size.Width, 31);
            // this.Paint += this.ThemedForm_Paint;
            this.Load += this.ThemedForm_Load;
            /*
            this.MouseUp += this.ThemedForm_MouseUp;
            this.MouseDown += this.ThemedForm_MouseDown;
            this.MouseMove += this.ThemedForm_MouseMove;
            this.MouseLeave += this.ThemedForm_MouseLeave;
            this.Activated += this.ThemedForm_Activated;
            this.Deactivate += this.ThemedForm_Deactivate;
            */
            if (ShareXResources.Theme != ShareXTheme.GetPresets()[0])
            {
                ShareXResources.Theme = ShareXTheme.GetPresets()[0];
            }

            // create state bitmaps.
            /*
            this.CreateBitmap(ref this.minimizeBitmap, this.MinimizeHitbox, true, false);
            this.CreateBitmap(ref this.minimizeHoverBitmap, this.MinimizeHitbox, true, true);
            this.CreateBitmap(ref this.minimizeDisabledBitmap, this.MinimizeHitbox, false, false);
            this.CreateBitmap(ref this.maximizeBitmap, this.MaximizeHitbox, true, false);
            this.CreateBitmap(ref this.maximizeHoverBitmap, this.MaximizeHitbox, true, true);
            this.CreateBitmap(ref this.maximizeDisabledBitmap, this.MaximizeHitbox, false, false);
            this.CreateBitmap(ref this.closeBitmap, this.CloseHitbox, true, false);
            this.CreateBitmap(ref this.closeHoverBitmap, this.CloseHitbox, true, true);
            this.CreateBitmap(ref this.closeClickedBitmap, this.CloseHitbox, true, false, false, true);
            this.CreateBitmap(ref this.helpBitmap, this.HelpHitbox, true, false, false);
            this.CreateBitmap(ref this.helpHoverBitmap, this.HelpHitbox, true, true, false);
            this.CreateBitmap(ref this.iconBitmap, this.IconHitbox, true, false);
            */
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Localizable(false)]
        public Container Components { get; } = new Container();

        /*
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Localizable(false)]
        public new Size Size
        {
            get => base.Size;
            set
            {
                // this._size = value

                // the size of the Windows 10 Window frame.
                Size tmp = default;
                tmp.Width += value.Width + 2;
                tmp.Height += value.Height + 32;
                var numchanged = value.Width - this.DefaultSize.Width;
                var captionrect = this.CaptionHitbox;
                var minrect = this.MinimizeHitbox;
                var maxrect = this.MaximizeHitbox;
                var closerect = this.CloseHitbox;
                var helprect = this.HelpHitbox;
                captionrect.Width = tmp.Width;
                this.CaptionHitbox = captionrect;
                minrect.X = this.DefaultSize.Width + numchanged - 136;
                this.MinimizeHitbox = minrect;
                maxrect.X = this.DefaultSize.Width + numchanged - 90;
                this.MaximizeHitbox = maxrect;
                closerect.X = this.DefaultSize.Width + numchanged - 44;
                this.CloseHitbox = closerect;
                helprect.X = this.DefaultSize.Width + numchanged - 90;
                this.HelpHitbox = helprect;
                base.Size = tmp;
            }
        }

        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Size ClientSize
        {
            get => base.ClientSize;
            set
            {
                // this._size = value

                // the size of the Windows 10 Window frame.
                Size tmp = default;
                tmp.Width += value.Width + 2;
                tmp.Height += value.Height + 32;
                var numchanged = value.Width - this.DefaultSize.Width;
                var captionrect = this.CaptionHitbox;
                var minrect = this.MinimizeHitbox;
                var maxrect = this.MaximizeHitbox;
                var closerect = this.CloseHitbox;
                var helprect = this.HelpHitbox;
                captionrect.Width = tmp.Width;
                this.CaptionHitbox = captionrect;
                minrect.X = this.DefaultSize.Width + numchanged - 136;
                this.MinimizeHitbox = minrect;
                maxrect.X = this.DefaultSize.Width + numchanged - 90;
                this.MaximizeHitbox = maxrect;
                closerect.X = this.DefaultSize.Width + numchanged - 44;
                this.CloseHitbox = closerect;
                helprect.X = this.DefaultSize.Width + numchanged - 90;
                this.HelpHitbox = helprect;
                base.ClientSize = tmp;
            }
        }

        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Icon Icon
        {
            get => base.Icon;

            set
            {
                base.Icon = value;
                // this.iconBitmap?.Dispose();
                // this.iconBitmap = null;
                // this.CreateBitmap(ref this.iconBitmap, this.IconHitbox, true, false);
                // this.Invalidate(this.IconHitbox);
           }
        }
        */

        protected override Size DefaultSize => new(300, 300);

        /*
        private Rectangle MinimizeHitbox { get; set; }

        private Rectangle MaximizeHitbox { get; set; }

        private Rectangle CloseHitbox { get; set; }

        private Rectangle HelpHitbox { get; set; }

        private Rectangle CaptionHitbox { get; set; }

        private Rectangle IconHitbox { get; set; }
        */

        internal static Image GetNativeMenuItemImage(HBITMAP hBitmap, bool enabled)
        {
            if (hBitmap != IntPtr.Zero && hBitmap > HBMMENU.HBMMENU_POPUP_MINIMIZE)
            {
                var image = Image.FromHbitmap(hBitmap);
                for (var x = 0; x < image.Width; x++)
                {
                    for (var y = 0; y < image.Height; y++)
                    {
                        var color = image.GetPixel(x, y);
                        if (color.A != (byte)0)
                        {
                            image.SetPixel(x, y, Color.FromArgb(color.A, Color.White));
                        }
                    }
                }

                return image;
            }
            else
            {
                // its a system defined bitmap
                var buttonToUse = (CaptionButton)(-1);
                if (hBitmap == HBMMENU.HBMMENU_MBAR_CLOSE
                    || hBitmap == HBMMENU.HBMMENU_MBAR_CLOSE_D
                    || hBitmap == HBMMENU.HBMMENU_POPUP_CLOSE)
                {
                    buttonToUse = CaptionButton.Close;
                }
                else if (hBitmap == HBMMENU.HBMMENU_MBAR_MINIMIZE
                    || hBitmap == HBMMENU.HBMMENU_MBAR_MINIMIZE_D
                    || hBitmap == HBMMENU.HBMMENU_POPUP_MINIMIZE)
                {
                    buttonToUse = CaptionButton.Minimize;
                }
                else if (hBitmap == HBMMENU.HBMMENU_MBAR_RESTORE
                    || hBitmap == HBMMENU.HBMMENU_POPUP_RESTORE)
                {
                    buttonToUse = CaptionButton.Restore;
                }
                else if (hBitmap == HBMMENU.HBMMENU_POPUP_MAXIMIZE)
                {
                    buttonToUse = CaptionButton.Maximize;
                }

                if ((int)buttonToUse > -1)
                {
                    // we've mapped to a system defined bitmap we know how to draw
                    var image = new Bitmap(16, 16);
                    using (var g = Graphics.FromImage(image))
                    {
                        ControlPaint.DrawCaptionButton(g, new Rectangle(Point.Empty, image.Size), buttonToUse, ButtonState.Flat);
                        g.DrawRectangle(SystemPens.Control, 0, 0, image.Width - 1, image.Height - 1);
                    }

                    for (var x = 0; x < image.Width; x++)
                    {
                        for (var y = 0; y < image.Height; y++)
                        {
                            var color = image.GetPixel(x, y);
                            if (color != SystemColors.Control && color != Color.FromArgb(255, 240, 240, 240) && enabled)
                            {
                                image.SetPixel(x, y, Color.White);
                            }
                        }
                    }

                    image.MakeTransparent(SystemColors.Control);
                    return image;
                }
            }

            return null;
        }

        protected override unsafe void WndProc(ref Message m)
        {
            if ((m.Msg is WM.WM_SYSCOMMAND
                && m.WParam.ToInt32() is SC.SC_MOUSEMENU or SC.SC_KEYMENU
                /* Currently is not what I expect to happen on Windows 11 instead of SC_MOUSEMENU.*/
                or 61587)
                /* Undocumented but works for "Taskbar right clicks". */
                || m.Msg is 0x313
                /* To handle right click in NC area. */
                || m.Msg is WM.WM_CONTEXTMENU)
            {
                this.systemMenuStrip.Show(Windows.GET_X_LPARAM(m.LParam), Windows.GET_Y_LPARAM(m.LParam));
                return;
            }
            else if (m.Msg is WM.WM_NCPAINT)
            {
                if (this.active)
                {
                    var tbInfo = GetTitleBarInfo((HWND)m.HWnd);
                    using var graphics = Graphics.FromHwnd(m.HWnd);
                    using var pen = new Pen(ShareXResources.Theme.BorderColor);

                    // draw on the "Title Bar".
                    // for some reason I cant see it though on Windows 11.
                    graphics.DrawRectangle(pen, Rectangle.Inflate(tbInfo.Items[0].Bounds, -1, -1));

                    // TODO: Get bounds of the NC area TEXT and paint over it in white.
                    // return;
                }
            }
            else if (m.Msg is WM.WM_NCACTIVATE)
            {
                this.active = m.WParam.ToInt32().Equals(Convert.ToInt32(true));
                var tbInfo = GetTitleBarInfo((HWND)m.HWnd);
                fixed (RECT* pRect = &tbInfo.Items[0].Rect)
                {
                    _ = Windows.InvalidateRect((HWND)m.HWnd, pRect, true);
                }

                if (this.active)
                {
                    return;
                }
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP007:Don't dispose injected.", Justification = "Needed to not leak the memory from the Icons.")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.systemMenuStrip.Dispose();
                this.Components?.Dispose();

                // this.minimizeBitmap?.Dispose();
                // this.minimizeHoverBitmap?.Dispose();
                // this.minimizeDisabledBitmap?.Dispose();
                // this.maximizeBitmap?.Dispose();
                // this.maximizeHoverBitmap?.Dispose();
                // this.maximizeDisabledBitmap?.Dispose();
                // this.closeBitmap?.Dispose();
                // this.closeHoverBitmap?.Dispose();
                // this.closeClickedBitmap?.Dispose();
                // this.helpBitmap?.Dispose();
                // this.helpHoverBitmap?.Dispose();
                // this.iconBitmap?.Dispose();
                // this.lastIcon?.Dispose()
            }

            base.Dispose(disposing);
        }

        /*
        private static void CheckBitmapIfNull(ref Bitmap bitmap, string message)
        {
            if (bitmap == null)
            {
                MessageBox.Show(message, "Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP003:Dispose previous before re-assigning.", Justification = "Does not need to be disposed of first.")]
        private void ThemedForm_Paint(object sender, PaintEventArgs e)
        {
            var fillRect = new Rectangle(1, 1, this.Size.Width - 2, 30);
            using (var pen = new Pen(ShareXResources.Theme.BorderColor))
            using (var brush = new LinearGradientBrush(fillRect, ShareXResources.Theme.LightBackgroundColor, ShareXResources.Theme.LightBackgroundColor, LinearGradientMode.Vertical))
            using (var brush2 = new LinearGradientBrush(fillRect, ShareXResources.Theme.DarkBackgroundColor, ShareXResources.Theme.DarkBackgroundColor, LinearGradientMode.Vertical))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.Size.Width - 1, this.Size.Height - 1);
                if (this.active)
                {
                    e.Graphics.FillRectangle(brush2, fillRect);
                }
                else
                {
                    e.Graphics.FillRectangle(brush, fillRect);
                }

                if (this.ControlBox)
                {
                    // draw the art work for the minimize, maximize, close, and help buttons.
                    if (this.MaximizeBox)
                    {
                        if (this.MinimizeBox)
                        {
                            if (this.minimizeHover)
                            {
                                CheckBitmapIfNull(ref this.minimizeHoverBitmap, "Minimize Hover Bitmap is null.");
                                e.Graphics.DrawImage(this.minimizeHoverBitmap, this.MinimizeHitbox);
                            }
                            else
                            {
                                CheckBitmapIfNull(ref this.minimizeDisabledBitmap, "Minimize Bitmap is null.");
                                e.Graphics.DrawImage(this.minimizeBitmap, this.MinimizeHitbox);
                            }
                        }
                        else
                        {
                            CheckBitmapIfNull(ref this.minimizeDisabledBitmap, "Minimize Disabled Bitmap is null.");
                            e.Graphics.DrawImage(this.minimizeDisabledBitmap, this.MinimizeHitbox);
                        }

                        if (this.maximizeHover)
                        {
                            CheckBitmapIfNull(ref this.maximizeHoverBitmap, "Maximize Hover bitmap is null.");
                            e.Graphics.DrawImage(this.maximizeHoverBitmap, this.MaximizeHitbox);
                        }
                        else
                        {
                            CheckBitmapIfNull(ref this.maximizeBitmap, "Maximize bitmap is null.");
                            e.Graphics.DrawImage(this.maximizeBitmap, this.MaximizeHitbox);
                        }

                        // close box is here. Sadly no check for this though.
                        if (this.closeHover)
                        {
                            CheckBitmapIfNull(ref this.closeHoverBitmap, "Close Hover bitmap is null.");
                            e.Graphics.DrawImage(this.closeHoverBitmap, this.CloseHitbox);
                        }
                        else if (this.closeClicked)
                        {
                            CheckBitmapIfNull(ref this.closeClickedBitmap, "Close Clicked bitmap is null.");
                            e.Graphics.DrawImage(this.closeClickedBitmap, this.CloseHitbox);
                        }
                        else
                        {
                            CheckBitmapIfNull(ref this.closeBitmap, "Close bitmap is null.");
                            e.Graphics.DrawImage(this.closeBitmap, this.CloseHitbox);
                        }
                    }
                    else
                    {
                        if (this.MinimizeBox)
                        {
                            if (this.minimizeHover)
                            {
                                CheckBitmapIfNull(ref this.minimizeHoverBitmap, "Minimize Hover bitmap is null.");
                                e.Graphics.DrawImage(this.minimizeHoverBitmap, this.MinimizeHitbox);
                            }
                            else
                            {
                                CheckBitmapIfNull(ref this.minimizeBitmap, "Minimize bitmap is null.");
                                e.Graphics.DrawImage(this.minimizeBitmap, this.MinimizeHitbox);
                            }

                            CheckBitmapIfNull(ref this.maximizeDisabledBitmap, "Maximize Disabled bitmap is null.");
                            e.Graphics.DrawImage(this.maximizeDisabledBitmap, this.MaximizeHitbox);
                        }
                        else if (this.HelpButton)
                        {
                            if (this.helpHover)
                            {
                                CheckBitmapIfNull(ref this.helpHoverBitmap, "Help Hover bitmap is null.");
                                e.Graphics.DrawImage(this.helpHoverBitmap, this.HelpHitbox);
                            }
                            else
                            {
                                CheckBitmapIfNull(ref this.helpBitmap, "Help bitmap is null.");
                                e.Graphics.DrawImage(this.helpBitmap, this.HelpHitbox);
                            }
                        }

                        // close box is here. Sadly no check for this though.
                        if (this.closeHover)
                        {
                            CheckBitmapIfNull(ref this.closeHoverBitmap, "Close Hover bitmap is null.");
                            e.Graphics.DrawImage(this.closeHoverBitmap, this.CloseHitbox);
                        }
                        else if (this.closeClicked)
                        {
                            CheckBitmapIfNull(ref this.closeClickedBitmap, "Close Clicked bitmap is null.");
                            e.Graphics.DrawImage(this.closeClickedBitmap, this.CloseHitbox);
                        }
                        else
                        {
                            CheckBitmapIfNull(ref this.closeBitmap, "Close bitmap is null.");
                            e.Graphics.DrawImage(this.closeBitmap, this.CloseHitbox);
                        }
                    }
                }
            }

            if (this.ControlBox)
            {
                // updates iconBitmap if it changed, otherwise resets it back to the old one anyway.
                this.CreateBitmap(ref this.iconBitmap, this.IconHitbox, true, false);

                // icons on Windows 10 seems to always be drawn at the Location(9 (x), 7 (y))
                // and is always 16x16 in pixels at least on FixedSingle frames.
                e.Graphics.DrawImage(this.iconBitmap, this.IconHitbox);

                // window text seems to always be at Location(30 (x), 17, (y))
                // at least on FixedSingle frames.
                TextRenderer.DrawText(
                    e.Graphics,
                    this.Text,
                    SystemFonts.CaptionFont,
                    new Point(30, 17),
                    ShareXResources.Theme.TextColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
            else
            {
                // TODO: get where the text is located at when ControlBox is disabled.
            }
        }
        */

        private void ThemedForm_Load(object sender, EventArgs e)
        {
            // this.FormBorderStyle = FormBorderStyle.None;
            // foreach (Control control in this.Controls)
            // {
            //     var tmp = new Point(control.Location.X, control.Location.Y);
            //     tmp.X += 1;
            //     tmp.Y += 31;
            //     control.Location = tmp;
            // }
            if (!this.DesignMode)
            {
                ShareXResources.ApplyTheme(this, !this.DesignMode);

                // is null so we need to create one.
                // get the system's menu and copy the data to a ContextMenuStrip.
                var hmenu = Windows.GetSystemMenu((HWND)this.Handle, false);
                this.AdjustSystemMenu(hmenu);
                var mENUITEMINFOs = GetMenuInfo(hmenu);
                this.systemMenuStrip = GetContextMenu(mENUITEMINFOs, this.Components, this);

                // this.systemMenuStrip.Opened += this.SystemMenuStrip_Opened;
                // this.systemMenuStrip.Closed += this.SystemMenuStrip_Closed;

                // theme this dark or whatever colors the theme is.
                ShareXResources.ApplyDarkThemeToControl(this.systemMenuStrip);
            }
        }

        /*
        private void ThemedForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDown = false;
            this.minimizeHover = false;
            this.maximizeHover = false;
            this.closeHover = false;
            this.helpHover = false;
            if (this.CaptionHitbox.Contains(e.Location))
            {
                if (this.ControlBox)
                {
                    if (this.MinimizeHitbox.Contains(e.Location) && this.MinimizeBox && this.minimizeClicked)
                    {
                        this.Capture = false;
                        var result = NativeMethods.SendMessageW(new HandleRef(this, this.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_MINIMIZE, IntPtr.Zero);
                        Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                        this.Capture = true;
                    }
                    else if (this.MaximizeHitbox.Contains(e.Location) && this.MaximizeBox && this.maximizeClicked)
                    {
                        // maximize disabled state is handled by the helphitbox one to avoid breaking the helpbox when it is enabled.
                        this.Capture = false;
                        var result = NativeMethods.SendMessageW(new HandleRef(this, this.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_MAXIMIZE, IntPtr.Zero);
                        Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                        this.Capture = true;
                    }
                    else if (this.HelpHitbox.Contains(e.Location) && this.HelpButton && this.helpClicked)
                    {
                        this.Capture = false;
                        var result = NativeMethods.SendMessageW(new HandleRef(this, this.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_CONTEXTHELP, IntPtr.Zero);
                        Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                        this.Capture = true;
                    }
                    else if (this.CloseHitbox.Contains(e.Location) && this.closeClicked)
                    {
                        this.Capture = false;
                        var result = NativeMethods.SendMessageW(new HandleRef(this, this.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_CLOSE, IntPtr.Zero);
                        Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                        if (!this.IsDisposed)
                        {
                            this.Capture = true;
                        }
                    }
                    else
                    {
                        this.minimizeClicked = false;
                        this.maximizeClicked = false;
                        this.closeClicked = false;
                        this.helpClicked = false;
                    }
                }
                else
                {
                    this.minimizeClicked = false;
                    this.maximizeClicked = false;
                    this.closeClicked = false;
                    this.helpClicked = false;
                }
            }
        }

        private void ThemedForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.CaptionHitbox.Contains(e.Location))
            {
                if (this.ControlBox)
                {
                    if (this.MinimizeHitbox.Contains(e.Location) && this.MinimizeBox)
                    {
                        this.minimizeClicked = true;
                    }
                    else if (this.MinimizeHitbox.Contains(e.Location))
                    {
                        // must handle this disabled state to avoid bugs.
                    }
                    else if (this.MaximizeHitbox.Contains(e.Location) && this.MaximizeBox)
                    {
                        // maximize disabled state is handled by the helphitbox one to avoid breaking the helpbox when it is enabled.
                        this.maximizeClicked = true;
                    }
                    else if (this.HelpHitbox.Contains(e.Location) && this.HelpButton)
                    {
                        this.helpClicked = true;
                    }
                    else if (this.HelpHitbox.Contains(e.Location))
                    {
                        // must handle this disabled state to avoid bugs.
                    }
                    else if (this.CloseHitbox.Contains(e.Location))
                    {
                        this.closeClicked = true;
                        this.Invalidate(this.CloseHitbox);
                    }
                    else if (this.IconHitbox.Contains(e.Location))
                    {
                        // todo: pop up the system menu like before.
                        if (this.IsHandleCreated && e.Button == MouseButtons.Left)
                        {
                            if (!this.menuShown)
                            {
                                this.SystemMenuStrip.Show(this, e.Location.X, e.Location.Y);
                            }
                            else
                            {
                                this.Capture = false;
                                var result = NativeMethods.SendMessageW(new HandleRef(this, this.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_CLOSE, IntPtr.Zero);
                                Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                                if (!this.IsDisposed)
                                {
                                    this.Capture = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        // now we make the form move now that we verified that none of the standard buttons was clicked on.
                        this.mouseDown = true;
                        this.lastLocation = e.Location;
                    }
                }
                else
                {
                    // now we make the form move now that we verified that none of the standard buttons was clicked on.
                    this.mouseDown = true;
                    this.lastLocation = e.Location;
                }
            }
        }

        private void ThemedForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.CaptionHitbox.Contains(e.Location) && this.ControlBox)
            {
                if (this.MinimizeHitbox.Contains(e.Location) && this.MinimizeBox)
                {
                    if (!this.minimizeHover)
                    {
                        this.minimizeHover = true;
                        this.Invalidate(this.MinimizeHitbox);
                    }
                }
                else
                {
                    if (this.minimizeHover)
                    {
                        this.minimizeHover = false;
                        this.Invalidate(this.MinimizeHitbox);
                    }
                }

                if (this.MaximizeHitbox.Contains(e.Location) && this.MaximizeBox)
                {
                    if (!this.maximizeHover)
                    {
                        this.maximizeHover = true;
                        this.Invalidate(this.MaximizeHitbox);
                    }
                }
                else
                {
                    if (this.maximizeHover)
                    {
                        this.maximizeHover = false;
                        this.Invalidate(this.MaximizeHitbox);
                    }
                }

                if (this.HelpHitbox.Contains(e.Location) && this.HelpButton)
                {
                    if (!this.helpHover)
                    {
                        this.helpHover = true;
                        this.Invalidate(this.HelpHitbox);
                    }
                }
                else
                {
                    if (this.helpHover)
                    {
                        this.helpHover = false;
                        this.Invalidate(this.HelpHitbox);
                    }
                }

                if (this.CloseHitbox.Contains(e.Location))
                {
                    if (!this.closeHover)
                    {
                        this.closeHover = true;
                        this.Invalidate(this.CloseHitbox);
                    }
                }
                else
                {
                    if (this.closeHover)
                    {
                        this.closeHover = false;
                        this.Invalidate(this.CloseHitbox);
                    }
                }

                if (this.mouseDown)
                {
                    this.Location = new Point(
                        this.Location.X - this.lastLocation.X + e.X, this.Location.Y - this.lastLocation.Y + e.Y);
                }
            }
        }

        private void ThemedForm_MouseLeave(object sender, EventArgs e)
        {
            if (this.mouseDown)
            {
                this.mouseDown = false;
            }

            if (this.minimizeHover)
            {
                this.minimizeHover = false;
                this.Invalidate(this.MinimizeHitbox);
            }

            if (this.maximizeHover)
            {
                this.maximizeHover = false;
                this.Invalidate(this.MaximizeHitbox);
            }

            if (this.closeHover)
            {
                this.closeHover = false;
                this.Invalidate(this.CloseHitbox);
            }

            if (this.helpHover)
            {
                this.helpHover = false;
                this.Invalidate(this.HelpHitbox);
            }
        }

        private void ThemedForm_Activated(object sender, EventArgs e)
        {
            Debug.WriteLine("Activated");
            this.active = true;
            this.Invalidate();
        }

        private void ThemedForm_Deactivate(object sender, EventArgs e)
        {
            Debug.WriteLine("Deactivated");
            this.active = false;
            this.Invalidate();
        }
        */

        /*
        private void SystemMenuStrip_Opened(object sender, EventArgs e)
            => this.menuShown = true;

        private void SystemMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
            => this.menuShown = false;
        */

        private void AdjustSystemMenu(HMENU hmenu)
        {
            // UpdateWindowState()
            var winState = this.WindowState;
            var borderStyle = this.FormBorderStyle;
            var sizableBorder = borderStyle is FormBorderStyle.SizableToolWindow or FormBorderStyle.Sizable;
            var showMin = this.MinimizeBox && winState != FormWindowState.Minimized;
            var showMax = this.MaximizeBox && winState != FormWindowState.Maximized;
            var showClose = this.ControlBox;
            var showRestore = winState != FormWindowState.Normal;
            var showSize = sizableBorder && (
                winState != FormWindowState.Minimized || winState != FormWindowState.Maximized);
            _ = !showMin
                ? Windows.EnableMenuItem(hmenu, SC.SC_MINIMIZE, MF.MF_BYCOMMAND | MF.MF_GRAYED)
                : Windows.EnableMenuItem(hmenu, SC.SC_MINIMIZE, MF.MF_BYCOMMAND | MF.MF_ENABLED);
            _ = !showMax
                ? Windows.EnableMenuItem(hmenu, SC.SC_MAXIMIZE, MF.MF_BYCOMMAND | MF.MF_GRAYED)
                : Windows.EnableMenuItem(hmenu, SC.SC_MAXIMIZE, MF.MF_BYCOMMAND | MF.MF_ENABLED);
            _ = !showClose
                ? Windows.EnableMenuItem(hmenu, SC.SC_CLOSE, MF.MF_BYCOMMAND | MF.MF_GRAYED)
                : Windows.EnableMenuItem(hmenu, SC.SC_CLOSE, MF.MF_BYCOMMAND | MF.MF_ENABLED);
            _ = !showRestore
                ? Windows.EnableMenuItem(hmenu, SC.SC_RESTORE, MF.MF_BYCOMMAND | MF.MF_GRAYED)
                : Windows.EnableMenuItem(hmenu, SC.SC_RESTORE, MF.MF_BYCOMMAND | MF.MF_ENABLED);
            _ = !showSize
                ? Windows.EnableMenuItem(hmenu, SC.SC_SIZE, MF.MF_BYCOMMAND | MF.MF_GRAYED)
                : Windows.EnableMenuItem(hmenu, SC.SC_SIZE, MF.MF_BYCOMMAND | MF.MF_ENABLED);
        }

        private static unsafe List<MENUITEMINFO> GetMenuInfo(HMENU hWnd)
        {
            var lst = new List<MENUITEMINFO>();
            var count = Windows.GetMenuItemCount(hWnd);
            for (var i = 0; i < count; i++)
            {
                var mif = new MENUITEMINFO()
                {
                    MenuItemInfo = new MENUITEMINFOW()
                    {
                        cbSize = (uint)sizeof(MENUITEMINFOW),
                        fMask = Windows.MIIM_STRING | Windows.MIIM_ID | Windows.MIIM_BITMAP | Windows.MIIM_DATA | Windows.MIIM_STATE,
                    },
                };
                var res = Windows.GetMenuItemInfo(hWnd, (uint)i, true, &mif.MenuItemInfo);
                if (res)
                {
                    ++mif.MenuItemInfo.cch;
                    mif.Text = new string(' ', (int)mif.MenuItemInfo.cch);
                    fixed (char* pTypeDataStr = mif.Text)
                    {
                        mif.MenuItemInfo.dwTypeData = (ushort*)pTypeDataStr;
                        _ = Windows.GetMenuItemInfo(hWnd, (uint)i, true, &mif.MenuItemInfo);
                    }

                    if (mif.Text.Equals("\0"))
                    {
                        mif.Text = null;
                    }
                }

                lst.Add(mif);
            }

            return lst;
        }

        // creates a new ContextMenuStrip based on the MENUITEMINFO list.
        private static ContextMenuStrip GetContextMenu(List<MENUITEMINFO> mENUITEMINFOs, IContainer container, Control control)
        {
            var result = new ContextMenuStrip(container)
            {
                AutoClose = true,
            };
            var items = new List<ToolStripItem>();
            foreach (var mENUITEMINFO in mENUITEMINFOs)
            {
                if (string.IsNullOrEmpty(mENUITEMINFO.Text))
                {
                    var item = new ToolStripSeparator();
                    items.Add(item);
                }
                else
                {
                    var item = new ToolStripMenuItem
                    {
                        Text = mENUITEMINFO.Text.Contains('\t') ? mENUITEMINFO.Text.Replace(mENUITEMINFO.Text[mENUITEMINFO.Text.IndexOf('\t', StringComparison.InvariantCulture)..], string.Empty) : mENUITEMINFO.Text,
                        ShortcutKeyDisplayString = mENUITEMINFO.Text.Contains('\t') ? mENUITEMINFO.Text[(mENUITEMINFO.Text.IndexOf('\t', StringComparison.InvariantCulture) + 1)..] : string.Empty,
                        ShortcutKeys = GetKeysFromString(mENUITEMINFO.Text[(mENUITEMINFO.Text.IndexOf('\t', StringComparison.InvariantCulture) + 1)..]),
                        Enabled = mENUITEMINFO.MenuItemInfo.fState == Windows.MFS_ENABLED,
                        Image = mENUITEMINFO.MenuItemInfo.hbmpItem != IntPtr.Zero ? GetNativeMenuItemImage(mENUITEMINFO.MenuItemInfo.hbmpItem, mENUITEMINFO.MenuItemInfo.fState == Windows.MFS_ENABLED) : null,
                    };

                    if (mENUITEMINFO.MenuItemInfo.wID is SC.SC_RESTORE
                        or SC.SC_MOVE
                        or SC.SC_SIZE
                        or SC.SC_MINIMIZE
                        or SC.SC_MAXIMIZE
                        or SC.SC_CLOSE)
                    {
                        item.Click += (sender, e) =>
                        {
                            IntPtr result = Windows.SendMessageW((HWND)control.Handle, WM.WM_SYSCOMMAND, mENUITEMINFO.MenuItemInfo.wID, IntPtr.Zero);
                            Debug.WriteLineIf(result != IntPtr.Zero, $"TerraFX.Interop.Windows.SendMessageW() failed with error code {result.ToInt32()}");
                        };
                    }

                    items.Add(item);
                }
            }

            result.Items.AddRange(items.ToArray());
            return result;
        }

        private static Keys GetKeysFromString(string keys)
        {
            var key = Keys.None;
            if (keys.Contains("Alt"))
            {
                key |= Keys.Alt;

                // strip the key from the string.
                keys = keys[(keys.IndexOf('+', StringComparison.InvariantCulture) + 1)..];
            }

            if (keys.Contains("F4"))
            {
                key |= Keys.F4;
            }

            return key;
        }

        private static unsafe TitleBarInfo GetTitleBarInfo(HWND hwnd)
        {
            var tbInfoEx = new TITLEBARINFOEX
            {
                cbSize = (uint)Unsafe.SizeOf<TITLEBARINFOEX>(),
            };
            Windows.SendMessageW(hwnd, WM.WM_GETTITLEBARINFOEX, 0u, new IntPtr(&tbInfoEx));
            return new TitleBarInfo(tbInfoEx);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Used for P/Invokes.")]
        private struct MENUITEMINFO
        {
            public MENUITEMINFOW MenuItemInfo;
            public string Text;
        }

        private struct TitleBarInfo
        {
            public unsafe TitleBarInfo(TITLEBARINFOEX tbInfo)
            {
                this.Items = new ItemInfo[6];
                for (var i = 0; i < this.Items.Length; i++)
                {
                    this.Items[i].State = (ItemState)tbInfo.rgstate[i];
                    this.Items[i].Rect = tbInfo.rgrect[i];
                    this.Items[i].Bounds = Rectangle.FromLTRB(
                        this.Items[i].Rect.left,
                        this.Items[i].Rect.top,
                        this.Items[i].Rect.right,
                        this.Items[i].Rect.bottom);
                }

                // replace item 0's bounds with rcTitleBar.
                this.Items[0].Rect = tbInfo.rcTitleBar;
                this.Items[0].Bounds = Rectangle.FromLTRB(
                    this.Items[0].Rect.left,
                    this.Items[0].Rect.top,
                    this.Items[0].Rect.right,
                    this.Items[0].Rect.bottom);
            }

            [Flags]
            internal enum ItemState : uint
            {
                STATE_SYSTEM_FOCUSABLE = 0x00100000,
                STATE_SYSTEM_INVISIBLE = 0x00008000,
                STATE_SYSTEM_OFFSCREEN = 0x00010000,
                STATE_SYSTEM_UNAVAILABLE = 0x00000001,
                STATE_SYSTEM_PRESSED = 0x00000008,
            }

            public ItemInfo[] Items { get; set; }

            internal struct ItemInfo
            {
                public ItemState State { get; set; }

                public Rectangle Bounds { get; set; }

                public RECT Rect;
            }
        }

        // lazily create the bitmap.
        /*
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP007:Don't dispose injected.", Justification = "Needed to cleanup old bitmap.")]
        private void CreateBitmap(ref Bitmap bitmap, Rectangle bounds, bool enabled, bool hovered, bool isMaximize = true, bool isClicked = false)
        {
            if (bitmap == this.iconBitmap && Icons.IconEquals(this.lastIcon, this.Icon))
            {
                return;
            }

            bitmap?.Dispose();
            bitmap = new Bitmap(bounds.Width, bounds.Height);
            var bmpRect = new Rectangle(0, 0, bounds.Width, bounds.Height);

            // in case we got to draw the buttons to the bitmap!!!
            // the 2 colors for the close button was taken from the default windows 10 theme file.
            using var pen = new Pen(ShareXResources.Theme.TextColor);
            using var brush = new LinearGradientBrush(bmpRect, ShareXResources.Theme.MenuHighlightColor, ShareXResources.Theme.MenuHighlightColor, LinearGradientMode.Vertical);
            using var closeHoverBrush = new LinearGradientBrush(bmpRect, Color.FromArgb(255, 232, 17, 35), Color.FromArgb(255, 232, 17, 35), LinearGradientMode.Vertical);
            using var closeClickedBrush = new LinearGradientBrush(bmpRect, Color.FromArgb(153, 231, 16, 34), Color.FromArgb(153, 231, 16, 34), LinearGradientMode.Vertical);
            using var gr = Graphics.FromImage(bitmap);
            if (bounds == this.IconHitbox)
            {
                this.lastIcon = this.Icon;
                using var iconbmp = this.Icon.ToBitmap();
                using var bmp = new Bitmap(iconbmp, 16, 16);
                gr.DrawImage(bmp, new Point(0, 0));
            }
            else if (bounds == this.MinimizeHitbox)
            {
                if (enabled)
                {
                    if (hovered)
                    {
                        gr.FillRectangle(brush, bmpRect);
                    }

                    gr.DrawLine(pen, new Point(18, 15), new Point(28, 15));
                }
                else
                {
                    gr.DrawLine(Pens.DarkGray, new Point(18, 15), new Point(28, 15));
                }
            }
            else if ((bounds == this.MaximizeHitbox) && isMaximize)
            {
                if (enabled)
                {
                    if (hovered)
                    {
                        gr.FillRectangle(brush, bmpRect);
                    }

                    gr.DrawRectangle(pen, 18, 10, 10, 10);
                }
                else
                {
                    // color it with Pens.LightGray.
                    gr.DrawRectangle(Pens.DarkGray, 18, 10, 10, 10);
                }
            }
            else if (bounds == this.CloseHitbox)
            {
                if (hovered)
                {
                    gr.FillRectangle(closeHoverBrush, bmpRect);
                }
                else if (isClicked)
                {
                    gr.FillRectangle(closeClickedBrush, bmpRect);
                }

                // TODO: Draw icon image of the button itself like windows does.
                gr.DrawImage(Properties.Resources.closeglyph, new Point(18, 10));
            }
            else if (bounds == this.HelpHitbox)
            {
                if (hovered)
                {
                    gr.FillRectangle(brush, bmpRect);
                }

                // TODO: Draw icon image of the button itself like windows does.
                gr.DrawImage(Properties.Resources.helpglyph, new Point(18, 10));
            }
        }
        */
    }
}
