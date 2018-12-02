// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using System.Runtime.InteropServices;
    using Els_kom_Core.Enums;

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
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "FindResourceW")]
        internal static extern IntPtr FindResourceW(IntPtr hModule, IntPtr lpName, IntPtr lpType);

        /// <summary>
        /// Gets the size of the resource information.
        /// </summary>
        /// <param name="hModule">The Assembly handle.</param>
        /// <param name="hResInfo">The resource information.</param>
        /// <returns>The size of the resource entry.</returns>
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "SizeofResource")]
        internal static extern uint SizeofResource(IntPtr hModule, IntPtr hResInfo);

        /// <summary>
        /// Loads the Resource the resource information contains.
        /// </summary>
        /// <param name="hModule">The Assembly handle.</param>
        /// <param name="hResInfo">The resource information.</param>
        /// <returns>A pointer to the resource data.</returns>
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "LoadResource")]
        internal static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

        /// <summary>
        /// Locks the Resource data to a value.
        /// </summary>
        /// <param name="hResData">The resource data pointer.</param>
        /// <returns>A pointer to the resource contents.</returns>
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "LockResource")]
        internal static extern IntPtr LockResource(IntPtr hResData);
    }
}
