/*
	extractorconsole.hpp
*/

#include <string>

bool WHY;
bool WHY_NOT;

int print(std::string data, bool Error, bool warn, bool input);
int WhenStartedDirectly();

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
	SetConsoleTitleA("komextract_new v" KOMEXTRACT_VERSION);
	WHY = true;
	WHY_NOT = true;
	std::cout << CONCOLGREEN << "komextract_new v" << KOMEXTRACT_VERSION << " (" << __DATE__ << ", " << __TIME__;
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
			std::cout << CONCOLRED << "Usage:\n" << CONCOLBLUE << "komextract_new.exe --in <KOM File Name> --out <Folder Name>" << CONCOLYELLOW << "\n<Folder Name> = Folder to feed files from <KOM File Name> into.\n" << CONCOLCYAN << "Note: The \"--in\" and the \"--out\" commands do not work in this console instance (can only be used directly in the command line in a batch file or executed with the commands directly from Command Prompt)." << CONCOLDEFAULT << nl;
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
