/*
	packerhandler.h
*/

/*
	To Avoid Compile Issues later.
*/
#ifndef HAVE_PACKERHANDLER_H_
	#define HAVE_PACKERHANDLER_H_
	#include <string>
	bool EndsWith(const std::string& a, const std::string& b);
	void TempKOMFileWriter(std::string FileName, int SizeOfData);
	int FolderIterator(std::string path, std::string FileName);
#else
#endif
