// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using System.IO;

    /// <summary>
    /// KOM file Packing failure error.
    /// </summary>
    [Serializable]
    public sealed class PackingError : IOException
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
        public PackingError(string s, Exception ex)
            : base(s, ex)
        {
        }
    }
}
