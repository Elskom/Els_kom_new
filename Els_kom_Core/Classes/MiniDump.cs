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
                // file does not exist until this line, but this throws a
                // "System.IO.IOException: The process cannot access the file
                // '%LocalAppData%\Els_kom-[Process ID].mdmp' because it is being used by another process."
                var fsToDump = new System.IO.FileStream(fileToDump,
                    System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite,
                    System.IO.FileShare.Write);
                var mINIDUMP_EXCEPTION_INFORMATION = new structs.MINIDUMP_EXCEPTION_INFORMATION
                {
                    ClientPointers = false,
                    ExceptionPointers = System.Runtime.InteropServices.Marshal.GetExceptionPointers(),
                    ThreadId = System.Convert.ToUInt32(System.Threading.Thread.CurrentThread.ManagedThreadId)
                };
                var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
                SafeNativeMethods.MiniDumpWriteDump(thisProcess.Handle, thisProcess.Id,
                    fsToDump.SafeFileHandle, Enums.MINIDUMP_TYPE.Normal,
                    ref mINIDUMP_EXCEPTION_INFORMATION, System.IntPtr.Zero, System.IntPtr.Zero);
                thisProcess.Dispose();
                fsToDump.Dispose();
                if (System.Runtime.InteropServices.Marshal.GetLastWin32Error() > 0)
                {
                    MessageManager.ShowError(
                        $"Mini-dumping failed with Code: {System.Runtime.InteropServices.Marshal.GetLastWin32Error()}",
                        "Error!");
                }
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
                // file does not exist until this line, but this throws a
                // "System.IO.IOException: The process cannot access the file
                // '%LocalAppData%\Els_kom-[Process ID].mdmp' because it is being used by another process."
                var fsToDump = new System.IO.FileStream(fileToDump,
                    System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite,
                    System.IO.FileShare.Write);
                var mINIDUMP_EXCEPTION_INFORMATION = new structs.MINIDUMP_EXCEPTION_INFORMATION
                {
                    ClientPointers = false,
                    ExceptionPointers = System.Runtime.InteropServices.Marshal.GetExceptionPointers(),
                    ThreadId = System.Convert.ToUInt32(System.Threading.Thread.CurrentThread.ManagedThreadId)
                };
                var thisProcess = System.Diagnostics.Process.GetCurrentProcess();
                SafeNativeMethods.MiniDumpWriteDump(thisProcess.Handle, thisProcess.Id,
                    fsToDump.SafeFileHandle, Enums.MINIDUMP_TYPE.WithDataSegs |
                    Enums.MINIDUMP_TYPE.WithFullMemory |
                    Enums.MINIDUMP_TYPE.WithProcessThreadData |
                    Enums.MINIDUMP_TYPE.WithFullMemoryInfo |
                    Enums.MINIDUMP_TYPE.WithThreadInfo |
                    Enums.MINIDUMP_TYPE.WithCodeSegs,
                    ref mINIDUMP_EXCEPTION_INFORMATION, System.IntPtr.Zero, System.IntPtr.Zero);
                thisProcess.Dispose();
                fsToDump.Dispose();
                if (System.Runtime.InteropServices.Marshal.GetLastWin32Error() > 0)
                {
                    MessageManager.ShowError(
                        $"Mini-dumping failed with Code: {System.Runtime.InteropServices.Marshal.GetLastWin32Error()}",
                        "Error!");
                }
            }
        }
    };
}
