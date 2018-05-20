// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    internal static class SafeNativeMethods
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "FindResourceW")]
        internal static extern System.IntPtr FindResourceW(System.IntPtr hModule, System.IntPtr lpName, System.IntPtr lpType);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "SizeofResource")]
        internal static extern uint SizeofResource(System.IntPtr hModule, System.IntPtr hResInfo);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "LoadResource")]
        internal static extern System.IntPtr LoadResource(System.IntPtr hModule, System.IntPtr hResInfo);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "LockResource")]
        internal static extern System.IntPtr LockResource(System.IntPtr hResData);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        internal static extern System.IntPtr LoadImage(System.IntPtr hInst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        internal static extern System.IntPtr LoadLibrary(string lpLibFileName);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        internal static extern int FreeLibrary(System.IntPtr hLibModule);
        [System.Runtime.InteropServices.DllImport("dbghelp.dll")]
        internal static extern bool MiniDumpWriteDump(System.IntPtr hProcess, int ProcessId, System.IntPtr hFile, Enums.MINIDUMP_TYPE DumpType, System.IntPtr ExceptionParam, System.IntPtr UserStreamParam, System.IntPtr CallackParam);

        internal static System.IntPtr _LoadIconErrorChecked(string resource, int Width, int Height, string filenamefallback, string fallbackmodule)
        {
            int error = 0;
            int llerr = 0;
            System.IntPtr hIcon = System.IntPtr.Zero;
            System.Reflection.Module hMod = null;
            try
            {
                hMod = System.Reflection.Assembly.GetEntryAssembly().GetModules()[0];
            }
            catch (System.Exception)
            {
            }
            if (hMod != null)
            {
                System.IntPtr hProc = System.Runtime.InteropServices.Marshal.GetHINSTANCE(hMod);
                hIcon = LoadImage(hProc, resource, (uint)Enums.LoadImageTypes.IMAGE_ICON, Width, Height, (uint)Enums.LoadImagefuLoad.LR_SHARED);
                error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                // LoadImage failed... Use fallback Icon to load.
                if (error != 0)
                {
                    hIcon = LoadImage((System.IntPtr)0, filenamefallback, (uint)Enums.LoadImageTypes.IMAGE_ICON, Width, Height, (uint)Enums.LoadImagefuLoad.LR_SHARED | (uint)Enums.LoadImagefuLoad.LR_LOADFROMFILE);
                    error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                    if (error != 0)
                    {
                        System.IntPtr mod = LoadLibrary(fallbackmodule);
                        llerr = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                        hIcon = LoadImage(mod, resource, (uint)Enums.LoadImageTypes.IMAGE_ICON, Width, Height, (uint)Enums.LoadImagefuLoad.LR_SHARED);
                        error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                        FreeLibrary(mod);
                    }
                }
            }
            else
            {
                // Forms Designer hack.
                System.IntPtr mod2 = LoadLibrary(BuildDir.buildfolder + fallbackmodule);
                llerr = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                hIcon = LoadImage(mod2, resource, (uint)Enums.LoadImageTypes.IMAGE_ICON, Width, Height, (uint)Enums.LoadImagefuLoad.LR_SHARED);
                error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                FreeLibrary(mod2);
            }
            if (hIcon == System.IntPtr.Zero)
            {
                if (llerr != 0)
                {
                    MessageManager.ShowError("LoadLibrary returned Error Code: " + llerr + ".", "Error!");
                }
                MessageManager.ShowError("LoadImage returned Error Code: " + error + ".", "Error!");
            }
            return hIcon;
        }
    }
}
