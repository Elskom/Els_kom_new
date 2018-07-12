// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Els_kom Icon Resource Class.
    /// </summary>
    public static class Icons
    {
        /// <summary>
        /// Gets the form icon from Resource Section.
        /// </summary>
        public static System.Drawing.Icon FormIcon
        {
            get
            {
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
                    System.Runtime.InteropServices.OSPlatform.Windows))
                {
                    return LoadResources.GetIconResource("#1");
                }
                else
                {
                    // load from elsewhere like from a resx file.
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets a 48 x 48 sized <see cref="System.Drawing.Image"/>
        /// from a icon in the Resource Section.
        /// </summary>
        public static System.Drawing.Image FormImage
        {
            get
            {
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
                    System.Runtime.InteropServices.OSPlatform.Windows))
                {
                    return LoadResources.GetImageResource("#1", 48, 48);
                }
                else
                {
                    // load from elsewhere like from a resx file.
                    return null;
                }
            }
        }
    }
}
