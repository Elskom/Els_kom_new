// Copyright (c) 2014-2019, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using Elskom.Generic.Libs;
    using XmlAbstraction;

    // Forms designer cannot see these if they are "internal" but I wanted them Internal.
    // The WinForms team probably should base the Forms Designer off of Roslyn sometime?

    /// <summary>
    /// Els_kom Icon Resource Class.
    /// </summary>
    public static class Icons
    {
        private static ResourceManager resourceMan;

        private static CultureInfo resourceCulture;

        /// <summary>
        /// Gets the form icon from the project's resources.
        /// </summary>
        /// <value>
        /// The form icon from the project's resources.
        /// </value>
        public static Icon FormIcon
        {
            get
            {
                if (resourceMan == null)
                {
                    resourceMan = new ResourceManager("Els_kom.Properties.Resources", typeof(Icons).Assembly);
                }

                if (resourceCulture == null)
                {
                    resourceCulture = CultureInfo.CurrentCulture;
                }

                var retIcon = (Icon)resourceMan.GetObject("els_kom", resourceCulture);
                try
                {
                    var iconVal = string.Empty;
                    if (SettingsFile.Settingsxml == null)
                    {
                        var settingsxml = new XmlObject(SettingsFile.Path, "<Settings></Settings>");
                        iconVal = settingsxml.TryRead("WindowIcon");

                        // dispose this temporary object.
                        settingsxml = null;
                    }
                    else
                    {
                        SettingsFile.Settingsxml.ReopenFile();
                        iconVal = SettingsFile.Settingsxml.TryRead("WindowIcon");
                    }

                    if (iconVal.Equals("1"))
                    {
                        retIcon = (Icon)resourceMan.GetObject("VP_Trans", resourceCulture);
                    }
                    else if (iconVal.Equals("2"))
                    {
                        retIcon = (Icon)resourceMan.GetObject("YR", resourceCulture);
                    }
                }
                catch (System.DllNotFoundException)
                {
                    // if a dependency of this method cannot be loaded it could be because this is executed
                    // in the forms designer and the retardid thing does not factor in nuget packages when
                    // looking for assemblies to load.
                }

                return retIcon;
            }
        }

        /// <summary>
        /// Gets a 48 x 48 sized <see cref="Image"/>
        /// from a icon in the project's resources.
        /// </summary>
        /// <value>
        /// A 48 x 48 sized <see cref="Image"/>
        /// from a icon in the project's resources.
        /// </value>
        public static Image FormImage
        {
            get
            {
                using (var newicon = new Icon(FormIcon, 48, 48))
                {
                    return newicon?.ToBitmap();
                }
            }
        }
    }
}
