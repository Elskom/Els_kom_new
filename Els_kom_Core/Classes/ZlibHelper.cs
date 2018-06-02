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
        public static void CompressData(byte[] inData, out byte[] outData) => CompressData(inData, out outData, Zlib.zlibConst.Z_DEFAULT_COMPRESSION);

        /// <summary>
        /// Compresses data using an specific compression level.
        /// </summary>
        /// <exception cref="Zlib.ZStreamException">Thrown when the stream Errors in any way.</exception>
        public static void CompressData(byte[] inData, out byte[] outData, int level)
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
