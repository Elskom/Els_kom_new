/*
	constants.hpp
*/

#include "../../externals/CColors/advconsole.h"
#include <iostream>

/* Version. */
#define KOMEXTRACT_VERSION "0.0.1a2"

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

#define printerr std::cerr
#define nl std::endl

#ifndef _M_X64
#define ARCH "32"
#define ARCH_NAME "Intel"
#else
#define ARCH "64"
#define ARCH_NAME "AMD64"
#endif
