// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

// Copyright (c) 2006, ComponentAce
// http://www.componentace.com
// All rights reserved.

// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

// Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer. 
// Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution. 
// Neither the name of ComponentAce nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission. 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

/*
Copyright (c) 2000,2001,2002,2003 ymnk, JCraft,Inc. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice,
this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright 
notice, this list of conditions and the following disclaimer in 
the documentation and/or other materials provided with the distribution.

3. The names of the authors may not be used to endorse or promote products
derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESSED OR IMPLIED WARRANTIES,
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL JCRAFT,
INC. OR ANY CONTRIBUTORS TO THIS SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
/*
* This program is based on zlib-1.1.3, so all credit should go authors
* Jean-loup Gailly(jloup@gzip.org) and Mark Adler(madler@alumni.caltech.edu)
* and contributors of zlib.
*/
namespace Els_kom_Core.Classes.Zlib
{
    /// <summary>
    /// the main zlib stuffs?
    /// </summary>
    sealed public class zlibConst
    {
        private const System.String version_Renamed_Field = "1.0.2";
        /// <summary>
        /// zlib version.
        /// </summary>
        public static System.String version()
        {
            return version_Renamed_Field;
        }

        // compression levels
        /// <summary>
        /// No compression.
        /// </summary>
        public const int Z_NO_COMPRESSION = 0;
        /// <summary>
        /// Best for speed.
        /// </summary>
        public const int Z_BEST_SPEED = 1;
        /// <summary>
        /// the highest compression level (slow).
        /// </summary>
        public const int Z_BEST_COMPRESSION = 9;
        /// <summary>
        /// Default compression level.
        /// </summary>
        public const int Z_DEFAULT_COMPRESSION = (- 1);

        // compression strategy
        /// <summary>
        /// Filtered compression strategy.
        /// </summary>
        public const int Z_FILTERED = 1;
        /// <summary>
        /// Huffman strategy.
        /// </summary>
        public const int Z_HUFFMAN_ONLY = 2;
        /// <summary>
        /// Default strategy.
        /// </summary>
        public const int Z_DEFAULT_STRATEGY = 0;

        /// <summary>
        /// Do not flush.
        /// </summary>
        public const int Z_NO_FLUSH = 0;
        /// <summary>
        /// Partial flush.
        /// </summary>
        public const int Z_PARTIAL_FLUSH = 1;
        /// <summary>
        /// sync flush.
        /// </summary>
        public const int Z_SYNC_FLUSH = 2;
        /// <summary>
        /// full flush.
        /// </summary>
        public const int Z_FULL_FLUSH = 3;
        /// <summary>
        /// finish of compression stuff?
        /// </summary>
        public const int Z_FINISH = 4;
        
        /// <summary>
        /// all was ok.
        /// </summary>
        public const int Z_OK = 0;
        /// <summary>
        /// stream ended.
        /// </summary>
        public const int Z_STREAM_END = 1;
        /// <summary>
        /// need something.
        /// </summary>
        public const int Z_NEED_DICT = 2;
        /// <summary>
        /// some error.
        /// </summary>
        public const int Z_ERRNO = - 1;
        /// <summary>
        /// some stream error.
        /// </summary>
        public const int Z_STREAM_ERROR = - 2;
        /// <summary>
        /// data was incorrect.
        /// </summary>
        public const int Z_DATA_ERROR = - 3;
        /// <summary>
        /// out of memory.
        /// </summary>
        public const int Z_MEM_ERROR = - 4;
        /// <summary>
        /// buffer overflow.
        /// </summary>
        public const int Z_BUF_ERROR = - 5;
        /// <summary>
        /// version missmatch.
        /// </summary>
        public const int Z_VERSION_ERROR = - 6;
    }
}
