namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class for V3 KOM support.
    /// </summary>
    public class EntryVer3
    {
        /// <summary>
        /// Entry File name.
        /// </summary>
        public string name;
        /// <summary>
        /// Entry original file size.
        /// </summary>
        public int uncompressed_size;
        /// <summary>
        /// Entry Compressed Size.
        /// </summary>
        public int compressed_size;
        /// <summary>
        /// Entry Relative File offset.
        /// </summary>
        public int relative_offset;
        /// <summary>
        /// Entry file time.
        /// </summary>
        public int file_time;
        /// <summary>
        /// Entry Compression Algorithm.
        /// </summary>
        public int algorithm;

        /// <summary>
        /// Sets Data for entries.
        /// </summary>
        public EntryVer3(string _name, int _uncompressed_size, int _compressed_size, int _relative_offset, int _file_time, int _algorithm)
        {
            name = _name;
            uncompressed_size = _uncompressed_size;
            compressed_size = _compressed_size;
            relative_offset = _relative_offset;
            file_time = _file_time;
            algorithm = _algorithm;
        }
    }
}
