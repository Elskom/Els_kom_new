#!/usr/bin/python

# Copyright (c) 2009 Peter S. Stevens
# 
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
# 
# The above copyright notice and this permission notice shall be included in
# all copies or substantial portions of the Software.
# 
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# THE SOFTWARE.


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
    kom_compressed_file_data = ''

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
                    if file_name.endswith('.___'):
                        file_name, ext = os.path.splitext(file_name)
                        file_name, ext = os.path.splitext(file_name)

                        algorithm = int(ext[1:])
                        file_name, ext = os.path.splitext(file_name)
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
                    file_data_crc32 = zlib.adler32(compressed_file_data)  & 0xffffffffL # work around for issue 1202

                    crc_file_info_file_item = crc.createElement("File")
                    crc_file_info_file_item.setAttribute("Name", file_name)
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
            kom_header = "KOG GC TEAM MASSFILE V.0.3."
            kom_header += struct.pack('<25x')
            kom_header += struct.pack('<2I', kom_file_entries, 1)
            kom_header += struct.pack('<3I', 0, zlib.adler32(crc_file_data) & 0xFFFFFFFFL, len(crc_file_data))
            file_object.write(kom_header)
            file_object.write(crc_file_data)
            file_object.write(kom_compressed_file_data)

        finally:
            if file_object is not None:
                file_object.close()

                file_object = None


        print("\nFile created.")



if __name__ == "__main__":
    main(sys.argv[1:])