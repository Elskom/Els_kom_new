// Copyright (c) 2014-2023, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom;

using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Svg;

internal static class Extensions
{
    public static void ConvertSVGTo16x16Image(this ToolStripMenuItem toolStripMenuItem, byte[] svgData, Color color)
    {
        var svgString = Encoding.UTF8.GetString(svgData);
        var root = XElement.Parse(svgString);
        var colors = root.XPathSelectElements("//*[@fill]");
        foreach (var node in colors)
        {
            node.Attribute("fill")!.Value = color.ToHexString();
        }

        svgString = root.ToString();
        var svgDoc = SvgDocument.FromSvg<SvgDocument>(svgString);
        svgDoc.FillRule = SvgFillRule.EvenOdd;
        toolStripMenuItem.Image = svgDoc.Draw(16, 16);
    }

    public static Rectangle LocationOffset(this Rectangle rect, int x, int y)
        => new(rect.X + x, rect.Y + y, rect.Width, rect.Height);

    public static Rectangle SizeOffset(this Rectangle rect, int width, int height)
        => new(rect.X, rect.Y, rect.Width + width, rect.Height + height);

    public static byte[] ToByteArray(this Icon icon)
    {
        if (icon == null)
        {
            return Array.Empty<byte>();
        }

        using var ms = new MemoryStream();
        icon.Save(ms);
        return ms.ToArray();
    }

    public static string ToHexString(this Color color)
        => $"#{color.R:X2}{color.G:X2}{color.B:X2}";
}
