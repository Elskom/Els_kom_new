/*
	FileChecker.cpp
*/

#include "kompact_new.h"
bool UnsupportedAlg;
bool UnsupportedExt;

void FileChecker(std::string path, std::string FileName)
{
	UnsupportedAlg = false;
	UnsupportedExt = false;
	int filecount = 0;
	int filecount2 = 0;
	for (auto p = directory_iterator(path); p != directory_iterator(); p++)
	{
		if (!is_directory(p->path()))
		{
			char* data;
			std::string desfile = p->path().filename().string();
			char* tmpdata;
			// From Lines 54-69 it seems that the Data Obtained from the files is nothing...-
			std::string fileopeninfo = p->path().string();
			FILE *fp = fopen(fileopeninfo.c_str(), "rb");
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
			else
			{
				tmpdata = NULL;
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
					filecount2++;
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
					filecount2++;
					print << CONCOLYELLOW << "Packing to Algorithm 3 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			/*
				More Problems than Good to Support these '.___' files from the Python Version. This means Algorithm 3 Support is now Critical.
			*/
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
			else if (EndsWith(desfile, ".tga"))
			{
				data = Pack_KOM_Generic(tmpdata, 2);  // Ever Since Void v1.8 and probably in all Official servers all tga's are alg 2 as I seen so far. I need more investigation.
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					filecount2++;
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
					filecount2++;
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
					filecount2++;
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
					filecount2++;
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
					filecount2++;
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".X"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					filecount2++;
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".Y"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					filecount2++;
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".XET"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					filecount2++;
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".XEt"))
			{
				data = Pack_KOM_Generic(tmpdata, 0);
				if (data != NULL)
				{
					TempKOMFileWriter(data);
				}
				else
				{
					filecount2++;
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
					filecount2++;
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
					filecount2++;
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
					filecount2++;
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
					filecount2++;
					print << CONCOLYELLOW << "Packing to Algorithm 0 is not implemented yet." << CONCOLDEFAULT << nl;
					UnsupportedAlg = true;
				}
			}
			else
			{
				filecount++;
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
		print << CONCOLRED << "File Could not be created due to a Unsupported Algorithm(s), Found " << filecount2 <<" files." << CONCOLDEFAULT << nl;
	}
	if (UnsupportedExt != false)
	{
		print << CONCOLRED << "File Could not be created due to a Unsupported File Extension(s), Found " << filecount <<" files." << CONCOLDEFAULT << nl;
	}
	else
	{
		print << CONCOLGREEN << "File Created." << CONCOLDEFAULT << nl;
		// TODO: Delete Temp Files here.
	}
}
