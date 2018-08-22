// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Structs
{
    // Pack=4 is important! So it works also for x64!

    /// <summary>
    /// Minidump Exception information struct.
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]
    internal struct MINIDUMP_EXCEPTION_INFORMATION
    {
        /// <summary>
        /// Thread ID of the exception.
        /// </summary>
        internal uint ThreadId;

        /// <summary>
        /// Pointers to the exception(s).
        /// </summary>
        internal System.IntPtr ExceptionPointers;

        /// <summary>
        /// Some client pointers bool.
        /// </summary>
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        internal bool ClientPointers;
    }
}
