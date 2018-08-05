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
            structs.MINIDUMP_EXCEPTION_INFORMATION mINIDUMP_EXCEPTION_INFORMATION = new structs.MINIDUMP_EXCEPTION_INFORMATION
            {
                ClientPointers = 1,
                ExceptionPointers = System.Runtime.InteropServices.Marshal.GetExceptionPointers(),
                ThreadId = System.Convert.ToUInt32(System.Threading.Thread.CurrentThread.ManagedThreadId)
            };
            System.Diagnostics.Process thisProcess = System.Diagnostics.Process.GetCurrentProcess();
            SafeNativeMethods.MiniDumpWriteDump(thisProcess.Handle, thisProcess.Id,
                fsToDump.SafeFileHandle.DangerousGetHandle(), Enums.MINIDUMP_TYPE.MiniDumpNormal,
                ref mINIDUMP_EXCEPTION_INFORMATION, System.IntPtr.Zero, System.IntPtr.Zero);
            thisProcess.Dispose();
            fsToDump.Dispose();
        }

        /// <summary>
        /// Creates a Full Mini-dump in the file specified.
        /// </summary>
        public static void FullMiniDumpToFile(string fileToDump)
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
            structs.MINIDUMP_EXCEPTION_INFORMATION mINIDUMP_EXCEPTION_INFORMATION = new structs.MINIDUMP_EXCEPTION_INFORMATION
            {
                ClientPointers = 1,
                ExceptionPointers = System.Runtime.InteropServices.Marshal.GetExceptionPointers(),
                ThreadId = System.Convert.ToUInt32(System.Threading.Thread.CurrentThread.ManagedThreadId)
            };
            System.Diagnostics.Process thisProcess = System.Diagnostics.Process.GetCurrentProcess();
            SafeNativeMethods.MiniDumpWriteDump(thisProcess.Handle, thisProcess.Id,
                fsToDump.SafeFileHandle.DangerousGetHandle(), Enums.MINIDUMP_TYPE.MiniDumpWithDataSegs |
                Enums.MINIDUMP_TYPE.MiniDumpWithFullMemory |
                Enums.MINIDUMP_TYPE.MiniDumpWithProcessThreadData |
                Enums.MINIDUMP_TYPE.MiniDumpWithFullMemoryInfo |
                Enums.MINIDUMP_TYPE.MiniDumpWithThreadInfo |
                Enums.MINIDUMP_TYPE.MiniDumpWithCodeSegs,
                ref mINIDUMP_EXCEPTION_INFORMATION, System.IntPtr.Zero, System.IntPtr.Zero);
            thisProcess.Dispose();
            fsToDump.Dispose();
        }
    };
}
