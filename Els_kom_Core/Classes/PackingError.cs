// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// KOM file Packing failure error.
    /// </summary>
    [System.Serializable]
    public sealed class PackingError : System.IO.IOException
    {
        /// <summary>
        /// constructor of KOM file Packing failure error with no argrument.
        /// </summary>
        public PackingError() : base()
        {
        }

        /// <summary>
        /// constructor of KOM file Packing failure error with an string argrument.
        /// </summary>
        public PackingError(string s) : base(s)
        {
        }
    }
}
