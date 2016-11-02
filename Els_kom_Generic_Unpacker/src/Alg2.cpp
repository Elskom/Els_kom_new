/*
	Alg2.cpp
*/

#ifdef __CPLUSPLUS
extern "C" {
#endif

#include <Windows.h>
#include <string>
#include "EManager.h"  //Impliments Crypto++ Class for use with Encryption/Decryption.
#include "ZManager.h"  //Impliments zlib class for Compression and Decompression.

int Alg2_Unpacker(std::string FileName) {
	DecompressFile(FileName);
	Decrypt_File(FileName);
	return 0;
}

int Alg2_Packer(std::string FileName) {
	Encrypt_File(FileName);
	CompressFile(FileName);
	return 0;
}

#ifdef __CPLUSPLUS
}
#endif
