/*
	Alg3.cpp
*/

#ifdef __CPLUSPLUS
extern "C" {
#endif

#include <Windows.h>
#include <string>
//TODO: Add Crypto++ to this.

char* Alg3_Unpacker(std::string FileName, char* FileData, std::string DestPath) {
	//TODO: Add Actual Unpacking here with Crypto++.
	return NULL;
}

char* Alg3_Packer(char* FileData) {
	//TODO: Add Actual Packing here with Crypto++.
	return NULL;
}

char* Alg3_Packer_s(char* FileData) {
	return FileData;
}

#ifdef __CPLUSPLUS
}
#endif
