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
from xml.dom.minidom import parse

class Entry_ver3(object):
    def __init__(self, name, uncompressed_size, compressed_size, relative_offset, file_time, algorithm):
        self.__name = name
        self.__uncompressed_size = uncompressed_size
        self.__compressed_size = compressed_size
        self.__relative_offset = relative_offset
        self.__file_time = file_time
        self.__algorithm = algorithm

    def get_name(self):
        return self.__name

    def get_uncompressed_size(self):
        return self.__uncompressed_size

    def get_compressed_size(self):
        return self.__compressed_size

    def get_relative_offset(self):
        return self.__relative_offset

    def get_file_time(self):
        return self.__file_time
        
    def get_algorithm(self):
        return self.__algorithm

    name = property(get_name)

    uncompressed_size = property(get_uncompressed_size)

    compressed_size = property(get_compressed_size)

    relative_offset = property(get_relative_offset)

    file_time = property(get_file_time)
    
    algorithm = property(get_algorithm)


def main(argv):
    if len(argv) < 1:
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

    if os.path.isfile(in_path) == False:
        sys.exit(2)
        
    if os.path.exists(out_path) == True:
        if os.path.isdir(out_path) == False:
            sys.exit(2)
    else:
        os.makedirs(out_path)

    file_object = None
    file_data = None

    try:
        file_object = open(in_path, 'rb')

        if file_object is not None:
            file_data = file_object.read()
    finally:
        if file_object is not None:
            file_object.close()

            file_object = None

    offset = 0

    version = struct.unpack_from('<26s26x', file_data, offset)[0]

    offset += 52

    entry_count = struct.unpack_from('<I4x', file_data, offset)[0]

    offset += 12

    file_timer = struct.unpack_from('<I', file_data, offset)[0]

    offset += 4

    xml_size_file = struct.unpack_from('<I', file_data, offset)[0]

    offset += 4

    theunpack_offset = offset + xml_size_file

    crc_creater = open(in_path, 'rb')

    crc_reader = open("crc.xml", 'w')

    crc_creater.seek(offset)

    crc_reader.write(crc_creater.read(xml_size_file))

    crc_reader.close()

    crc_reader = open("crc.xml", 'r')

    crc_er = parse(crc_reader)

    crc_reader.close()

    entries = []

    relative_offseterr = 0

    access_time = 0

    for x in xrange(entry_count):
        entry = Entry_ver3(crc_er.firstChild.firstChild.getAttribute("Name"), crc_er.firstChild.firstChild.getAttribute("Size"), int(crc_er.firstChild.firstChild.getAttribute("CompressedSize")), int(relative_offseterr), int(crc_er.firstChild.firstChild.getAttribute("FileTime"),16), int(crc_er.firstChild.firstChild.getAttribute("Algorithm")))

        entries.append(entry)
   
        crc_er.firstChild.removeChild(crc_er.firstChild.firstChild)

        relative_offseterr += entry.compressed_size


    for entry in entries:
        entry_file_data = file_data[theunpack_offset + entry.relative_offset:theunpack_offset + entry.relative_offset + entry.compressed_size]

        if entry.algorithm == 0:
            entry_file_data = zlib.decompress(entry_file_data)

        try:
            if entry.algorithm == 0:
                file_object = open(out_path+'/'+entry.name, 'wb')
            else:
                file_object = open(out_path+'/'+entry.name + '.' + str(entry.uncompressed_size) + '.' + str(entry.algorithm) + ".___", 'wb')

            if file_object is not None:
                file_object.write(entry_file_data)
#                    os.utime(entry.name, (access_time, long(entry.file_time)))

        finally:
            if file_object is not None:
                file_object.close()

                file_object = None


    print("\nExtraction Complete.")




if __name__ == "__main__":
    main(sys.argv[1:])
