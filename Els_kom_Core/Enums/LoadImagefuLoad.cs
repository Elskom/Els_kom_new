// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Enums
{
    /// <summary>
    /// Image loading flags for images on native PE resource sections.
    /// </summary>
    [System.Flags]
    internal enum LoadImagefuLoad
    {
        /// <summary>
        /// Create DIB section.
        /// </summary>
        LR_CREATEDIBSECTION = 0x00002000,

        /// <summary>
        /// Use default colors.
        /// </summary>
        LR_DEFAULTCOLOR = 0x00000000,

        /// <summary>
        /// Use default size.
        /// </summary>
        LR_DEFAULTSIZE = 0x00000040,

        /// <summary>
        /// Load from a file.
        /// </summary>
        LR_LOADFROMFILE = 0x00000010,

        /// <summary>
        /// Map 3D colors.
        /// </summary>
        LR_LOADMAP3DCOLORS = 0x00001000,

        /// <summary>
        /// Load with transparency.
        /// </summary>
        LR_LOADTRANSPARENT = 0x00000020,

        /// <summary>
        /// Load monochrome version.
        /// </summary>
        LR_MONOCHROME = 0x00000001,

        /// <summary>
        /// Load as a shared resource.
        /// </summary>
        LR_SHARED = 0x00008000,

        /// <summary>
        /// Load with VGA (256?) colors.
        /// </summary>
        LR_VGACOLOR = 0x00000080
    }
}
