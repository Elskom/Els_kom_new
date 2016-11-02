/*
	komextract_new.h
	==========================================================================================
		This Header File is for Including all the headings and other things and defining like
		the Platform variables. Nothing more.
*/

/*
	Disable Some Security Warnings.
*/
#ifndef _CRT_SECURE_NO_WARNINGS
#define _CRT_SECURE_NO_WARNINGS
#endif

/*
	All Known Includes here.
*/

#include <windows.h>
#include <stdio.h>
#include <tchar.h>
#include <cstring>
#include <sstream>
#include <iostream>
#include <filesystem>
#include <algorithm>
#include <stdlib.h>
#include <fstream>
#include <cstdint>
#include "..\\..\\Els_kom_Generic_Unpacker\\src\\Els_kom_Generic_Unpacker.h"
#include "KOMReader.h"
#include "CColors\\advconsole.h"
#include "extractorconsole.h"

#define KOMEXTRACT_VERSION "0.0.1a2"

// TODO: Finish the list of Known file Extensions in KOM files and their Algorithms.
/*
	|	File Extension	|	Algorithm	|
	|	*.txt			|	3			|
	|	*.tga			|	0			|
	|	*.lua			|	3			|
	|	*.dds			|	0			|
	|	*.x				|	0			|
	|	*.y				|	0			|
	|	*.xet			|	0			|
	|	*.xml			|	0			|
	|	*.font			|	0			|
	|	*.ini			|	0			|
	|	*.ess			|	0			|
	|	*.kim			|	3			|
*/

#ifndef _M_X64
#define ARCH "32"
#define ARCH_NAME "Intel"
#else
#define ARCH "64"
#define ARCH_NAME "AMD64"
#endif

using namespace std::experimental::filesystem;
using namespace AdvancedConsole;

/*
	This is where the Console Colors are Defined.
*/
#define CONCOLBLACK Color(AC_BLACK)
#define CONCOLRED Color(AC_RED)
#define CONCOLGREEN Color(AC_GREEN)
#define CONCOLYELLOW Color(AC_YELLOW)
#define CONCOLBLUE Color(AC_BLUE)
#define CONCOLMAGENTA Color(AC_MAGENTA)
#define CONCOLCYAN Color(AC_CYAN)
#define CONCOLWHITE Color(AC_WHITE)
#define CONCOLDEFAULT Color(AC_DEFAULT)

/*
	Defines the print here to be std::cout
*/
#define printerr std::cerr
#define nl std::endl
