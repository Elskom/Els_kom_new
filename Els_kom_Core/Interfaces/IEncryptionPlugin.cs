// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

/* 0x01050000 = v1.5.0.0 */
#if VERSION_0x01050000
namespace Els_kom_Core.Interfaces
{
    using Elskom.Generic.Libs;

    /// <summary>
    /// Interface for Els_kom kom entry Encryption and Decryption plugins (Version 1.5.0.0 or newer).
    ///
    /// Interface plugins can be made in C++ Common Language Runtime (/clr) to mix
    /// unmanaged and managed code to avoid anyone from easily decompiling the code to
    /// C# and getting private things like encryption keys to an particular kom file.
    /// </summary>
    public interface IEncryptionPlugin
    {
        /// <summary>
        /// Gets the name of the Encryption plugin.
        /// </summary>
        string PluginName { get; }

        /// <summary>
        /// Decrypts an KOM file entry. If KOM file algorithm is not supported
        /// throw <see cref="NotUnpackableException"/>.
        /// </summary>
        /// <exception cref="NotUnpackableException">
        /// When the KOM file algorithm is not suppoted by the curently installed
        /// encryption plugin.
        /// </exception>
        /// <param name="input">The input data to Decrypt.</param>
        /// <param name="output">The decrypted data.</param>
        /// <param name="kOMFileName">The file name the entry is from.</param>
        /// <param name="algorithm">The algorithm the entry is.</param>
        void DecryptEntry(byte[] input, out byte[] output, string kOMFileName, int algorithm);

        /// <summary>
        /// Encrypts an KOM file entry. If KOM file algorithm is not supported
        /// throw <see cref="NotPackableException"/>.
        /// </summary>
        /// <exception cref="NotPackableException">
        /// When the KOM file algorithm is not suppoted by the curently installed
        /// encryption plugin.
        /// </exception>
        /// <param name="input">The input data to Encrypt.</param>
        /// <param name="output">The Encrypted data.</param>
        /// <param name="kOMFileName">The file name the entry is from.</param>
        /// <param name="algorithm">The algorithm the entry is.</param>
        void EncryptEntry(byte[] input, out byte[] output, string kOMFileName, int algorithm);
    }
}
#endif
