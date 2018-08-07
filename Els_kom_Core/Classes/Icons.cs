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
                    var iconVal = string.Empty;
                    XMLObject Settingsxml = null;
                    if (SettingsFile.Settingsxml == null)
                    {
                        Settingsxml = new XMLObject(SettingsFile.Path, "<Settings></Settings>");
                        iconVal = Settingsxml.Read("WindowIcon");
                    }
                    else
                    {
                        SettingsFile.Settingsxml.ReopenFile();
                        iconVal = SettingsFile.Settingsxml.Read("WindowIcon");
                    }
                    // dispose this temporary object.
                    Settingsxml?.Dispose();
                    var resource = "#1";
                    if (iconVal.Equals("1"))
                    {
                        resource = "#3";
                    }
                    else if (iconVal.Equals("2"))
                    {
                        resource = "#2";
                    }
                    return LoadResources.GetIconResource(resource);
                }
                else
                {
                    // load from elsewhere like from a resx file
                    // or embedded entry application resources.
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
                    var iconVal = string.Empty;
                    XMLObject Settingsxml = null;
                    if (SettingsFile.Settingsxml == null)
                    {
                        Settingsxml = new XMLObject(SettingsFile.Path, "<Settings></Settings>");
                        iconVal = Settingsxml.Read("WindowIcon");
                    }
                    else
                    {
                        SettingsFile.Settingsxml.ReopenFile();
                        iconVal = SettingsFile.Settingsxml.Read("WindowIcon");
                    }
                    // dispose this temporary object.
                    Settingsxml?.Dispose();
                    var resource = "#1";
                    if (iconVal.Equals("1"))
                    {
                        resource = "#3";
                    }
                    else if (iconVal.Equals("2"))
                    {
                        resource = "#2";
                    }
                    return LoadResources.GetImageResource(resource, 48, 48);
                }
                else
                {
                    // load from elsewhere like from a resx file
                    // or embedded entry application resources.
                    return null;
                }
            }
        }
    }
}
