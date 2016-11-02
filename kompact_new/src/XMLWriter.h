/*
	XMLWriter.h
*/

/*
	To Avoid Compile Issues later.
*/
#ifndef HAVE_XMLWRITER_H_
	#define HAVE_XMLWRITER_H_
	#include <string>
	class XMLWriter {
		public:
			int WriteHeader();
			int AppendFile(std::string Name, int Size, int CompressedSize, std::string Checksum, std::string FileTime, int alg);
			int WriteEnding();
	};
#else
#endif
