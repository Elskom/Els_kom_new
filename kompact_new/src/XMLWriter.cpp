/*
	XMLWriter.cpp
*/

#include "kompact_new.h"
bool _XMLWRITER_NOT_DONE = false;
bool _DEBUG_XML = true;


int XMLWriter::WriteHeader()
{
	if (_XMLWRITER_NOT_DONE == true)
	{
		print("Error: The XML Writer is not completed yet.", true, false, false);
	}
	else
	{
		std::string xmlheader = "<?xml version=\"1.0\"?><Files>";
		std::string filename = "crc.xml";
		std::ofstream fout(filename.c_str(), std::ios::out | std::ios::app);
		if (!fout)
		{
			print("Error: open file for output failed!", true, false, false);
			abort();
		}
		fout << xmlheader;
		fout.close();
	}
	return 0;
}

// Looks like Checksum and FileTime are always the same.
int XMLWriter::AppendFile(std::string Name, int Size, int CompressedSize, std::string Checksum, std::string FileTime, int alg)
{
	if (_DEBUG_XML == true)
	{
		print("XMLWriter::AppendFile called", false, false, false);
	}
	std::string new_node;
	if (_XMLWRITER_NOT_DONE == true)
	{
		print("Error: The XML Writer is not completed yet.", true, false, false);
	}
	else
	{
		new_node = "<File Name=\"";
		new_node += Name;
		new_node += "\" Size=\"";
		new_node += std::to_string(Size);
		new_node += "\" CompressedSize=\"";
		new_node += std::to_string(CompressedSize);
		new_node += "\" Checksum=\"";
		new_node += Checksum;
		new_node += "\" FileTime=\"";
		new_node += FileTime;
		new_node += "\" Algorithm=\"";
		new_node += std::to_string(alg);
		new_node += "\" />";
		
		std::string filename = "crc.xml";
		std::ofstream fout(filename.c_str(), std::ios::out | std::ios::app);
		if (!fout)
		{
			print("Error: open file for output failed!", true, false, false);
			abort();
		}
		fout << new_node;
		fout.close();
	}
	return 0;
}

int XMLWriter::WriteEnding()
{
	if (_XMLWRITER_NOT_DONE == true)
	{
		print("Error: The XML Writer is not completed yet.", true, false, false);
	}
	else
	{
		std::string ending = "</Files>";
		std::string filename = "crc.xml";
		std::ofstream fout(filename.c_str(), std::ios::out | std::ios::app);
		if (!fout)
		{
			print("Error: open file for output failed!", true, false, false);
			abort();
		}
		fout << ending;
		fout.close();
	}
	return 0;
}
