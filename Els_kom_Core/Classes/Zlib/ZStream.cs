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
    /// base zlib stream.
    /// </summary>
    internal sealed class ZStream
    {
        
        private const int MAX_WBITS = 15; // 32K LZ77 window        
        private static readonly int DEF_WBITS = MAX_WBITS;
        
        private const int Z_NO_FLUSH = 0;
        private const int Z_PARTIAL_FLUSH = 1;
        private const int Z_SYNC_FLUSH = 2;
        private const int Z_FULL_FLUSH = 3;
        private const int Z_FINISH = 4;
        
        private const int MAX_MEM_LEVEL = 9;
        
        private const int Z_OK = 0;
        private const int Z_STREAM_END = 1;
        private const int Z_NEED_DICT = 2;
        private const int Z_ERRNO = - 1;
        private const int Z_STREAM_ERROR = - 2;
        private const int Z_DATA_ERROR = - 3;
        private const int Z_MEM_ERROR = - 4;
        private const int Z_BUF_ERROR = - 5;
        private const int Z_VERSION_ERROR = - 6;

        /// <summary>
        /// next input byte
        /// </summary>
        public byte[] next_in;
        /// <summary>
        /// index of next input byte.
        /// </summary>
        public int next_in_index;
        /// <summary>
        /// number of bytes available at next_in
        /// </summary>
        public int avail_in;
        /// <summary>
        /// total nb of input bytes read so far
        /// </summary>
        public long total_in;

        /// <summary>
        /// next output byte should be put there
        /// </summary>
        public byte[] next_out;
        /// <summary>
        /// index of next output byte.
        /// </summary>
        public int next_out_index;
        /// <summary>
        /// remaining free space at next_out
        /// </summary>
        public int avail_out;
        /// <summary>
        /// total nb of bytes output so far
        /// </summary>
        public long total_out;
        
        /// <summary>
        /// zlib stream error message.
        /// </summary>
        public System.String msg;
        
        internal Deflate dstate;
        internal Inflate istate;
        
        /// <summary>
        /// best guess about the data type: ascii or binary
        /// </summary>
        internal int data_type;
        
        /// <summary>
        /// adler32 of the stream data.
        /// </summary>
        internal long adler;
        internal Adler32 _adler = new Adler32();
        
        /// <summary>
        /// init inflate.
        /// </summary>
        internal int inflateInit()
        {
            return inflateInit(DEF_WBITS);
        }
        /// <summary>
        /// init inflate with size.
        /// </summary>
        internal int inflateInit(int w)
        {
            istate = new Inflate();
            return istate.inflateInit(this, w);
        }
        
        /// <summary>
        /// inflate with some int.
        /// </summary>
        internal int inflate(int f)
        {
            if (istate == null)
                return Z_STREAM_ERROR;
            return istate.inflate(this, f);
        }
        /// <summary>
        /// end inflate.
        /// </summary>
        internal int inflateEnd()
        {
            if (istate == null)
                return Z_STREAM_ERROR;
            int ret = istate.inflateEnd(this);
            istate = null;
            return ret;
        }
        /// <summary>
        /// sync the inflate.
        /// </summary>
        internal int inflateSync()
        {
            if (istate == null)
                return Z_STREAM_ERROR;
            return istate.inflateSync(this);
        }
        /// <summary>
        /// set the inflate dictionary.
        /// </summary>
        internal int inflateSetDictionary(byte[] dictionary, int dictLength)
        {
            if (istate == null)
                return Z_STREAM_ERROR;
            return istate.inflateSetDictionary(this, dictionary, dictLength);
        }
        
        /// <summary>
        /// deflate init.
        /// </summary>
        internal int deflateInit(int level)
        {
            return deflateInit(level, MAX_WBITS);
        }
        /// <summary>
        /// deflate init with level and bits.
        /// </summary>
        internal int deflateInit(int level, int bits)
        {
            dstate = new Deflate();
            return dstate.deflateInit(this, level, bits);
        }
        /// <summary>
        /// deflate with optional flush setting.
        /// </summary>
        internal int deflate(int flush)
        {
            if (dstate == null)
            {
                return Z_STREAM_ERROR;
            }
            return dstate.deflate(this, flush);
        }
        /// <summary>
        /// deflate end.
        /// </summary>
        internal int deflateEnd()
        {
            if (dstate == null)
                return Z_STREAM_ERROR;
            int ret = dstate.deflateEnd();
            dstate = null;
            return ret;
        }
        /// <summary>
        /// deflate params with level and strategy.
        /// </summary>
        internal int deflateParams(int level, int strategy)
        {
            if (dstate == null)
                return Z_STREAM_ERROR;
            return dstate.deflateParams(this, level, strategy);
        }
        /// <summary>
        /// deflate dictionary setter.
        /// </summary>
        internal int deflateSetDictionary(byte[] dictionary, int dictLength)
        {
            if (dstate == null)
                return Z_STREAM_ERROR;
            return dstate.deflateSetDictionary(this, dictionary, dictLength);
        }
        
        // Flush as much pending output as possible. All deflate() output goes
        // through this function so some applications may wish to modify it
        // to avoid allocating a large strm->next_out buffer and copying into it.
        // (See also read_buf()).
        internal void flush_pending()
        {
            int len = dstate.pending;
            
            if (len > avail_out)
                len = avail_out;
            if (len == 0)
                return ;
            
            if (dstate.pending_buf.Length <= dstate.pending_out || next_out.Length <= next_out_index || dstate.pending_buf.Length < (dstate.pending_out + len) || next_out.Length < (next_out_index + len))
            {
                //System.Console.Out.WriteLine(dstate.pending_buf.Length + ", " + dstate.pending_out + ", " + next_out.Length + ", " + next_out_index + ", " + len);
                //System.Console.Out.WriteLine("avail_out=" + avail_out);
            }

            System.Array.Copy(dstate.pending_buf, dstate.pending_out, next_out, next_out_index, len);
            
            next_out_index += len;
            dstate.pending_out += len;
            total_out += len;
            avail_out -= len;
            dstate.pending -= len;
            if (dstate.pending == 0)
            {
                dstate.pending_out = 0;
            }
        }
        
        // Read a new buffer from the current input stream, update the adler32
        // and total number of bytes read.  All deflate() input goes through
        // this function so some applications may wish to modify it to avoid
        // allocating a large strm->next_in buffer and copying from it.
        // (See also flush_pending()).
        internal int read_buf(byte[] buf, int start, int size)
        {
            int len = avail_in;
            
            if (len > size)
                len = size;
            if (len == 0)
                return 0;
            
            avail_in -= len;
            
            if (dstate.noheader == 0)
            {
                adler = _adler.adler32(adler, next_in, next_in_index, len);
            }
            System.Array.Copy(next_in, next_in_index, buf, start, len);
            next_in_index += len;
            total_in += len;
            return len;
        }
        
        /// <summary>
        /// free all the internal state.
        /// </summary>
        internal void free()
        {
            next_in = null;
            next_out = null;
            msg = null;
            _adler = null;
        }
    }
}
