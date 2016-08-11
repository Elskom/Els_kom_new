/*
	Els_kom_CryptoPP_Wrapper.cpp
*/
/*
	Crypto++ Wrapper for Els_kom to support Algorithm 3 in kom files.
	Note: This is a work in Progress it does not work with Algorithm 3 yet.
*/

#ifdef __CPLUSPLUS
extern "C" {
#endif

#include <Windows.h>
#include <exception>
//TODO: Add zlib to this.
//#include <zlib.h>
//TODO: Add Crypto++ to this as well.
#include "Els_kom_CryptoPP_Wrapper.h"

using namespace std;
static HINSTANCE Els_kom_Wrapper_inst;

int Unpack_Alg0(LPCSTR FileName, byte FileData, LPCSTR DestPath)
{
	try
	{
		//TODO: Add Actual Unpacking here.
	}
	catch (exception Ex)
	{
		//A Error happened while trying to decode so lets save the File byte data provided to the file instead because of a Error.
	}
	return 0;
}

int Unpack_Alg1(LPCSTR FileName, byte FileData, LPCSTR DestPath)
{
	try
	{
		//TODO: Add Actual Unpacking here.
	}
	catch (exception Ex)
	{
		//A Error happened while trying to decode so lets save the File byte data provided to the file instead because of a Error.
	}
	return 0;
}

int Unpack_Alg2(LPCSTR FileName, byte FileData, LPCSTR DestPath)
{
	try
	{
		//TODO: Add Actual Unpacking here.
	}
	catch (exception Ex)
	{
		//A Error happened while trying to decode so lets save the File byte data provided to the file instead because of a Error.
	}
	return 0;
}

int Unpack_Alg3(LPCSTR FileName, byte FileData, LPCSTR DestPath)
{
	try
	{
		//TODO: Add Actual Unpacking here with Crypto++.
	}
	catch (exception Ex)
	{
		//A Error happened while trying to decode so lets save the File byte data provided to the file instead because of a Error.
	}
	return 0;
}

Els_kom_Wrapper_Extern int __cdecl Unpack_KOM_Generic(LPCSTR FileName, byte FileData, LPCSTR DestPath, int Alg)
{
	/*
		This Function is simply a Good way of Calling the Actual Unpackers so that way I can just Export 1 function in total.
		Algorithm 0 is Store so that means that the Bytedata passed in Algorithm 0 must definitely be using zlib.
	*/
	if (Alg == 0)
	{
		//Call Algorithm 0 Unpacking Function.
		Unpack_Alg0(FileName, FileData, DestPath);
	}
	if (Alg == 1)
	{
		//Call Algorithm 1 Unpacking Function.
		Unpack_Alg1(FileName, FileData, DestPath);
	}
	if (Alg == 2)
	{
		//Call Algorithm 2 Unpacking Function.
		Unpack_Alg2(FileName, FileData, DestPath);
	}
	if (Alg == 3)
	{
		//Call Algorithm 3 Unpacking Function.
		Unpack_Alg3(FileName, FileData, DestPath);
	}
	return 0;
}

BOOL WINAPI DllMain(HINSTANCE hDLL, DWORD reason, LPVOID reserved)
{
	switch (reason)
	{
		case DLL_PROCESS_ATTACH:
			Els_kom_Wrapper_inst=hDLL;
			DisableThreadLibraryCalls(hDLL);
			break;
	}
	return TRUE;
}

#ifdef __CPLUSPLUS
}
#endif
