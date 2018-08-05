// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.structs
{
    internal struct MINIDUMP_EXCEPTION_INFORMATION : System.IDisposable
    {
        internal uint ThreadId;
        internal System.IntPtr ExceptionPointers;
        internal int ClientPointers;

        public void Dispose()
        {
            ThreadId = 0;
            ExceptionPointers = System.IntPtr.Zero;
            ClientPointers = 0;
        }
    }
}
