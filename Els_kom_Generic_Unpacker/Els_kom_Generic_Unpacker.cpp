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
// Algorithm Source Files for Unpacking/Repacking Data.
#include "Alg0.h"
#include "Alg1.h"
#include "Alg2.h"
#include "Alg3.h"
//TODO: Add zlib to this.
//#include <zlib.h>
#include "Els_kom_Generic_Unpacker.h"

using namespace std;
static HINSTANCE Els_kom_Wrapper_inst;

int Unpack_Alg0(LPCSTR FileName, byte FileData, LPCSTR DestPath)
{
	try
	{
		Alg0_Unpacker(FileName, FileData, DestPath);
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
		Alg1_Unpacker(FileName, FileData, DestPath);
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
		Alg2_Unpacker(FileName, FileData, DestPath);
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
		Alg3_Unpacker(FileName, FileData, DestPath);
	}
	catch (exception Ex)
	{
		//A Error happened while trying to decode so lets save the File byte data provided to the file instead because of a Error.
	}
	return 0;
}

int Pack_Alg0(LPCSTR FileName, byte FileData, LPCSTR DestPath)
{
	try
	{
		Alg0_Packer(FileName, FileData, DestPath);
	}
	catch (exception Ex)
	{
		//A Error happened while trying to encode so lets save the File byte data provided to the file instead because of a Error.
	}
	return 0;
}

int Pack_Alg1(LPCSTR FileName, byte FileData, LPCSTR DestPath)
{
	try
	{
		Alg1_Packer(FileName, FileData, DestPath);
	}
	catch (exception Ex)
	{
		//A Error happened while trying to encode so lets save the File byte data provided to the file instead because of a Error.
	}
	return 0;
}

int Pack_Alg2(LPCSTR FileName, byte FileData, LPCSTR DestPath)
{
	try
	{
		Alg2_Packer(FileName, FileData, DestPath);
	}
	catch (exception Ex)
	{
		//A Error happened while trying to encode so lets save the File byte data provided to the file instead because of a Error.
	}
	return 0;
}

int Pack_Alg3(LPCSTR FileName, byte FileData, LPCSTR DestPath)
{
	try
	{
		Alg3_Packer(FileName, FileData, DestPath);
	}
	catch (exception Ex)
	{
		//A Error happened while trying to encode so lets save the File byte data provided to the file instead because of a Error.
	}
	return 0;
}

Els_kom_Wrapper_Extern int __cdecl Unpack_KOM_Generic(LPCSTR FileName, byte FileData, LPCSTR DestPath, int Alg)
{
	/*
		This Function is simply a Good way of Calling the Actual Unpackers so that way I can just Export 2 functions in total.
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

Els_kom_Wrapper_Extern int __cdecl Pack_KOM_Generic(LPCSTR FileName, byte FileData, LPCSTR DestPath, int Alg)
{
	/*
		This Function is simply a Good way of Calling the Actual Packers so that way I can just Export 2 functions in total.
	*/
	if (Alg == 0)
	{
		//Call Algorithm 0 Packing Function.
		Pack_Alg0(FileName, FileData, DestPath);
	}
	if (Alg == 1)
	{
		//Call Algorithm 1 Packing Function.
		Pack_Alg1(FileName, FileData, DestPath);
	}
	if (Alg == 2)
	{
		//Call Algorithm 2 Packing Function.
		Pack_Alg2(FileName, FileData, DestPath);
	}
	if (Alg == 3)
	{
		//Call Algorithm 3 Packing Function.
		Pack_Alg3(FileName, FileData, DestPath);
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
