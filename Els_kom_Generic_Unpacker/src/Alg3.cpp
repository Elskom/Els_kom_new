/*
	Alg3.cpp
*/

#ifdef __CPLUSPLUS
extern "C" {
#endif

#include <Windows.h>
#include <string>
#include "EManager.h"  //Impliments Crypto++ Class for use with Encryption/Decryption.
#include "ZManager.h"  //Impliments zlib class for Compression and Decompression.

int Alg3_Unpacker(std::string FileName) {
	Decrypt_File(FileName);
	DecompressFile(FileName);
	return 0;
}

int Alg3_Packer(std::string FileName) {
	CompressFile(FileName);
	Encrypt_File(FileName);
	return 0;
}

#ifdef __CPLUSPLUS
}
#endif
