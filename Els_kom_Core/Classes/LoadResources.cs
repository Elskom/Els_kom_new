// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Loads Images from the resource section.
    /// </summary>
    internal static class LoadResources
    {
        /// <summary>
        /// Gets plain image Resource data and uses that data to construct an .NET Image Object.
        /// </summary>
        /// <exception cref="System.PlatformNotSupportedException">When the platform the function is run on is not Windows.</exception>
        internal static System.Drawing.Image GetImageResource(int resource, int type)
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
                System.Runtime.InteropServices.OSPlatform.Windows))
            {
                var hProc = System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetEntryAssembly().GetModules()[0]);
                var image_resource = SafeNativeMethods.FindResourceW(hProc, (System.IntPtr)resource, (System.IntPtr)type);
                if (image_resource == System.IntPtr.Zero)
                {
                    return null;
                }
                var image_size = SafeNativeMethods.SizeofResource(hProc, image_resource);
                if (image_size == 0)
                {
                    return null;
                }
                var image_global = SafeNativeMethods.LoadResource(hProc, image_resource);
                if (image_global == System.IntPtr.Zero)
                {
                    return null;
                }
                var MemPtr = new byte[image_size + 2u];
                var p_imagedata = SafeNativeMethods.LockResource(image_global);
                if (p_imagedata == System.IntPtr.Zero)
                {
                    return null;
                }
                System.Runtime.InteropServices.Marshal.Copy(p_imagedata, MemPtr, 0, (int)image_size);
                var stream = new System.IO.MemoryStream(MemPtr);
                stream.Write(MemPtr, 0, (int)image_size);
                stream.Position = 0;
                return System.Drawing.Image.FromStream(stream);
            }
            else
            {
                throw new System.PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Gets plain image Resource data with a specific Width and Height and uses that data to construct an .NET Image Object.
        /// </summary>
        internal static System.Drawing.Image GetImageResource(string resource, int Width, int Height) => GetIconResource(resource, Width, Height)?.ToBitmap();

        private static string GetFileBaseName(string FileName)
        {
            var fi = new System.IO.FileInfo(FileName);
            // return file base name without path to file.
            return fi.Name;
        }

        /// <summary>
        /// Gets plain Icon image Resource data with a specific Width and Height and uses that data to construct an .NET Icon Object.
        /// </summary>
        private static System.Drawing.Icon GetIconResource(string resource, int Width, int Height)
        {
            System.Drawing.Icon retIcon = null;
            // Load from a *.resx if it exists, otherwise make one.
            var iconfiles = new string[] {
                ResourcesDir.iconpath,
                ResourcesDir.iconpath.Replace("els_kom", "YR"),
                ResourcesDir.iconpath.Replace("els_kom", "VP_Trans")
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
            if (!System.IO.File.Exists(ResourcesDir.resourcespath))
            {
                var resx = new System.Resources.ResXResourceWriter(ResourcesDir.resourcespath);
                foreach (var iconFile in iconfiles)
                {
                    var iconstream = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(iconFile));
                    var icon2Dump = new System.Drawing.Icon(iconstream);
                    resx.AddResource(GetFileBaseName(iconFile).Trim(".ico".ToCharArray()), icon2Dump);
                    icon2Dump.Dispose();
                }
                // write resource file.
                resx.Dispose();
            }
            // now read it.
            var resxSet = new System.Resources.ResXResourceSet(ResourcesDir.resourcespath);
            var iconold = (System.Drawing.Icon)resxSet.GetObject(GetFileBaseName(iconfile).Trim(".ico".ToCharArray()));
            retIcon = new System.Drawing.Icon(iconold, Width, Height);
            resxSet.Dispose();
            return retIcon;
        }

        /// <summary>
        /// Gets plain Icon image Resource data and uses that data to construct an .NET Icon Object.
        /// </summary>
        internal static System.Drawing.Icon GetIconResource(string resource) => GetIconResource(resource, 16, 16);
    }
}
