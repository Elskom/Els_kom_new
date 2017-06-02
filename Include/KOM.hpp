/*
	KOM.hpp
*/
/*
	file that holds the internal data structures of the kom file format.
*/

struct KOMFormat {
	//TODO: Populate this.
}

struct XMLData {
	//Hopefully this will work correctly.
	// This means that <vector> must be included.
	std::vector<FileEntry> files;
}

struct FileEntry {
	std::string Name;
	int Size;
	int CompressedSize;
	auto Checksum;  //unknown type currently. (could be float or int idk)
	auto FileTime;  //unknown type currently. (could be float or int idk)
	int Algorithm;  // Could be 0, 1, 2, or 3. (1 is unused normally)
}
