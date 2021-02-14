// Copyright (c) 2014-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System.Drawing;
    using System.Windows.Forms;

    public class ToolStripCustomRenderer : ToolStripRoundedEdgeRenderer
    {
        public ToolStripCustomRenderer(ProfessionalColorTable professionalColorTable)
            : base(professionalColorTable)
        {
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item is ToolStripMenuItem tsmi && tsmi.Checked)
            {
                e.TextFont = new Font(tsmi.Font, FontStyle.Bold);
            }

            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (e.Item is ToolStripDropDownButton tsddb && tsddb.Owner is ToolStripBorderRight)
            {
                e.Direction = ArrowDirection.Right;
            }

            base.OnRenderArrow(e);
        }
    }
}
