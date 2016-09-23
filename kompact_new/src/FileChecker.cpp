/*
	FileChecker.cpp
*/

#include "kompact_new.h"
bool UnsupportedAlg;
bool UnsupportedExt;

long GetFileSize(std::string path)
{
    struct stat buffer;
    int result = stat(path.c_str(), &buffer);
    return result == 0 ? buffer.st_size : 0;
}

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
			long filesize;
			long compressedfilesize;
			std::string desfile = p->path().filename().string();
			std::string fileopeninfo = p->path().string();
			filesize = GetFileSize(fileopeninfo);
			//This Below is for Packing.
			if (EndsWith(desfile, ".txt"))
			{
				Algorithm = 3;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 3 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".lua"))
			{
				Algorithm = 3;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 3 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			/*
				More Problems than Good to Support these '.3' files from the Python Version. This means Algorithm 2 Support is now Critical.
			*/
			else if (EndsWith(desfile, ".3"))
			{
				Algorithm = 3;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 3 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			/*
				More Problems than Good to Support these '.2' files from the Python Version. This means Algorithm 2 Support is now Critical.
			*/
			else if (EndsWith(desfile, ".2"))
			{
				Algorithm = 2;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 2 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".tga"))
			{
				Algorithm = 2;  // Ever Since Void v1.8 and probably in all Official servers all tga's are alg 2 as I seen so far. I need more investigation.
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 2 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".dds"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".x"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".y"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".xet"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".X"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".Y"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".XET"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".XEt"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".xml"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".font"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".ini"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
				}
			}
			else if (EndsWith(desfile, ".ess"))
			{
				Algorithm = 0;
				Pack_KOM_Generic(fileopeninfo, Algorithm);
				compressedfilesize = GetFileSize(fileopeninfo);
				if (filesize == compressedfilesize)
				{
					filecount2++;
					print("Packing to Algorithm 0 is not implemented yet.", false, true, false);
					UnsupportedAlg = true;
				}
				else
				{
					xml.AppendFile(desfile,  filesize, compressedfilesize, "NULL", "NULL", Algorithm);
					TempKOMFileWriter(fileopeninfo, compressedfilesize);
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
