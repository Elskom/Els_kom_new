// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    public class MiniDump
    {
        internal enum MINIDUMP_TYPE
        {
            MiniDumpNormal = 0x00000000,
            MiniDumpWithDataSegs = 0x00000001,
            MiniDumpWithFullMemory = 0x00000002,
            MiniDumpWithHandleData = 0x00000004,
            MiniDumpFilterMemory = 0x00000008,
            MiniDumpScanMemory = 0x00000010,
            MiniDumpWithUnloadedModules = 0x00000020,
            MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
            MiniDumpFilterModulePaths = 0x00000080,
            MiniDumpWithProcessThreadData = 0x00000100,
            MiniDumpWithPrivateReadWriteMemory = 0x00000200,
            MiniDumpWithoutOptionalData = 0x00000400,
            MiniDumpWithFullMemoryInfo = 0x00000800,
            MiniDumpWithThreadInfo = 0x00001000,
            MiniDumpWithCodeSegs = 0x00002000
        }

        [DllImport( "dbghelp.dll" )]
        static extern bool MiniDumpWriteDump(
            System.IntPtr hProcess,
            int ProcessId,
            System.IntPtr hFile,
            MINIDUMP_TYPE DumpType,
            System.IntPtr ExceptionParam,
            System.IntPtr UserStreamParam,
            System.IntPtr CallackParam);

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
            Process thisProcess = Process.GetCurrentProcess();
            MiniDumpWriteDump(thisProcess.Handle, thisProcess.Id,
                fsToDump.SafeFileHandle.DangerousGetHandle(), MINIDUMP_TYPE.MiniDumpNormal,
                System.IntPtr.Zero, System.IntPtr.Zero, System.IntPtr.Zero);
            fsToDump.Close();
        }
    };
}
