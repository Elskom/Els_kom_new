// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System.Windows.Forms;

    public class ToolStripDarkRenderer : ToolStripCustomRenderer
    {
        public ToolStripDarkRenderer()
            : base(new DarkColorTable())
        {
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = ShareXResources.Theme.TextColor;

            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = ShareXResources.Theme.TextColor;

            base.OnRenderArrow(e);
        }
    }
}
