// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom;

using TerraFX.Interop.Windows;

// for screenshots.
internal static class ScreenShots
{
    internal static Bitmap MakeScreenShot()
    {
        var sz = default(Size);
        foreach (var screen in Screen.AllScreens.Select(screen => screen.Bounds).Select(screen => screen.Size))
        {
            sz.Width += screen.Width;
            sz.Height += screen.Height;
        }

        var hDesk = Windows.GetDesktopWindow();
        var hSrce = Windows.GetWindowDC(hDesk);
        var hDest = Windows.CreateCompatibleDC(hSrce);
        var hBmp = Windows.CreateCompatibleBitmap(hSrce, sz.Width, sz.Height);
        var hOldBmp = Windows.SelectObject(hDest, hBmp);
        _ = Windows.BitBlt(hDest, 0, 0, sz.Width, sz.Height, hSrce, 0, 0, Windows.SRCCOPY | Windows.CAPTUREBLT);
        var bmp = Image.FromHbitmap(hBmp);
        _ = Windows.SelectObject(hDest, hOldBmp);
        _ = Windows.DeleteObject(hBmp);
        _ = Windows.DeleteDC(hDest);
        _ = Windows.ReleaseDC(hDesk, hSrce);
        return bmp;
    }
}
