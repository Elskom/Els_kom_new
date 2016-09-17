// Advanced Console Library (Cursor Example - cursorex.cpp)
// by Jeff Weiner
// http://tilde.planet-nebula.com/projects/advconsole/
//
// Demonstrates various abilities of the Cursor interface.
//
// Compile:	`make cursorex` OR
// 		`g++ cursorex.cpp advconsole.cpp advconsole.<os>.cpp -o cursorex`
// Usage:	`./cursorex`

#include <iostream>
#include "advconsole.h"
using std::cout; using std::endl;
using namespace AdvancedConsole;

int main(int argc, char *argv[])
{
	cout << "This library sucks!" << Cursor(0, -6, AC_RELATIVE) << "ro" << endl;
	cout << Cursor(3, 10) << "Look ma', no spaces!" << endl;	
	return EXIT_SUCCESS;
}
