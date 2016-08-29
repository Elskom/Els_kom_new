/*
	packerconsole.cpp
*/

#include "kompact_new.h"
bool UnsupportedAlg;
bool UnsupportedExt;

bool EndsWith(const std::string& a, const std::string& b)
{
	if (b.size() > a.size()) return false;
	return std::equal(a.begin() + a.size() - b.size(), a.end(), b.begin());
}

int TempKOMFileWriter(unsigned char* data)
{
	unsigned char* data3;
	FILE *fp2 = fopen("data.tmp", "wb");
	if (fp2 != NULL)
	{
		fseek(fp2, 0, SEEK_END);
		size_t size = ftell(fp2);
		fseek(fp2, 0, SEEK_SET);
		fseek(fp2, 0, SEEK_END);
		data3 = new unsigned char[size];
		size_t file_data = fread(data3, 1, size, fp2);
		fwrite(data3, 1, size, fp2);
		fclose(fp2);
	}
	return 0;
}

int CRCFileWriter()
{
	// TODO: Write Code that Creates the CRC XML data.
	return 0;
}

void FolderIterator(std::string path, std::string FileName)
{
	UnsupportedAlg = false;
	UnsupportedExt = false;
	for (auto p = directory_iterator(path); p != directory_iterator(); p++)
	{
		if (!is_directory(p->path()))
		{
			unsigned char* data;
			std::string desfile = p->path().filename().string();
			unsigned char* tmpdata;
			// From Lines 51-63 it seems that the Data Obtained from the files is nothing...
			unsigned char* data2;
			std::string fileopeninfo = p->path().string();
			FILE *fp = fopen(fileopeninfo.c_str(), "rb");
			if (fp != NULL)
			{
				fseek(fp, 0, SEEK_END);
				size_t size = ftell(fp);
				fseek(fp, 0, SEEK_SET);
				data2 = new unsigned char[size];
				size_t file_data = fread(data2, 1, size, fp);
				tmpdata = data2;
				fclose(fp);
			}
			//This Below is for Packing.
			if (EndsWith(desfile, ".txt"))
			{
				data = Pack_KOM_Generic(tmpdata, 3);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 3 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".lua"))
			{
				data = Pack_KOM_Generic(tmpdata, 3);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 3 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			/*
				More Problems than Good to Support these .___ files from the Python Version. This means Algorithm 3 Support is now Critical.
			else if (EndsWith(desfile, ".___"))
			{
				data = Pack_KOM_Generic(tmpdata, 4);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 3 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			*/
			else if (EndsWith(desfile, ".tga"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".dds"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".x"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".y"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".xet"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".xml"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".font"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".ini"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".ess"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else
			{
				print << CONCOLYELLOW << desfile << " has a unsupported extension." << CONCOLDEFAULT << nl;
				UnsupportedExt = true;
			}
		}
		else
		{
			continue;
		}
	}
	// TODO: Have All of the Data returned from the packers of each file to be written to a file (the kom files namely)
	// TODO: Delete Temp Files when packing is complete.
	if (UnsupportedAlg != false)
	{
		print << CONCOLRED << "File Could not be created due to Unsupported Algorithm(s)." << CONCOLDEFAULT << nl;
	}
	if (UnsupportedAlg != false)
	{
		print << CONCOLRED << "File Could not be created due to Unsupported File Extensions(s)." << CONCOLDEFAULT << nl;
	}
	else
	{
		print << CONCOLGREEN << "File Created." << CONCOLDEFAULT << nl;
		// TODO: Delete Temp Files here.
	}
}

int WhenStartedDirectly()
{
	print << "kompact_new v0.0.1a1 (" << __DATE__ << ", " << __TIME__ << ") [MSC v." << _MSC_VER << " " << ARCH << " bit (" << ARCH_NAME << ")] on win32\nType \"--help\" for more information." << nl;
	for(;;)
	{
		std::string buffer;
		print << ">>> ";
		std::getline(std::cin, buffer);
		if (!strcmp(buffer.c_str(), "--help"))
		{
			print << "Usage:\n" << CONCOLBLUE << "kompact_new.exe --in <Folder Name> --out <KOM File Name>" << CONCOLDEFAULT << "\n<Folder Name> = Folder to feed into the packer.\n<KOM File Name> = KOM File to create from the files in <Folder Name>.\nNote: <Folder Name> and <KOM File Name> must not have 0 length.\nType \"--exit\" to close this console.\nNote:The \"--in\" and the \"--out\" commands do not work in this console instance (can only be used directly in the command line in a batch file or executed with the commands directly from Command Prompt)." << nl;
		}
		else if (!strcmp(buffer.c_str(), "--exit"))
		{
			break;
		}
		else if (!strcmp(buffer.c_str(), ""))
		{
		}
		else
		{
			print << CONCOLMAGENTA << "Invalid Command." << CONCOLDEFAULT << nl;
		}
	}
	return 0;
}
