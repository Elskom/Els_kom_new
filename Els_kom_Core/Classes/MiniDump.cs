// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class in the Core that allows creating Mini-dumps when a fatal exception occurs.
    /// </summary>
    internal static class MiniDump
    {
        /// <summary>
        /// Creates a Mini-dump in the file specified.
        /// </summary>
        internal static void MiniDumpToFile(string fileToDump)
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
                System.Runtime.InteropServices.OSPlatform.Windows))
            {
                var fsToDump = System.IO.File.Exists(fileToDump)
                ? System.IO.File.Open(fileToDump, System.IO.FileMode.Append)
                : System.IO.File.Create(fileToDump);
                var mINIDUMP_EXCEPTION_INFORMATION = new structs.MINIDUMP_EXCEPTION_INFORMATION
                {
                    ClientPointers = 1,
                    ExceptionPointers = System.Runtime.InteropServices.Marshal.GetExceptionPointers(),
                    ThreadId = System.Convert.ToUInt32(System.Threading.Thread.CurrentThread.ManagedThreadId)
                };
                var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
                SafeNativeMethods.MiniDumpWriteDump(thisProcess.Handle, thisProcess.Id,
                    fsToDump.SafeFileHandle.DangerousGetHandle(), Enums.MINIDUMP_TYPE.MiniDumpNormal,
                    ref mINIDUMP_EXCEPTION_INFORMATION, System.IntPtr.Zero, System.IntPtr.Zero);
                thisProcess.Dispose();
                fsToDump.Dispose();
            }
        }

        /// <summary>
        /// Creates a Full Mini-dump in the file specified.
        /// </summary>
        internal static void FullMiniDumpToFile(string fileToDump)
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
                System.Runtime.InteropServices.OSPlatform.Windows))
            {
                var fsToDump = System.IO.File.Exists(fileToDump)
                ? System.IO.File.Open(fileToDump, System.IO.FileMode.Append)
                : System.IO.File.Create(fileToDump);
                var mINIDUMP_EXCEPTION_INFORMATION = new structs.MINIDUMP_EXCEPTION_INFORMATION
                {
                    ClientPointers = 1,
                    ExceptionPointers = System.Runtime.InteropServices.Marshal.GetExceptionPointers(),
                    ThreadId = System.Convert.ToUInt32(System.Threading.Thread.CurrentThread.ManagedThreadId)
                };
                var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
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
        }
    };
}
