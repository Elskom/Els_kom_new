/*
* memimp_setup.c
*/
#include <windows.h>
#include <stdio.h>
//#include <shlobj.h>
#include <Python.h>
//#include <marshal.h>

#include "../externals/py2exe/source/MyLoadLibrary.h"
#include "../externals/py2exe/source/python-dynload.h"

BOOL calc_dirname(HMODULE hmod)
{
	int is_special;
	wchar_t *modulename_start;
	wchar_t *cp;

	// get module filename
	if (!GetModuleFileNameW(hmod, modulename, sizeof(modulename))) {
		SystemError(GetLastError(), "Retrieving module name");
		return FALSE;
	}
	// get directory of modulename.  Note that in some cases
	// (eg, ISAPI), GetModuleFileName may return a leading "\\?\"
	// (which is a special format you can pass to the Unicode API
	// to avoid MAX_PATH limitations).  Python currently can't understand
	// such names, and as it uses the ANSI API, neither does Windows!
	// So fix that up here.
	is_special = wcslen(modulename) > 4 &&
		wcsncmp(modulename, L"\\\\?\\", 4)==0;
	modulename_start = is_special ? modulename + 4 : modulename;
	wcscpy(dirname, modulename_start);
	cp = wcsrchr(dirname, L'\\');
	*cp = L'\0';
	return TRUE;
}

HMODULE load_pythondll(void)
{
	HMODULE hmod_pydll;
	HRSRC hrsrc;
	HMODULE hmod = LoadLibraryExW(libfilename, NULL, LOAD_LIBRARY_AS_DATAFILE);

	// Try to locate pythonxy.dll as resource in the exe
	hrsrc = FindResourceA(hmod, MAKEINTRESOURCEA(1), PYTHONDLL);
	if (hrsrc) {
		HGLOBAL hgbl;
		DWORD size;
		char *ptr;
		hgbl = LoadResource(hmod, hrsrc);
		size = SizeofResource(hmod, hrsrc);
		ptr = LockResource(hgbl);
		hmod_pydll = MyLoadLibrary(PYTHONDLL, ptr, NULL);
	} else
		/*
		  XXX We should probably call LoadLibraryEx with
		  LOAD_WITH_ALTERED_SEARCH_PATH so that really our own one is
		  used.
		 */
		hmod_pydll = LoadLibraryA(PYTHONDLL);
	FreeLibrary(hmod);
	return hmod_pydll;
}

int _dummy_init(HMODULE hmod_exe)
{

	int rc = 0;
	HMODULE hmod_pydll;

	calc_dirname(hmod_exe);

	hmod_pydll = load_pythondll();
	if (hmod_pydll == NULL) {
		printf("FATAL ERROR: Could not load python library\n");
		return -1;
	}
	if (PythonLoaded(hmod_pydll) < 0) {
		printf("FATAL ERROR: Failed to load some Python symbols\n");
		return -1;
	}

	return rc;
}
