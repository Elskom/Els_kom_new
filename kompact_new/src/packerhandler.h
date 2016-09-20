/*
	packerhandler.h
*/

#include <string>

bool EndsWith(const std::string& a, const std::string& b);
void TempKOMFileWriter(char* data, int SizeOfData);
int FolderIterator(std::string path, std::string FileName);
