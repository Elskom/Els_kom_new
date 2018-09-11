// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Enums
{
    using System;

    /// <summary>
    /// Image loading types for images on native PE resource sections.
    /// </summary>
    [Flags]
    internal enum LoadImageTypes
    {
        /// <summary>
        /// Load bitmap resource.
        /// </summary>
        IMAGE_BITMAP = 0,

        /// <summary>
        /// Load icon resource.
        /// </summary>
        IMAGE_ICON = 1,

        /// <summary>
        /// Load cursor resoruce.
        /// </summary>
        IMAGE_CURSOR = 2,
    }
}
