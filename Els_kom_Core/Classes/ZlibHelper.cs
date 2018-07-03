// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Zlib Compression and Decompression Helper Class.
    /// </summary>
    public static class ZlibHelper
    {
        /// <summary>
        /// Compresses data using the default compression level.
        /// </summary>
        /// <exception cref="Zlib.ZStreamException">Thrown when the stream Errors in any way.</exception>
        /// if _adler32 is not optional then komv2 packing and unpacking will fail to work at all.
        public static void CompressData(byte[] inData, out byte[] outData, [System.Runtime.InteropServices.Optional]out int _adler32) => CompressData(inData, out outData, Zlib.zlibConst.Z_DEFAULT_COMPRESSION, out _adler32);

        /// <summary>
        /// Compresses data using an specific compression level.
        /// </summary>
        /// <exception cref="Zlib.ZStreamException">Thrown when the stream Errors in any way.</exception>
        /// if _adler32 is not optional then komv2 packing and unpacking will fail to work at all.
        public static void CompressData(byte[] inData, out byte[] outData, int level, [System.Runtime.InteropServices.Optional]out int _adler32)
        {
            System.IO.MemoryStream outMemoryStream = new System.IO.MemoryStream();
            Zlib.ZOutputStream outZStream = new Zlib.ZOutputStream(outMemoryStream, level);
            System.IO.Stream inMemoryStream = new System.IO.MemoryStream(inData);
            try
            {
                inMemoryStream.CopyTo(outZStream);
            }
            catch (Zlib.ZStreamException)
            {
                // the compression or decompression failed.
            }
            outZStream.Flush();
            try
            {
                outZStream.finish();
            }
            catch (Zlib.ZStreamException)
            {
                throw;
            }
            outData = outMemoryStream.ToArray();
            _adler32 = (int)(outZStream.z.adler & 0xffff);
            outZStream.Dispose();
            inMemoryStream.Dispose();
        }

        /// <summary>
        /// Decompresses data.
        /// </summary>
        /// <exception cref="Zlib.ZStreamException">Thrown when the stream Errors in any way.</exception>
        public static void DecompressData(byte[] inData, out byte[] outData)
        {
            System.IO.MemoryStream outMemoryStream = new System.IO.MemoryStream();
            Zlib.ZOutputStream outZStream = new Zlib.ZOutputStream(outMemoryStream);
            System.IO.Stream inMemoryStream = new System.IO.MemoryStream(inData);
            try
            {
                inMemoryStream.CopyTo(outZStream);
            }
            catch (Zlib.ZStreamException)
            {
                // the compression or decompression failed.
            }
            outZStream.Flush();
            try
            {
                outZStream.finish();
            }
            catch (Zlib.ZStreamException)
            {
                throw;
            }
            outData = outMemoryStream.ToArray();
            outZStream.Dispose();
            inMemoryStream.Dispose();
        }
    }
}
