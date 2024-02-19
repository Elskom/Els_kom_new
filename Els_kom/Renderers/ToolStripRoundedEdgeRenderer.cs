// Copyright (c) 2014-2024, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Renderers;

internal class ToolStripRoundedEdgeRenderer : ToolStripProfessionalRenderer
{
    public ToolStripRoundedEdgeRenderer()
        => this.RoundedEdges = false;

    public ToolStripRoundedEdgeRenderer(ProfessionalColorTable professionalColorTable)
        : base(professionalColorTable)
        => this.RoundedEdges = false;
}
