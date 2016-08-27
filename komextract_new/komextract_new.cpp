// komextract_new.cpp : Defines the entry point for the console application.
//

#include <windows.h>
#include <stdio.h>
#include <tchar.h>
#include <cstring>
#include <sstream>
#include <iostream>
#include "..\\Els_kom_Generic_Unpacker\\Els_kom_Generic_Unpacker.h"

int main(int argc, char* argv[])
{
	if(argc==1)
	{
		std::cout << "Usage:\nkomextract_new.exe --in <KOM File Name> --out <Folder Name>\n<Folder Name> = Folder to feed files from <KOM File Name> into." << std::endl;
	}
	else if(argc==2)
	{
		std::cout << "Usage:\nkomextract_new.exe --in <KOM File Name> --out <Folder Name>\n<Folder Name> = Folder to feed files from <KOM File Name> into." << std::endl;
	}
	else if(argc==3)
	{
		std::cout << "Usage:\nkomextract_new.exe --in <KOM File Name> --out <Folder Name>\n<Folder Name> = Folder to feed files from <KOM File Name> into." << std::endl;
	}
	else if(argc==4)
	{
		std::cout << "Usage:\nkomextract_new.exe --in <KOM File Name> --out <Folder Name>\n<Folder Name> = Folder to feed files from <KOM File Name> into." << std::endl;
	}
	else
	{
		for(int i=1;i<argc;i++)
		{
			if(!strcmp(argv[i], "--in"))
			{
				//Open if block for now. I want this block to be required.
				//lets print out the number to know what place it should always be at as a constant to look for.
				std::stringstream inCount;
				inCount << "--in is the " << i + 1 << "nd arg.";
				std::cout << inCount.str() << std::endl;
			}
			else if(!strcmp(argv[i], "--out"))
			{
				//Open if block for now. I want this block to be required.
				//lets print out the number to know what place it should always be at as a constant to look for.
				std::stringstream outCount;
				outCount << "--out is the " << i + 1 << "th arg.";
				std::cout << outCount.str() << std::endl;
			}
			else
			{
				//everything else here.
				std::cout << argv[i] << std::endl;
			}
		}
	}
	return 0;
}

