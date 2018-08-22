// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
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
                args[1] = args[1].Replace(".\\", System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar);
            }
            else if (args[1].StartsWith("./"))
            {
                // Replace spaces with periods.
                outfilename = args[1].Replace(" ", ".");
                args[1] = args[1].Replace(" ", ".");
                args[1] = args[1].Replace("./", System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar);
            }
            else
            {
                // Replace spaces with periods.
                outfilename = args[1].Replace(" ", ".");
                args[1] = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + args[1].Replace(" ", ".");
            }

            if (args[0].Equals("-p"))
            {
                System.Console.WriteLine("Writing build files to " + outfilename + ".");
                if (System.IO.File.Exists(args[1]))
                {
                    System.IO.File.Delete(args[1]);
                }

                var zipFile = System.IO.Compression.ZipFile.Open(args[1], System.IO.Compression.ZipArchiveMode.Update);
                var di1 = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
                foreach (var fi1 in di1.GetFiles("*.exe"))
                {
                    var exe_file = fi1.Name;
                    System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, exe_file, exe_file);
                }

                foreach (var fi2 in di1.GetFiles("*.dll"))
                {
                    var dll_file = fi2.Name;
                    System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, dll_file, dll_file);
                }

                foreach (var fi3 in di1.GetFiles("*.xml"))
                {
                    var xml_file = fi3.Name;
                    if (!xml_file.EndsWith(".CodeAnalysisLog.xml"))
                    {
                        System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, xml_file, xml_file);
                    }
                }

                foreach (var fi4 in di1.GetFiles("*.txt"))
                {
                    var txt_file = fi4.Name;
                    System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, txt_file, txt_file);
                }

                foreach (var di2 in di1.GetDirectories())
                {
                    foreach (var fi5 in di2.GetFiles("*.dll"))
                    {
                        var dll_file1 = fi5.Name;
                        System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, di2.Name + System.IO.Path.DirectorySeparatorChar + dll_file1, di2.Name + System.IO.Path.DirectorySeparatorChar + dll_file1);
                    }

                    foreach (var fi6 in di2.GetFiles("*.xml"))
                    {
                        var xml_file1 = fi6.Name;
                        if (!xml_file1.EndsWith(".CodeAnalysisLog.xml"))
                        {
                            System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, di2.Name + System.IO.Path.DirectorySeparatorChar + xml_file1, di2.Name + System.IO.Path.DirectorySeparatorChar + xml_file1);
                        }
                    }

                    foreach (var fi7 in di2.GetFiles("*.txt"))
                    {
                        var txt_file1 = fi7.Name;
                        System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, di2.Name + System.IO.Path.DirectorySeparatorChar + txt_file1, di2.Name + System.IO.Path.DirectorySeparatorChar + txt_file1);
                    }
                }

                zipFile.CreateEntry("koms\\");
                zipFile.Dispose();
            }

            // make a zip with pdb's only.
            else if (args[0].Equals("-d"))
            {
                System.Console.WriteLine("Writing debug symbol files to " + outfilename + ".");
                if (System.IO.File.Exists(args[1]))
                {
                    System.IO.File.Delete(args[1]);
                }

                var zipFile = System.IO.Compression.ZipFile.Open(args[1], System.IO.Compression.ZipArchiveMode.Update);
                var di1 = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
                foreach (var fi1 in di1.GetFiles("*.pdb"))
                {
                    var pdb_file = fi1.Name;
                    System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, pdb_file, pdb_file);
                }

                foreach (var di2 in di1.GetDirectories())
                {
                    foreach (var fi2 in di2.GetFiles("*.pdb"))
                    {
                        var pdb_file1 = fi2.Name;
                        System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, di2.Name + System.IO.Path.DirectorySeparatorChar + pdb_file1, di2.Name + System.IO.Path.DirectorySeparatorChar + pdb_file1);
                    }
                }

                zipFile.Dispose();
            }
        }
    }
}
