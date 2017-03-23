/*
	Alg3.hpp
*/

#include <Windows.h>
#include <string>

namespace Alg3
{
	inline int Unpacker(std::string FileName)
	{
		EManager::Decrypt_File(FileName);
		ZManager::DecompressFile(FileName);
		return 0;
	}

	inline int Packer(std::string FileName)
	{
		ZManager::CompressFile(FileName);
		EManager::Encrypt_File(FileName);
		return 0;
	}
};
