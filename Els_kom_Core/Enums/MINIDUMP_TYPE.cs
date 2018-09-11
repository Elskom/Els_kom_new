// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Enums
{
    using System;

    /// <summary>
    /// The flags for the type of mini-dumps to generate.
    /// </summary>
    [Flags]
    internal enum MINIDUMP_TYPE
    {
        /// <summary>
        /// A normal mini-dump.
        /// </summary>
        Normal = 0x00000000,

        /// <summary>
        /// Include data segments.
        /// </summary>
        WithDataSegs = 0x00000001,

        /// <summary>
        /// Include full memory.
        /// </summary>
        WithFullMemory = 0x00000002,

        /// <summary>
        /// Include handle data.
        /// </summary>
        WithHandleData = 0x00000004,

        /// <summary>
        /// Filter the memory.
        /// </summary>
        FilterMemory = 0x00000008,

        /// <summary>
        /// Scan the memory.
        /// </summary>
        ScanMemory = 0x00000010,

        /// <summary>
        /// Include unloaded dll's.
        /// </summary>
        WithUnloadedModules = 0x00000020,

        /// <summary>
        /// Include indirectly referenced memory.
        /// </summary>
        WithIndirectlyReferencedMemory = 0x00000040,

        /// <summary>
        /// Filter dll paths.
        /// </summary>
        FilterModulePaths = 0x00000080,

        /// <summary>
        /// Include process thread data.
        /// </summary>
        WithProcessThreadData = 0x00000100,

        /// <summary>
        /// Include private read and write memory.
        /// </summary>
        WithPrivateReadWriteMemory = 0x00000200,

        /// <summary>
        /// Include optional data.
        /// </summary>
        WithoutOptionalData = 0x00000400,

        /// <summary>
        /// Include full memory information.
        /// </summary>
        WithFullMemoryInfo = 0x00000800,

        /// <summary>
        /// Include thread information.
        /// </summary>
        WithThreadInfo = 0x00001000,

        /// <summary>
        /// Include code segments.
        /// </summary>
        WithCodeSegs = 0x00002000,

        /// <summary>
        /// Exclude Auxiliary state.
        /// </summary>
        WithoutAuxiliaryState = 0x00004000,

        /// <summary>
        /// Include full Auxiliary state.
        /// </summary>
        WithFullAuxiliaryState = 0x00008000,

        /// <summary>
        /// Include private write and copy memory.
        /// </summary>
        WithPrivateWriteCopyMemory = 0x00010000,

        /// <summary>
        /// Ignore inaccessible memory.
        /// </summary>
        IgnoreInaccessibleMemory = 0x00020000,

        /// <summary>
        /// Include Valid types?.
        /// </summary>
        ValidTypeFlags = 0x0003ffff,
    }
}
