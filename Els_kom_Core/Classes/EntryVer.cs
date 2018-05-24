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
        /// Entry File name.
        /// </summary>
        public string name;
        /// <summary>
        /// Entry unpacked size.
        /// </summary>
        public int uncompressed_size;
        /// <summary>
        /// Entry Compressed Size.
        /// </summary>
        public int compressed_size;
        /// <summary>
        /// Use in KOM V2 only.
        /// </summary>
        public int relative_offset;
        /// <summary>
        /// For packing.
        /// </summary>
        public byte[] entrydata;
        // KOM V3 Members.
        /// <summary>
        /// Entry Checksum.
        /// </summary>
        public int checksum;
        /// <summary>
        /// Entry File time.
        /// </summary>
        public int file_time;
        /// <summary>
        /// Entry Algorithm.
        /// </summary>
        public int algorithm;
        // KOM V4 Members.
        /// <summary>
        /// Entry Mapped ID.
        /// </summary>
        public string MappedID;
        /// <summary>
        /// For internal crap.
        /// </summary>
        internal int version;

        /// <summary>
        /// Constructor for unpacking files from KOM V2.
        /// </summary>
        public EntryVer(string _name, int _uncompressed_size, int _compressed_size, int _relative_offset)
        {
            version = 2;
            name = _name;
            uncompressed_size = _uncompressed_size;
            compressed_size = _compressed_size;
            relative_offset = _relative_offset;
        }

        /// <summary>
        /// Constructor for packing files to KOM V2.
        /// </summary>
        public EntryVer(byte[] _entrydata, string _name, int _uncompressed_size, int _compressed_size, int _relative_offset)
        {
            version = 2;
            entrydata = _entrydata;
            name = _name;
            uncompressed_size = _uncompressed_size;
            compressed_size = _compressed_size;
            relative_offset = _relative_offset;
        }

        /// <summary>
        /// Constructor for unpacking files from KOM V3.
        /// </summary>
        public EntryVer(string _name, int _uncompressed_size, int _compressed_size, int _checksum, int _file_time, int _algorithm)
        {
            version = 3;
            name = _name;
            uncompressed_size = _uncompressed_size;
            compressed_size = _compressed_size;
            checksum = _checksum;
            file_time = _file_time;
            algorithm = _algorithm;
        }

        /// <summary>
        /// Constructor for packing files to KOM V3.
        /// </summary>
        public EntryVer(byte[] _entrydata, string _name, int _uncompressed_size, int _compressed_size, int _checksum, int _file_time, int _algorithm)
        {
            version = 3;
            entrydata = _entrydata;
            name = _name;
            uncompressed_size = _uncompressed_size;
            compressed_size = _compressed_size;
            checksum = _checksum;
            file_time = _file_time;
            algorithm = _algorithm;
        }

        /// <summary>
        /// Constructor for unpacking files from KOM V4.
        /// </summary>
        public EntryVer(string _name, int _uncompressed_size, int _compressed_size, int _checksum, int _file_time, int _algorithm, string _MappedID)
        {
            version = 4;
            name = _name;
            uncompressed_size = _uncompressed_size;
            compressed_size = _compressed_size;
            checksum = _checksum;
            file_time = _file_time;
            algorithm = _algorithm;
            MappedID = _MappedID;
        }

        /// <summary>
        /// Constructor for packing files to KOM V4.
        /// </summary>
        public EntryVer(byte[] _entrydata, string _name, int _uncompressed_size, int _compressed_size, int _checksum, int _file_time, int _algorithm, string _MappedID)
        {
            version = 4;
            entrydata = _entrydata;
            name = _name;
            uncompressed_size = _uncompressed_size;
            compressed_size = _compressed_size;
            checksum = _checksum;
            file_time = _file_time;
            algorithm = _algorithm;
            MappedID = _MappedID;
        }
    }
}
