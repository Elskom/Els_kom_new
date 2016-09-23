// Advanced Console Library (Unix Platform Support - advconsole.unix.cpp)
// by Jeff Weiner
// http://tilde.planet-nebula.com/projects/advconsole/
//
// Unix/linux-specific implementation file for the advanced console library.

#include <iostream>
#include <sstream>
#include <termios.h>	// For Cursor::getUnbufKey()
#include <unistd.h>	// For Cursor::getUnbufKey()
#include "advconsole.h"
using namespace std;
using namespace AdvancedConsole;

// Cursor Functions
ostream& AdvancedConsole::operator<<(ostream& o, const Cursor& c)
{
	if (c.getMovement() == AC_ABSOLUTE)
		o << "\033[" << (c.getRow() > 0 ? c.getRow() : 1) << ";" << (c.getCol() > 0 ? c.getCol() : 1) << "H";
	else
	{
		if (c.getRow() > 0)
			o << "\033[" << c.getRow() << "B";
		else if (c.getRow() < 0)
			o << "\033[" << (c.getRow() * -1) << "A";
		if (c.getCol() > 0)
			o << "\033[" << c.getCol() << "C";
		else if (c.getCol() < 0)
			o << "\033[" << (c.getCol() * -1) << "D";
	}

	return o;
}

char getUnbufKey()
{
	termios oTermOld, oTermNew;
	tcgetattr(STDIN_FILENO, &oTermOld);
	oTermNew = oTermOld;
	oTermNew.c_lflag &= ~(ICANON | ECHO);
	
	tcsetattr(STDIN_FILENO, TCSANOW, &oTermNew);
	char cKey = getchar();
	tcsetattr(STDIN_FILENO, TCSANOW, &oTermOld);
	return cKey;
}
void AdvancedConsole::Cursor::getPosition(int& row, int& col)
{
	cin.sync();
	cout << "\033[6n";
	cout.flush();
	
	row = col = 0;
	bool bReadingCol = false;
	char c;
	do
	{
		c = getUnbufKey();
		if (c == '\033' || c == '[')
			continue;
		else if (c == ';')
			bReadingCol = true;
		else if ('0' <= c && c <= '9')
		{
			if (bReadingCol == false)
				row = ((row * 10) + (c - '0'));
			else
				col = ((col * 10) + (c - '0'));
		}
	} while (c != 'R');
}

void AdvancedConsole::Cursor::getSize(int& rows, int& cols)
{
	int iOldRow, iOldCol;
	getPosition(iOldRow, iOldCol);
	
	int iTempLastRow = iOldRow, iTempLastCol = iOldCol;
	while (true)
	{
		cout << Cursor(100, 100, AC_RELATIVE);
		int iTempRow, iTempCol;
		getPosition(iTempRow, iTempCol);
		
		if (iTempRow != iTempLastRow || iTempCol != iTempLastCol)
		{
			iTempLastRow = iTempRow;
			iTempLastCol = iTempCol;
		}
		else
			break;
	}
	
	cout << Cursor(iOldRow, iOldCol, AC_ABSOLUTE);
	rows = iTempLastRow;
	cols = iTempLastCol;
}

void AdvancedConsole::Cursor::hide()
{
	cout << "\033[?25l";
	cout.flush();
}
void AdvancedConsole::Cursor::show()
{
	cout << "\033[?25h";
	cout.flush();
}

// Erase Functions
ostream& AdvancedConsole::operator<<(ostream& o, const Erase& e)
{
	return (o << "\033[" << e.getRegion() << static_cast<char>('J' + e.getTarget()));
}

// Scroll Functions
ostream& AdvancedConsole::operator<<(ostream& o, const Scroll& s)
{
	return (o << "\033[" << s.getNum() << static_cast<char>('S' + s.getDir()));
}

// Color Functions
ostream& AdvancedConsole::operator<<(ostream& o, const Color& c)
{
	static int lastForeground = 0;
	static int lastBackground = 0;
	
	if (c.getLayer() == AC_BACKGROUND)
		lastBackground = (c.getColor() != AC_DEFAULT ? 40 + c.getColor() : 0);
	else
		lastForeground = (c.getColor() != AC_DEFAULT ? 30 + c.getColor() : 0);
	
	bool bold = (c.getLayer() == AC_FOREGROUND ? (c.getWeight() == AC_BOLD) : false);
	o << "\033[" << (bold ? 1 : 0) << ';' << lastForeground;
	if (lastBackground != 0)	// If we don't incldue this, it resets everything...
		o << ';' << lastBackground;
	return (o << 'm');
}
