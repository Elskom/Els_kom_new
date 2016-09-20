/*
	XMLWriter.h
*/

#include <string>
class XMLWriter {
	public:
		int WriteHeader();
		int AppendFile(std::string Name, int Size, int CompressedSize, std::string Checksum, std::string FileTime, int alg);
		int WriteEnding();
};
