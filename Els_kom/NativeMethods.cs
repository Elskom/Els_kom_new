// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Els_kom.Enums;
    using Svg;

    internal static class NativeMethods
    {
        // Corresponds to bitmaps in MENUITEMINFO
        public const int HBMMENU_CALLBACK = -1;
        public const int HBMMENU_SYSTEM = 1;
        public const int HBMMENU_MBAR_RESTORE = 2;
        public const int HBMMENU_MBAR_MINIMIZE = 3;
        public const int HBMMENU_MBAR_CLOSE = 5;
        public const int HBMMENU_MBAR_CLOSE_D = 6;
        public const int HBMMENU_MBAR_MINIMIZE_D = 7;
        public const int HBMMENU_POPUP_CLOSE = 8;
        public const int HBMMENU_POPUP_RESTORE = 9;
        public const int HBMMENU_POPUP_MAXIMIZE = 10;
        public const int HBMMENU_POPUP_MINIMIZE = 11;

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

            /// <summary>DCX_EXCLUDEUPDATE: Unknown...Undocumented.</summary>
            ExcludeUpdate = 0x00000100,

            /// <summary>DCX_INTERSECTUPDATE: Unknown...Undocumented.</summary>
            IntersectUpdate = 0x00000200,

            /// <summary>DCX_LOCKWINDOWUPDATE: Allows drawing even if there is a LockWindowUpdate
            /// call in effect that would otherwise exclude this window. Used for drawing during
            /// tracking.</summary>
            LockWindowUpdate = 0x00000400,

            /// <summary>DCX_VALIDATE When specified with DCX_INTERSECTUPDATE, causes the DC to
            /// be completely validated. Using this function with both DCX_INTERSECTUPDATE and
            /// DCX_VALIDATE is identical to using the BeginPaint function.</summary>
            Validate = 0x00200000,

            /// <summary>Unknown... Undocumented.</summary>
            Unknown = 0x10000,
        }

        [Flags]
        public enum RedrawWindowOptions : uint
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

        public enum CombineRgnStyles
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
            HTREDUCE = HTMINBUTTON, // In a Minimize button.
            HTRIGHT = 11, // In the right border of a resizable window (the user can click the mouse to resize the window horizontally).
            HTSIZE = HTGROWBOX, // In a size box(same as HTGROWBOX).
            HTSYSMENU = 3, // In a window menu or in a Close button in a child window.
            HTTOP = 12, // In the upper-horizontal border of a window.
            HTTOPLEFT = 13, // In the upper-left corner of a window border.
            HTTOPRIGHT = 14, // In the upper-right corner of a window border.
            HTTRANSPARENT = -1, // In a window currently covered by another window in the same thread (the message will be sent to underlying windows in the same thread until one of them returns a code that is not HTTRANSPARENT).
            HTVSCROLL = 7, // In the vertical scroll bar.
            HTZOOM = HTMAXBUTTON, // In a Maximize button.
        }

        [Flags]
        public enum MIIM
        {
            BITMAP = 0x00000080,
            CHECKMARKS = 0x00000008,
            DATA = 0x00000020,
            FTYPE = 0x00000100,
            ID = 0x00000002,
            STATE = 0x00000001,
            STRING = 0x00000040,
            SUBMENU = 0x00000004,
            TYPE = 0x00000010,
        }

        [Flags]
        internal enum MF : uint
        {
            INSERT = 0x00000000,
            CHANGE = 0x00000080,
            APPEND = 0x00000100,
            DELETE = 0x00000200,
            REMOVE = 0x00001000,
            BYCOMMAND = INSERT,
            BYPOSITION = 0x00000400,
            SEPARATOR = 0x00000800,
            ENABLED = INSERT,
            GRAYED = 0x00000001,
            DISABLED = 0x00000002,
            UNCHECKED = INSERT,
            CHECKED = 0x00000008,
            USECHECKBITMAPS = DELETE,
            STRING = INSERT,
            BITMAP = 0x00000004,
            OWNERDRAW = APPEND,
            POPUP = 0x00000010,
            MENUBARBREAK = 0x00000020,
            MENUBREAK = 0x00000040,
            UNHILITE = INSERT,
            HILITE = CHANGE,
            DEFAULT = REMOVE,
            SYSMENU = 0x00002000,
            HELP = 0x00004000,
            RIGHTJUSTIFY = HELP,
            MOUSESELECT = 0x00008000,
            END = CHANGE,  /* Obsolete -- only used by old RES files */
            MFT_STRING = STRING,
            MFT_BITMAP = BITMAP,
            MFT_MENUBARBREAK = MENUBARBREAK,
            MFT_MENUBREAK = MENUBREAK,
            MFT_OWNERDRAW = OWNERDRAW,
            MFT_RADIOCHECK = DELETE,
            MFT_SEPARATOR = SEPARATOR,
            MFT_RIGHTORDER = SYSMENU,
            MFT_RIGHTJUSTIFY = RIGHTJUSTIFY,
            MFS_GRAYED = 0x00000003,
            MFS_DISABLED = MFS_GRAYED,
            MFS_CHECKED = CHECKED,
            MFS_HILITE = HILITE,
            MFS_ENABLED = ENABLED,
            MFS_UNCHECKED = UNCHECKED,
            MFS_UNHILITE = UNHILITE,
            MFS_DEFAULT = DEFAULT,
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("User32.DLL")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam)
        {
            var result = SendMessage(hWnd.Handle, Msg, wParam, lParam);
            GC.KeepAlive(hWnd.Wrapper);
            return result;
        }

        [DllImport("User32.DLL", ExactSpelling = true)]
        public static extern IntPtr SendMessageW(
            IntPtr hWnd,
            WindowsMessages Msg,
            IntPtr wParam = default,
            IntPtr lParam = default);

        public static IntPtr SendMessageW(
            HandleRef hWnd,
            WindowsMessages Msg,
            IntPtr wParam = default,
            IntPtr lParam = default)
        {
            var result = SendMessageW(hWnd.Handle, Msg, wParam, lParam);
            GC.KeepAlive(hWnd.Wrapper);
            return result;
        }

        [DllImport("User32.DLL")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        public static IntPtr GetSystemMenu(HandleRef hWnd, bool bRevert)
        {
            var result = GetSystemMenu(hWnd.Handle, bRevert);
            GC.KeepAlive(hWnd.Wrapper);
            return result;
        }

        [DllImport("User32.DLL")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        public static bool EnableMenuItem(HandleRef hWnd, uint uIDEnableItem, uint uEnable)
        {
            var result = EnableMenuItem(hWnd.Handle, uIDEnableItem, uEnable);
            GC.KeepAlive(hWnd.Wrapper);
            return result;
        }

        public static bool EnableMenuItem(HandleRef hWnd, SYSCOMMANDS uIDEnableItem, uint uEnable)
            => EnableMenuItem(hWnd, (uint)uIDEnableItem, uEnable);

        [DllImport("user32.dll")]
        public static extern int TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr prcRect);

        public static int TrackPopupMenu(HandleRef hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr prcRect)
        {
            var result = TrackPopupMenu(hMenu.Handle, uFlags, x, y, nReserved, hWnd, prcRect);
            GC.KeepAlive(hMenu.Wrapper);
            return result;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition, [In, Out] MENUITEMINFO lpmii);

        public static bool GetMenuItemInfo(HandleRef hWnd, uint uItem, bool fByPosition, [In, Out] MENUITEMINFO lpmii)
        {
            var result = GetMenuItemInfo(hWnd.Handle, uItem, fByPosition, lpmii);
            GC.KeepAlive(hWnd.Wrapper);
            return result;
        }

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        public static int GetMenuItemCount(HandleRef hWnd)
        {
            var result = GetMenuItemCount(hWnd.Handle);
            GC.KeepAlive(hWnd.Wrapper);
            return result;
        }

        public static List<MENUITEMINFO> GetMenuInfo(HandleRef hWnd)
        {
            var lst = new List<MENUITEMINFO>();
            var count = GetMenuItemCount(hWnd);
            for (var i = 0; i < count; i++)
            {
                var mif = new MENUITEMINFO(MIIM.STRING | MIIM.ID | MIIM.BITMAP | MIIM.DATA | MIIM.STATE);
                var res = GetMenuItemInfo(hWnd, (uint)i, true, mif);
                if (res)
                {
                    ++mif.cch;
                    mif.dwTypeData = new string(' ', (int)mif.cch);
                    _ = GetMenuItemInfo(hWnd, (uint)i, true, mif);
                }

                lst.Add(mif);
            }

            return lst;
        }

        // creates a new ContextMenuStrip based on the MENUITEMINFO list.
        public static ContextMenuStrip GetContextMenu(List<MENUITEMINFO> mENUITEMINFOs, IContainer container, Control control)
        {
            var result = new ContextMenuStrip(container)
            {
                AutoClose = true,
            };
            var items = new List<ToolStripItem>();
            foreach (var mENUITEMINFO in mENUITEMINFOs)
            {
                if (string.IsNullOrEmpty(mENUITEMINFO.dwTypeData))
                {
                    var item = new ToolStripSeparator();
                    items.Add(item);
                }
                else
                {
                    var item = new ToolStripMenuItem
                    {
                        Text = mENUITEMINFO.dwTypeData.Contains("\t") ? mENUITEMINFO.dwTypeData.Replace(mENUITEMINFO.dwTypeData[mENUITEMINFO.dwTypeData.IndexOf("\t", StringComparison.InvariantCulture)..], string.Empty) : mENUITEMINFO.dwTypeData,
                        ShortcutKeyDisplayString = mENUITEMINFO.dwTypeData.Contains("\t") ? mENUITEMINFO.dwTypeData[(mENUITEMINFO.dwTypeData.IndexOf("\t", StringComparison.InvariantCulture) + 1)..] : string.Empty,
                        ShortcutKeys = GetKeysFromString(mENUITEMINFO.dwTypeData[(mENUITEMINFO.dwTypeData.IndexOf("\t", StringComparison.InvariantCulture) + 1)..]),
                        Enabled = mENUITEMINFO.fState == (int)MF.MFS_ENABLED,
                        Image = mENUITEMINFO.hbmpItem != IntPtr.Zero ? GetNativeMenuItemImage(mENUITEMINFO.hbmpItem, mENUITEMINFO.fState == (int)MF.MFS_ENABLED) : null,
                    };
                    if (mENUITEMINFO.wID == (uint)SYSCOMMANDS.SC_RESTORE)
                    {
                        item.Click += (sender, e) =>
                        {
                            var result = SendMessageW(new HandleRef(control, control.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_RESTORE, IntPtr.Zero);
                            Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                        };
                    }
                    else if (mENUITEMINFO.wID == (uint)SYSCOMMANDS.SC_MOVE)
                    {
                        item.Click += (sender, e) =>
                        {
                            var result = SendMessageW(new HandleRef(control, control.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_MOVE, IntPtr.Zero);
                            Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                        };
                    }
                    else if (mENUITEMINFO.wID == (uint)SYSCOMMANDS.SC_SIZE)
                    {
                        item.Click += (sender, e) =>
                        {
                            var result = SendMessageW(new HandleRef(control, control.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_SIZE, IntPtr.Zero);
                            Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                        };
                    }
                    else if (mENUITEMINFO.wID == (uint)SYSCOMMANDS.SC_MINIMIZE)
                    {
                        item.Click += (sender, e) =>
                        {
                            var result = SendMessageW(new HandleRef(control, control.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_MINIMIZE, IntPtr.Zero);
                            Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                        };
                    }
                    else if (mENUITEMINFO.wID == (uint)SYSCOMMANDS.SC_MAXIMIZE)
                    {
                        item.Click += (sender, e) =>
                        {
                            var result = SendMessageW(new HandleRef(control, control.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_MAXIMIZE, IntPtr.Zero);
                            Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                        };
                    }
                    else if (mENUITEMINFO.wID == (uint)SYSCOMMANDS.SC_CLOSE)
                    {
                        item.Click += (sender, e) =>
                        {
                            var result = SendMessageW(new HandleRef(control, control.Handle), WindowsMessages.SYSCOMMAND, (IntPtr)(int)SYSCOMMANDS.SC_CLOSE, IntPtr.Zero);
                            Debug.WriteLineIf(result != IntPtr.Zero, $"NativeMethods.SendMessageW() failed with error code {result.ToInt32()}");
                        };
                    }

                    items.Add(item);
                }
            }

            result.Items.AddRange(items.ToArray());
            return result;
        }

        public static Image GetNativeMenuItemImage(IntPtr hBitmap, bool enabled)
        {
            if (hBitmap != IntPtr.Zero && hBitmap.ToInt32() > HBMMENU_POPUP_MINIMIZE)
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
                var buttonToUse = -1;
                switch (hBitmap.ToInt32())
                {
                    case HBMMENU_MBAR_CLOSE:
                    case HBMMENU_MBAR_CLOSE_D:
                    case HBMMENU_POPUP_CLOSE:
                        buttonToUse = (int)CaptionButton.Close;
                        break;
                    case HBMMENU_MBAR_MINIMIZE:
                    case HBMMENU_MBAR_MINIMIZE_D:
                    case HBMMENU_POPUP_MINIMIZE:
                        buttonToUse = (int)CaptionButton.Minimize;
                        break;
                    case HBMMENU_MBAR_RESTORE:
                    case HBMMENU_POPUP_RESTORE:
                        buttonToUse = (int)CaptionButton.Restore;
                        break;
                    case HBMMENU_POPUP_MAXIMIZE:
                        buttonToUse = (int)CaptionButton.Maximize;
                        break;
                    case HBMMENU_SYSTEM:
                    case HBMMENU_CALLBACK:
                    default:
                        // owner draw not supported
                        break;
                }

                if (buttonToUse > -1)
                {
                    // we've mapped to a system defined bitmap we know how to draw
                    var image = new Bitmap(16, 16);
                    using (var g = Graphics.FromImage(image))
                    {
                        ControlPaint.DrawCaptionButton(g, new Rectangle(Point.Empty, image.Size), (CaptionButton)buttonToUse, ButtonState.Flat);
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

        public static Image ConvertSVGTo16x16Image(byte[] svgData, Color color)
        {
            var svgString = Encoding.UTF8.GetString(svgData);
            var root = XElement.Parse(svgString);
            var colors = root.XPathSelectElements("//*[@fill]");
            foreach (var node in colors)
            {
                node.Attribute("fill").Value = color.ToHexString();
            }

            svgString = root.ToString();
            var svgDoc = SvgDocument.FromSvg<SvgDocument>(svgString);
            svgDoc.FillRule = SvgFillRule.EvenOdd;
            var image = svgDoc.Draw(16, 16);
            return image;
        }

        public static Keys GetKeysFromString(string keys)
        {
            var key = Keys.None;
            if (keys.Contains("Alt"))
            {
                key |= Keys.Alt;

                // strip the key from the string.
                keys = keys[(keys.IndexOf("+", StringComparison.InvariantCulture) + 1)..];
            }

            if (keys.Contains("F4"))
            {
                key |= Keys.F4;
            }

            return key;
        }

        public static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
                var useImmersiveDarkMode = enabled ? 1 : 0;
                var result = DwmSetWindowAttribute(handle, (int)DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref useImmersiveDarkMode, sizeof(int));
                return result == 0;
            }

            return false;
        }

        // for Non-Client painting.
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("gdi32.dll")]
        internal static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode);

        [DllImport("gdi32.dll", EntryPoint = "CreateRectRgnIndirect", SetLastError = true)]
        internal static extern IntPtr CreateRectRgnIndirect([In] ref RECT lprc);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, DeviceContextValues flags);

        [DllImport("user32.dll")]
        internal static extern bool RedrawWindow(IntPtr hWnd, [In] ref RECT lprcUpdate, IntPtr hrgnUpdate, RedrawWindowOptions flags);

        [DllImport("user32.dll")]
        internal static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowOptions flags);

        [DllImport("user32.dll")]
        internal static extern IntPtr BeginPaint(IntPtr hwnd, out PAINTSTRUCT lpPaint);

        [DllImport("user32.dll")]
        internal static extern bool EndPaint(IntPtr hWnd, [In] ref PAINTSTRUCT lpPaint);

        // for making screenshots.
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

        // for checking the version of windows (the build).
        private static bool IsWindows10OrGreater(int build = -1)
            => OSVersion.Major >= 10 && OSVersion.Build >= build;

        [StructLayout(LayoutKind.Sequential)]
        [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Used for P/Invokes.")]
        public struct MARGINS : IEquatable<MARGINS>
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;

            public bool Equals(MARGINS other)
                => this.leftWidth == other.leftWidth && this.rightWidth == other.rightWidth && this.topHeight == other.topHeight && this.bottomHeight == other.bottomHeight;

            public override bool Equals(object obj)
                => obj is MARGINS margins && this.Equals(margins);

            public override int GetHashCode()
                => throw new NotImplementedException();
        }

        [StructLayout(LayoutKind.Sequential)]
        [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Used for P/Invokes.")]
        public struct RECT : IEquatable<RECT>
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
                get => new(this.Left, this.Top);
                set
                {
                    this.X = value.X;
                    this.Y = value.Y;
                }
            }

            public Size Size
            {
                get => new(this.Width, this.Height);
                set
                {
                    this.Width = value.Width;
                    this.Height = value.Height;
                }
            }

            public static implicit operator Rectangle(RECT r)
                => new(r.Left, r.Top, r.Width, r.Height);

            public static implicit operator RECT(Rectangle r)
                => new(r);

            public static bool operator ==(RECT r1, RECT r2)
                => r1.Equals(r2);

            public static bool operator !=(RECT r1, RECT r2)
                => !r1.Equals(r2);

            public bool Equals(RECT other)
                => other.Left == this.Left && other.Top == this.Top && other.Right == this.Right && other.Bottom == this.Bottom;

            public override bool Equals(object obj)
            {
                if (obj is RECT rECT)
                {
                    return this.Equals(rECT);
                }
                else if (obj is Rectangle rect)
                {
                    return this.Equals(new RECT(rect));
                }

                return false;
            }

            public override int GetHashCode()
                => ((Rectangle)this).GetHashCode();

            public override string ToString()
                => string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", this.Left, this.Top, this.Right, this.Bottom);
        }

        [StructLayout(LayoutKind.Sequential)]
        [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Used for P/Invokes.")]
        public struct PAINTSTRUCT : IEquatable<PAINTSTRUCT>
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;

            public bool Equals(PAINTSTRUCT other)
                => this.hdc == other.hdc && this.fErase == other.fErase && this.rcPaint == other.rcPaint && this.fRestore == other.fRestore && this.fIncUpdate == other.fIncUpdate && this.rgbReserved == other.rgbReserved;

            public override bool Equals(object obj)
                => obj is PAINTSTRUCT paintstruct && this.Equals(paintstruct);

            public override int GetHashCode()
                => throw new NotImplementedException();
        }

        [StructLayout(LayoutKind.Sequential)]
        [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Used for P/Invokes.")]
        public struct WINDOWPOS : IEquatable<WINDOWPOS>
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;

            public bool Equals(WINDOWPOS other)
                => this.hwnd == other.hwnd && this.hwndInsertAfter == other.hwndInsertAfter && this.x == other.x && this.y == other.y && this.cx == other.cx && this.cy == other.cy && this.flags == other.flags;

            public override bool Equals(object obj)
                => obj is WINDOWPOS windowpos && this.Equals(windowpos);

            public override int GetHashCode()
                => throw new NotImplementedException();
        }

        [StructLayout(LayoutKind.Sequential)]
        [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Used for P/Invokes.")]
        public struct NCCALCSIZE_PARAMS : IEquatable<NCCALCSIZE_PARAMS>
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public RECT[] rgrc;
            public WINDOWPOS lppos;

            public bool Equals(NCCALCSIZE_PARAMS other)
                => this.rgrc.Equals(other.rgrc) && this.lppos.Equals(other.lppos);

            public override bool Equals(object obj)
                => obj is NCCALCSIZE_PARAMS nccalsizeparams && this.Equals(nccalsizeparams);

            public override int GetHashCode()
                => throw new NotImplementedException();
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Used for P/Invokes.")]
        public class MENUITEMINFO
        {
            public MENUITEMINFO()
            {
            }

            public MENUITEMINFO(MIIM pfMask)
                => this.fMask = pfMask;

            public int cbSize { get; set; } = Marshal.SizeOf(typeof(MENUITEMINFO));

            public MIIM fMask { get; set; }

            public uint fType { get; set; }

            public uint fState { get; set; }

            public uint wID { get; set; }

            public IntPtr hSubMenu { get; set; }

            public IntPtr hbmpChecked { get; set; }

            public IntPtr hbmpUnchecked { get; set; }

            public IntPtr dwItemData { get; set; }

            public string dwTypeData { get; set; }

            public uint cch { get; set; } // length of dwTypeData

            public IntPtr hbmpItem { get; set; }
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
        }
    }
}
