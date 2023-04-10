// Copyright (c) 2014-2023, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Renderers;

using Els_kom.Themes;

internal class ToolStripDarkRenderer : ToolStripCustomRenderer
{
    public ToolStripDarkRenderer()
        : base(new DarkColorTable())
    {
    }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
        // e.TextColor = Color.FromArgb((int)Windows.GetThemeSysColor(Windows.GetWindowTheme((HWND)e.ToolStrip.FindForm().Handle), Windows.TMT_WINDOWTEXT));
        e.TextColor = ApplicationResources.Theme!.TextColor;
        base.OnRenderItemText(e);
    }

    protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
    {
        // e.ArrowColor = Color.FromArgb((int)Windows.GetThemeSysColor(Windows.GetWindowTheme((HWND)e.Item.Owner.FindForm().Handle), Windows.TMT_WINDOWTEXT));
        e.ArrowColor = ApplicationResources.Theme!.TextColor;
        base.OnRenderArrow(e);
    }
}
