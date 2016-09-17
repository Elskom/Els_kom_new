/*
	XMLWriter.h
*/

#include <string>

int XML_WriteHeader();
int XML_AppendFile(std::string Name, int Size, int CompressedSize, std::string Checksum, std::string FileTime, int alg);
int XML_WriteEnding();
