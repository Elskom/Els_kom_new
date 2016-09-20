/*
	packerhandler.cpp
*/

/*
	This is where the Files I try to make the console program to read the data from are used.
*/

#include "kompact_new.h"
bool TEMP_FILE_WRITER_NOT_DONE = false;

bool EndsWith(const std::string& a, const std::string& b)
{
	if (b.size() > a.size()) return false;
	return std::equal(a.begin() + a.size() - b.size(), a.end(), b.begin());
}

void TempKOMFileWriter(char* data, int SizeOfData)
{
	if (TEMP_FILE_WRITER_NOT_DONE == true)
	{
		print("Error: The Temp File Writer is not completed yet.", true, false, false);
	}
	else
	{
		char* data3;
		FILE *fp2 = fopen("data.tmp", "wb");
		if (fp2 != NULL)
		{
			fseek(fp2, 0, SEEK_END);
			std::size_t size = ftell(fp2);
			fseek(fp2, 0, SEEK_SET);
			fseek(fp2, 0, SEEK_END);
			data3 = new char[size];
			std::size_t file_data = fread(data3, 1, size, fp2);
			fwrite(data, 1, SizeOfData, fp2);
			fclose(fp2);
		}
	}
}

int FolderIterator(std::string path, std::string FileName)
{
	XMLWriter xml;
	xml.WriteHeader();
	FileIterator(path, FileName);
	return 0;
}
