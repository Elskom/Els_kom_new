/*
Header and Implementation to the Zlib class.
*/
/*
This is an attempt to pring python's simple to the
core zlib module to C++. This is because in
python you can memory compress anything using
zlib without any manual work on your end to
set up zlib or anything. As such this is why
this class is needed to help users use zlib
like they do in Python with minimal hardships
trying to figure out why it wont work.
*/
#include "../../externals/zlib/zlib.h"


class Zlib {
public:
	/*
	Compresses the data fed into this
	function to memory and returns it.

	Note: Function returns the same data for now until it is fully implemented.
	*/
	unsigned char *compress(unsigned char *data) {
		return data;
	}
	/*
	Decompresses the data fed into this
	function to memory and returns it.
	Note: Function returns the same data for now until it is fully implemented.
	*/
	unsigned char *decompress(unsigned char *data) {
		return data;
	}
	/*
	alculates the CRC32 of the data.
	Note: Function returns the same data value for now until it is fully implemented.
	*/
	unsigned int CRC32(unsigned int will_be_fixed_later) {
		return will_be_fixed_later;
	}
	/*
	alculates the adler32 of the data.
	Note: Function returns the same data value for now until it is fully implemented.
	*/
	unsigned int adler32(unsigned int will_be_fixed_later) {
		return will_be_fixed_later;
	}
};

