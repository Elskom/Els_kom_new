// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Reflection;

    /// <summary>
    /// Loads Assemblies from a zip file.
    /// </summary>
    internal static class AssemblyExtensions
    {
        /// <summary>
        /// Loads the specified assembly from the specified zip file, optionally loading it's debugging symbols.
        /// </summary>
        /// <param name="zipFileName">The zip file to load the assembly from.</param>
        /// <param name="assemblyName">The assembly to load.</param>
        /// <param name="loadPDBFile">If symbols should be loaded (if false and in debug mode they are loaded anyway).</param>
        /// <returns>A new <see cref="Assembly"/> object of the loaded assembly.</returns>
        internal static Assembly LoadFromZip(string zipFileName, string assemblyName, bool loadPDBFile)
        {
            // check if the assembly is in the zip file.
            // If it is, get it’s bytes then load it.
            // If not throw an exception. Also throw
            // an exception if the pdb file is not found.
            var found = false;
            var pdbfound = false;
            byte[] asmbytes = null;
            byte[] pdbbytes = null;
            var pdbFileName = assemblyName.Replace("dll", "pdb");
            var zipFile = ZipFile.OpenRead(zipFileName);
            foreach (var entry in zipFile.Entries)
            {
                if (entry.FullName.Equals(assemblyName))
                {
                    found = true;
                    var strm = entry.Open();
                    var ms = new MemoryStream();
                    strm.CopyTo(ms);
                    asmbytes = ms.ToArray();
                    ms.Dispose();
                    strm.Dispose();
                }
                else if (entry.FullName.Equals(pdbFileName))
                {
                    pdbfound = true;
                    var strm = entry.Open();
                    var ms = new MemoryStream();
                    strm.CopyTo(ms);
                    pdbbytes = ms.ToArray();
                    ms.Dispose();
                    strm.Dispose();
                }
            }

            zipFile.Dispose();
            if (!found)
            {
                throw new Exception(
                    "Assembly specified to load in ZipFile not found.");
            }

            if (!pdbfound)
            {
                throw new Exception(
                    "pdb to Assembly specified to load in ZipFile not found.");
            }

            // always load pdb when debugging.
            // PDB should be automatically downloaded to zip file always
            // and really *should* always be present.
            var loadPDB = loadPDBFile ? loadPDBFile : Debugger.IsAttached;
            return loadPDB ? Assembly.Load(asmbytes, pdbbytes) : Assembly.Load(asmbytes);
        }
    }
}
