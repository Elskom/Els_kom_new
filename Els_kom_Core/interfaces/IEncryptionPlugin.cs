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
        /// Name of the Encryption plugin.
        /// </summary>
        string PluginName { get; }

        /// <summary>
        /// Decrypts an KOM file entry. If KOM file algorithm is not supported
        /// throw <see cref="Classes.UnpackingError"/>.
        /// </summary>
        /// <exception cref="Classes.UnpackingError">
        /// When the KOM file algorith is not suppoted by the curently installed
        /// encryption plugin.
        /// </exception>
        void DecryptEntry(byte[] input, out byte[] output, string KOMFileName, int algorithm);
        /// <summary>
        /// Encrypts an KOM file entry. If KOM file algorithm is not supported
        /// throw <see cref="Classes.PackingError"/>.
        /// </summary>
        /// <exception cref="Classes.PackingError">
        /// When the KOM file algorith is not suppoted by the curently installed
        /// encryption plugin.
        /// </exception>
        void EncryptEntry(byte[] input, out byte[] output, string KOMFileName, int algorithm);
    }
}
#endif
