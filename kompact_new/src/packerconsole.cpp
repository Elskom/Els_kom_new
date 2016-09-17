/*
	packerconsole.cpp
*/

#include "kompact_new.h"
bool WHY;
bool WHY_NOT;

int WhenStartedDirectly()
{
	WHY = true;
	WHY_NOT = true;
	print << CONCOLGREEN << "kompact_new v0.0.1a1 (" << __DATE__ << ", " << __TIME__;
	print << ") [MSC v." << _MSC_VER << " " << ARCH << " bit (" << ARCH_NAME << ")] on win32" << CONCOLDEFAULT << nl;
	print << CONCOLCYAN << "Type \"--help\" for more information." << CONCOLDEFAULT << nl;
	for(;;){
		if(WHY != WHY_NOT)
		{
			break;
		}
		std::string buffer;
		print << CONCOLDEFAULT << ">>> " << CONCOLCYAN;
		std::getline(std::cin, buffer);
		print << CONCOLDEFAULT << "";
		if (!strcmp(buffer.c_str(), "--help"))
		{
			print << CONCOLRED << "Usage:\n" << CONCOLBLUE << "kompact_new.exe --in <Folder Name> --out <KOM File Name>" << CONCOLYELLOW << "\n<Folder Name> = Folder to feed into the packer.\n<KOM File Name> = KOM File to create from the files in <Folder Name>.\nNote: <Folder Name> and <KOM File Name> must not have 0 length.\nType \"--exit\" to close this console.\n" << CONCOLMAGENTA << "Note: The \"--in\" and the \"--out\" commands do not work in this console instance (can only be used directly in the command line in a batch file or executed with the commands directly from Command Prompt)." << CONCOLDEFAULT << nl;
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
			print << CONCOLRED << "Invalid Command." << CONCOLDEFAULT << nl;
		}
	}
	return 0;
}
