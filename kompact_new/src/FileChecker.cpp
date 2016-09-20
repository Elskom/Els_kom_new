/*
	FileChecker.cpp
*/

#include "kompact_new.h"
bool UnsupportedAlg;
bool UnsupportedExt;

size_t strlen_s(char *str)
{
	size_t value;
	if (str != NULL) {
		size_t i;
		for (i = 0; str[i]; i++);
		value = i;
	} else {
		value = 0;
	}
	return value;
}


void FileIterator(std::string path, std::string FileName)
{
	XMLWriter xml;
	UnsupportedAlg = false;
	UnsupportedExt = false;
	int filecount = 0;
	int filecount2 = 0;
	int Algorithm;
	for (auto p = directory_iterator(path); p != directory_iterator(); p++)
	{
		if (!is_directory(p->path()))
		{
			char* data;
			std::string desfile = p->path().filename().string();
			char* tmpdata;
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
				Algorithm = 3;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 3 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 3 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".lua"))
			{
				Algorithm = 3;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 3 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 3 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			/*
				More Problems than Good to Support these '.3' files from the Python Version. This means Algorithm 2 Support is now Critical.
			*/
			else if (EndsWith(desfile, ".3"))
			{
				Algorithm = 3;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 3 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					print("Packing to Algorithm 3 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			/*
				More Problems than Good to Support these '.2' files from the Python Version. This means Algorithm 2 Support is now Critical.
			*/
			else if (EndsWith(desfile, ".2"))
			{
				Algorithm = 2;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 2 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					print("Packing to Algorithm 2 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".tga"))
			{
				Algorithm = 2;  // Ever Since Void v1.8 and probably in all Official servers all tga's are alg 2 as I seen so far. I need more investigation.
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 2 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 2 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".dds"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".x"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".y"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".xet"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".X"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".Y"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".XET"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".XEt"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".xml"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".font"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".ini"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else if (EndsWith(desfile, ".ess"))
			{
				Algorithm = 0;
				data = Pack_KOM_Generic(tmpdata, Algorithm);
				if (my_strlen(tmpdata) == my_strlen(data) & my_strlen(data) != 0)  // My hack of a Secured version of strlen returns the size_t of 0 if a NULL pointer is provided.
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else if (data != NULL)
				{
					xml.AppendFile(desfile,  my_strlen(tmpdata), my_strlen(data), "NULL", "NULL", Algorithm);
					TempKOMFileWriter(data, my_strlen(data));
				}
				else
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
			}
			else
			{
				filecount++;
				std::cout << CONCOLYELLOW << desfile << " has a unsupported extension." << CONCOLDEFAULT << nl;
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
		printerr << CONCOLRED << "File Could not be created due to a Unsupported Algorithm(s), Found " << filecount2 <<" files." << CONCOLDEFAULT << nl;
	}
	if (UnsupportedExt != false)
	{
		printerr << CONCOLRED << "File Could not be created due to a Unsupported File Extension(s), Found " << filecount <<" files." << CONCOLDEFAULT << nl;
	}
	if (UnsupportedAlg == false)
	{
		if (UnsupportedExt == false)
		{
			xml.WriteEnding();
			print("File Created.", false, false, false);
			// TODO: Delete Temp Files here.
		}
	}
	else
	{
		if(remove("crc.xml") != 0 )
		{
			print("Error: deleting crc.xml.", true, false, false);
		}
	}
}
