// Advanced Console Library (Core Header - advconsole.h)
// by Jeff Weiner
// http://tilde.planet-nebula.com/projects/advconsole/
//
// Header file for the advanced console library.

#include <iosfwd>

char getUnbufKey();

namespace AdvancedConsole
{
	// Cursor Functions
	enum AC_CURSORMVMT
	{
		AC_RELATIVE,
		AC_ABSOLUTE
	};
	class Cursor
	{
		private:
			const int r, c;
			const AC_CURSORMVMT m;
		
		public:
			Cursor(int row, int col, AC_CURSORMVMT mvmt = AC_ABSOLUTE);
			int getRow() const;
			int getCol() const;
			AC_CURSORMVMT getMovement() const;
			
			static void getPosition(int& row, int& col);
			static void getSize(int& rows, int& cols);
			static void hide();
			static void show();
	};
	std::ostream& operator<<(std::ostream& o, const Cursor& c);
	
	// Erase Functions
	enum AC_ERASEREGION
	{
		AC_AFTER,
		AC_BEFORE,
		AC_ALL
	};
	enum AC_ERASETARGET
	{
		AC_SCREEN,
		AC_LINE
	};
	class Erase
	{
		private:
			const AC_ERASEREGION r;
			const AC_ERASETARGET t;
		
		public:
			Erase(AC_ERASEREGION region = AC_ALL, AC_ERASETARGET target = AC_SCREEN);
			AC_ERASEREGION getRegion() const;
			AC_ERASETARGET getTarget() const;
	};
	std::ostream& operator<<(std::ostream& o, const Erase& e);
	
	// Scroll Functions
	enum AC_SCROLLDIR
	{
		AC_UP,
		AC_DOWN
	};
	class Scroll
	{
		private:
			const int n;
			const AC_SCROLLDIR d;
		
		public:
			Scroll(int num, AC_SCROLLDIR dir = AC_DOWN);
			int getNum() const;
			AC_SCROLLDIR getDir() const;
	};
	std::ostream& operator<<(std::ostream& o, const Scroll& s);
	
	// Color Functions
	enum AC_COLOR
	{
		AC_BLACK,
		AC_RED,
		AC_GREEN,
		AC_YELLOW,
		AC_BLUE,
		AC_MAGENTA,
		AC_CYAN,
		AC_WHITE,
		AC_DEFAULT
	};
	enum AC_COLORLAYER
	{
		AC_FOREGROUND,
		AC_BACKGROUND
	};
	enum AC_COLORWEIGHT
	{
		AC_NORMAL,
		AC_BOLD
	};
	class Color
	{
		private:
			const AC_COLOR c;
			const AC_COLORLAYER l;
			const AC_COLORWEIGHT w;
		
		public:
			Color(AC_COLOR color = AC_DEFAULT, AC_COLORLAYER layer = AC_FOREGROUND, AC_COLORWEIGHT weight = AC_NORMAL);
			Color(AC_COLOR color, AC_COLORWEIGHT weight);
			AC_COLOR getColor() const;
			AC_COLORLAYER getLayer() const;
			AC_COLORWEIGHT getWeight() const;
	};
	std::ostream& operator<<(std::ostream& o, const Color& c);
}
