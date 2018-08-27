// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    // Forms designer cannot see these if they are "internal" but I wanted them Internal.
    // The WinForms team probably should base the Forms Designer off of Roslyn sometime?

    /// <summary>
    /// Els_kom Icon Resource Class.
    /// </summary>
    public static class Icons
    {
        /// <summary>
        /// Gets the form icon from the project's resources.
        /// </summary>
        public static System.Drawing.Icon FormIcon
        {
            get
            {
                var iconVal = string.Empty;
                Els_kom_Core.Classes.XMLObject settingsxml = null;
                if (Els_kom_Core.Classes.SettingsFile.Settingsxml == null)
                {
                    settingsxml = new Els_kom_Core.Classes.XMLObject(Els_kom_Core.Classes.SettingsFile.Path, "<Settings></Settings>");
                    iconVal = settingsxml.Read("WindowIcon");
                }
                else
                {
                    Els_kom_Core.Classes.SettingsFile.Settingsxml.ReopenFile();
                    iconVal = Els_kom_Core.Classes.SettingsFile.Settingsxml.Read("WindowIcon");
                }

                // dispose this temporary object.
                settingsxml?.Dispose();
                var retIcon = Properties.Resources.els_kom;
                if (iconVal.Equals("1"))
                {
                    retIcon = Properties.Resources.VP_Trans;
                }
                else if (iconVal.Equals("2"))
                {
                    retIcon = Properties.Resources.YR;
                }

                return retIcon;
            }
        }

        /// <summary>
        /// Gets a 48 x 48 sized <see cref="System.Drawing.Image"/>
        /// from a icon in the project's resources.
        /// </summary>
        public static System.Drawing.Image FormImage
        {
            get
            {
                var iconVal = string.Empty;
                Els_kom_Core.Classes.XMLObject settingsxml = null;
                if (Els_kom_Core.Classes.SettingsFile.Settingsxml == null)
                {
                    settingsxml = new Els_kom_Core.Classes.XMLObject(Els_kom_Core.Classes.SettingsFile.Path, "<Settings></Settings>");
                    iconVal = settingsxml.Read("WindowIcon");
                }
                else
                {
                    Els_kom_Core.Classes.SettingsFile.Settingsxml.ReopenFile();
                    iconVal = Els_kom_Core.Classes.SettingsFile.Settingsxml.Read("WindowIcon");
                }

                // dispose this temporary object.
                settingsxml?.Dispose();
                var oldicon = Properties.Resources.els_kom;
                if (iconVal.Equals("1"))
                {
                    oldicon = Properties.Resources.VP_Trans;
                }
                else if (iconVal.Equals("2"))
                {
                    oldicon = Properties.Resources.YR;
                }

                var newicon = new System.Drawing.Icon(oldicon, 48, 48);
                return newicon?.ToBitmap();
            }
        }
    }
}
