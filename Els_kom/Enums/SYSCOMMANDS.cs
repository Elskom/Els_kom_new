// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Enums
{
    using System;

    /// <summary>
    /// System Window Commands.
    /// </summary>
    internal enum SYSCOMMANDS
    {
        /// <summary>
        /// Did a Window size command happen?
        /// </summary>
        WM_SYSCOMMAND = 0x0112,

        SC_SIZE = 0xF000,

        SC_MOVE = 0xF010,

        /// <summary>
        /// Window is maximized.
        /// </summary>
        SC_MAXIMIZE = 0xf030,

        /// <summary>
        /// Window is minimized.
        /// </summary>
        SC_MINIMIZE = 0xf020,

        SC_NEXTWINDOW = 0xF040,

        SC_PREVWINDOW = 0xF050,

        SC_CLOSE = 0xF060,

        SC_VSCROLL = 0xF070,

        SC_HSCROLL = 0xF080,

        SC_MOUSEMENU = 0xF090,

        SC_KEYMENU = 0xF100,

        SC_ARRANGE = 0xF110,

        /// <summary>
        /// Window is restored back to normal size.
        /// </summary>
        SC_RESTORE = 0xf120,

        SC_TASKLIST = 0xF130,

        SC_SCREENSAVE = 0xF140,

        SC_HOTKEY = 0xF150,

        SC_DEFAULT = 0xF160,

        SC_MONITORPOWER = 0xF170,

        SC_CONTEXTHELP = 0xF180,

        SC_SEPARATOR = 0xF00F,

        SCF_ISSECURE = 0x00000001,

        [Obsolete("A Old version of SC_MINIMIZE. Provided to be consistent with WinUser.h", false)]
        SC_ICON = SC_MINIMIZE,

        [Obsolete("A Old version of SC_MAXIMIZE. Provided to be consistent with WinUser.h", false)]
        SC_ZOOM = SC_MAXIMIZE,
    }
}
