/*
	XMLWriter.cpp
*/

#include "kompact_new.h"
bool _XMLWRITER_NOT_DONE = true;


int XML_WriteHeader()
{
	std::string xmlheader = "<?xml version=\"1.0\"?><Files>";
	if (_XMLWRITER_NOT_DONE == true)
	{
		print << CONCOLRED << "Error: The XML Writer is not completed yet." << nl;
	}
	return 0;
}

// Looks like Checksum and FileTime are always the same.
int XML_AppendFile(std::string Name, int Size, int CompressedSize, std::string Checksum, std::string FileTime, int alg)
{
	//this line right below has to be commented out till it is fixed RIP.
	//std::string new_node = "<File Name=" Name " Size=" Size " CompressedSize=" CompressedSize " Checksum=" Checksum " FileTime=" FileTime " Algorithm=" alg " />";
	if (_XMLWRITER_NOT_DONE == true)
	{
		print << CONCOLRED << "Error: The XML Writer is not completed yet." << nl;
	}
	return 0;
}

int XML_WriteEnding()
{
	std::string ending = "</Files>";
	if (_XMLWRITER_NOT_DONE == true)
	{
		print << CONCOLRED << "Error: The XML Writer is not completed yet." << nl;
	}
	return 0;
}
