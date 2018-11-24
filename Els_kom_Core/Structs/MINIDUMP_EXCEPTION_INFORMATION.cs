// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Structs
{
    using System;
    using System.Runtime.InteropServices;

    // Pack=4 is important! So it works also for x64!

    /// <summary>
    /// Minidump Exception information struct.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct MINIDUMP_EXCEPTION_INFORMATION
    {
        /// <summary>
        /// Thread ID of the exception.
        /// </summary>
        internal uint ThreadId;

        /// <summary>
        /// Pointers to the exception(s).
        /// </summary>
        internal IntPtr ExceptionPointers;

        /// <summary>
        /// Some client pointers bool.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        internal bool ClientPointers;
    }
}
