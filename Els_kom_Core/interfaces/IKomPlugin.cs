// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.interfaces
{
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
        /// Name of the KOM plugin.
        /// </summary>
        string PluginName { get; }
        /// <summary>
        /// List of KOM File header string constant for this plugin's supported KOM version.
        /// </summary>
        string KOMHeaderString { get; }
        /// <summary>
        /// Returns the supported KOM version for packing/unpacking.
        /// </summary>
        int SupportedKOMVersion { get; }

        /// <summary>
        /// Unpacker for the KOM Version this plugin supports.
        /// </summary>
        /// <param name="in_path">input path.</param>
        /// <param name="out_path">output (target) path.</param>
        /// <param name="KOMFileName">KOM File name to use internally for unpacking files.</param>
        /// <exception cref="Classes.UnpackingError">Thrown when unpacking fails badly.</exception>
        /// <exception cref="System.NotImplementedException">Thrown when an plugin does not have this implemented yet.</exception>
        void Unpack(string in_path, string out_path, string KOMFileName);
        /// <summary>
        /// Packer for the KOM Version this plugin supports.
        /// </summary>
        /// <param name="in_path">input path.</param>
        /// <param name="out_path">output (target) path.</param>
        /// <param name="KOMFileName">KOM File name to use internally for packing files.</param>
        /// <exception cref="Classes.PackingError">Thrown when packing fails badly.</exception>
        /// <exception cref="System.NotImplementedException">Thrown when an plugin does not have this implemented yet.</exception>
        void Pack(string in_path, string out_path, string KOMFileName);
        /// <summary>
        /// Deletes the File or Folder that the plugin supports with processing.
        /// </summary>
        /// <param name="in_path">The input path to delete.</param>
        /// <param name="folder">Denotes if the input path is an file or folder.</param>
        void Delete(string in_path, bool folder);
    }
}
