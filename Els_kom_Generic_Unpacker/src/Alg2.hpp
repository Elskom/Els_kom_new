/*
	Alg2.hpp
*/

#include <Windows.h>
#include <string>

namespace Alg2
{
	inline int Unpacker(std::string FileName)
	{
		ZManager::DecompressFile(FileName);
		EManager::Decrypt_File(FileName);
		return 0;
	}

	inline int Packer(std::string FileName)
	{
		EManager::Encrypt_File(FileName);
		ZManager::CompressFile(FileName);
		return 0;
	}
};
