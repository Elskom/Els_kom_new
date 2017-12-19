namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class in the Core that allows Packing and Unpacking kom Files.
    /// </summary>
    public static class KOMManager
    {
        /* required to see if we are packing or unpacking. Default to false. */
        private static bool is_packing = false;
        private static bool is_unpacking = false;

        /// <summary>
        /// Gets the current state on packing KOM files.
        /// </summary>
        public static bool GetPackingState() {
            return is_packing;
        }

        /// <summary>
        /// Gets the current state on unpacking KOM files.
        /// </summary>
        public static bool GetUnpackingState() {
            return is_unpacking;
        }

        /// <summary>
        /// Copies Modified KOM files to the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory.
        /// </summary>
        public static object CopyKomFiles(string FileName, string OrigFileDir, string DestFileDir)
        {
            if (System.IO.File.Exists(FileName))
            {
                if ((!System.IO.Directory.Exists(DestFileDir)))
                {
                    return 1;
                }
                else
                {
                    MoveOriginalKomFiles(FileName, DestFileDir, DestFileDir + "\\backup");
                    System.IO.File.Copy(OrigFileDir + FileName, DestFileDir + FileName);
                }
            }
            return 0;
        }

        /// <summary>
        /// Backs up Original KOM files to a sub folder in the Elsword Directory that was Set in the Settings Dialog in Els_kom. Requires: File Name, Original Directory the File is in, And Destination Directory. USED INSIDE OF CopyKomFiles SO, USE THAT FUNCTION INSTEAD.
        /// </summary>
        private static object MoveOriginalKomFiles(string FileName, string OrigFileDir, string DestFileDir)
        {
            if (System.IO.File.Exists(FileName))
            {
                if ((!System.IO.Directory.Exists(DestFileDir)))
                {
                    System.IO.Directory.CreateDirectory(DestFileDir);
                }
                System.IO.File.Move(OrigFileDir + FileName, DestFileDir + FileName);
            }
            return 0;
        }

        /// <summary>
        /// Checks KOM file headers and returns the version of kom it is. An return of 0 is an Error.
        /// </summary>
        private static int GetHeaderVersion(string komfile)
        {
            int ret = 0;
            System.IO.StreamReader reader = new System.IO.StreamReader(System.IO.File.Open(komfile, System.IO.FileMode.Open));
            char[] headerbuffer = new char[27];
            // 27 is the size of the header string denoting the KOM file version number.
            reader.Read(headerbuffer, 0, 27);
            string headerstring = new string(headerbuffer);
            reader.Close();
            reader.Dispose();
            if (headerstring == "KOG GC TEAM MASSFILE V.0.2.")
            {
                ret = 2;
            }
            else if (headerstring == "KOG GC TEAM MASSFILE V.0.3.")
            {
                ret = 3;
            }
            else if (headerstring == "KOG GC TEAM MASSFILE V.0.4.")
            {
                ret = 4;
            }
            return ret;
        }

        /// <summary>
        /// Checks Data folders for dummy KOM version specifier files and returns the version of kom it should be packed to. An return of 0 is an Error.
        /// </summary>
        private static int CheckFolderVersion(string datafolder) {
            int ret = 0;
            if (System.IO.File.Exists(datafolder + "\\KOMVERSION.2")) {
                System.IO.File.Delete(datafolder + "\\KOMVERSION.2");
                ret = 2;
            }
            else if (System.IO.File.Exists(datafolder + "\\KOMVERSION.3")) {
                System.IO.File.Delete(datafolder + "\\KOMVERSION.3");
                ret = 3;
            }
            else if (System.IO.File.Exists(datafolder + "\\KOMVERSION.4")) {
                System.IO.File.Delete(datafolder + "\\KOMVERSION.4");
                ret = 4;
            }
            return ret;
        }

        /// <summary>
        /// Unpacks KOM files by invoking the extractor.
        /// </summary>
        public static void UnpackKoms()
        {
            is_unpacking = true;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
            foreach (var fi in di.GetFiles("*.kom"))
            {
                string _kom_file = fi.Name;
                int kom_ver = GetHeaderVersion(_kom_file);
                if (kom_ver != 0 && kom_ver != 4)
                {
                    // remove ".kom" on end of string.
                    string _kom_data_folder = System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_file);
                    ExecutionManager.Shell(System.Windows.Forms.Application.StartupPath + "\\komextract_new.exe", "--" + kom_ver + " --in \"" + System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_file + "\" --out \"" + System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_data_folder + "\"", false, false, true, System.Diagnostics.ProcessWindowStyle.Hidden, System.Windows.Forms.Application.StartupPath, true);
                    fi.Delete();
                    // make the version dummy file for the packer.
                    System.IO.File.Create(_kom_data_folder + "\\KOMVERSION." + kom_ver);
                }
                else if (kom_ver == 4)
                {
                    MessageManager.ShowError("KOM V4 is currently not supported yet. Please wait until it is.", "Error!");
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
        public static void PackKoms()
        {
            is_packing = true;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\koms");
            foreach (var dri in di.GetDirectories())
            {
                string _kom_data_folder = dri.Name;
                int kom_ver = CheckFolderVersion(System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_data_folder);
                if (kom_ver != 0 && kom_ver != 4)
                {
                    string _kom_file = _kom_data_folder + ".kom";
                    // pack kom based on the version of kom supplied.
                    ExecutionManager.Shell(System.Windows.Forms.Application.StartupPath + "\\kompact_new.exe", "--" + kom_ver + " --in \"" + System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_data_folder + "\" --out \"" + System.Windows.Forms.Application.StartupPath + "\\koms\\" + _kom_file + "\"", false, false, true, System.Diagnostics.ProcessWindowStyle.Hidden, System.Windows.Forms.Application.StartupPath, true);
                    foreach (var fi in dri.GetFiles()) {
                        fi.Delete();
                    }
                    dri.Delete();
                }
                else if (kom_ver == 4)
                {
                    MessageManager.ShowError("KOM V4 is currently not supported yet. Please wait until it is.", "Error!");
                }
            }
            is_packing = false;
        }
    }
}
