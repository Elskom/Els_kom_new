/*
	packerhandler.h
*/

#include <string>

bool EndsWith(const std::string& a, const std::string& b);
void TempKOMFileWriter(std::string FileName, int SizeOfData);
int FolderIterator(std::string path, std::string FileName);
