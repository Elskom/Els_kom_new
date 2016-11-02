/*
	packerconsole.h
*/

/*
	To Avoid Compile Issues later.
*/
#ifndef HAVE_PACKERCONSOLE_H_
	#define HAVE_PACKERCONSOLE_H_
	int print(std::string data, bool Error, bool warn, bool input);
	int WhenStartedDirectly();
#else
#endif
