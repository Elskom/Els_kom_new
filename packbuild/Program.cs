// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace packbuild
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // files to exclude from Release zip.
            string ExcludeFile1 = "packbuild.exe";
            string ExcludeFile2 = "packbuild.pdb";
            string ExcludeFile3 = ".CodeAnalysisLog.xml";
            if ((args.Length - 1) > -1)
            {
                string outfilename = "";
                if (args[1].StartsWith(".\\"))
                {
                    outfilename = args[1];
                    args[1] = args[1].Replace(".\\", System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar);
                }
                else if (args[1].StartsWith("./"))
                {
                    outfilename = args[1];
                    args[1] = args[1].Replace("./", System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar);
                }
                else
                {
                    outfilename = args[1];
                    args[1] = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + args[1];
                }
                if (args[0].Equals("-p"))
                {
                    System.Console.WriteLine("Writing build files to " + outfilename + ".");
                    System.IO.Compression.ZipArchive zipFile = System.IO.Compression.ZipFile.Open(args[1], System.IO.Compression.ZipArchiveMode.Update);
                    System.IO.DirectoryInfo di1 = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
                    foreach (var fi1 in di1.GetFiles("*.exe"))
                    {
                        string _exe_file = fi1.Name;
                        if (!_exe_file.Equals(ExcludeFile1))
                        {
                            System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, _exe_file, _exe_file);
                        }
                    }
                    foreach (var fi2 in di1.GetFiles("*.dll"))
                    {
                        string _dll_file = fi2.Name;
                        System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, _dll_file, _dll_file);
                    }
                    foreach (var fi3 in di1.GetFiles("*.xml"))
                    {
                        string _xml_file = fi3.Name;
                        if (!_xml_file.EndsWith(ExcludeFile3))
                        {
                            System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, _xml_file, _xml_file);
                        }
                    }
                    foreach (var fi4 in di1.GetFiles("*.txt"))
                    {
                        string _txt_file = fi4.Name;
                        System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, _txt_file, _txt_file);
                    }
                    foreach (var di2 in di1.GetDirectories())
                    {
                        foreach (var fi5 in di2.GetFiles("*.dll"))
                        {
                            string _dll_file = fi5.Name;
                            System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, di2.Name + System.IO.Path.DirectorySeparatorChar + _dll_file, di2.Name + System.IO.Path.DirectorySeparatorChar + _dll_file);
                        }
                        foreach (var fi6 in di2.GetFiles("*.xml"))
                        {
                            string _xml_file = fi6.Name;
                            if (!_xml_file.EndsWith(ExcludeFile3))
                            {
                                System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, di2.Name + System.IO.Path.DirectorySeparatorChar + _xml_file, di2.Name + System.IO.Path.DirectorySeparatorChar + _xml_file);
                            }
                        }
                        foreach (var fi7 in di2.GetFiles("*.txt"))
                        {
                            string _txt_file = fi7.Name;
                            System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, di2.Name + System.IO.Path.DirectorySeparatorChar + _txt_file, di2.Name + System.IO.Path.DirectorySeparatorChar + _txt_file);
                        }
                    }
                    zipFile.CreateEntry("koms\\");
                    zipFile.Dispose();
                }
                // make a zip with pdb's only.
                else if (args[0].Equals("-d"))
                {
                    System.Console.WriteLine("Writing debug symbol files to " + outfilename + ".");
                    System.IO.Compression.ZipArchive zipFile = System.IO.Compression.ZipFile.Open(args[1], System.IO.Compression.ZipArchiveMode.Update);
                    System.IO.DirectoryInfo di1 = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
                    foreach (var fi1 in di1.GetFiles("*.pdb"))
                    {
                        string _pdb_file = fi1.Name;
                        if (!_pdb_file.Equals(ExcludeFile2))
                        {
                            System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, _pdb_file, _pdb_file);
                        }
                    }
                    foreach (var di2 in di1.GetDirectories())
                    {
                        foreach (var fi2 in di2.GetFiles("*.pdb"))
                        {
                            string _pdb_file = fi2.Name;
                            if (!_pdb_file.Equals(ExcludeFile2))
                            {
                                System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, di2.Name + System.IO.Path.DirectorySeparatorChar + _pdb_file, di2.Name + System.IO.Path.DirectorySeparatorChar + _pdb_file);
                            }
                        }
                    }
                    zipFile.Dispose();
                }
            }
            else
            {
                System.Console.WriteLine("Usage:");
                System.Console.WriteLine("\tpackbuild [-p] [-d] <output file name.zip>");
            }
        }
    }
}
