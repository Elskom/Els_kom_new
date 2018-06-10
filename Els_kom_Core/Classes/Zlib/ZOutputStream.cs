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
Copyright (c) 2001 Lapo Luchini.

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
FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHORS
OR ANY CONTRIBUTORS TO THIS SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT,
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
    /// zlib output stream.
    /// </summary>
    internal class ZOutputStream: System.IO.Stream
    {
        private void  InitBlock()
        {
            flush_Renamed_Field = zlibConst.Z_NO_FLUSH;
            buf = new byte[bufsize];
        }
        /// <summary>
        /// flush mode of the stream.
        /// </summary>
        virtual public int FlushMode
        {
            get
            {
                return (flush_Renamed_Field);
            }
            
            set
            {
                this.flush_Renamed_Field = value;
            }
            
        }
        /// <summary> Returns the total number of bytes input so far.</summary>
        virtual public long TotalIn
        {
            get
            {
                return z.total_in;
            }
            
        }
        /// <summary> Returns the total number of bytes output so far.</summary>
        virtual public long TotalOut
        {
            get
            {
                return z.total_out;
            }
            
        }
        
        /// <summary>
        /// internal stream.
        /// </summary>
        protected internal ZStream z = new ZStream();
        /// <summary>
        /// internal stream buffer size.
        /// </summary>
        protected internal int bufsize = 4096;
        /// <summary>
        /// value of the internal stream flush setting.
        /// </summary>
        protected internal int flush_Renamed_Field;
        /// <summary>
        /// stream buffers.
        /// </summary>
        protected internal byte[] buf, buf1 = new byte[1];
        /// <summary>
        /// compression or decompression?
        /// </summary>
        protected internal bool compress;
        
        private System.IO.Stream out_Renamed;

        /// <summary>
        /// constructs output stream for decompression.
        /// </summary>
        public ZOutputStream(System.IO.Stream out_Renamed):base()
        {
            InitBlock();
            this.out_Renamed = out_Renamed;
            z.inflateInit();
            compress = false;
        }
        
        /// <summary>
        /// constructs output stream for compression.
        /// </summary>
        public ZOutputStream(System.IO.Stream out_Renamed, int level):base()
        {
            InitBlock();
            this.out_Renamed = out_Renamed;
            z.deflateInit(level);
            compress = true;
        }
        
        /// <summary>
        /// writes an int that becomes an byte to the stream.
        /// </summary>
        /// <param name="b"></param>
        public  void  WriteByte(int b)
        {
            buf1[0] = (byte) b;
            Write(buf1, 0, 1);
        }

        /// <summary>
        /// writes an single byte to the stream.
        /// </summary>
        public override  void  WriteByte(byte b)
        {
            WriteByte((int) b);
        }
        
        /// <summary>
        /// write to the stream.
        /// </summary>
        public override void  Write(System.Byte[] b1, int off, int len)
        {
            if (len == 0)
                return ;
            int err;
            byte[] b = new byte[b1.Length];
            System.Array.Copy(b1, 0, b, 0, b1.Length); 
            z.next_in = b;
            z.next_in_index = off;
            z.avail_in = len;
            do 
            {
                z.next_out = buf;
                z.next_out_index = 0;
                z.avail_out = bufsize;
                if (compress)
                    err = z.deflate(flush_Renamed_Field);
                else
                    err = z.inflate(flush_Renamed_Field);
                if (err != zlibConst.Z_OK && err != zlibConst.Z_STREAM_END) 
                    throw new ZStreamException((compress?"de":"in") + "flating: " + z.msg);
                out_Renamed.Write(buf, 0, bufsize - z.avail_out);
            }
            while (z.avail_in > 0 || z.avail_out == 0);
        }
        
        /// <summary>
        /// finish up the stream.
        /// </summary>
        public virtual void  finish()
        {
            int err;
            do 
            {
                z.next_out = buf;
                z.next_out_index = 0;
                z.avail_out = bufsize;
                if (compress)
                {
                    err = z.deflate(zlibConst.Z_FINISH);
                }
                else
                {
                    err = z.inflate(zlibConst.Z_FINISH);
                }
                if (err != zlibConst.Z_STREAM_END && err != zlibConst.Z_OK)
                    throw new ZStreamException((compress?"de":"in") + "flating: " + z.msg);
                if (bufsize - z.avail_out > 0)
                {
                    out_Renamed.Write(buf, 0, bufsize - z.avail_out);
                }
            }
            while (z.avail_in > 0 || z.avail_out == 0);
            try
            {
                Flush();
            }
            catch
            {
            }
        }
        /// <summary>
        /// end deflate or inflate.
        /// </summary>
        public virtual void  end()
        {
            if (compress)
            {
                z.deflateEnd();
            }
            else
            {
                z.inflateEnd();
            }
            z.free();
            z = null;
        }
        /// <summary>
        /// close the stream.
        /// </summary>
        public override void Close()
        {
            Dispose(true);
        }

        /// <summary>
        /// dummy thing.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            try
            {
                try
                {
                    finish();
                }
                catch
                {
                }
            }
            finally
            {
                end();
                out_Renamed.Dispose();
                out_Renamed = null;
            }
        }

        /// <summary>
        /// flush the stream.
        /// </summary>
        public override void Flush()
        {
            out_Renamed.Flush();
        }

        /// <summary>
        /// reads from the stream.
        /// </summary>
        public override System.Int32 Read(System.Byte[] buffer, System.Int32 offset, System.Int32 count)
        {
            return 0;
        }

        /// <summary>
        /// sets the length of the stream.
        /// </summary>
        /// <param name="value"></param>
        public override void  SetLength(System.Int64 value)
        {
        }

        /// <summary>
        /// seek to an specific place in the stream.
        /// </summary>
        public override System.Int64 Seek(System.Int64 offset, System.IO.SeekOrigin origin)
        {
            return 0;
        }

        /// <summary>
        /// return if the stream can read.
        /// </summary>
        public override System.Boolean CanRead
        {
            get
            {
                return false;
            }
            
        }

        /// <summary>
        /// return if the strea, can seek.
        /// </summary>
        public override System.Boolean CanSeek
        {
            get
            {
                return false;
            }
            
        }

        /// <summary>
        /// Return if the stream can write.
        ///
        /// Without this set to true System.IO.Stream.CopyTo
        /// thinks the stream is an closed stream and then
        /// makes the shortcut copy function fail
        /// (raises System.ObjectDisposedException).
        /// We do not want this since we can actually Write so
        /// we set this to true!!!
        /// </summary>
        public override System.Boolean CanWrite
        {
            get
            {
                return true;
            }
            
        }

        /// <summary>
        /// gets the stream length.
        /// </summary>
        public override System.Int64 Length
        {
            get
            {
                return 0;
            }
            
        }

        /// <summary>
        /// gets or sets the stream position.
        /// </summary>
        public override System.Int64 Position
        {
            get
            {
                return 0;
            }
            
            set
            {
            }
            
        }
    }
}
