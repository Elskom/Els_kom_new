/*
	Els_kom_Generic_Unpacker.cpp
*/
/*
	Crypto++ Wrapper for Els_kom to support Algorithm 3 in kom files.
	Note: This is a work in Progress it does not work with Algorithm 3 yet.
*/

#ifdef __CPLUSPLUS
extern "C" {
#endif

#include <Windows.h>
#include <string>
#include "Alg0.h"
#include "Alg1.h"
#include "Alg2.h"
#include "Alg3.h"
#include "Els_kom_Generic_Unpacker.h"

static HINSTANCE Els_kom_Wrapper_inst;

unsigned char* Unpack_Alg0(std::string FileName, unsigned char* FileData, std::string DestPath)
{
	unsigned char* data;
	if (FileData != NULL)
	{
		data = Alg0_Unpacker(FileName, FileData, DestPath);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	else
	{
		return NULL;
	}
	return NULL;
}

unsigned char* Unpack_Alg1(std::string FileName, unsigned char* FileData, std::string DestPath)
{
	unsigned char* data;
	if (FileData != NULL)
	{
		data = Alg1_Unpacker(FileName, FileData, DestPath);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	else
	{
		return NULL;
	}
	return NULL;
}

unsigned char* Unpack_Alg2(std::string FileName, unsigned char* FileData, std::string DestPath)
{
	unsigned char* data;
	if (FileData != NULL)
	{
		data = Alg2_Unpacker(FileName, FileData, DestPath);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	else
	{
		return NULL;
	}
	return NULL;
}

unsigned char* Unpack_Alg3(std::string FileName, unsigned char* FileData, std::string DestPath)
{
	unsigned char* data;
	if (FileData != NULL)
	{
		data = Alg3_Unpacker(FileName, FileData, DestPath);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	else
	{
		return NULL;
	}
	return NULL;
}

unsigned char* Pack_Alg0(unsigned char* FileData)
{
	unsigned char* data;
	if (FileData != NULL)
	{
		data = Alg0_Packer(FileData);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	else
	{
		return NULL;
	}
	return NULL;
}

unsigned char* Pack_Alg1(unsigned char* FileData)
{
	unsigned char* data;
	if (FileData != NULL)
	{
		data = Alg1_Packer(FileData);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	else
	{
		return NULL;
	}
	return NULL;
}

unsigned char* Pack_Alg2(unsigned char* FileData)
{
	unsigned char* data;
	if (FileData != NULL)
	{
		data = Alg2_Packer(FileData);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	else
	{
		return NULL;
	}
	return NULL;
}

unsigned char* Pack_Alg3(unsigned char* FileData)
{
	unsigned char* data;
	if (sizeof(FileData) == 1)
	{
		return NULL;
	}
	else if (FileData != NULL)
	{
		data = Alg3_Packer(FileData);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	else
	{
		return NULL;
	}
	return NULL;
}

// Special Algorithm 3 Packing Function (This is Easy to Complete).
unsigned char* Pack_Alg3_s(unsigned char* FileData)
{
	unsigned char* data;
	if (FileData != NULL)
	{
		data = Alg3_Packer_s(FileData);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	else
	{
		return NULL;
	}
	return NULL;
}

Els_kom_Wrapper_Extern unsigned char* __cdecl Unpack_KOM_Generic(std::string FileName, unsigned char* FileData, std::string DestPath, int Alg)
{
	unsigned char* data;
	/*
		This Function is simply a Good way of Calling the Actual Unpackers so that way I can just Export 2 functions in total.
	*/
	if (Alg == 0)
	{
		data = Unpack_Alg0(FileName, FileData, DestPath);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	if (Alg == 1)
	{
		//NOT USED SO UNPACKING FROM ALG 1 IS IMPOSSIBLE.
		data = Unpack_Alg1(FileName, FileData, DestPath);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	if (Alg == 2)
	{
		data = Unpack_Alg2(FileName, FileData, DestPath);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	if (Alg == 3)
	{
		data = Unpack_Alg3(FileName, FileData, DestPath);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	return NULL;
}

Els_kom_Wrapper_Extern unsigned char* __cdecl Pack_KOM_Generic(unsigned char* FileData, int Alg)
{
	unsigned char* data;
	/*
		This Function is simply a Good way of Calling the Actual Packers so that way I can just Export 2 functions in total.
	*/
	if (Alg == 0)
	{
		data = Pack_Alg0(FileData);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	if (Alg == 1)
	{
		//NOT USED SO PACKING TO ALG 1 IS IMPOSSIBLE.
		data = Pack_Alg1(FileData);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	if (Alg == 2)
	{
		data = Pack_Alg2(FileData);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	if (Alg == 3)
	{
		data = Pack_Alg3(FileData);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	if (Alg == 4)  // Special algorithm 3 Packer. Basically this is for when files have the .___ extension.
	{
		data = Pack_Alg3_s(FileData);
		if (data != NULL)
		{
			return data;
		}
		else
		{
			return NULL;
		}
	}
	return NULL;
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
