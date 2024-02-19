// Copyright (c) 2014-2024, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Els_kom Icon Resource Class.
/// </summary>
internal static class Icons
{
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
            var iconVal = SettingsFile.SettingsJson!.WindowIcon;
            var retIcon = iconVal switch
            {
                1 => Properties.Resources.VP_Trans,
                2 => Properties.Resources.YR,
                _ => Properties.Resources.els_kom,
            };
            return retIcon!;
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
    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP012:Property should not return created disposable.", Justification = "Needed.")]
    public static Image FormImage
    {
        get
        {
            using var newicon = new Icon(FormIcon, 48, 48);
            return newicon.ToBitmap();
        }
    }

    /// <summary>
    /// Gets the Void Elsword official logo based on the logo
    /// from their website.
    /// </summary>
    /// <value>
    /// The Void Elsword official logo based on the logo
    /// from their website.
    /// </value>
    public static Image VoidElsLogo
        => Properties.Resources.voidels_logo!;

    /// <summary>
    /// Gets the Elsword official logo based on the logo
    /// from x2.exe.
    /// </summary>
    /// <value>
    /// The Elsword official logo based on the logo
    /// from x2.exe.
    /// </value>
    public static Image ElsLogo
        => Properties.Resources.els_logo!;

    /// <summary>
    /// Checks if 2 icons are Equal.
    /// </summary>
    /// <param name="icon1">The 1st icon to check.</param>
    /// <param name="icon2">The 2nd icon to check.</param>
    /// <returns><see langword="true"/>, if they are both Equal or <see langword="null"/>, <see langword="false"/> otherwise.</returns>
    public static bool IconEquals(Icon icon1, Icon icon2)
    {
        var result = true;
        using (var bitmap1 = icon1?.ToBitmap())
        using (var bitmap2 = icon2?.ToBitmap())
        {
            if (bitmap1 is null && bitmap2 is null)
            {
                result = true;
            }
            else if (bitmap1 is null || bitmap2 is null)
            {
                result = false;
            }
            else if (bitmap1.Size == bitmap2.Size)
            {
                for (var y = 0; y < bitmap1.Height; y++)
                {
                    for (var x = 0; x < bitmap1.Width; x++)
                    {
                        var col1 = bitmap1.GetPixel(x, y);
                        var col2 = bitmap2.GetPixel(x, y);
                        if (!col1.Equals(col2))
                        {
                            result = false;
                            break;
                        }
                    }

                    if (!result)
                    {
                        break;
                    }
                }
            }
        }

        return result;
    }
}
