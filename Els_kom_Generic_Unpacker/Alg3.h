/*
	Alg3.h
*/

#ifdef __CPLUSPLUS
extern "C" {
#endif

unsigned char* Alg3_Unpacker(std::string FileName, unsigned char* FileData, std::string DestPath);
unsigned char* Alg3_Packer(unsigned char* FileData);
unsigned char* Alg3_Packer_s(unsigned char* FileData);

#ifdef __CPLUSPLUS
}
#endif
