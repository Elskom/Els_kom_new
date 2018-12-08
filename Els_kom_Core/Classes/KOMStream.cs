// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Linq;
    using Els_kom_Core.Interfaces;
    using Elskom.Generic.Libs;

    /// <summary>
    /// KOM File format related stream.
    /// </summary>
    public class KOMStream : Stream
    {
        private static List<IKomPlugin> komplugins;
        private static List<IEncryptionPlugin> encryptionplugins;

        /// <summary>
        /// Initializes a new instance of the <see cref="KOMStream"/> class.
        /// </summary>
        public KOMStream()
            : base()
        {
        }

        /// <summary>
        /// Gets The list of <see cref="IKomPlugin"/> plugins.
        /// </summary>
        /// <value>
        /// The list of <see cref="IKomPlugin"/> plugins.
        /// </value>
        public static List<IKomPlugin> Komplugins
        {
            get
            {
                if (komplugins == null)
                {
                    komplugins = new List<IKomPlugin>();
                }

                return komplugins;
            }
        }

        /// <summary>
        /// Gets The list of <see cref="IEncryptionPlugin"/> plugins.
        /// </summary>
        /// <value>
        /// The list of <see cref="IEncryptionPlugin"/> plugins.
        /// </value>
        public static List<IEncryptionPlugin> Encryptionplugins
        {
            get
            {
                if (encryptionplugins == null)
                {
                    encryptionplugins = new List<IEncryptionPlugin>();
                }

                return encryptionplugins;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Stream can Read.
        /// </summary>
        /// <value>
        /// A value indicating whether the Stream can Read.
        /// </value>
        public override bool CanRead => false;

        /// <summary>
        /// Gets a value indicating whether the Stream can Seek.
        /// </summary>
        /// <value>
        /// A value indicating whether the Stream can Seek.
        /// </value>
        public override bool CanSeek => false;

        /// <summary>
        /// Gets a value indicating whether the Stream can Write.
        /// </summary>
        /// <value>
        /// A value indicating whether the Stream can Write.
        /// </value>
        public override bool CanWrite => false;

        /// <summary>
        /// Gets a value indicating whether the Stream can Timeout.
        /// </summary>
        /// <value>
        /// A value indicating whether the Stream can Timeout.
        /// </value>
        public override bool CanTimeout => false;

        /// <summary>
        /// Gets the length of the stream.
        /// Always 0 as no real stuff is supported.
        /// This is only to support extending this
        /// class for more kom plugins if needed.
        /// </summary>
        /// <value>
        /// The length of the stream.
        /// Always 0 as no real stuff is supported.
        /// This is only to support extending this
        /// class for more kom plugins if needed.
        /// </value>
        public override long Length => 0;

        /// <summary>
        /// Gets or sets the KOMStream Position.
        /// </summary>
        /// <value>
        /// The KOMStream Position.
        /// </value>
        public override long Position
        {
            get => 0;
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Writes the KOM File entry to file.
        /// </summary>
        /// <param name="reader">The reader for the kom file.</param>
        /// <param name="outpath">The output folder for every entry in the kom file.</param>
        /// <param name="entry">The kom file entry instance.</param>
        /// <param name="version">The kom file version.</param>
        /// <param name="xmldata">The crc.xml data to write.</param>
#if VERSION_0x01050000
        /// <param name="kOMFileName">The name of the kom file the entry is from.</param>
        public static void WriteOutput(BinaryReader reader, string outpath, EntryVer entry, int version, string xmldata, string kOMFileName)
#else
        public static void WriteOutput(BinaryReader reader, string outpath, EntryVer entry, int version, string xmldata)
#endif
        {
            if (version > 2)
            {
                if (!Directory.Exists(outpath))
                {
                    Directory.CreateDirectory(outpath);
                }

                var xmldatabuffer = Encoding.ASCII.GetBytes(xmldata);
                if (!File.Exists(outpath + Path.DirectorySeparatorChar + "crc.xml"))
                {
                    using (var fs = File.Create(outpath + Path.DirectorySeparatorChar + "crc.xml"))
                    {
                        fs.Write(xmldatabuffer, 0, xmldatabuffer.Length);
                    }
                }

                var entrydata = reader.ReadBytes(entry.CompressedSize);
                if (entry.Algorithm == 0)
                {
                    var failure = false;
                    using (var entryfile = File.Create(outpath + "\\" + entry.Name))
                    {
                        try
                        {
                            MemoryZlib.DecompressData(entrydata, out var dec_entrydata);
                            entryfile.Write(dec_entrydata, 0, entry.UncompressedSize);
                        }
                        catch (ArgumentException ex)
                        {
                            throw new NotUnpackableException("Something failed...", ex);
                        }
                        catch (NotUnpackableException ex)
                        {
                            throw new NotUnpackableException("decompression failed...", ex);
                        }
                    }

                    if (failure)
                    {
                        File.Move(outpath + "\\" + entry.Name, outpath + "\\" + entry.Name + "." + entry.UncompressedSize + "." + entry.Algorithm);
                    }
                }
                else
                {
                    var path = outpath + "\\" + entry.Name + "." + entry.UncompressedSize + "." + entry.Algorithm;
                    if (entrydata.Length == entry.UncompressedSize)
                    {
                        path = outpath + "\\" + entry.Name;
                    }

                    using (var entryfile = File.Create(path))
                    {
#if VERSION_0x01050000
                        byte[] dec_entrydata = null;
                        var failure = false;
                        if (entry.Algorithm == 3)
                        {
                            // algorithm 3 code.
                            byte[] zdec_entrydata = null;
                            try
                            {
                                MemoryZlib.DecompressData(entrydata, out zdec_entrydata);
                            }
                            catch (ArgumentException ex)
                            {
                                throw new NotUnpackableException("Something failed...", ex);
                            }
                            catch (NotUnpackableException ex)
                            {
                                throw new NotUnpackableException("decompression failed...", ex);
                            }

                            // Decrypt the data from a encryption plugin.
                            Encryptionplugins[0].DecryptEntry(zdec_entrydata, out dec_entrydata, LoadResources.GetFileBaseName(kOMFileName), entry.Algorithm);
                        }
                        else
                        {
                            // algorithm 2 code.
                            // Decrypt the data from a encryption plugin.
                            Encryptionplugins[0].DecryptEntry(entrydata, out byte[] decr_entrydata, LoadResources.GetFileBaseName(kOMFileName), entry.Algorithm);
                            try
                            {
                                MemoryZlib.DecompressData(decr_entrydata, out dec_entrydata);
                            }
                            catch (ArgumentException ex)
                            {
                                throw new NotUnpackableException("Something failed...", ex);
                            }
                            catch (NotUnpackableException ex)
                            {
                                throw new NotUnpackableException("decompression failed...", ex);
                            }
                        }

                        entryfile.Write(dec_entrydata, 0, entry.UncompressedSize);
                        entryfile.Dispose();
                        if (failure)
                        {
                            File.Move(outpath + "\\" + entry.Name, outpath + "\\" + entry.Name + "." + entry.UncompressedSize + "." + entry.Algorithm);
                        }
                    }
#else
                        if (entry.Algorithm == 3)
                        {
                            // algorithm 3 code.
                        }
                        else
                        {
                            // algorithm 2 code.
                        }

                        // for now until I can decompress this crap.
                        entryfile.Write(entrydata, 0, entry.CompressedSize);
                    }
#endif
                }
            }
            else
            {
                // Write KOM V2 output to file.
                if (!Directory.Exists(outpath))
                {
                    Directory.CreateDirectory(outpath);
                }

                var entrydata = reader.ReadBytes(entry.CompressedSize);
                using (var entryfile = File.Create(outpath + "\\" + entry.Name))
                {
                    byte[] dec_entrydata;
                    try
                    {
                        MemoryZlib.DecompressData(entrydata, out dec_entrydata);
                    }
                    catch (NotUnpackableException)
                    {
                        // copyright symbols... Really funny xor key...
                        var xorkey = Encoding.UTF8.GetBytes("\xa9\xa9\xa9\xa9\xa9\xa9\xa9\xa9\xa9\xa9");

                        // xor this shit then try again...
                        BlowFish.XorBlock(ref entrydata, xorkey);
                        try
                        {
                            MemoryZlib.DecompressData(entrydata, out dec_entrydata);
                            using (File.Create(outpath + "\\XoRNeeded.dummy"))
                            {
                            }
                        }
                        catch (NotUnpackableException ex)
                        {
                            throw new NotUnpackableException("failed with zlib decompression of entries.", ex);
                        }
                    }

                    entryfile.Write(dec_entrydata, 0, entry.UncompressedSize);
                }
            }
        }

        /// <summary>
        /// Converts the KOM crc.xml file to the provided version,
        /// if it is not already that version.
        /// </summary>
        /// <param name="toVersion">The version to convert the CRC.xml file to.</param>
        /// <param name="crcpath">The path to the crc.xml file.</param>
        public static void ConvertCRC(int toVersion, string crcpath)
        {
            if (File.Exists(crcpath))
            {
                var crcversion = GetCRCVersion(Encoding.ASCII.GetString(File.ReadAllBytes(crcpath)));
                if (crcversion != toVersion)
                {
                    foreach (var plugin in Komplugins)
                    {
                        if (toVersion == plugin.SupportedKOMVersion)
                        {
                            plugin.ConvertCRC(crcversion, crcpath);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the crc.xml file if the folder has new files
        /// not in the crc.xml, or removes files listed in crc.xml that are
        /// no longer in the folder.
        /// </summary>
        /// <param name="crcversion">The version of the crc.xml file.</param>
        /// <param name="crcpath">The path to the crc.xml file.</param>
        /// <param name="checkpath">The directry that contains the crc.xml file.</param>
        public static void UpdateCRC(int crcversion, string crcpath, string checkpath)
        {
            var crcfile = new FileInfo(crcpath);
            var di1 = new DirectoryInfo(checkpath);
            foreach (var fi1 in di1.GetFiles())
            {
                if (!fi1.Name.Equals(crcfile.Name))
                {
                    var found = false;

                    // lookup the file entry in the crc.xml.
                    var xmldata = Encoding.UTF8.GetString(File.ReadAllBytes(crcpath));
                    var xml = XElement.Parse(xmldata);
                    if (crcversion > 2)
                    {
                        foreach (var fileElement in xml.Elements("File"))
                        {
                            var nameAttribute = fileElement.Attribute("Name");
                            var name = nameAttribute?.Value ?? "no value";
                            if (name.Equals(fi1.Name))
                            {
                                found = true;
                            }
                        }
                    }
                    else
                    {
                        // TODO: Iterate through every entry in the kom v2 crc.xml file.
                    }

                    if (!found)
                    {
                        foreach (var plugin in Komplugins)
                        {
                            if (crcversion == plugin.SupportedKOMVersion)
                            {
                                plugin.UpdateCRC(crcpath, checkpath);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Flushes the Stream. Does nothing really.
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// Reads specific amount of data from the Stream.
        /// Does nothing really.
        /// </summary>
        /// <param name="buffer">The buffer to read into.</param>
        /// <param name="offset">The offset to read into the buffer.</param>
        /// <param name="count">The amount to read into the buffer.</param>
        /// <returns>The amount read into the buffer.</returns>
        public override int Read(byte[] buffer, int offset, int count) => 0;

        /// <summary>
        /// The amount to seek to. Always at 0 as Seeks are not supported.
        /// </summary>
        /// <param name="offset">The offset to seek to.</param>
        /// <param name="origin">The origin option.</param>
        /// <returns>The new position.</returns>
        public override long Seek(long offset, SeekOrigin origin) => 0;

        /// <summary>
        /// Sets the length of the Stream.
        /// Does nothing really.
        /// </summary>
        /// <param name="value">The new length of the stream.</param>
        public override void SetLength(long value)
        {
        }

        /// <summary>
        /// Writes specific amount of data to the Stream.
        /// Does nothing really.
        /// </summary>
        /// <param name="buffer">The buffer to write.</param>
        /// <param name="offset">The offset to write to.</param>
        /// <param name="count">The amount from buffer to write.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
        }

        /// <summary>
        /// Closes the Stream.
        /// </summary>
        public override void Close()
            => base.Close();

        /// <summary>
        /// Gets the crc.xml file version.
        /// </summary>
        /// <param name="xmldata">The data in the crc.xml file.</param>
        /// <returns>The version of the crc.xml file.</returns>
        internal static int GetCRCVersion(string xmldata)
        {
            var xml = XElement.Parse(xmldata);
            if (xml.Element("File") != null)
            {
                // version 3 or 4.
                foreach (var fileElement in xml.Elements("File"))
                {
                    var mappedIDAttribute = fileElement.Attribute("MappedID");
                    return mappedIDAttribute != null ? 4 : 3;
                }
            }
            else
            {
                return 2;
            }

            return 0;
        }

        /// <summary>
        /// Disposes the Stream.
        /// </summary>
        /// <param name="disposing">determines if we should dispose native resources (if any at all).</param>
        protected override void Dispose(bool disposing)
            => base.Dispose(disposing);
    }
}
