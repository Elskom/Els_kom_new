// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Loads Images from resource section.
    /// </summary>
    public static class LoadResources
    {
        /// <summary>
        /// Gets plain image Resource data and uses that data to construct an .NET Image Object.
        /// </summary>
        public static System.Drawing.Image GetImageResource(int resource, int type)
        {
            System.IntPtr hProc = System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetEntryAssembly().GetModules()[0]);
            System.IntPtr image_resource = SafeNativeMethods.FindResourceW(hProc, (System.IntPtr)resource, (System.IntPtr)type);
            if (image_resource == System.IntPtr.Zero)
            {
                return null;
            }
            uint image_size = SafeNativeMethods.SizeofResource(hProc, image_resource);
            if (image_size == 0)
            {
                return null;
            }
            System.IntPtr image_global = SafeNativeMethods.LoadResource(hProc, image_resource);
            if (image_global == System.IntPtr.Zero)
            {
                return null;
            }
            byte[] MemPtr = new byte[image_size + 2u];
            System.IntPtr p_imagedata = SafeNativeMethods.LockResource(image_global);
            if (p_imagedata == System.IntPtr.Zero)
            {
                return null;
            }
            System.Runtime.InteropServices.Marshal.Copy(p_imagedata, MemPtr, 0, (int)image_size);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(MemPtr);
            stream.Write(MemPtr, 0, (int)image_size);
            stream.Position = 0;
            return System.Drawing.Image.FromStream(stream);
        }

        /// <summary>
        /// Gets plain image Resource data with a specific Width and Height and uses that data to construct an .NET Image Object.
        /// </summary>
        public static System.Drawing.Image GetImageResource(string resource, int Width, int Height, string filenamefallback, string fallbackmodule) => GetIconResource(resource, Width, Height, filenamefallback, fallbackmodule)?.ToBitmap();

        /// <summary>
        /// Gets plain Icon image Resource data with a specific Width and Height and uses that data to construct an .NET Icon Object.
        /// </summary>
        private static System.Drawing.Icon GetIconResource(string resource, int Width, int Height, string filenamefallback, string fallbackmodule)
        {
            System.IntPtr hIcon = SafeNativeMethods._LoadIconErrorChecked(resource, Width, Height, filenamefallback, fallbackmodule);
            System.Drawing.Icon ico = null;
            if (hIcon != System.IntPtr.Zero)
            {
                ico = System.Drawing.Icon.FromHandle(hIcon);
            }
            return ico;
        }

        /// <summary>
        /// Gets plain Icon image Resource data and uses that data to construct an .NET Icon Object.
        /// </summary>
        public static System.Drawing.Icon GetIconResource(string resource, string filenamefallback, string fallbackmodule) => GetIconResource(resource, 16, 16, filenamefallback, fallbackmodule);
    }
}
