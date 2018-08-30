// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// Handles Els_kom's release packaging command line.
    /// </summary>
    internal static class ReleasePackaging
    {
        /// <summary>
        /// Packages an Els_kom Release build to a zip file.
        /// </summary>
        /// <param name="args">The command line arguments passed into Els_kom.exe.</param>
        internal static void PackageRelease(string[] args)
        {
            // files to exclude from Release zip.
            var outfilename = string.Empty;
            if (args[1].StartsWith(".\\"))
            {
                // Replace spaces with periods.
                outfilename = args[1].Replace(" ", ".");
                args[1] = args[1].Replace(" ", ".");
                args[1] = args[1].Replace(".\\", Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar);
            }
            else if (args[1].StartsWith("./"))
            {
                // Replace spaces with periods.
                outfilename = args[1].Replace(" ", ".");
                args[1] = args[1].Replace(" ", ".");
                args[1] = args[1].Replace("./", Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar);
            }
            else
            {
                // Replace spaces with periods.
                outfilename = args[1].Replace(" ", ".");
                args[1] = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + args[1].Replace(" ", ".");
            }

            if (args[0].Equals("-p"))
            {
                Console.WriteLine("Writing build files and debug symbol files to " + outfilename + ".");
                if (File.Exists(args[1]))
                {
                    File.Delete(args[1]);
                }

                var zipFile = ZipFile.Open(args[1], ZipArchiveMode.Update);
                var di1 = new DirectoryInfo(Directory.GetCurrentDirectory());
                foreach (var fi1 in di1.GetFiles("*.exe"))
                {
                    var exe_file = fi1.Name;
                    zipFile.CreateEntryFromFile(exe_file, exe_file);
                }

                foreach (var fi2 in di1.GetFiles("*.dll"))
                {
                    var dll_file = fi2.Name;
                    zipFile.CreateEntryFromFile(dll_file, dll_file);
                }

                foreach (var fi3 in di1.GetFiles("*.xml"))
                {
                    var xml_file = fi3.Name;
                    if (!xml_file.EndsWith(".CodeAnalysisLog.xml"))
                    {
                        zipFile.CreateEntryFromFile(xml_file, xml_file);
                    }
                }

                foreach (var fi4 in di1.GetFiles("*.txt"))
                {
                    var txt_file = fi4.Name;
                    zipFile.CreateEntryFromFile(txt_file, txt_file);
                }

                foreach (var fi5 in di1.GetFiles("*.pdb"))
                {
                    var pdb_file = fi5.Name;
                    zipFile.CreateEntryFromFile(pdb_file, pdb_file);
                }

                foreach (var di2 in di1.GetDirectories())
                {
                    foreach (var fi6 in di2.GetFiles("*.pdb"))
                    {
                        var pdb_file1 = fi6.Name;
                        zipFile.CreateEntryFromFile(di2.Name + Path.DirectorySeparatorChar + pdb_file1, di2.Name + Path.DirectorySeparatorChar + pdb_file1);
                    }

                    foreach (var fi7 in di2.GetFiles("*.dll"))
                    {
                        var dll_file1 = fi7.Name;
                        zipFile.CreateEntryFromFile(di2.Name + Path.DirectorySeparatorChar + dll_file1, di2.Name + Path.DirectorySeparatorChar + dll_file1);
                    }

                    foreach (var fi8 in di2.GetFiles("*.xml"))
                    {
                        var xml_file1 = fi8.Name;
                        if (!xml_file1.EndsWith(".CodeAnalysisLog.xml"))
                        {
                            zipFile.CreateEntryFromFile(di2.Name + Path.DirectorySeparatorChar + xml_file1, di2.Name + Path.DirectorySeparatorChar + xml_file1);
                        }
                    }

                    foreach (var fi9 in di2.GetFiles("*.txt"))
                    {
                        var txt_file1 = fi9.Name;
                        zipFile.CreateEntryFromFile(di2.Name + Path.DirectorySeparatorChar + txt_file1, di2.Name + Path.DirectorySeparatorChar + txt_file1);
                    }
                }

                zipFile.Dispose();
            }
        }
    }
}
