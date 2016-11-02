/*
	komextract_new.cpp
*/

#include "komextract_new.h"

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
			if(!strcmp(argv[i], "--in"))
			{
				std::string KOMFileName = argv[i+1];
				if(!strcmp(argv[i+2], "--out"))
				{
					std::string DestPath = argv[i+3];
					KOMFileReader(KOMFileName, DestPath);
				}
			}
		}
	}
	return 0;
}
