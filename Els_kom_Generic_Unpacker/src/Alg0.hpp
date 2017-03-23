/*
	Alg0.hpp
*/

#include <Windows.h>
#include <string>

namespace Alg0
{
	inline int Unpacker(std::string FileName)
	{
		ZManager::DecompressFile(FileName);
		return 0;
	}

	inline int Packer(std::string FileName)
	{
		ZManager::CompressFile(FileName);
		return 0;
	}
};
