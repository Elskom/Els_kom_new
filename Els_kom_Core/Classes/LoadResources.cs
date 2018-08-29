// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Loads Images from the resource section.
    /// </summary>
    internal static class LoadResources
    {
        /// <summary>
        /// Gets plain image Resource data and uses that data to construct an .NET Image Object.
        /// </summary>
        /// <exception cref="PlatformNotSupportedException">When the platform the function is run on is not Windows.</exception>
        /// <param name="resource">The number of the input resource.</param>
        /// <param name="type">The input resource type as a number.</param>
        /// <returns>A new <see cref="Image"/> from the specified resource.</returns>
        internal static Image GetImageResource(int resource, int type)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var hProc = Marshal.GetHINSTANCE(Assembly.GetEntryAssembly().GetModules()[0]);
                var image_resource = SafeNativeMethods.FindResourceW(hProc, (IntPtr)resource, (IntPtr)type);
                if (image_resource == IntPtr.Zero)
                {
                    return null;
                }

                var image_size = SafeNativeMethods.SizeofResource(hProc, image_resource);
                if (image_size == 0)
                {
                    return null;
                }

                var image_global = SafeNativeMethods.LoadResource(hProc, image_resource);
                if (image_global == IntPtr.Zero)
                {
                    return null;
                }

                var memPtr = new byte[image_size + 2u];
                var p_imagedata = SafeNativeMethods.LockResource(image_global);
                if (p_imagedata == IntPtr.Zero)
                {
                    return null;
                }

                Marshal.Copy(p_imagedata, memPtr, 0, (int)image_size);
                var stream = new MemoryStream(memPtr);
                stream.Write(memPtr, 0, (int)image_size);
                stream.Position = 0;
                return Image.FromStream(stream);
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Gets plain image Resource data with a specific Width and Height and uses that data to construct an .NET Image Object.
        /// </summary>
        /// <param name="resource">The id or name of the target resouce.</param>
        /// <param name="width">The width for the output icon.</param>
        /// <param name="height">The height for the output icon.</param>
        /// <returns>A new <see cref="Image"/> with the input size.</returns>
        internal static Image GetImageResource(string resource, int width, int height) => GetIconResource(resource, width, height)?.ToBitmap();

        /// <summary>
        /// Gets plain Icon image Resource data and uses that data to construct an .NET Icon Object.
        /// </summary>
        /// <param name="resource">The id or name of the target resouce.</param>
        /// <returns>A new <see cref="Icon"/> with the default small window icon size.</returns>
        internal static Icon GetIconResource(string resource) => GetIconResource(resource, 16, 16);

        /// <summary>
        /// Gets a file's base name.
        /// </summary>
        /// <param name="fileName">The file path to get the base name of.</param>
        /// <returns>The base name of the path provided containing the file's name.</returns>
        internal static string GetFileBaseName(string fileName)
        {
            var fi = new FileInfo(fileName);

            // return file base name without path to file.
            return fi.Name;
        }

        /// <summary>
        /// Gets plain Icon image Resource data with a specific Width and Height and uses that data to construct an .NET Icon Object.
        /// </summary>
        /// <param name="resource">The id or name of the target resouce.</param>
        /// <param name="width">The width for the output icon.</param>
        /// <param name="height">The height for the output icon.</param>
        /// <returns>A new <see cref="Icon"/> with the input size.</returns>
        private static Icon GetIconResource(string resource, int width, int height)
        {
            Icon retIcon = null;

            // Load from a *.resx if it exists, otherwise make one.
            var iconfiles = new string[]
            {
                ResourcesDir.Iconpath,
                ResourcesDir.Iconpath.Replace("els_kom", "YR"),
                ResourcesDir.Iconpath.Replace("els_kom", "VP_Trans")
            };
            var iconfile = string.Empty;
            if (resource.Equals("#1"))
            {
                iconfile = iconfiles[0];
            }
            else if (resource.Equals("#2"))
            {
                iconfile = iconfiles[1];
            }
            else if (resource.Equals("#3"))
            {
                iconfile = iconfiles[2];
            }

            if (!File.Exists(ResourcesDir.Resourcespath))
            {
                var resx = new ResXResourceWriter(ResourcesDir.Resourcespath);
                foreach (var iconFile in iconfiles)
                {
                    var iconstream = new MemoryStream(File.ReadAllBytes(iconFile));
                    var icon2Dump = new Icon(iconstream);
                    resx.AddResource(GetFileBaseName(iconFile).Trim(".ico".ToCharArray()), icon2Dump);
                    icon2Dump.Dispose();
                }

                // write resource file.
                resx.Dispose();
            }

            // now read it.
            var resxSet = new ResXResourceSet(ResourcesDir.Resourcespath);
            var iconold = (Icon)resxSet.GetObject(GetFileBaseName(iconfile).Trim(".ico".ToCharArray()));
            retIcon = new Icon(iconold, width, height);
            resxSet.Dispose();
            return retIcon;
        }
    }
}
