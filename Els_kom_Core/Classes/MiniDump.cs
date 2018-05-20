// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    public class MiniDump
    {
        public static void MiniDumpToFile(string fileToDump)
        {
            System.IO.FileStream fsToDump = null;
            if (System.IO.File.Exists(fileToDump))
            {
                fsToDump = System.IO.File.Open(fileToDump, System.IO.FileMode.Append);
            }
            else
            {
                fsToDump = System.IO.File.Create(fileToDump);
            }
            System.Diagnostics.Process thisProcess = System.Diagnostics.Process.GetCurrentProcess();
            SafeNativeMethods.MiniDumpWriteDump(thisProcess.Handle, thisProcess.Id,
                fsToDump.SafeFileHandle.DangerousGetHandle(), MINIDUMP_TYPE.MiniDumpNormal,
                System.IntPtr.Zero, System.IntPtr.Zero, System.IntPtr.Zero);
            thisProcess.Dispose();
            fsToDump.Dispose();
        }
    };
}
