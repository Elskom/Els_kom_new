// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    internal class XMLElementData
    {
        internal string name = string.Empty;
        internal XMLElementData[] Subelements = null;
        // xml attributes to check for in the Dictionary, if it has any.
        internal System.Collections.Generic.List<XMLAttributeData> Attributes;
        // element value.
        internal string value;
    }
}
