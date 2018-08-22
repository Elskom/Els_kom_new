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
        /// <param name="inData">The original input data.</param>
        /// <param name="outData">The compressed output data.</param>
        /// <param name="adler32">The output adler32 of the data.</param>
        /// <exception cref="PackingError">Thrown when the stream Errors in any way.</exception>
        public static void CompressData(byte[] inData, out byte[] outData, out int adler32) => CompressData(inData, out outData, ComponentAce.Compression.Libs.zlib.zlibConst.Z_DEFAULT_COMPRESSION, out adler32);

        /// <summary>
        /// Compresses data using the default compression level.
        /// </summary>
        /// <param name="inData">The original input data.</param>
        /// <param name="outData">The compressed output data.</param>
        /// <exception cref="PackingError">Thrown when the stream Errors in any way.</exception>
        public static void CompressData(byte[] inData, out byte[] outData) => CompressData(inData, out outData, ComponentAce.Compression.Libs.zlib.zlibConst.Z_DEFAULT_COMPRESSION);

        /// <summary>
        /// Compresses data using an specific compression level.
        /// </summary>
        /// <param name="inData">The original input data.</param>
        /// <param name="outData">The compressed output data.</param>
        /// <param name="level">The compression level to use.</param>
        /// <exception cref="PackingError">Thrown when the stream Errors in any way.</exception>
        // discard returned adler32. The caller does not want it.
        public static void CompressData(byte[] inData, out byte[] outData, int level) => CompressData(inData, out outData, level, out var adler32);

        /// <summary>
        /// Compresses data using an specific compression level.
        /// </summary>
        /// <param name="inData">The original input data.</param>
        /// <param name="outData">The compressed output data.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="adler32">The output adler32 of the data.</param>
        /// <exception cref="PackingError">Thrown when the stream Errors in any way.</exception>
        public static void CompressData(byte[] inData, out byte[] outData, int level, out int adler32)
        {
            var outMemoryStream = new System.IO.MemoryStream();
            var outZStream = new ComponentAce.Compression.Libs.zlib.ZOutputStream(outMemoryStream, level);
            System.IO.Stream inMemoryStream = new System.IO.MemoryStream(inData);
            try
            {
                inMemoryStream.CopyTo(outZStream);
            }
            catch (ComponentAce.Compression.Libs.zlib.ZStreamException)
            {
                // the compression or decompression failed.
            }

            outZStream.Flush();
            try
            {
                outZStream.finish();
            }
            catch (ComponentAce.Compression.Libs.zlib.ZStreamException ex)
            {
                throw new PackingError("Compression Failed.", ex);
            }

            outData = outMemoryStream.ToArray();
            adler32 = (int)(outZStream.z.adler & 0xffff);
            outZStream.Dispose();
            inMemoryStream.Dispose();
        }

        /// <summary>
        /// Decompresses data.
        /// </summary>
        /// <param name="inData">The compressed input data.</param>
        /// <param name="outData">The decompressed output data.</param>
        /// <exception cref="UnpackingError">Thrown when the stream Errors in any way.</exception>
        public static void DecompressData(byte[] inData, out byte[] outData)
        {
            var outMemoryStream = new System.IO.MemoryStream();
            var outZStream = new ComponentAce.Compression.Libs.zlib.ZOutputStream(outMemoryStream);
            System.IO.Stream inMemoryStream = new System.IO.MemoryStream(inData);
            try
            {
                inMemoryStream.CopyTo(outZStream);
            }
            catch (ComponentAce.Compression.Libs.zlib.ZStreamException)
            {
                // the compression or decompression failed.
            }

            outZStream.Flush();
            try
            {
                outZStream.finish();
            }
            catch (ComponentAce.Compression.Libs.zlib.ZStreamException ex)
            {
                throw new UnpackingError("Unpacking Failed.", ex);
            }

            outData = outMemoryStream.ToArray();
            outZStream.Dispose();
            inMemoryStream.Dispose();
        }
    }
}
