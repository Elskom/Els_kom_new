/*
	kompact_new.cpp
*/

#include "kompact_new.h"


int main(int argc, char* argv[])
{
	if(argc < 2)
	{
		WhenStartedDirectly();
	}
	else
	{
		for(int i=1;i<argc;i++)
		{
			if (argc == 2)
			{
				if (!strcmp(argv[i], "--help"))
				{
					print << "Usage:\n" << CONCOLBLUE << "kompact_new.exe --in <Folder Name> --out <KOM File Name>" << CONCOLDEFAULT << "\n<Folder Name> = Folder to feed into the packer.\n<KOM File Name> = KOM File to create from the files in <Folder Name>.\nNote: <Folder Name> and <KOM File Name> must not have 0 length." << nl;
				}
			}
			else
			{
				if (!strcmp(argv[i], "--in"))
				{
					size_t DirArgLegth = strlen(argv[i + 1]);
					if(!strcmp(argv[i + 2], "--out"))
					{
						size_t DestFileLength = strlen(argv[i + 3]);
						if (DirArgLegth != NULL)
						{
							std::string path = argv[i + 1];
							if (DestFileLength != NULL)
							{
								FolderIterator(path, argv[i + 3]);
							}
							else
							{
								print << CONCOLRED << "A kom file name must be specified." << CONCOLDEFAULT << nl;
							}
						}
						else
						{
							print << CONCOLRED  << "A Folder with the files you want to pack into a kom file must be specified." << CONCOLDEFAULT << nl;
						}
					}
				}
			}
		}
	}
	return 0;
}
