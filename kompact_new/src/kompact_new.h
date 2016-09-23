/*
	kompact_new.h
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

/*
	Define this to avoid Data Corruptions.
*/
#if defined(MSDOS) || defined(OS2) || defined(WIN32) || defined(__CYGWIN__)
#  include <fcntl.h>
#  include <io.h>
#  define SET_BINARY_MODE(file) setmode(fileno(file), O_BINARY)
#else
#  define SET_BINARY_MODE(file)
#endif

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
#include <cstdlib>
#include <cstdint>
#include <sys\stat.h>
#pragma once
#ifdef _CONSOLE
#include "..\\..\\Els_kom_Generic_Unpacker\\src\\Els_kom_Generic_Unpacker.h"
#include "XMLWriter.h"
#include "FileChecker.h"
#include "CColors\\advconsole.h"
#include "packerconsole.h"
#include "packerhandler.h"
#else
#error "This has somehow been changed from a console Application to something else. Please Change it back."
#endif

#define KOMPACT_VERSION "0.0.1a2"

// TODO: Finish the list of Known file Extensions in KOM files and their Algorithms.
/*
	|	File Extension	|	Algorithm	|
	|	*.txt			|	3			|
	|	*.tga			|	2			|
	|	*.lua			|	3			|
	|	*.dds			|	0			|
	|	*.x				|	0			|
	|	*.y				|	0			|
	|	*.xet			|	0			|
	|	*.X				|	0			|
	|	*.Y				|	0			|
	|	*.XET			|	0			|
	|	*.XEt			|	0			|


	Some Files within data036.kom.

	|	*.xml			|	0			|
	|	*.font			|	0			|
	|	*.ini			|	0			|
	|	*.ess			|	0			|
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
