/*
	komextract_new.cpp
*/

#include "komextract_new.h"

int main(int argc, char* argv[])
{
	if(argc >=1)
	{
		print << "Usage:\nkomextract_new.exe --in <KOM File Name> --out <Folder Name>\n<Folder Name> = Folder to feed files from <KOM File Name> into." << nl;
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
				print << inCount.str() << nl;
			}
			else if(!strcmp(argv[i], "--out"))
			{
				//Open if block for now. I want this block to be required.
				//lets print out the number to know what place it should always be at as a constant to look for.
				std::stringstream outCount;
				outCount << "--out is the " << i + 1 << "th arg.";
				print << outCount.str() << nl;
			}
			else
			{
				//everything else here.
				print << argv[i] << nl;
			}
		}
	}
	return 0;
}

