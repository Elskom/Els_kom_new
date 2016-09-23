/*
	Alg0.cpp
*/

#ifdef __CPLUSPLUS
extern "C" {
#endif

#include <Windows.h>
#include <string>
#include "ZManager.h"

int Alg0_Unpacker(std::string FileName) {
	DecompressFile(FileName);
	return 0;
}

int Alg0_Packer(std::string FileName) {
	CompressFile(FileName);
	return 0;
}

#ifdef __CPLUSPLUS
}
#endif
