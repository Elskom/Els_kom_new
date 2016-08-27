/*
	Alg2.h
*/

#ifdef __CPLUSPLUS
extern "C" {
#endif

unsigned char* Alg2_Unpacker(std::string FileName, unsigned char* FileData, std::string DestPath);
unsigned char* Alg2_Packer(unsigned char* FileData);

#ifdef __CPLUSPLUS
}
#endif
