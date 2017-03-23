/*
	packerhandler.hpp
*/

/*
	This is where the Files I try to make the console program to read the data from are used.
*/

#include <string>

bool EndsWith(const std::string& a, const std::string& b);
void TempKOMFileWriter(std::string FileName, int SizeOfData);

bool TEMP_FILE_WRITER_NOT_DONE = true;

bool EndsWith(const std::string& a, const std::string& b)
{
	if (b.size() > a.size()) return false;
	return std::equal(a.begin() + a.size() - b.size(), a.end(), b.begin());
}

void TempKOMFileWriter(std::string FileName, int SizeOfData)
{
	if (TEMP_FILE_WRITER_NOT_DONE == true)
	{
		print("Error: The Temp File Writer is not completed yet.", true, false, false);
	}
	else
	{
		char* tmpdata;
		FILE *fp = fopen(FileName.c_str(), "rb");
		if (fp != NULL)
		{
			fseek(fp, 0, SEEK_END);
			long size = ftell(fp);
			rewind(fp);
			char * buffer = (char*)malloc(sizeof(char)*size);
			size_t file_data = fread(buffer, 1, size, fp);
			tmpdata = buffer;
			fclose(fp);
			free(buffer);
		}
		FILE *fp2 = fopen("data.tmp", "r+b");
		if (fp2 != NULL)
		{
			fseek(fp2, 0, SEEK_END);
			fwrite(tmpdata, 1, SizeOfData, fp2);
			fclose(fp2);
		}
	}
}
