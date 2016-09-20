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
					std::cout << CONCOLRED << "Usage:\n" << CONCOLBLUE << "kompact_new.exe --in <Folder Name> --out <KOM File Name>" << CONCOLYELLOW << "\n<Folder Name> = Folder to feed into the packer.\n<KOM File Name> = KOM File to create from the files in <Folder Name>.\nNote: <Folder Name> and <KOM File Name> must not have 0 length.\nType \"--exit\" to close this console.\n" << CONCOLCYAN << "Note: The \"--in\" and the \"--out\" commands do not work in this console instance (can only be used directly in the command line in a batch file or executed with the commands directly from Command Prompt)." << CONCOLDEFAULT << nl;
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
								print("A kom file name must be specified.", true, false, false);
							}
						}
						else
						{
							print("A Folder with the files you want to pack into a kom file must be specified.", true, false, false);
						}
					}
				}
			}
		}
	}
	return 0;
}
