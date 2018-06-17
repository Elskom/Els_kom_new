// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.
/* define if not defined already. */
#if !KOMV2
#define KOMV2
#endif

namespace komv2_plugin
{
    public class Komv2_plugin : Els_kom_Core.interfaces.IKomPlugin
    {
        public string PluginName => "KOM V2 Plugin";
        public string KOMHeaderString => "KOG GC TEAM MASSFILE V.0.2.";
        public int SupportedKOMVersion => 2;

        public void Pack(string in_path, string out_path, string KOMFileName)
        {
            bool use_XoR = false;
            if (System.IO.File.Exists(in_path + "\\XoRNeeded.dummy"))
            {
                use_XoR = true;
                System.IO.File.Delete(in_path + "\\XoRNeeded.dummy");
            }
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(System.IO.File.Create(out_path), System.Text.Encoding.ASCII);
            int entry_count = 0;
            int crc_size = 0;
            Els_kom_Core.Classes.KOMStream kOMStream = new Els_kom_Core.Classes.KOMStream();
            // convert the crc.xml file to the version for this plugin, if needed.
            kOMStream.ConvertCRC(2, in_path + System.IO.Path.DirectorySeparatorChar + "crc.xml");
            kOMStream.ReadCrc(in_path + "\\crc.xml", out byte[] crc_data, ref entry_count, ref crc_size);
            kOMStream.Dispose();
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(in_path);
            int offset = 0;
            System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer> entries = new System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer>();
            foreach (var fi in di.GetFiles())
            {
                entry_count++;
                byte[] file_data = System.IO.File.ReadAllBytes(in_path + "\\" + fi.Name);
                int originalsize = file_data.Length;
                if (use_XoR)
                {
                    byte[] xorkey = System.Text.Encoding.UTF8.GetBytes("\xa9\xa9\xa9\xa9\xa9\xa9\xa9\xa9\xa9\xa9");
                    Els_kom_Core.Classes.BlowFish.XorBlock(ref file_data, xorkey);
                }
                byte[] compressedData;
                try
                {
                    Els_kom_Core.Classes.ZlibHelper.CompressData(file_data, out compressedData);
                }
                catch (Els_kom_Core.Classes.Zlib.ZStreamException)
                {
                    throw new Els_kom_Core.Classes.PackingError("failed with zlib compression of entries.");
                }
                int compressedSize = compressedData.Length;
                offset += compressedSize;
                entries.Add(new Els_kom_Core.Classes.EntryVer(compressedData, fi.Name, originalsize, compressedSize, offset));
            }
            if (use_XoR)
            {
                byte[] xorkey = System.Text.Encoding.UTF8.GetBytes("\xa9\xa9\xa9\xa9\xa9\xa9\xa9\xa9\xa9\xa9");
                Els_kom_Core.Classes.BlowFish.XorBlock(ref crc_data, xorkey);
            }
            byte[] compressedcrcData;
            try
            {
                Els_kom_Core.Classes.ZlibHelper.CompressData(crc_data, out compressedcrcData);
            }
            catch (Els_kom_Core.Classes.Zlib.ZStreamException)
            {
                throw new Els_kom_Core.Classes.PackingError("failed with zlib compression of crc.xml.");
            }
            int compressedcrc = compressedcrcData.Length;
            offset += compressedcrc;
            entries.Add(new Els_kom_Core.Classes.EntryVer(compressedcrcData, "crc.xml", crc_size, compressedcrc, offset));
            writer.Write(KOMHeaderString.ToCharArray(), 0, KOMHeaderString.Length);
            writer.BaseStream.Position = 52;
            writer.Write(entry_count);
            writer.Write(1);
            foreach (var entry in entries)
            {
                writer.Write(entry.name.ToCharArray(), 0, entry.name.Length);
                int seek_amount = 60 - entry.name.Length;
                writer.BaseStream.Position += seek_amount;
                writer.Write(entry.uncompressed_size);
                writer.Write(entry.compressed_size);
                writer.Write(entry.relative_offset);
            }
            foreach (var entry in entries)
            {
                writer.Write(entry.entrydata, 0, entry.compressed_size);
            }
            writer.Dispose();
        }

        public void Unpack(string in_path, string out_path, string KOMFileName)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(System.IO.File.OpenRead(in_path), System.Text.Encoding.ASCII);
            reader.BaseStream.Position = 52;
            Els_kom_Core.Classes.KOMStream kOMStream = new Els_kom_Core.Classes.KOMStream();
            kOMStream.ReadInFile(reader, out int entry_count);
            // without this dummy read the entry instances would not get the correct
            // data leading to an crash when tring to make an file with the entry name in the output path.
            kOMStream.ReadInFile(reader, out int size);
            System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer> entries = kOMStream.Make_entries_v2(entry_count, reader);
            foreach (var entry in entries)
            {
                // we iterate through every entry here and unpack the data.
                kOMStream.WriteOutput(reader, out_path, entry, SupportedKOMVersion, string.Empty);
            }
            kOMStream.Dispose();
            reader.Dispose();
        }

        public void Delete(string in_path, bool folder)
        {
            if (folder)
            {
                // delete kom folder data.
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(in_path);
                foreach (var fi in di.GetFiles())
                {
                    fi.Delete();
                }
                di.Delete();
            }
            else
            {
                // delete kom file.
                System.IO.FileInfo fi = new System.IO.FileInfo(in_path);
                fi.Delete();
            }
        }
    }
}
