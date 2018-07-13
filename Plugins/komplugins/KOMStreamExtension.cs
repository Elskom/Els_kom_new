// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

#if KOMV2
namespace komv2_plugin
#elif KOMV3
namespace komv3_plugin
#elif KOMV4
namespace komv4_plugin
#endif
{
    internal static class KOMStreamExtension
    {
#if KOMV2
        internal static void ReadCrc(this Els_kom_Core.Classes.KOMStream kOMStream, string crcfile, out byte[] crcdata, ref int entry_count, ref int crc_size)
        {
            crcdata = System.IO.File.ReadAllBytes(crcfile);
            System.IO.File.Delete(crcfile);
            entry_count++;
            crc_size = crcdata.Length;
        }

        internal static System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer> Make_entries_v2(this Els_kom_Core.Classes.KOMStream kOMStream, int entrycount, System.IO.BinaryReader reader)
        {
            System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer> entries = new System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer>();
            for (int i = 0; i < entrycount; i++)
            {
                kOMStream.ReadInFile(reader, out string key, 60, System.Text.Encoding.ASCII);
                kOMStream.ReadInFile(reader, out int originalsize);
                kOMStream.ReadInFile(reader, out int compressedSize);
                kOMStream.ReadInFile(reader, out int offset);
                var entry = new Els_kom_Core.Classes.EntryVer(kOMStream.GetSafeString(key), originalsize, compressedSize, offset);
                entries.Add(entry);
            }
            return entries;
        }

        internal static string GetSafeString(this Els_kom_Core.Classes.KOMStream kOMStream, string source)
        {
            if (source.Contains(new string(char.MinValue, 1)))
            {
                return source.Substring(0, source.IndexOf(char.MinValue));
            }
            return source;
        }

        internal static bool ReadInFile(this Els_kom_Core.Classes.KOMStream kOMStream, System.IO.BinaryReader binaryReader, out string destString, int length, System.Text.Encoding encoding)
        {
            if (binaryReader == null)
            {
                throw new System.ArgumentNullException(nameof(binaryReader));
            }
            if (encoding == null)
            {
                throw new System.ArgumentNullException(nameof(encoding));
            }
            long position = binaryReader.BaseStream.Position;
            byte[] readBytes = binaryReader.ReadBytes(length);
            if ((binaryReader.BaseStream.Position - position) == length)
            {
                destString = encoding.GetString(readBytes);
                return true;
            }
            destString = null;
            return false;
        }

        internal static bool ReadInFile(this Els_kom_Core.Classes.KOMStream kOMStream, System.IO.BinaryReader binaryReader, out int destInt)
        {
            if (binaryReader == null)
            {
                throw new System.ArgumentNullException(nameof(binaryReader));
            }
            long position = binaryReader.BaseStream.Position;
            int readInt = binaryReader.ReadInt32();
            if ((binaryReader.BaseStream.Position - position) == sizeof(int))
            {
                destInt = readInt;
                return true;
            }
            destInt = int.MinValue;
            return false;
        }
#elif KOMV3
        internal static System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer> Make_entries_v3(this Els_kom_Core.Classes.KOMStream kOMStream, string xmldata, int entry_count)
        {
            System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer> entries = new System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer>();
            var xml = System.Xml.Linq.XElement.Parse(xmldata);
            foreach (var fileElement in xml.Elements("File"))
            {
                var nameAttribute = fileElement.Attribute("Name");
                var name = nameAttribute?.Value ?? "no value";
                var sizeAttribute = fileElement.Attribute("Size");
                var size = sizeAttribute == null ? -1 : System.Convert.ToInt32(sizeAttribute.Value);
                var CompressedSizeAttribute = fileElement.Attribute("CompressedSize");
                var CompressedSize = CompressedSizeAttribute == null ? -1 : System.Convert.ToInt32(CompressedSizeAttribute.Value);
                var ChecksumAttribute = fileElement.Attribute("Checksum");
                var Checksum = ChecksumAttribute == null ? -1 : int.Parse(ChecksumAttribute.Value, System.Globalization.NumberStyles.HexNumber);
                var FileTimeAttribute = fileElement.Attribute("FileTime");
                var FileTime = FileTimeAttribute == null ? -1 : int.Parse(FileTimeAttribute.Value, System.Globalization.NumberStyles.HexNumber);
                var AlgorithmAttribute = fileElement.Attribute("Algorithm");
                var Algorithm = AlgorithmAttribute == null ? -1 : System.Convert.ToInt32(AlgorithmAttribute.Value);
                var entry = new Els_kom_Core.Classes.EntryVer(name, size, CompressedSize, Checksum, FileTime, Algorithm);
                entries.Add(entry);
            }
            return entries;
        }
#elif KOMV4
        private static System.Collections.Generic.Dictionary<int, int> KeyMap { get; set; } = new System.Collections.Generic.Dictionary<int, int>();

        private static void LoadKeyMap()
        {
            try
            {
                System.IO.FileStream KeyMapfs = System.IO.File.OpenRead(System.Windows.Forms.Application.StartupPath + "\\plugins\\komKeyMap.dms");
                System.IO.BinaryReader KeyMapreader = new System.IO.BinaryReader(KeyMapfs, System.Text.Encoding.ASCII);
                // TODO: Read KeyMap data properly.
                for (long i = 0; i < KeyMapreader.BaseStream.Length;)
                {
                    int key = KeyMapreader.ReadInt32();
                    int value = KeyMapreader.ReadInt32();
                    try
                    {
                        KeyMap.Add(key, value);
                    }
                    catch (System.ArgumentException)
                    {
                    }
                    i = KeyMapreader.BaseStream.Position;
                }
                KeyMapreader.Dispose();
            }
            catch (System.IO.FileNotFoundException)
            {
                // keymap file not found.
            }
        }

        internal static void DecryptCRCXml(this Els_kom_Core.Classes.KOMStream kOMStream, int key, ref byte[] data, int length, System.Text.Encoding encoding)
        {
            // Load the KOM V4 keymap file.
            LoadKeyMap();
            if (!KeyMap.ContainsKey(key))
                return;

            string keyStr = KeyMap[key].ToString();
            string sha1Key = System.BitConverter.ToString(new System.Security.Cryptography.SHA1CryptoServiceProvider().ComputeHash(encoding.GetBytes(keyStr))).Replace("-", "");

            Els_kom_Core.Classes.BlowFish blowfish = new Els_kom_Core.Classes.BlowFish(sha1Key);
            data = blowfish.Decrypt(data, System.Security.Cryptography.CipherMode.ECB);
            blowfish.Dispose();
        }

        internal static System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer> Make_entries_v4(this Els_kom_Core.Classes.KOMStream kOMStream, string xmldata, int entry_count)
        {
            System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer> entries = new System.Collections.Generic.List<Els_kom_Core.Classes.EntryVer>();
            var xml = System.Xml.Linq.XElement.Parse(xmldata);
            foreach (var fileElement in xml.Elements("File"))
            {
                var nameAttribute = fileElement.Attribute("Name");
                var name = nameAttribute?.Value ?? "no value";
                var sizeAttribute = fileElement.Attribute("Size");
                var size = sizeAttribute == null ? -1 : System.Convert.ToInt32(sizeAttribute.Value);
                var CompressedSizeAttribute = fileElement.Attribute("CompressedSize");
                var CompressedSize = CompressedSizeAttribute == null ? -1 : System.Convert.ToInt32(CompressedSizeAttribute.Value);
                var ChecksumAttribute = fileElement.Attribute("Checksum");
                var Checksum = ChecksumAttribute == null ? -1 : int.Parse(ChecksumAttribute.Value, System.Globalization.NumberStyles.HexNumber);
                var FileTimeAttribute = fileElement.Attribute("FileTime");
                var FileTime = FileTimeAttribute == null ? -1 : int.Parse(FileTimeAttribute.Value, System.Globalization.NumberStyles.HexNumber);
                var AlgorithmAttribute = fileElement.Attribute("Algorithm");
                var Algorithm = AlgorithmAttribute == null ? -1 : System.Convert.ToInt32(AlgorithmAttribute.Value);
                // on v4 at least on Elsword there is now an MappedID attribute.
                // this is even more of an reason to store some cache
                // file for not only kom v3 for the algorithm 2 & 3
                // files to be able to get repacked to those
                // algorithm’s but also to store these unique
                // map id’s to this version of kom.
                var MappedIDAttribute = fileElement.Attribute("MappedID");
                var MappedID = MappedIDAttribute?.Value ?? "no value";
                var entry = new Els_kom_Core.Classes.EntryVer(name, size, CompressedSize, Checksum, FileTime, Algorithm, MappedID);
                entries.Add(entry);
            }
            return entries;
        }
#endif
    }
}
