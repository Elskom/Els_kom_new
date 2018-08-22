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
        /// Initializes a new instance of the <see cref="PackingError"/> class.
        /// </summary>
        public PackingError()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackingError"/> class.
        /// </summary>
        /// <param name="s">The error string.</param>
        public PackingError(string s)
            : base(s)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackingError"/> class.
        /// </summary>
        /// <param name="s">The error string.</param>
        /// <param name="ex">The Exception that caused this Exception.</param>
        public PackingError(string s, System.Exception ex)
            : base(s, ex)
        {
        }
    }
}
