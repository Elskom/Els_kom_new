// Advanced Console Library (Core - advconsole.cpp)
// by Jeff Weiner
// http://tilde.planet-nebula.com/projects/advconsole/
//
// Platform-independent implementation file for the advanced console library.

#include "advconsole.h"
using namespace AdvancedConsole;

// Cursor Functions
Cursor::Cursor(int row, int col, AC_CURSORMVMT mvmt)
	: r(row), c(col), m(mvmt)
{}
int Cursor::getRow() const
{
	return r;
}
int Cursor::getCol() const
{
	return c;
}
AC_CURSORMVMT Cursor::getMovement() const
{
	return m;
}

// Erase Functions
Erase::Erase(AC_ERASEREGION region, AC_ERASETARGET target)
	: r(region), t(target)
{}
AC_ERASEREGION Erase::getRegion() const
{
	return r;
}
AC_ERASETARGET Erase::getTarget() const
{
	return t;
}

// Scroll Functions
Scroll::Scroll(int num, AC_SCROLLDIR dir)
	: n(num), d(dir)
{}
int Scroll::getNum() const
{
	return n;
}
AC_SCROLLDIR Scroll::getDir() const
{
	return d;
}

// Color Functions
Color::Color(AC_COLOR color, AC_COLORLAYER layer, AC_COLORWEIGHT weight)
	: c(color), l(layer), w(weight)
{}
Color::Color(AC_COLOR color, AC_COLORWEIGHT weight)
	: c(color), l(AC_FOREGROUND), w(weight)
{}
AC_COLOR Color::getColor() const
{
	return c;
}
AC_COLORLAYER Color::getLayer() const
{
	return l;
}
AC_COLORWEIGHT Color::getWeight() const
{
	return w;
}
