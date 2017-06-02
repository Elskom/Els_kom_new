/*
	ZManager.hpp
*/

/*
	This file is for Managing the use of zlib functions For The Algorithms.
	This file needs to have methods of doing memory
	compressions / decompressions without temporary files
	and return the compressed data crc32's and other things
	required for the kom format when packing if possible.
*/

//TODO: Figure out how to use all of zlib for this.

#include "Zlib.hpp"
#include <string>

namespace ZManager
{
	inline int CompressFile(std::string FileName)
	{
		return 0;  // Sadly Not Implemented yet.
	}

	inline int DecompressFile(std::string FileName)
	{
		return 0;  // Sadly Not Implemented yet.
	}

	inline int CalculateCRC32(std::string FileName)
	{
		return 0;  // Sadly Not Implemented yet.
	}
};
