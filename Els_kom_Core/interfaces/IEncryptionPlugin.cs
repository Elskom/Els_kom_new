// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

/* 0x01050000 = v1.5.0.0 */
#if VERSION_0x01050000
namespace Els_kom_Core.interfaces
{
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
        /// Decrypts an KOM file entry.
        /// </summary>
        void DecryptEntry(byte[] input, out byte[] output, string KOMFileName, int algorithm);
        /// <summary>
        /// Encrypts an KOM file entry.
        /// </summary>
        void EncryptEntry(byte[] input, out byte[] output, string KOMFileName, int algorithm);
    }
}
#endif
