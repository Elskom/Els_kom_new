namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Unpacker Class for Els_kom that unpacks KOM files.
    /// </summary>
    public class Unpacker
    {
        /// <summary>
        /// Makes KOM V3 Entries for unpacking.
        /// </summary>
        private System.Tuple<System.Collections.Generic.List<EntryVer3>, int> Make_entries_v3(string xmldata, int entry_count)
        {
            System.Collections.Generic.List<EntryVer3> entries = new System.Collections.Generic.List<EntryVer3>();
            int relative_offseterr = 0;
            var xml = System.Xml.Linq.XElement.Parse(xmldata);
            foreach (var fileElement in xml.Elements("File"))
            {
                var nameAttribute = fileElement.Attribute("Name");
                var name = nameAttribute?.Value ?? "no value";
                var sizeAttribute = fileElement.Attribute("Size");
                var size = sizeAttribute == null ? -1 : System.Convert.ToInt32(sizeAttribute.Value);
                var CompressedSizeAttribute = fileElement.Attribute("CompressedSize");
                var CompressedSize = CompressedSizeAttribute == null ? -1 : System.Convert.ToInt32(CompressedSizeAttribute.Value);
                var FileTimeAttribute = fileElement.Attribute("FileTime");
                var FileTime = FileTimeAttribute == null ? -1 : System.Convert.ToInt32(FileTimeAttribute.Value);
                var AlgorithmAttribute = fileElement.Attribute("Algorithm");
                var Algorithm = AlgorithmAttribute == null ? -1 : System.Convert.ToInt32(AlgorithmAttribute.Value);
                var entry = new EntryVer3(name, size, CompressedSize, relative_offseterr, FileTime, Algorithm);
                relative_offseterr += entry.compressed_size;
                entries.Add(entry);
                fileElement.Remove();
                relative_offseterr += entry.compressed_size;
            }
            return System.Tuple.Create(entries, relative_offseterr);
        }

        /// <summary>
        /// Unpacks V4 KOM Files.
        /// </summary>
        public static void Kom_v4_unpack(string in_path, string out_path)
        {
            // not implemented yet due to lack of information on v4 koms.
        }

        /// <summary>
        /// Unpacks V3 KOM Files.
        /// </summary>
        public static void Kom_v3_unpack(string in_path, string out_path)
        {
            // not fully implemented yet due to crash in the actual testing code.
            /*
            if you dont understand why we dont do the header check here,
            the check is actually in the KOM Manager that invokes these packers / unpackers anyway.
            and would be redundant to have here as well so they are left out.
            */
            int offset = 0;
            System.IO.FileStream reader = new System.IO.FileStream(in_path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] entry_count_buffer = new byte[System.Convert.ToInt32(KOM_DATA.KOM_ENTRY_COUNT_SIZE)];
            byte[] file_timer_buffer = new byte[System.Convert.ToInt32(KOM_DATA.KOM_FILE_TIMER_SIZE)];
            byte[] xml_size_file_buffer = new byte[System.Convert.ToInt32(KOM_DATA.KOM_XML_SIZE_FILE_SIZE)];
            offset += 52;
            reader.Read(entry_count_buffer, offset, System.Convert.ToInt32(KOM_DATA.KOM_ENTRY_COUNT_SIZE));
            MessageManager.ShowInfo(System.Convert.ToInt32(entry_count_buffer).ToString(), "Debug!");
            offset += 12;
            reader.Read(file_timer_buffer, offset, System.Convert.ToInt32(KOM_DATA.KOM_FILE_TIMER_SIZE));
            MessageManager.ShowInfo(System.Convert.ToInt32(file_timer_buffer).ToString(), "Debug!");
            offset += 4;
            reader.Read(xml_size_file_buffer, offset, System.Convert.ToInt32(KOM_DATA.KOM_XML_SIZE_FILE_SIZE));
            MessageManager.ShowInfo(System.Convert.ToInt32(xml_size_file_buffer).ToString(), "Debug!");
            // clean up the reader to prevent leaks.
            reader.Close();
            reader.Dispose();
        }
    }
}
