// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.structs
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]  // Pack=4 is important! So it works also for x64!
    internal struct MINIDUMP_EXCEPTION_INFORMATION
    {
        internal uint ThreadId;
        internal System.IntPtr ExceptionPointers;
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        internal bool ClientPointers;
    }
}
