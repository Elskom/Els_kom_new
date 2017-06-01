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

class Zlib {
public:
	/*
	Compresses the data fed into this
	function to memory and returns it.
	
	Note: It is recommended to feed and assume everything is 
        of the 'unsigned char' or 'unsigned wchar_t' type to
        be safe than sorry later.
	
	Function returns the same data for now until it is fully
    implemented.
	*/
	template<typename T>
	T compress(T data) {
		return data;
	}
	/*
	Decompresses the data fed into this
	function to memory and returns it.
	
	Note: It is recommended to feed and assume everything is
        of the 'unsigned char' or 'unsigned wchar_t' type to
        be safe than sorry later.
	
	Function returns the same data for now until it is fully
    implemented.
	*/
	template<typename T>
	T decompress(T data) {
		return data;
	}
};
