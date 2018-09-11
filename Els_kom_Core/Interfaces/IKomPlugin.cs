// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Interfaces
{
    using System;
    using Els_kom_Core.Classes;

    /// <summary>
    /// Interface for Els_kom plugins for some optional add-ons to internal workings.
    ///
    /// Every plugin is encouraged to provide an packer / unpacker for an kom file
    /// version they support. The SupportedKOMVersion value must not be 0 or
    /// the plugin will not be used.
    /// </summary>
    public interface IKomPlugin
    {
        /// <summary>
        /// Gets name of the KOM plugin.
        /// </summary>
        /// <value>
        /// Name of the KOM plugin.
        /// </value>
        string PluginName { get; }

        /// <summary>
        /// Gets the KOM File header string constant for this plugin's supported KOM version.
        /// </summary>
        /// <value>
        /// The KOM File header string constant for this plugin's supported KOM version.
        /// </value>
        string KOMHeaderString { get; }

        /// <summary>
        /// Gets the supported KOM version for packing/unpacking.
        /// </summary>
        /// <value>
        /// The supported KOM version for packing/unpacking.
        /// </value>
        int SupportedKOMVersion { get; }

        /// <summary>
        /// Unpacker for the KOM Version this plugin supports.
        /// </summary>
        /// <param name="in_path">input path.</param>
        /// <param name="out_path">output (target) path.</param>
        /// <param name="kOMFileName">KOM File name to use internally for unpacking files.</param>
        /// <exception cref="UnpackingError">Thrown when unpacking fails badly.</exception>
        /// <exception cref="NotImplementedException">Thrown when an plugin does not have this implemented yet.</exception>
        void Unpack(string in_path, string out_path, string kOMFileName);

        /// <summary>
        /// Packer for the KOM Version this plugin supports.
        /// </summary>
        /// <param name="in_path">input path.</param>
        /// <param name="out_path">output (target) path.</param>
        /// <param name="kOMFileName">KOM File name to use internally for packing files.</param>
        /// <exception cref="PackingError">Thrown when packing fails badly.</exception>
        /// <exception cref="NotImplementedException">Thrown when an plugin does not have this implemented yet.</exception>
        void Pack(string in_path, string out_path, string kOMFileName);

        /// <summary>
        /// Deletes the File or Folder that the plugin supports with processing.
        /// </summary>
        /// <param name="in_path">The input path to delete.</param>
        /// <param name="folder">Denotes if the input path is an file or folder.</param>
        void Delete(string in_path, bool folder);

        /// <summary>
        /// Converts the crc.xml file to the format supported
        /// by this version of KOM files.
        /// </summary>
        /// <param name="crcversion">The current version of crc.xml.</param>
        /// <param name="crcpath">The path to the crc.xml file to convert (if needed).</param>
        void ConvertCRC(int crcversion, string crcpath);

        /// <summary>
        /// Updates the crc.xml file to the format supported
        /// by this version of KOM files.
        /// </summary>
        /// <param name="crcpath">The path to the crc.xml file to update (if needed).</param>
        /// <param name="backuppath">The ath to save the backup crc.xml file to.</param>
        void UpdateCRC(string crcpath, string backuppath);
    }
}
