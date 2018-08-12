// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    internal static class SafeNativeMethods
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "FindResourceW")]
        internal static extern System.IntPtr FindResourceW(System.IntPtr hModule, System.IntPtr lpName, System.IntPtr lpType);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "SizeofResource")]
        internal static extern uint SizeofResource(System.IntPtr hModule, System.IntPtr hResInfo);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "LoadResource")]
        internal static extern System.IntPtr LoadResource(System.IntPtr hModule, System.IntPtr hResInfo);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "LockResource")]
        internal static extern System.IntPtr LockResource(System.IntPtr hResData);
        [System.Runtime.InteropServices.DllImport("dbghelp.dll")]
        internal static extern bool MiniDumpWriteDump(System.IntPtr hProcess, int ProcessId, System.IntPtr hFile, Enums.MINIDUMP_TYPE DumpType, ref structs.MINIDUMP_EXCEPTION_INFORMATION ExceptionParam, System.IntPtr UserStreamParam, System.IntPtr CallackParam);
    }
}
