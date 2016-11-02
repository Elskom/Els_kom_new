/*
	FileChecker.h
*/

/*
	To Avoid Compile Issues later.
*/
#ifndef HAVE_FILECHECKER_H_
	#define HAVE_FILECHECKER_H_
	#include <string>
	size_t strlen_s(char *str);
	#define my_strlen(str) static_cast<int>(strlen_s(str)) // Bypass issue with the original one. making this NULL a size_t of 0.
	void FileIterator(std::string path, std::string FileName);
#else
#endif
