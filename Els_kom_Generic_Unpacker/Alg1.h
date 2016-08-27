/*
	Alg1.h
*/

#ifdef __CPLUSPLUS
extern "C" {
#endif

unsigned char* Alg1_Unpacker(std::string FileName, unsigned char* FileData, std::string DestPath);
unsigned char* Alg1_Packer(unsigned char* FileData);

#ifdef __CPLUSPLUS
}
#endif
