// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class in the Core that allows creating Mini-dumps when a fatal exception occurs.
    /// </summary>
    public class MiniDump
    {
        /// <summary>
        /// Creates a Mini-dump in the file specified.
        /// </summary>
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
                fsToDump.SafeFileHandle.DangerousGetHandle(), Enums.MINIDUMP_TYPE.MiniDumpNormal,
                System.IntPtr.Zero, System.IntPtr.Zero, System.IntPtr.Zero);
            thisProcess.Dispose();
            fsToDump.Dispose();
        }
    };
}
