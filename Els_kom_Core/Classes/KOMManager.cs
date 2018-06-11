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
        /* required to see if we are packing or unpacking. Default to false. */
        private static bool is_packing = false;
        private static bool is_unpacking = false;
        internal static System.Collections.Generic.List<interfaces.IKomPlugin> komplugins;

        /// <summary>
        /// Gets the current state on packing KOM files.
        /// </summary>
        internal static bool GetPackingState() => is_packing;
        /// <summary>
        /// Gets the current state on unpacking KOM files.
        /// </summary>
        internal static bool GetUnpackingState() => is_unpacking;

        /// <summary>
        /// Copies Modified KOM files to the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory.
        /// </summary>
        internal static void CopyKomFiles(string FileName, string OrigFileDir, string DestFileDir)
        {
            if (System.IO.File.Exists(OrigFileDir + FileName))
            {
                if ((System.IO.Directory.Exists(DestFileDir)))
                {
                    MoveOriginalKomFiles(FileName, DestFileDir, DestFileDir + "\\backup");
                    if (!DestFileDir.EndsWith("\\"))
                    {
                        // we must add this before copying the file to the target location.
                        DestFileDir += "\\";
                    }
                    System.IO.File.Copy(OrigFileDir + FileName, DestFileDir + FileName);
                }
            }
        }

        /// <summary>
        /// Backs up Original KOM files to a sub folder in the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory. USED INSIDE OF CopyKomFiles SO, USE THAT FUNCTION INSTEAD.
        /// </summary>
        private static void MoveOriginalKomFiles(string FileName, string OrigFileDir, string DestFileDir)
        {
            if (!OrigFileDir.EndsWith("\\"))
            {
                OrigFileDir += "\\";
            }
            if (!DestFileDir.EndsWith("\\"))
            {
                DestFileDir += "\\";
            }
            if (System.IO.File.Exists(OrigFileDir + FileName))
            {
                if ((!System.IO.Directory.Exists(DestFileDir)))
                {
                    System.IO.Directory.CreateDirectory(DestFileDir);
                }
                if (!System.IO.File.Exists(DestFileDir + FileName))
                {
                    System.IO.File.Delete(DestFileDir + FileName);
                }
                System.IO.File.Move(OrigFileDir + FileName, DestFileDir + FileName);
            }
        }

        /// <summary>
        /// Moves the Original KOM Files back to their original locations, overwriting the modified ones.
        /// </summary>
        internal static void MoveOriginalKomFilesBack(string FileName, string OrigFileDir, string DestFileDir)
        {
            if (!OrigFileDir.EndsWith("\\"))
            {
                OrigFileDir += "\\";
            }
            if (!DestFileDir.EndsWith("\\"))
            {
                DestFileDir += "\\";
            }
            if (System.IO.File.Exists(OrigFileDir + FileName))
            {
                if (System.IO.File.Exists(DestFileDir + FileName))
                {
                    System.IO.File.Copy(OrigFileDir + FileName, DestFileDir + FileName, true);
                    System.IO.File.Delete(OrigFileDir + FileName);
                }
            }
        }

        /// <summary>
        /// Checks KOM file headers and returns the version of kom it is. An return of 0 is an Error.
        /// </summary>
        private static int GetHeaderVersion(string komfile)
        {
            int ret = 0;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(System.IO.File.OpenRead(System.Windows.Forms.Application.StartupPath + "\\koms\\" + komfile), System.Text.Encoding.ASCII);
            byte[] headerbuffer = new byte[27];
            // 27 is the size of the header string denoting the KOM file version number.
            int offset = 0;
            reader.Read(headerbuffer, offset, 27);
            string headerstring = System.Text.Encoding.UTF8.GetString(headerbuffer);
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
        private static int CheckFolderVersion(string datafolder)
        {
            int ret = 0;
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

        /// <summary>
        /// Unpacks KOM files by invoking the extractor.
        /// </summary>
        internal static void UnpackKoms()
        {
            is_unpacking = true;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
            foreach (var fi in di.GetFiles("*.kom"))
            {
                string _kom_file = fi.Name;
                int kom_ver = GetHeaderVersion(_kom_file);
                if (kom_ver != 0)
                {
                    // remove ".kom" on end of string.
                    string _kom_data_folder = System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_file);
                    foreach (var komplugin in komplugins)
                    {
                        try
                        {
                            if (kom_ver == komplugin.SupportedKOMVersion)
                            {
                                komplugin.Unpack(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_file, System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_data_folder, _kom_file);
                            }
                            else
                            {
                                // loop until the right plugin for this kom version is found.
                                continue;
                            }
                            // make the version dummy file for the packer.
                            try
                            {
                                System.IO.File.Create(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_data_folder + "\\KOMVERSION." + kom_ver).Dispose();
                            }
                            catch (System.IO.DirectoryNotFoundException)
                            {
                                // cannot create this since nothing was written or made.
                            }
                            // delete original kom file.
                            komplugin.Delete(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_file, false);
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
            is_unpacking = false;
        }

        /// <summary>
        /// Packs KOM files by invoking the packer.
        /// </summary>
        internal static void PackKoms()
        {
            is_packing = true;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
            foreach (var dri in di.GetDirectories())
            {
                string _kom_data_folder = dri.Name;
                int kom_ver = CheckFolderVersion(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_data_folder);
                if (kom_ver != 0)
                {
                    string _kom_file = _kom_data_folder + ".kom";
                    // pack kom based on the version of kom supplied.
                    if (kom_ver != -1)
                    {
                        foreach (var komplugin in komplugins)
                        {
                            try
                            {
                                if (kom_ver == komplugin.SupportedKOMVersion)
                                {
                                    komplugin.Pack(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_data_folder, System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_file, _kom_file);
                                    // delete unpacked kom folder data.
                                    komplugin.Delete(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_data_folder, true);
                                }
                            }
                            catch (PackingError)
                            {
                                // do not delete kom data folder.
                                System.IO.File.Create(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_data_folder + "\\KOMVERSION." + komplugin.SupportedKOMVersion).Dispose();
                                MessageManager.ShowError("Packing an folder to an KOM file failed.", "Error!");
                            }
                            catch (System.NotImplementedException)
                            {
                                System.IO.File.Create(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_data_folder + "\\KOMVERSION." + komplugin.SupportedKOMVersion).Dispose();
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
            is_packing = false;
        }
    }
}
