// Copyright (c) 2014-2024, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Themes;

internal class Theme
{
    public string? Name { get; set; }

    public Color BackgroundColor { get; set; }

    public Color LightBackgroundColor { get; set; }

    public Color DarkBackgroundColor { get; set; }

    public Color TextColor { get; set; }

    public Color BorderColor { get; set; }

    public Color CheckerColor { get; set; }

    public Color CheckerColor2 { get; set; }

    public int CheckerSize { get; set; } = 15;

    public Color LinkColor { get; set; }

    public Color MenuHighlightColor { get; set; }

    public Color MenuHighlightBorderColor { get; set; }

    public Color MenuBorderColor { get; set; }

    public Color MenuCheckBackgroundColor { get; set; }

    public Color SeparatorLightColor { get; set; }

    public Color SeparatorDarkColor { get; set; }

    public static Theme GetDarkTheme()
        => new()
        {
            Name = "Dark",
            BackgroundColor = Color.FromArgb(42, 47, 56),
            LightBackgroundColor = Color.FromArgb(52, 57, 65),
            DarkBackgroundColor = Color.FromArgb(28, 32, 38),
            TextColor = Color.FromArgb(235, 235, 235),
            BorderColor = Color.FromArgb(28, 32, 38),
            CheckerColor = Color.FromArgb(60, 60, 60),
            CheckerColor2 = Color.FromArgb(50, 50, 50),
            CheckerSize = 15,
            LinkColor = Color.FromArgb(166, 212, 255),
            MenuHighlightColor = Color.FromArgb(30, 34, 40),
            MenuHighlightBorderColor = Color.FromArgb(116, 129, 152),
            MenuBorderColor = Color.FromArgb(22, 26, 31),
            MenuCheckBackgroundColor = Color.FromArgb(56, 64, 75),
            SeparatorLightColor = Color.FromArgb(56, 64, 75),
            SeparatorDarkColor = Color.FromArgb(22, 26, 31),
        };

    public static Theme GetLightTheme()
        => new()
        {
            Name = "Light",
            BackgroundColor = Color.FromArgb(242, 242, 242),
            LightBackgroundColor = Color.FromArgb(247, 247, 247),
            DarkBackgroundColor = Color.FromArgb(235, 235, 235),
            TextColor = Color.FromArgb(69, 69, 69),
            BorderColor = Color.FromArgb(201, 201, 201),
            CheckerColor = Color.FromArgb(60, 60, 60),
            CheckerColor2 = Color.FromArgb(50, 50, 50),
            CheckerSize = 15,
            LinkColor = Color.FromArgb(166, 212, 255),
            MenuHighlightColor = Color.FromArgb(247, 247, 247),
            MenuHighlightBorderColor = Color.FromArgb(96, 143, 226),
            MenuBorderColor = Color.FromArgb(201, 201, 201),
            MenuCheckBackgroundColor = Color.FromArgb(225, 233, 244),
            SeparatorLightColor = Color.FromArgb(253, 253, 253),
            SeparatorDarkColor = Color.FromArgb(189, 189, 189),
        };

    public static Theme GetPinkTheme()
        => new()
        {
            Name = "Pink",
            BackgroundColor = Color.HotPink,
            LightBackgroundColor = Color.LightPink,

            // DarkBackgroundColor = Color.DarkPink,
            TextColor = Color.Black,

            // BorderColor = Color.DarkPink,
            // CheckerColor = ???
            // CheckerColor2 = ???
            CheckerSize = 15,

            // LinkColor = ???
            MenuHighlightColor = Color.LightPink,

            // MenuHighlightBorderColor = ???,
            // MenuBorderColor = Color.DarkPink,
            // MenuCheckBackgroundColor = ???,
            // SeparatorLightColor = ???,
            // SeparatorDarkColor = ???,
        };

    public static Theme GetPurpleTheme()
        => new()
        {
            Name = "Purple",
        };

    public static List<Theme> GetPresets()
        => new() { GetDarkTheme(), GetLightTheme(), GetPinkTheme(), GetPurpleTheme() };

    public override string ToString()
        => this.Name ?? string.Empty;
}
