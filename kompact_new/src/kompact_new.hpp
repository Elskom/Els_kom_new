/*
	kompact_new.hpp
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
#include "..\\..\\Els_kom_Generic_Unpacker\\src\\Els_kom_Generic_Unpacker.hpp"
#include "constants.hpp"
#include "packerconsole.hpp"
#include "XMLWriter.hpp"
#include "packerhandler.hpp"
#include "FileChecker.hpp"
#else
#error "This has somehow been changed from a console Application to something else. Please Change it back."
#endif

// TODO: Finish the list of Known file Extensions in KOM files and their Algorithms.
/*
	|	File Extension	|	Algorithm	|
	|	*.txt			|	3			|
	|	*.tga			|	2			|
	|	*.lua			|	3			|
	|	*.dds			|	2			|
	|	*.x				|	0			|
	|	*.y				|	0			|
	|	*.X				|	0			|
	|	*.Y				|	0			|
	|	*.xet			|	0			|
	|	*.XET			|	0			|
	|	*.XEt			|	0			|
	|	*.xml			|	3			|
	|	*.font			|	0			|
	|	*.ini			|	0			|
	|	*.ess			|	0			|
	|	*.kim			|	3			|
*/

