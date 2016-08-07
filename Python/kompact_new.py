#kompact_new.py
import getopt
import os
import struct
import sys
import zlib
from xml.dom.minidom import Document
import time

def main(argv):
    if len(argv) < 2:
        sys.exit(2)
    try:
        options, arguments = getopt.getopt(argv, 'i:o:', ['in=', 'out='])
    except getopt.GetoptError:
        sys.exit(2)
    in_path = None
    out_path = None
    for option, argument in options:
        if option in ('i', '--in'):
            in_path = argument
        elif option in ('o', '--out'):
            out_path = argument
    if in_path is None or out_path is None:
        sys.exit(2)
    if os.path.isdir(in_path) == False:
        sys.exit(2)
    crc = Document()
    crc_file_info = crc.createElement("Files")
    crc.appendChild(crc_file_info)
    kom_file_entries = 0
    kom_compressed_file_data = b''
    for file_name in os.listdir(in_path):
        file_path = os.path.join(in_path, file_name)
        if os.path.isfile(file_path):
            file_size = os.path.getsize(file_path)
            if file_size <= 0:
                continue
            try:
                file_object = open(file_path, 'rb')
                file_data = file_object.read()
            except IOError:
                pass
            else:
                file_name_new = file_name
                file_size = 0
                compressed_size = 0
                algorithm = 0
                try:
                    #if file_name_new.lower().endswith('.lua'):
                        # Packing lua's and txt files back to Algorithm 3 Not supported yet. Need Crypto++ C++ DLL wrapper for this.
                    #    raise NotImplementedError('Packing lau files back to Algorithm 3 is not Supported yet, Sorry.')
                    #if file_name_new.lower().endswith('.txt'):
                        # Packing lua's and txt files back to Algorithm 3 Not supported yet. Need Crypto++ C++ DLL wrapper for this.
                    #    raise NotImplementedError('Packing txt files back to Algorithm 3 is not Supported yet, Sorry.')
                    if file_name_new.endswith('.___'):
                        file_name_new, ext = os.path.splitext(file_name_new)
                        file_name_new, ext = os.path.splitext(file_name_new)
                        algorithm = int(ext[1:])
                        file_name_new, ext = os.path.splitext(file_name_new)
                        file_size = int(ext[1:])
                        compressed_file_data = file_data
                        compressed_size = len(compressed_file_data)
                    else:
                        compressed_file_data = zlib.compress(file_data)
                        file_size = len(file_data)
                        compressed_size = len(compressed_file_data)
                except zlib.error:
                    pass
                else:
                    kom_file_entries += 1
                    kom_compressed_file_data += compressed_file_data
                    file_data_crc32 = zlib.adler32(compressed_file_data)  & 0xffffffff # work around for issue 1202
                    crc_file_info_file_item = crc.createElement("File")
                    crc_file_info_file_item.setAttribute("Name", file_name_new)
                    crc_file_info_file_item.setAttribute("Size", str(file_size))
                    crc_file_info_file_item.setAttribute("CompressedSize", str(compressed_size))
                    crc_file_info_file_item.setAttribute("Checksum", "%08x" % file_data_crc32)
                    crc_file_info_file_item.setAttribute("FileTime", "%08x" % 0)
                    crc_file_info_file_item.setAttribute("Algorithm", str(algorithm))
                    crc_file_info.appendChild(crc_file_info_file_item)
            finally:
                if file_object is not None:
                    file_object.close()
                    file_object = None
    if kom_file_entries > 0 and len(kom_compressed_file_data) > 0:
        crc_file_data = crc.toxml()
        try:
            file_object = open(out_path, 'wb')
            kom_header = b"KOG GC TEAM MASSFILE V.0.3."
            kom_header += struct.pack(b'<25x')
            kom_header += struct.pack(b'<2I', kom_file_entries, 1)
            _size = int(len(crc_file_data))
            compressed_data = zlib.adler32(bytes(crc_file_data, 'utf-8'))
            kom_header += struct.pack(b'<3I', 0, compressed_data & 0xFFFFFFFF, _size)
            file_object.write(kom_header)
            file_object.write(bytes(crc_file_data, 'utf-8'))
            file_object.write(kom_compressed_file_data)
        finally:
            if file_object is not None:
                file_object.close()
                file_object = None
        print("File created.")
if __name__ == "__main__":
    main(sys.argv[1:])