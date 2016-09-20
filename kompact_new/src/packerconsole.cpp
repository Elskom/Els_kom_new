/*
	packerconsole.cpp
*/

#include "kompact_new.h"
bool WHY;
bool WHY_NOT;


int print(std::string data, bool Error, bool warn, bool input)
{
	if (Error == false)
	{
		if (warn == false)
		{
			if (input == false)
			{
				std::cout << CONCOLGREEN << data << CONCOLDEFAULT << nl;
			}
			else
			{
				std::getline(std::cin, data);
			}
		}
		else
		{
			std::cout << CONCOLYELLOW << data << CONCOLDEFAULT << nl;
		}
	}
	else
	{
		printerr << CONCOLRED << data << CONCOLDEFAULT << nl;
	}
	return 0;
}

int WhenStartedDirectly()
{
	SetConsoleTitleA("kompact_new v0.0.1a2");
	WHY = true;
	WHY_NOT = true;
	std::cout << CONCOLGREEN << "kompact_new v0.0.1a2 (" << __DATE__ << ", " << __TIME__;
	std::cout << ") [MSC v." << _MSC_VER << " " << ARCH << " bit (" << ARCH_NAME << ")] on win32" << CONCOLDEFAULT << nl;
	std::cout << CONCOLCYAN << "Type \"--help\" for more information." << CONCOLDEFAULT << nl;
	for(;;)
	{
		if(WHY != WHY_NOT)
		{
			break;
		}
		std::string buffer;
		std::cout << CONCOLDEFAULT << ">>> " << CONCOLCYAN;
		std::getline(std::cin, buffer);
		std::cout << CONCOLDEFAULT << "";
		if (!strcmp(buffer.c_str(), "--help"))
		{
			std::cout << CONCOLRED << "Usage:\n" << CONCOLBLUE << "kompact_new.exe --in <Folder Name> --out <KOM File Name>" << CONCOLYELLOW << "\n<Folder Name> = Folder to feed into the packer.\n<KOM File Name> = KOM File to create from the files in <Folder Name>.\nNote: <Folder Name> and <KOM File Name> must not have 0 length.\nType \"--exit\" to close this console.\n" << CONCOLCYAN << "Note: The \"--in\" and the \"--out\" commands do not work in this console instance (can only be used directly in the command line in a batch file or executed with the commands directly from Command Prompt)." << CONCOLDEFAULT << nl;
		}
		else if (!strcmp(buffer.c_str(), "--exit"))
		{
			WHY = false;
		}
		else if (!strcmp(buffer.c_str(), ""))
		{
			
		}
		else
		{
			print("Invalid Command.", true, false, false);
		}
	}
	return 0;
}
