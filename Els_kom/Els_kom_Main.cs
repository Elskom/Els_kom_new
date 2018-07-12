// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

internal static class Els_kom_Main
{
    [System.STAThread]
    [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, Flags = System.Security.Permissions.SecurityPermissionFlag.ControlAppDomain)]
    internal static int Main(string[] args)
    {
        // files to exclude from Release zip.
        string ExcludeFile = ".CodeAnalysisLog.xml";
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
                if (System.IO.File.Exists(args[1]))
                {
                    System.IO.File.Delete(args[1]);
                }
                System.IO.Compression.ZipArchive zipFile = System.IO.Compression.ZipFile.Open(args[1], System.IO.Compression.ZipArchiveMode.Update);
                System.IO.DirectoryInfo di1 = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
                foreach (var fi1 in di1.GetFiles("*.exe"))
                {
                    string _exe_file = fi1.Name;
                    System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, _exe_file, _exe_file);
                }
                foreach (var fi2 in di1.GetFiles("*.dll"))
                {
                    string _dll_file = fi2.Name;
                    System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, _dll_file, _dll_file);
                }
                foreach (var fi3 in di1.GetFiles("*.xml"))
                {
                    string _xml_file = fi3.Name;
                    if (!_xml_file.EndsWith(ExcludeFile))
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
                        if (!_xml_file.EndsWith(ExcludeFile))
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
                if (System.IO.File.Exists(args[1]))
                {
                    System.IO.File.Delete(args[1]);
                }
                System.IO.Compression.ZipArchive zipFile = System.IO.Compression.ZipFile.Open(args[1], System.IO.Compression.ZipArchiveMode.Update);
                System.IO.DirectoryInfo di1 = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
                foreach (var fi1 in di1.GetFiles("*.pdb"))
                {
                    string _pdb_file = fi1.Name;
                    System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, _pdb_file, _pdb_file);
                }
                foreach (var di2 in di1.GetDirectories())
                {
                    foreach (var fi2 in di2.GetFiles("*.pdb"))
                    {
                        string _pdb_file = fi2.Name;
                        System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(zipFile, di2.Name + System.IO.Path.DirectorySeparatorChar + _pdb_file, di2.Name + System.IO.Path.DirectorySeparatorChar + _pdb_file);
                    }
                }
                zipFile.Dispose();
            }
        }
        else
        {
            System.AppDomain currentDomain = System.AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(ExceptionHandler);
            System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadExceptionHandler);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Els_kom.Forms.MainForm());
        }
        return 0;
    }

    static void ExceptionHandler(object sender, System.UnhandledExceptionEventArgs args)
    {
        System.Exception e = (System.Exception)args.ExceptionObject;
        string exceptionData = e.GetType().ToString() + ": " + e.Message + System.Environment.NewLine + e.StackTrace + System.Environment.NewLine;
        byte[] outputData = System.Text.Encoding.ASCII.GetBytes(exceptionData);
        // do not dump or close if in a debugger.
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            Els_kom.Forms.MainForm._closable = true;
            System.IO.FileStream fileStream = System.IO.File.OpenWrite(Els_kom_Core.Classes.SettingsFile.ErrorLogPath);
            fileStream.Write(outputData, 0, outputData.Length);
            fileStream.Dispose();
            Els_kom_Core.Classes.MiniDump.FullMiniDumpToFile(Els_kom_Core.Classes.SettingsFile.MiniDumpPath);
            System.Windows.Forms.MessageBox.Show("Please send a copy of " + Els_kom_Core.Classes.SettingsFile.ErrorLogPath + " to https://github.com/Elskom/Els_kom_new/issues by making an issue and attaching the log(s) and mini-dump(s).", "Unhandled Exception!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            System.Windows.Forms.Application.Exit();
        }
    }

    static void ThreadExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
        System.Exception ex = e.Exception;
        string exceptionData = ex.GetType().ToString() + ": " + ex.Message + System.Environment.NewLine + ex.StackTrace + System.Environment.NewLine;
        byte[] outputData = System.Text.Encoding.ASCII.GetBytes(exceptionData);
        // do not dump or close if in a debugger.
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            Els_kom.Forms.MainForm._closable = true;
            System.IO.FileStream fileStream = System.IO.File.OpenWrite(Els_kom_Core.Classes.SettingsFile.ErrorLogPath);
            fileStream.Write(outputData, 0, outputData.Length);
            fileStream.Dispose();
            Els_kom_Core.Classes.MiniDump.FullMiniDumpToFile(Els_kom_Core.Classes.SettingsFile.MiniDumpPath);
            System.Windows.Forms.MessageBox.Show("Please send a copy of " + Els_kom_Core.Classes.SettingsFile.ErrorLogPath + " to https://github.com/Elskom/Els_kom_new/issues by making an issue and attaching the log(s) and mini-dump(s).", "Unhandled Thread Exception!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            System.Windows.Forms.Application.Exit();
        }
    }
}
