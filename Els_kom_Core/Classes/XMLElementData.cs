// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System.Collections.Generic;

    /// <summary>
    /// XML Element data stuff.
    /// </summary>
    internal class XMLElementData
    {
        /// <summary>
        /// Gets or sets the name of any subelemets (if this element has any).
        /// </summary>
        internal string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the array of subelements with the Name above.
        /// </summary>
        internal XMLElementData[] Subelements { get; set; } = null;

        /// <summary>
        /// Gets or sets the xml attributes to check for in the Dictionary, if it has any.
        /// </summary>
        internal List<XMLAttributeData> Attributes { get; set; } = new List<XMLAttributeData>();

        /// <summary>
        /// Gets or sets the value of the element.
        /// </summary>
        internal string Value { get; set; } = string.Empty;
    }
}
