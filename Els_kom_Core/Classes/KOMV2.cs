namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class for V3 KOM support.
    /// </summary>
    public class EntryVer2
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
        /// Sets Data for entries.
        /// </summary>
        public EntryVer2(string _name, int _uncompressed_size, int _compressed_size, int _relative_offset)
        {
            name = _name;
            uncompressed_size = _uncompressed_size;
            compressed_size = _compressed_size;
            relative_offset = _relative_offset;
        }
    }
}
