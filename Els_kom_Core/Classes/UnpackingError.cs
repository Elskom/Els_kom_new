// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// KOM file Unpacking failure error.
    /// </summary>
    [System.Serializable]
    public sealed class UnpackingError : System.IO.IOException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnpackingError"/> class with no argrument.
        /// </summary>
        public UnpackingError()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnpackingError"/> class with an string argrument.
        /// </summary>
        /// <param name="s">The error string.</param>
        public UnpackingError(string s)
            : base(s)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnpackingError"/> class with an string argrument
        /// and the exception that cuased this exception.
        /// </summary>
        /// <param name="s">The error string.</param>
        /// <param name="ex">The Exception that caused this Exception.</param>
        public UnpackingError(string s, System.Exception ex)
            : base(s, ex)
        {
        }
    }
}
