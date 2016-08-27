/*
	Alg3.cpp
*/

#ifdef __CPLUSPLUS
extern "C" {
#endif

#include <Windows.h>
#include <string>
//TODO: Add Crypto++ to this.

unsigned char* Alg3_Unpacker(std::string FileName, unsigned char* FileData, std::string DestPath) {
	//TODO: Add Actual Unpacking here with Crypto++.
	return NULL;
}

unsigned char* Alg3_Packer(unsigned char* FileData) {
	//TODO: Add Actual Packing here with Crypto++.
	return NULL;
}

unsigned char* Alg3_Packer_s(unsigned char* FileData) {
	return FileData;
}

#ifdef __CPLUSPLUS
}
#endif
