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
                return LoadResources.GetIconResource("#1",
                    System.Windows.Forms.Application.StartupPath + "\\els_kom.ico",
                    "Els_kom_resources.dll");
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
                return LoadResources.GetImageResource("#1",
                    48, 48, System.Windows.Forms.Application.StartupPath + "\\els_kom.ico",
                    "Els_kom_resources.dll");
            }
        }
    }
}
