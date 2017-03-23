// Advanced Console Library (Gradient Example - gradient.cpp)
// by Jeff Weiner
// http://tilde.planet-nebula.com/projects/advconsole/
// 
// Generates a diagonal gradient across the screen.
// 
// Compile:	`make gradient` OR
// 		`g++ gradient.cpp advconsole.cpp advconsole.<os>.cpp -o gradient`
// Usage:	`./gradient <width> <height>`

#include <iostream>
#include "advconsole.h"
using namespace std;
using namespace AdvancedConsole;

const int NUM_COLORS = 8;
const AC_COLOR COLORS[] = { AC_BLACK, AC_RED, AC_YELLOW, AC_GREEN, AC_CYAN, AC_BLUE, AC_MAGENTA, AC_WHITE };

int main(int argc, char *argv[])
{
	// Initialize
	if (argc != 3)
	{
		cout << "Usage:  " << argv[0] << " <width> <height>" << endl;
		return EXIT_FAILURE;
	}
	int width = atoi(argv[1]), height = atoi(argv[2]);

	// Do top
	cout << '/';
	for (int x = 0; x < width - 2; x++)
		cout << '-';
	cout << '\\' << endl;

	// Do inside
	for (int y = 0; y < height - 2; y++)
	{
		cout << '|';

		for (int x = 0; x < width - 2; x++)
		{
			int color = x + y;
			int color_foreground = (color % NUM_COLORS);
			int color_background = (color / NUM_COLORS) % NUM_COLORS;
			cout << Color(COLORS[color_foreground], AC_FOREGROUND) <<
				Color(COLORS[color_background], AC_BACKGROUND) << "O";
		}

		cout << Color(AC_DEFAULT, AC_FOREGROUND) << Color(AC_DEFAULT, AC_BACKGROUND) << '|' << endl;
	}

	// Do bottom
	cout << '\\';
	for (int x = 0; x < width - 2; x++)
		cout << '-';
	cout << '/' << endl;

	return EXIT_SUCCESS;
}
