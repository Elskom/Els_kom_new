// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal static class NativeMethods
    {
        private static readonly Version OSVersion = Environment.OSVersion.Version;

        public enum DwmWindowAttribute : uint
        {
            /// <summary>
            /// Use with DwmGetWindowAttribute. Discovers whether non-client rendering is enabled. The retrieved value is of type BOOL. TRUE if non-client rendering is enabled; otherwise, FALSE.
            /// </summary>
            DWMWA_NCRENDERING_ENABLED = 1,

            /// <summary>
            /// Use with DwmSetWindowAttribute. Sets the non-client rendering policy. The pvAttribute parameter points to a value from the DWMNCRENDERINGPOLICY enumeration.
            /// </summary>
            DWMWA_NCRENDERING_POLICY,

            /// <summary>
            /// Use with DwmSetWindowAttribute. Enables or forcibly disables DWM transitions. The pvAttribute parameter points to a value of TRUE to disable transitions or FALSE to enable transitions.
            /// </summary>
            DWMWA_TRANSITIONS_FORCEDISABLED,

            /// <summary>
            /// Use with DwmSetWindowAttribute. Enables content rendered in the non-client area to be visible on the frame drawn by DWM. The pvAttribute parameter points to a value of TRUE to enable content rendered in the non-client area to be visible on the frame; otherwise, it points to FALSE.
            /// </summary>
            DWMWA_ALLOW_NCPAINT,

            /// <summary>
            /// Use with DwmGetWindowAttribute. Retrieves the bounds of the caption button area in the window-relative space. The retrieved value is of type RECT.
            /// </summary>
            DWMWA_CAPTION_BUTTON_BOUNDS,

            /// <summary>
            /// Use with DwmSetWindowAttribute. Specifies whether non-client content is right-to-left (RTL) mirrored. The pvAttribute parameter points to a value of TRUE if the non-client content is right-to-left (RTL) mirrored; otherwise, it points to FALSE.
            /// </summary>
            DWMWA_NONCLIENT_RTL_LAYOUT,

            /// <summary>
            /// Use with DwmSetWindowAttribute. Forces the window to display an iconic thumbnail or peek representation (a static bitmap), even if a live or snapshot representation of the window is available. This value normally is set during a window's creation and not changed throughout the window's lifetime. Some scenarios, however, might require the value to change over time. The pvAttribute parameter points to a value of TRUE to require a iconic thumbnail or peek representation; otherwise, it points to FALSE.
            /// </summary>
            DWMWA_FORCE_ICONIC_REPRESENTATION,

            /// <summary>
            /// Use with DwmSetWindowAttribute. Sets how Flip3D treats the window. The pvAttribute parameter points to a value from the DWMFLIP3DWINDOWPOLICY enumeration.
            /// </summary>
            DWMWA_FLIP3D_POLICY,

            /// <summary>
            /// Use with DwmGetWindowAttribute. Retrieves the extended frame bounds rectangle in screen space. The retrieved value is of type RECT.
            /// </summary>
            DWMWA_EXTENDED_FRAME_BOUNDS,

            /// <summary>
            /// Use with DwmSetWindowAttribute. The window will provide a bitmap for use by DWM as an iconic thumbnail or peek representation (a static bitmap) for the window. DWMWA_HAS_ICONIC_BITMAP can be specified with DWMWA_FORCE_ICONIC_REPRESENTATION. DWMWA_HAS_ICONIC_BITMAP normally is set during a window's creation and not changed throughout the window's lifetime. Some scenarios, however, might require the value to change over time. The pvAttribute parameter points to a value of TRUE to inform DWM that the window will provide an iconic thumbnail or peek representation; otherwise, it points to FALSE.
            /// </summary>
            DWMWA_HAS_ICONIC_BITMAP,

            /// <summary>
            /// Use with DwmSetWindowAttribute. Do not show peek preview for the window. The peek view shows a full-sized preview of the window when the mouse hovers over the window's thumbnail in the taskbar. If this attribute is set, hovering the mouse pointer over the window's thumbnail dismisses peek (in case another window in the group has a peek preview showing). The pvAttribute parameter points to a value of TRUE to prevent peek functionality or FALSE to allow it.
            /// </summary>
            DWMWA_DISALLOW_PEEK,

            /// <summary>
            /// Use with DwmSetWindowAttribute. Prevents a window from fading to a glass sheet when peek is invoked. The pvAttribute parameter points to a value of TRUE to prevent the window from fading during another window's peek or FALSE for normal behavior.
            /// </summary>
            DWMWA_EXCLUDED_FROM_PEEK,

            /// <summary>
            /// Use with DwmSetWindowAttribute. Cloaks the window such that it is not visible to the user. The window is still composed by DWM.
            /// </summary>
            DWMWA_CLOAK,

            /// <summary>
            /// Use with DwmGetWindowAttribute.
            /// </summary>
            DWMWA_CLOAKED,

            /// <summary>
            /// Use with DwmSetWindowAttribute. Freeze the window's thumbnail image with its current visuals. Do no further live updates on the thumbnail image to match the window's contents.
            /// </summary>
            DWMWA_FREEZE_REPRESENTATION,

            /// <summary>
            /// The maximum recognized DWMWINDOWATTRIBUTE value, used for validation purposes.
            /// </summary>
            DWMWA_LAST,

            // Undocumented, available since October 2018 update (build 17763)
            DWMWA_USE_IMMERSIVE_DARK_MODE = 19,
        }

        /// <summary>Values to pass to the GetDCEx method.</summary>
        [Flags]
        public enum DeviceContextValues : uint
        {
            /// <summary>DCX_WINDOW: Returns a DC that corresponds to the window rectangle rather
            /// than the client rectangle.</summary>
            Window = 0x00000001,

            /// <summary>DCX_CACHE: Returns a DC from the cache, rather than the OWNDC or CLASSDC
            /// window. Essentially overrides CS_OWNDC and CS_CLASSDC.</summary>
            Cache = 0x00000002,

            /// <summary>DCX_NORESETATTRS: Does not reset the attributes of this DC to the
            /// default attributes when this DC is released.</summary>
            NoResetAttrs = 0x00000004,

            /// <summary>DCX_CLIPCHILDREN: Excludes the visible regions of all child windows
            /// below the window identified by hWnd.</summary>
            ClipChildren = 0x00000008,

            /// <summary>DCX_CLIPSIBLINGS: Excludes the visible regions of all sibling windows
            /// above the window identified by hWnd.</summary>
            ClipSiblings = 0x00000010,

            /// <summary>DCX_PARENTCLIP: Uses the visible region of the parent window. The
            /// parent's WS_CLIPCHILDREN and CS_PARENTDC style bits are ignored. The origin is
            /// set to the upper-left corner of the window identified by hWnd.</summary>
            ParentClip = 0x00000020,

            /// <summary>DCX_EXCLUDERGN: The clipping region identified by hrgnClip is excluded
            /// from the visible region of the returned DC.</summary>
            ExcludeRgn = 0x00000040,

            /// <summary>DCX_INTERSECTRGN: The clipping region identified by hrgnClip is
            /// intersected with the visible region of the returned DC.</summary>
            IntersectRgn = 0x00000080,

            /// <summary>DCX_EXCLUDEUPDATE: Unknown...Undocumented</summary>
            ExcludeUpdate = 0x00000100,

            /// <summary>DCX_INTERSECTUPDATE: Unknown...Undocumented</summary>
            IntersectUpdate = 0x00000200,

            /// <summary>DCX_LOCKWINDOWUPDATE: Allows drawing even if there is a LockWindowUpdate
            /// call in effect that would otherwise exclude this window. Used for drawing during
            /// tracking.</summary>
            LockWindowUpdate = 0x00000400,

            /// <summary>DCX_VALIDATE When specified with DCX_INTERSECTUPDATE, causes the DC to
            /// be completely validated. Using this function with both DCX_INTERSECTUPDATE and
            /// DCX_VALIDATE is identical to using the BeginPaint function.</summary>
            Validate = 0x00200000,

            /// <summary>Inknown... Undocumented</summary>
            Unknown = 0x10000,
        }

        [Flags]
        public enum RedrawWindowFlags : uint
        {
            /// <summary>
            /// Invalidates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
            /// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_INVALIDATE invalidates the entire window.
            /// </summary>
            Invalidate = 0x1,

            /// <summary>Causes the OS to post a WM_PAINT message to the window regardless of whether a portion of the window is invalid.</summary>
            InternalPaint = 0x2,

            /// <summary>
            /// Causes the window to receive a WM_ERASEBKGND message when the window is repainted.
            /// Specify this value in combination with the RDW_INVALIDATE value; otherwise, RDW_ERASE has no effect.
            /// </summary>
            Erase = 0x4,

            /// <summary>
            /// Validates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
            /// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_VALIDATE validates the entire window.
            /// This value does not affect internal WM_PAINT messages.
            /// </summary>
            Validate = 0x8,

            NoInternalPaint = 0x10,

            /// <summary>Suppresses any pending WM_ERASEBKGND messages.</summary>
            NoErase = 0x20,

            /// <summary>Excludes child windows, if any, from the repainting operation.</summary>
            NoChildren = 0x40,

            /// <summary>Includes child windows, if any, in the repainting operation.</summary>
            AllChildren = 0x80,

            /// <summary>Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND and WM_PAINT messages before the RedrawWindow returns, if necessary.</summary>
            UpdateNow = 0x100,

            /// <summary>
            /// Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND messages before RedrawWindow returns, if necessary.
            /// The affected windows receive WM_PAINT messages at the ordinary time.
            /// </summary>
            EraseNow = 0x200,

            Frame = 0x400,

            NoFrame = 0x800,
        }

        public enum CombineRgnStyles : int
        {
            RGN_AND = 1,
            RGN_OR = 2,
            RGN_XOR = 3,
            RGN_DIFF = 4,
            RGN_COPY = 5,
            RGN_MIN = RGN_AND,
            RGN_MAX = RGN_COPY,
        }

        public enum HitTestResult
        {
            HTBORDER = 18, // In the border of a window that does not have a sizing border.
            HTBOTTOM = 15, // In the lower-horizontal border of a resizable window (the user can click the mouse to resize the window vertically).
            HTBOTTOMLEFT = 16, // In the lower-left corner of a border of a resizable window(the user can click the mouse to resize the window diagonally).
            HTBOTTOMRIGHT = 17, // In the lower-right corner of a border of a resizable window(the user can click the mouse to resize the window diagonally).
            HTCAPTION = 2, // In a title bar.
            HTCLIENT = 1, // In a client area.
            HTCLOSE = 20, // In a Close button.
            HTERROR = -2, // On the screen background or on a dividing line between windows (same as HTNOWHERE, except that the DefWindowProc function produces a system beep to indicate an error).
            HTGROWBOX = 4, // In a size box(same as HTSIZE).
            HTHELP = 21, // In a Help button.
            HTHSCROLL = 6, // In a horizontal scroll bar.
            HTLEFT = 10, // In the left border of a resizable window (the user can click the mouse to resize the window horizontally).
            HTMENU = 5, // In a menu.
            HTMAXBUTTON = 9, // In a Maximize button.
            HTMINBUTTON = 8, // In a Minimize button.
            HTNOWHERE = 0, // On the screen background or on a dividing line between windows.
            HTREDUCE = 8, // In a Minimize button.
            HTRIGHT = 11, // In the right border of a resizable window (the user can click the mouse to resize the window horizontally).
            HTSIZE = 4, // In a size box(same as HTGROWBOX).
            HTSYSMENU = 3, // In a window menu or in a Close button in a child window.
            HTTOP = 12, // In the upper-horizontal border of a window.
            HTTOPLEFT = 13, // In the upper-left corner of a window border.
            HTTOPRIGHT = 14, // In the upper-right corner of a window border.
            HTTRANSPARENT = -1, // In a window currently covered by another window in the same thread (the message will be sent to underlying windows in the same thread until one of them returns a code that is not HTTRANSPARENT).
            HTVSCROLL = 7, // In the vertical scroll bar.
            HTZOOM = 9, // In a Maximize button.
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        public static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
                // try
                // {
                var useImmersiveDarkMode = enabled ? 1 : 0;
                var result = DwmSetWindowAttribute(handle, (int)DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref useImmersiveDarkMode, sizeof(int));
                return result == 0;

                // }
                // catch (Exception e)
                // {
                //     DebugHelper.WriteException(e);
                // }
            }

            return false;
        }

        private static bool IsWindows10OrGreater(int build = -1)
            => OSVersion.Major >= 10 && OSVersion.Build >= build;

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }

            public RECT(Rectangle r)
                : this(r.Left, r.Top, r.Right, r.Bottom)
            {
            }

            public int X
            {
                get => this.Left;
                set
                {
                    this.Right -= this.Left - value;
                    this.Left = value;
                }
            }

            public int Y
            {
                get => this.Top;
                set
                {
                    this.Bottom -= this.Top - value;
                    this.Top = value;
                }
            }

            public int Height
            {
                get => this.Bottom - this.Top;
                set => this.Bottom = value + this.Top;
            }

            public int Width
            {
                get => this.Right - this.Left;
                set => this.Right = value + this.Left;
            }

            public Point Location
            {
                get => new Point(this.Left, this.Top);
                set
                {
                    this.X = value.X;
                    this.Y = value.Y;
                }
            }

            public Size Size
            {
                get => new Size(this.Width, this.Height);
                set
                {
                    this.Width = value.Width;
                    this.Height = value.Height;
                }
            }

            public static implicit operator Rectangle(RECT r)
                => new Rectangle(r.Left, r.Top, r.Width, r.Height);

            public static implicit operator RECT(Rectangle r)
                => new RECT(r);

            public static bool operator ==(RECT r1, RECT r2)
                => r1.Equals(r2);

            public static bool operator !=(RECT r1, RECT r2)
                => !r1.Equals(r2);

            public bool Equals(RECT r)
                => r.Left == this.Left && r.Top == this.Top && r.Right == this.Right && r.Bottom == this.Bottom;

            public override bool Equals(object obj)
            {
                if (obj is RECT)
                {
                    return this.Equals((RECT)obj);
                }
                else if (obj is Rectangle)
                {
                    return this.Equals(new RECT((Rectangle)obj));
                }

                return false;
            }

            public override int GetHashCode()
                => ((Rectangle)this).GetHashCode();

            public override string ToString()
                => string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", this.Left, this.Top, this.Right, this.Bottom);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public RECT[] rgrc;
            public WINDOWPOS lppos;
        }

        // for screenshots.
        internal static class ScreenShots
        {
            internal static Bitmap MakeScreenShot()
            {
                var sz = default(Size);
                foreach (var screen in Screen.AllScreens)
                {
                    sz.Width += screen.Bounds.Size.Width;
                    sz.Height += screen.Bounds.Size.Height;
                }

                var hDesk = GetDesktopWindow();
                var hSrce = GetWindowDC(hDesk);
                var hDest = CreateCompatibleDC(hSrce);
                var hBmp = CreateCompatibleBitmap(hSrce, sz.Width, sz.Height);
                var hOldBmp = SelectObject(hDest, hBmp);
                _ = BitBlt(hDest, 0, 0, sz.Width, sz.Height, hSrce, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
                var bmp = Image.FromHbitmap(hBmp);
                _ = SelectObject(hDest, hOldBmp);
                _ = DeleteObject(hBmp);
                _ = DeleteDC(hDest);
                _ = ReleaseDC(hDesk, hSrce);
                return bmp;
            }

            [DllImport("gdi32.dll")]
            internal static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);

            [DllImport("user32.dll")]
            internal static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr DeleteDC(IntPtr hDC);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr DeleteObject(IntPtr hDC);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

            [DllImport("gdi32.dll")]
            internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

            [DllImport("user32.dll")]
            internal static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll")]
            internal static extern IntPtr GetWindowDC(IntPtr ptr);
        }

        // for Non-Client painting.
        internal static class NCPainting
        {
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

            [DllImport("gdi32.dll")]
            internal static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode);

            [DllImport("gdi32.dll", EntryPoint = "CreateRectRgnIndirect", SetLastError = true)]
            internal static extern IntPtr CreateRectRgnIndirect([In] ref RECT lprc);

            [DllImport("user32.dll")]
            internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, DeviceContextValues flags);

            [DllImport("user32.dll")]
            internal static extern bool RedrawWindow(IntPtr hWnd, [In] ref RECT lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

            [DllImport("user32.dll")]
            internal static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

            [DllImport("user32.dll")]
            internal static extern IntPtr BeginPaint(IntPtr hwnd, out PAINTSTRUCT lpPaint);

            [DllImport("user32.dll")]
            internal static extern bool EndPaint(IntPtr hWnd, [In] ref PAINTSTRUCT lpPaint);
        }
    }
}
