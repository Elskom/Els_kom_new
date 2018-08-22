// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Windows Native Methods.
    /// </summary>
    internal static class SafeNativeMethods
    {
        /// <summary>
        /// Find a resource in the Resource section of a assembly.
        /// </summary>
        /// <param name="hModule">The Assembly handle.</param>
        /// <param name="lpName">The resource name as a pointer.</param>
        /// <param name="lpType">The resource type as a pointer.</param>
        /// <returns>The Resoruce information pointer.</returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "FindResourceW")]
        internal static extern System.IntPtr FindResourceW(System.IntPtr hModule, System.IntPtr lpName, System.IntPtr lpType);

        /// <summary>
        /// Gets the size of the resource information.
        /// </summary>
        /// <param name="hModule">The Assembly handle.</param>
        /// <param name="hResInfo">The resource information.</param>
        /// <returns>The size of the resource entry.</returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "SizeofResource")]
        internal static extern uint SizeofResource(System.IntPtr hModule, System.IntPtr hResInfo);

        /// <summary>
        /// Loads the Resource the resource information contains.
        /// </summary>
        /// <param name="hModule">The Assembly handle.</param>
        /// <param name="hResInfo">The resource information.</param>
        /// <returns>A pointer to the resource data.</returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "LoadResource")]
        internal static extern System.IntPtr LoadResource(System.IntPtr hModule, System.IntPtr hResInfo);

        /// <summary>
        /// Locks the Resource data to a value.
        /// </summary>
        /// <param name="hResData">The resource data pointer.</param>
        /// <returns>A pointer to the resource contents.</returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "LockResource")]
        internal static extern System.IntPtr LockResource(System.IntPtr hResData);

        /// <summary>
        /// Gets the current thread.
        /// </summary>
        /// <returns>The current native thread id.</returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        internal static extern uint GetCurrentThreadId();

        /// <summary>
        /// Writes the process execution information to a mini-dump for debugging.
        /// </summary>
        /// <param name="hProcess">The process handle.</param>
        /// <param name="ProcessId">The process ID.</param>
        /// <param name="hFile">The file handle to dump to.</param>
        /// <param name="DumpType">The type of mini-dump.</param>
        /// <param name="ExceptionParam">The exception information stuff.</param>
        /// <param name="UserStreamParam">User stream stuff for the dumps.</param>
        /// <param name="CallackParam">Callback function pointer?</param>
        /// <returns>if the mini-dump worked or not?</returns>
        [System.Runtime.InteropServices.DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        internal static extern bool MiniDumpWriteDump(System.IntPtr hProcess, int ProcessId, System.Runtime.InteropServices.SafeHandle hFile, Enums.MINIDUMP_TYPE DumpType, ref Structs.MINIDUMP_EXCEPTION_INFORMATION ExceptionParam, System.IntPtr UserStreamParam, System.IntPtr CallackParam);
    }
}
