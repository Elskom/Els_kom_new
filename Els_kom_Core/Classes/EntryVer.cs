// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// This Class holds all the KOM entry Information.
    /// </summary>
    public class EntryVer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntryVer"/> class for unpacking files from KOM V2.
        /// </summary>
        /// <param name="name">The Entry file name.</param>
        /// <param name="uncompressedsize">The original file size.</param>
        /// <param name="compressedsize">The current, compressed file size.</param>
        /// <param name="relativeoffset">The relative offset of the entry file.</param>
        public EntryVer(string name, int uncompressedsize, int compressedsize, int relativeoffset)
        {
            this.Version = 2;
            this.Name = name;
            this.UncompressedSize = uncompressedsize;
            this.CompressedSize = compressedsize;
            this.RelativeOffset = relativeoffset;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryVer"/> class for packing files to KOM V2.
        /// </summary>
        /// <param name="entrydata">The input file data that is to be compressed.</param>
        /// <param name="name">The Entry file name.</param>
        /// <param name="uncompressedsize">The current, file size.</param>
        /// <param name="compressedsize">The target, compressed file size.</param>
        /// <param name="relativeoffset">The relative offset of the entry file.</param>
        public EntryVer(byte[] entrydata, string name, int uncompressedsize, int compressedsize, int relativeoffset)
        {
            this.Version = 2;
            this.Entrydata = entrydata;
            this.Name = name;
            this.UncompressedSize = uncompressedsize;
            this.CompressedSize = compressedsize;
            this.RelativeOffset = relativeoffset;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryVer"/> class unpacking files from KOM V3.
        /// </summary>
        /// <param name="name">The Entry file name.</param>
        /// <param name="uncompressedsize">The original file size.</param>
        /// <param name="compressedsize">The current, compressed file size.</param>
        /// <param name="checksum">The input file crc32 checksum.</param>
        /// <param name="filetime">The input file time.</param>
        /// <param name="algorithm">The input file's compression algorithm.</param>
        public EntryVer(string name, int uncompressedsize, int compressedsize, int checksum, int filetime, int algorithm)
        {
            this.Version = 3;
            this.Name = name;
            this.UncompressedSize = uncompressedsize;
            this.CompressedSize = compressedsize;
            this.Checksum = checksum;
            this.File_time = filetime;
            this.Algorithm = algorithm;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryVer"/> class for packing files to KOM V3.
        /// </summary>
        /// <param name="entrydata">The input file data that is to be compressed.</param>
        /// <param name="name">The Entry file name.</param>
        /// <param name="uncompressedsize">The current, file size.</param>
        /// <param name="compressedsize">The target, compressed file size.</param>
        /// <param name="checksum">The input file crc32 checksum.</param>
        /// <param name="filetime">The input file time.</param>
        /// <param name="algorithm">The input file's compression algorithm.</param>
        public EntryVer(byte[] entrydata, string name, int uncompressed_size, int compressed_size, int checksum, int filetime, int algorithm)
        {
            this.Version = 3;
            this.Entrydata = entrydata;
            this.Name = name;
            this.UncompressedSize = uncompressedsize;
            this.CompressedSize = compressedsize;
            this.Checksum = checksum;
            this.FileTime = filetime;
            this.Algorithm = algorithm;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryVer"/> class for unpacking files from KOM V4.
        /// </summary>
        /// <param name="name">The Entry file name.</param>
        /// <param name="uncompressedsize">The original file size.</param>
        /// <param name="compressedsize">The current, compressed file size.</param>
        /// <param name="checksum">The input file crc32 checksum.</param>
        /// <param name="filetime">The input file time.</param>
        /// <param name="algorithm">The input file's compression algorithm.</param>
        /// <param name="mappedID">The input file's mapped id.</param>
        public EntryVer(string name, int uncompressedsize, int compressedsize, int checksum, int filetime, int algorithm, string mappedID)
        {
            this.Version = 4;
            this.Name = name;
            this.UncompressedSize = uncompressedsize;
            this.CompressedSize = compressedsize;
            this.Checksum = checksum;
            this.FileTime = filetime;
            this.Algorithm = algorithm;
            this.MappedID = mappedID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryVer"/> class for packing files to KOM V4.
        /// </summary>
        /// <param name="entrydata">The input file data that is to be compressed.</param>
        /// <param name="name">The Entry file name.</param>
        /// <param name="uncompressedsize">The current, file size.</param>
        /// <param name="compressedsize">The target, compressed file size.</param>
        /// <param name="checksum">The input file crc32 checksum.</param>
        /// <param name="filetime">The input file time.</param>
        /// <param name="algorithm">The input file's compression algorithm.</param>
        /// <param name="mappedID">The input file's mapped id.</param>
        public EntryVer(byte[] entrydata, string name, int uncompressedsize, int compressedsize, int checksum, int filetime, int algorithm, string mappedID)
        {
            this.Version = 4;
            this.Entrydata = entrydata;
            this.Name = name;
            this.UncompressedSize = uncompressedsize;
            this.CompressedSize = compressedsize;
            this.Checksum = checksum;
            this.FileTime = filetime;
            this.Algorithm = algorithm;
            this.MappedID = mappedID;
        }

        /// <summary>
        /// Gets the entry File name.
        /// </summary>
        /// <value>
        /// The entry File name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the entry unpacked size.
        /// </summary>
        /// <value>
        /// The entry unpacked size.
        /// </value>
        public int UncompressedSize { get; private set; }

        /// <summary>
        /// Gets the entry Compressed Size.
        /// </summary>
        /// <value>
        /// The entry Compressed Size.
        /// </value>
        public int CompressedSize { get; private set; }

        /// <summary>
        /// Gets the entry Relative offset.
        /// </summary>
        /// <value>
        /// The entry Relative offset.
        /// </value>
        public int RelativeOffset { get; private set; }

        /// <summary>
        /// Gets entry file data for packing.
        /// </summary>
        /// <value>
        /// Entry file data for packing.
        /// </value>
        public byte[] Entrydata { get; private set; }

        // KOM V3 Members.

        /// <summary>
        /// Gets entry Checksum.
        /// </summary>
        /// <value>
        /// Entry Checksum.
        /// </value>
        public int Checksum { get; private set; }

        /// <summary>
        /// Gets entry File time.
        /// </summary>
        /// <value>
        /// Entry File time.
        /// </value>
        public int FileTime { get; private set; }

        /// <summary>
        /// Gets entry Algorithm.
        /// </summary>
        /// <value>
        /// Entry Algorithm.
        /// </value>
        public int Algorithm { get; private set; }

        // KOM V4 Members.

        /// <summary>
        /// Gets entry Mapped ID.
        /// </summary>
        /// <value>
        /// Entry Mapped ID.
        /// </value>
        public string MappedID { get; private set; }

        /// <summary>
        /// Gets entry KOM Version number.
        /// </summary>
        internal int Version { get; private set; }
    }
}
