// Advanced Console Library (Windows Platform Support - advconsole.win32.cpp)
// by Jeff Weiner
// http://tilde.planet-nebula.com/projects/advconsole/
//
// Windows-specific implementation file for the advanced console library.

#include <windows.h>
#include <wincon.h>
#include "advconsole.h"
using namespace std;
using namespace AdvancedConsole;

char getUnbufKey()
{
	INPUT_RECORD keyEv;
	DWORD iCharsRead;
	while (ReadConsoleInput(GetStdHandle(STD_INPUT_HANDLE), &keyEv, 2, &iCharsRead))
	{
		if (keyEv.EventType == KEY_EVENT && keyEv.Event.KeyEvent.bKeyDown == TRUE && keyEv.Event.KeyEvent.uChar.AsciiChar != 0)
			return keyEv.Event.KeyEvent.uChar.AsciiChar;
	}
	return -1;
}

// Cursor Functions
ostream& AdvancedConsole::operator<<(ostream& o, const Cursor& c)
{
	COORD oCoord;
	oCoord.Y = oCoord.X = 0;
	if (c.getMovement() == AC_RELATIVE)
	{
		int iY, iX;
		Cursor::getPosition(iY, iX);
		oCoord.Y = static_cast<SHORT>(iY);
		oCoord.X = static_cast<SHORT>(iX);
	}
	oCoord.X += c.getCol() - 1;		// Windows uses (0,0)
	oCoord.Y += c.getRow() - 1;

	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), oCoord);
	return o;
}

void AdvancedConsole::Cursor::getPosition(int& row, int& col)
{
	CONSOLE_SCREEN_BUFFER_INFO oBufferInfo;
	GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), &oBufferInfo);

	row = oBufferInfo.dwCursorPosition.Y + 1;	// Windows uses (0,0)
	col = oBufferInfo.dwCursorPosition.X + 1;
}

void AdvancedConsole::Cursor::getSize(int& rows, int& cols)
{
	CONSOLE_SCREEN_BUFFER_INFO oBufferInfo;
	GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), &oBufferInfo);

	rows = (oBufferInfo.srWindow.Bottom - oBufferInfo.srWindow.Top) + 1;
	cols = (oBufferInfo.srWindow.Right - oBufferInfo.srWindow.Left) + 1;
}

static void setCursor(bool bVisible)
{
	CONSOLE_CURSOR_INFO oCursorInfo;
	GetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &oCursorInfo);
	
	oCursorInfo.bVisible = bVisible;
	SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &oCursorInfo);
}
void AdvancedConsole::Cursor::hide()
{
	setCursor(false);
}
void AdvancedConsole::Cursor::show()
{
	setCursor(true);
}

// Color Functions
ostream& AdvancedConsole::operator<<(ostream& o, const AdvancedConsole::Color& c)
{
	static WORD wCurrentColor = 0;

	WORD wColor = 0;
	AC_COLOR color = c.getColor();
	if (color == AC_DEFAULT)
	{		// Possibly not the best way to handle this...
		if (c.getLayer() == AC_FOREGROUND)
			color = AC_WHITE;
		else
			color = AC_BLACK;
	}
	switch (color)
	{
		case AC_BLACK:
			wColor = 0; break;
		case AC_RED:
			wColor = FOREGROUND_RED; break;
		case AC_GREEN:
			wColor = FOREGROUND_GREEN; break;
		case AC_YELLOW:
			wColor = FOREGROUND_RED | FOREGROUND_GREEN; break;
		case AC_BLUE:
			wColor = FOREGROUND_BLUE; break;
		case AC_MAGENTA:
			wColor = FOREGROUND_RED | FOREGROUND_BLUE; break;
		case AC_CYAN:
			wColor = FOREGROUND_GREEN | FOREGROUND_BLUE; break;
		case AC_WHITE:
		case AC_DEFAULT:
		default:
			wColor = FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE; break;
	}
	if (c.getWeight() == AC_BOLD)
		wColor |= FOREGROUND_INTENSITY;
	if (c.getLayer() == AC_BACKGROUND)
		wColor <<= 4;	// Shift if necessary...

	if (c.getLayer() == AC_BACKGROUND)
		wCurrentColor &= (FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_INTENSITY);
	else
		wCurrentColor &= (BACKGROUND_RED | BACKGROUND_GREEN | BACKGROUND_BLUE | BACKGROUND_INTENSITY);
	wCurrentColor |= wColor;

	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), wCurrentColor);
	return o;
}

// Erase Functions
ostream& AdvancedConsole::operator<<(ostream& o, const Erase& e)
{		// Lame, I know...
	system("cls");
	return o;
}

// Scroll Functions
ostream& AdvancedConsole::operator<<(ostream& o, const Scroll& s)
{		// Even worse!
	return o;
}
