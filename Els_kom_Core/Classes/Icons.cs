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
                var iconVal = string.Empty;
                var resource = string.Empty;
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
                if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    resource = "#1";
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
                resource = "els_kom";
                if (iconVal.Equals("1"))
                {
                    resource = "VP_Trans";
                }
                else if (iconVal.Equals("2"))
                {
                    resource = "YR";
                }
                var assembly = System.Reflection.Assembly.GetEntryAssembly();
                var type = assembly.GetType("Els_kom.Properties.Resources", true);
                var property = type.GetProperty(resource);
                var methodinfo = property.GetGetMethod();
                return (System.Drawing.Icon)methodinfo.Invoke(null, null);
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
                var iconVal = string.Empty;
                var resource = string.Empty;
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
                if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    resource = "#1";
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
                resource = "els_kom";
                if (iconVal.Equals("1"))
                {
                    resource = "VP_Trans";
                }
                else if (iconVal.Equals("2"))
                {
                    resource = "YR";
                }
                var assembly = System.Reflection.Assembly.GetEntryAssembly();
                var type = assembly.GetType("Els_kom.Properties.Resources", true);
                var property = type.GetProperty(resource);
                var methodinfo = property.GetGetMethod();
                var oldicon = (System.Drawing.Icon)methodinfo.Invoke(null, null);
                var newicon = new System.Drawing.Icon(oldicon, 48, 48);
                return newicon?.ToBitmap();
            }
        }
    }
}
