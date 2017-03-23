/*
	KOMReader.hpp
*/

//#include "komextract_new.hpp"
#include <string>
bool KOM_FILE_READER_NOT_COMPLETE = true;

int KOMFileReader(std::string FileName, std::string DestPath);

/*
	This file is for Reading from the kom files and extract the crc.xml file just so we can write the files to a folder (the out path).
	NOTE: This is not complete as I do not know how to convert some python code. All of that code is Commented out below.
*/

/*
#komextract_new.py
import getopt
import os
import struct
import sys
import zlib
from xml.dom.minidom import parse

+class Entry_ver3(object):
+    def __init__(self, name, uncompressed_size, compressed_size, relative_offset, file_time, algorithm):
+        self.__name = name
+        self.__uncompressed_size = uncompressed_size
+        self.__compressed_size = compressed_size
+        self.__relative_offset = relative_offset
+        self.__file_time = file_time
+        self.__algorithm = algorithm
+    def get_name(self):
+        return self.__name
+    def get_uncompressed_size(self):
+        return self.__uncompressed_size
+    def get_compressed_size(self):
+        return self.__compressed_size
+    def get_relative_offset(self):
+        return self.__relative_offset
+    def get_file_time(self):
+        return self.__file_time
+    def get_algorithm(self):
+        return self.__algorithm
+    name = property(get_name)
+    uncompressed_size = property(get_uncompressed_size)
+    compressed_size = property(get_compressed_size)
+    relative_offset = property(get_relative_offset)
+    file_time = property(get_file_time)
+    algorithm = property(get_algorithm)
-def main(argv):
-    if len(argv) < 1:
-        sys.exit(2)
-    try:
-        options, arguments = getopt.getopt(argv, 'i:o:', ['in=', 'out='])
-    except getopt.GetoptError:
-        sys.exit(2)
-    in_path = None
-    out_path = None
-    for option, argument in options:
-        if option in ('i', '--in'):
-            in_path = argument
-        elif option in ('o', '--out'):
-            out_path = argument
-    if in_path is None or out_path is None:
-        sys.exit(2)
-    if os.path.isfile(in_path) == False:
-        sys.exit(2)
-    if os.path.exists(out_path) == True:
-        if os.path.isdir(out_path) == False:
-            sys.exit(2)
-    else:
-        os.makedirs(out_path)
-    file_object = None
-    file_data = None
-    try:
-        file_object = open(in_path, 'rb')
-        if file_object is not None:
-            file_data = file_object.read()
-    finally:
-        if file_object is not None:
-            file_object.close()
-            file_object = None
+    offset = 0
+    version = struct.unpack_from(b'<26s26x', file_data, offset)[0]
+    offset += 52
+    entry_count = struct.unpack_from(b'<I4x', file_data, offset)[0]
+    offset += 12
+    file_timer = struct.unpack_from(b'<I', file_data, offset)[0]
+    offset += 4
+    xml_size_file = struct.unpack_from(b'<I', file_data, offset)[0]
+    offset += 4
+    theunpack_offset = offset + xml_size_file
+    crc_creater = open(in_path, 'rb')
+    crc_reader = open("crc.xml", 'wb')
+    crc_creater.seek(offset)
+    crc_reader.write(crc_creater.read(xml_size_file))
+    crc_reader.close()
+    crc_reader = open("crc.xml", 'r')
+    crc_er = parse(crc_reader)
+    crc_reader.close()
+    entries = []
+    relative_offseterr = 0
+    access_time = 0
+    for x in range(entry_count):
+        entry = Entry_ver3(crc_er.firstChild.firstChild.getAttribute("Name"), crc_er.firstChild.firstChild.getAttribute("Size"), int(crc_er.firstChild.firstChild.getAttribute("CompressedSize")), int(relative_offseterr), int(crc_er.firstChild.firstChild.getAttribute("FileTime"),16), int(crc_er.firstChild.firstChild.getAttribute("Algorithm")))
+        entries.append(entry)
+        crc_er.firstChild.removeChild(crc_er.firstChild.firstChild)
+        relative_offseterr += entry.compressed_size
+    for entry in entries:
+        entry_file_data = file_data[theunpack_offset + entry.relative_offset:theunpack_offset + entry.relative_offset + entry.compressed_size]
-        if entry.algorithm == 0:
-            entry_file_data = zlib.decompress(entry_file_data)
-        try:
-            if entry.algorithm == 0:
-                file_object = open(out_path+'/'+entry.name, 'wb')
-            #if entry.algorithm == 3:
-                # Unpacking Algorithm 3 Not supported yet. Need Crypto++ C++ DLL wrapper for this.
-            #    raise NotImplementedError('Unpacking Algorithm 3 data Not Supported yet, Sorry.')
-            else:
-                file_object = open(out_path+'/'+entry.name + '.' + str(entry.uncompressed_size) + '.' + str(entry.algorithm), 'wb')
-            if file_object is not None:
-                file_object.write(entry_file_data)
-#                    os.utime(entry.name, (access_time, long(entry.file_time)))
-        finally:
-            if file_object is not None:
-                file_object.close()
-                file_object = None
-    print("Extraction Complete.")

-if __name__ == "__main__":
-    main(sys.argv[1:])

*/

/*
Note All lines that start with a + are lines I need, while all lines that start with a - are lines I do not need converted.
*/

int KOMFileReader(std::string FileName, std::string DestPath)
{
	//After All the Reading and extracting of Unprocessed files to the DestPath lets iterate through the files and finally process them.
	//TODO: Add File Iterator just like packer has after extracting the base data from the KOM Files.
	if (KOM_FILE_READER_NOT_COMPLETE == true)
	{
		print("Error: KOM File Reader is not finished yet.", true, false, false);
	}
	return 0;  //Not Complete yet.
}
