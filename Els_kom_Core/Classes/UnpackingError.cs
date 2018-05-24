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
        /// constructor of KOM file Unpacking failure error with no argrument.
        /// </summary>
        public UnpackingError() : base()
        {
        }
        /// <summary>
        /// constructor of KOM file Unpacking failure error with an string argrument.
        /// </summary>
        public UnpackingError(System.String s) : base(s)
        {
        }
    }
}
