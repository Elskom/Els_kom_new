// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class in the Core that allows managing kom Files.
    /// </summary>
    internal static class KOMManager
    {
        private static System.Collections.Generic.List<Interfaces.IKomPlugin> komplugins;

        /// <summary>
        /// Gets The list of <see cref="Interfaces.IKomPlugin"/> plugins.
        /// </summary>
        internal static System.Collections.Generic.List<Interfaces.IKomPlugin> Komplugins
        {
            get
            {
                if (komplugins == null)
                {
                    komplugins = new System.Collections.Generic.List<Interfaces.IKomPlugin>();
                }

                return komplugins;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current state on packing KOM files.
        /// </summary>
        /// <returns>The current state on packing KOM files.</returns>
        internal static bool PackingState { get; private set; } = false;

        /// <summary>
        /// Gets a value indicating whether the current state on unpacking KOM files.
        /// </summary>
        /// <returns>The current state on unpacking KOM files.</returns>
        internal static bool UnpackingState { get; private set; } = false;

        /// <summary>
        /// Copies Modified KOM files to the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory.
        /// </summary>
        /// <param name="fileName">The name of the file to copy.</param>
        /// <param name="origFileDir">The original kom file location.</param>
        /// <param name="destFileDir">The target to copy the kom file too.</param>
        internal static void CopyKomFiles(string fileName, string origFileDir, string destFileDir)
        {
            if (System.IO.File.Exists(origFileDir + fileName))
            {
                if (System.IO.Directory.Exists(destFileDir))
                {
                    MoveOriginalKomFiles(fileName, destFileDir, destFileDir + "\\backup");
                    if (!destFileDir.EndsWith("\\"))
                    {
                        // we must add this before copying the file to the target location.
                        destFileDir += "\\";
                    }

                    System.IO.File.Copy(origFileDir + fileName, destFileDir + fileName);
                }
            }
        }

        /// <summary>
        /// Moves the Original KOM Files back to their original locations, overwriting the modified ones.
        /// </summary>
        /// <param name="fileName">The name of the file to move.</param>
        /// <param name="origFileDir">The original kom file location.</param>
        /// <param name="destFileDir">The target to move the kom file too.</param>
        internal static void MoveOriginalKomFilesBack(string fileName, string origFileDir, string destFileDir)
        {
            if (!origFileDir.EndsWith("\\"))
            {
                origFileDir += "\\";
            }

            if (!destFileDir.EndsWith("\\"))
            {
                destFileDir += "\\";
            }

            if (System.IO.File.Exists(origFileDir + fileName))
            {
                if (System.IO.File.Exists(destFileDir + fileName))
                {
                    System.IO.File.Copy(origFileDir + fileName, destFileDir + fileName, true);
                    System.IO.File.Delete(origFileDir + fileName);
                }
            }
        }

        /// <summary>
        /// Unpacks KOM files by invoking the extractor.
        /// </summary>
        internal static void UnpackKoms()
        {
            UnpackingState = true;
            var di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
            foreach (var fi in di.GetFiles("*.kom"))
            {
                var kom_file = fi.Name;
                var kom_ver = GetHeaderVersion(kom_file);
                if (kom_ver != 0)
                {
                    // remove ".kom" on end of string.
                    var kom_data_folder = System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_file);
                    foreach (var komplugin in komplugins)
                    {
                        try
                        {
                            if (kom_ver == komplugin.SupportedKOMVersion)
                            {
                                komplugin.Unpack(System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_file, System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_data_folder, kom_file);
                            }
                            else
                            {
                                // loop until the right plugin for this kom version is found.
                                continue;
                            }

                            // make the version dummy file for the packer.
                            try
                            {
                                System.IO.File.Create(System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_data_folder + "\\KOMVERSION." + kom_ver).Dispose();
                            }
                            catch (System.IO.DirectoryNotFoundException)
                            {
                                // cannot create this since nothing was written or made.
                            }

                            // delete original kom file.
                            komplugin.Delete(System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_file, false);
                        }
                        catch (UnpackingError)
                        {
                            // do not delete kom file.
                            MessageManager.ShowError("Unpacking this KOM file failed.", "Error!");
                        }
                        catch (System.NotImplementedException)
                        {
                            MessageManager.ShowError("The KOM V" + komplugin.SupportedKOMVersion + " plugin does not implement an unpacker function yet. Although it should.", "Error!");
                        }
                    }
                }
                else
                {
                    MessageManager.ShowError("Unknown KOM version Detected. Please send this KOM to the Els_kom Developers file for inspection.", "Error!");
                }
            }

            UnpackingState = false;
        }

        /// <summary>
        /// Packs KOM files by invoking the packer.
        /// </summary>
        internal static void PackKoms()
        {
            PackingState = true;
            var di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
            foreach (var dri in di.GetDirectories())
            {
                var kom_data_folder = dri.Name;
                var kom_ver = CheckFolderVersion(System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_data_folder);
                if (kom_ver != 0)
                {
                    var kom_file = kom_data_folder + ".kom";

                    // pack kom based on the version of kom supplied.
                    if (kom_ver != -1)
                    {
                        foreach (var komplugin in komplugins)
                        {
                            try
                            {
                                if (kom_ver == komplugin.SupportedKOMVersion)
                                {
                                    komplugin.Pack(System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_data_folder, System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_file, kom_file);

                                    // delete unpacked kom folder data.
                                    komplugin.Delete(System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_data_folder, true);
                                }
                            }
                            catch (PackingError)
                            {
                                // do not delete kom data folder.
                                System.IO.File.Create(System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_data_folder + "\\KOMVERSION." + komplugin.SupportedKOMVersion).Dispose();
                                MessageManager.ShowError("Packing an folder to an KOM file failed.", "Error!");
                            }
                            catch (System.NotImplementedException)
                            {
                                System.IO.File.Create(System.Windows.Forms.Application.StartupPath + "\\koms\\" + kom_data_folder + "\\KOMVERSION." + komplugin.SupportedKOMVersion).Dispose();
                                MessageManager.ShowError("The KOM V" + komplugin.SupportedKOMVersion + " plugin does not implement an packer function yet. Although it should.", "Error!");
                            }
                        }
                    }
                    else
                    {
                        MessageManager.ShowError("An error occured while packing the file(s) to an KOM file.", "Error!");
                    }
                }
                else
                {
                    MessageManager.ShowError("Unknown KOM version Detected. Please send this KOM to the Els_kom Developers file for inspection.", "Error!");
                }
            }

            PackingState = false;
        }

        /// <summary>
        /// Backs up Original KOM files to a sub folder in the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory. USED INSIDE OF CopyKomFiles SO, USE THAT FUNCTION INSTEAD.
        /// </summary>
        /// <param name="fileName">The name of the file to move.</param>
        /// <param name="origFileDir">The original kom file location.</param>
        /// <param name="destFileDir">The target to move the kom file too.</param>
        private static void MoveOriginalKomFiles(string fileName, string origFileDir, string destFileDir)
        {
            if (!origFileDir.EndsWith("\\"))
            {
                origFileDir += "\\";
            }

            if (!destFileDir.EndsWith("\\"))
            {
                destFileDir += "\\";
            }

            if (System.IO.File.Exists(origFileDir + fileName))
            {
                if (!System.IO.Directory.Exists(destFileDir))
                {
                    System.IO.Directory.CreateDirectory(destFileDir);
                }

                if (!System.IO.File.Exists(destFileDir + fileName))
                {
                    System.IO.File.Delete(destFileDir + fileName);
                }

                System.IO.File.Move(origFileDir + fileName, destFileDir + fileName);
            }
        }

        /// <summary>
        /// Checks KOM file headers and returns the version of kom it is. An return of 0 is an Error.
        /// </summary>
        /// <param name="komfile">The kom file name.</param>
        /// <returns>The KOM file header version number.</returns>
        private static int GetHeaderVersion(string komfile)
        {
            var ret = 0;
            var reader = new System.IO.BinaryReader(System.IO.File.OpenRead(System.Windows.Forms.Application.StartupPath + "\\koms\\" + komfile), System.Text.Encoding.ASCII);
            var headerbuffer = new byte[27];

            // 27 is the size of the header string denoting the KOM file version number.
            var offset = 0;
            reader.Read(headerbuffer, offset, 27);
            var headerstring = System.Text.Encoding.UTF8.GetString(headerbuffer);
            reader.Dispose();
            foreach (var komplugin in komplugins)
            {
                // get version of kom file for unpacking it.
                if (komplugin.KOMHeaderString == string.Empty)
                {
                    // skip this plugin it does not implement an packer or unpacker.
                    continue;
                }

                if (headerstring == komplugin.KOMHeaderString)
                {
                    ret = komplugin.SupportedKOMVersion;
                }
            }

            return ret;
        }

        /// <summary>
        /// Checks Data folders for dummy KOM version specifier files and returns the version of kom it should be packed to. An return of 0 is an Error.
        /// </summary>
        /// <param name="datafolder">The path to the folder of the unpacked kom file content.</param>
        /// <returns>The version of the kom file the folder will pack to.</returns>
        private static int CheckFolderVersion(string datafolder)
        {
            var ret = 0;
            foreach (var komplugin in komplugins)
            {
                if (komplugin.SupportedKOMVersion != 0)
                {
                    if (System.IO.File.Exists(datafolder + "\\KOMVERSION." + komplugin.SupportedKOMVersion))
                    {
                        try
                        {
                            System.IO.File.Delete(datafolder + "\\KOMVERSION." + komplugin.SupportedKOMVersion);
                            ret = komplugin.SupportedKOMVersion;
                        }
                        catch (System.IO.IOException)
                        {
                            ret = -1;
                        }
                    }
                }
            }

            return ret;
        }
    }
}
