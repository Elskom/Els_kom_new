/*
	constants.hpp
*/
#include "../../externals/CColors/advconsole.h"

using namespace std::experimental::filesystem;
using namespace AdvancedConsole;

#include <iostream>

/* Version. */
#ifndef KOMPACT_VERSION
#define KOMPACT_VERSION "0.0.1a2"
#endif

/*
	This is where the Console Colors are Defined.
*/
#ifndef CONCOLBLACK
#define CONCOLBLACK Color(AC_BLACK)
#endif
#ifndef CONCOLRED
#define CONCOLRED Color(AC_RED)
#endif
#ifndef CONCOLGREEN
#define CONCOLGREEN Color(AC_GREEN)
#endif
#ifndef CONCOLYELLOW
#define CONCOLYELLOW Color(AC_YELLOW)
#endif
#ifndef CONCOLBLUE
#define CONCOLBLUE Color(AC_BLUE)
#endif
#ifndef CONCOLMAGENTA
#define CONCOLMAGENTA Color(AC_MAGENTA)
#endif
#ifndef CONCOLCYAN
#define CONCOLCYAN Color(AC_CYAN)
#endif
#ifndef CONCOLWHITE
#define CONCOLWHITE Color(AC_WHITE)
#endif
#ifndef CONCOLDEFAULT
#define CONCOLDEFAULT Color(AC_DEFAULT)
#endif

/*
	Defines the print here to be std::cout
*/
#ifndef printerr
#define printerr std::cerr
#endif
#ifndef nl
#define nl std::endl
#endif

#ifndef _M_X64
#define ARCH "32"
#define ARCH_NAME "Intel"
#else
#define ARCH "64"
#define ARCH_NAME "AMD64"
#endif
