// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Enums
{
    /// <summary>
    /// System Window Commands.
    /// </summary>
    internal enum SYSCOMMANDS
    {
        /// <summary>
        /// Did a Window size command happen?
        /// </summary>
        WM_SYSCOMMAND = 0x0112,

        /// <summary>
        /// Window is maximized.
        /// </summary>
        SC_MAXIMIZE = 0xf030,

        /// <summary>
        /// Window is minimized.
        /// </summary>
        SC_MINIMIZE = 0xf020,

        /// <summary>
        /// Window is restored back to normal size.
        /// </summary>
        SC_RESTORE = 0xf120,
    }
}
