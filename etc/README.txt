This folder contains the patch files used by Els_kom to pack and unpack kom files.

These batch files are and can be rather large. This is because of the fact that all kom files must be able to be unpacked.

This executes the command like packer and unpacker executables. (Embedded python 3.6.1+ interpreters on official builds of Els_kom currently).

Why did you decide instead of removing the python stuff to instead put them inside of embedded python?

Well this is because not all systems have python 3.6+. Not only that but I do not want the use of word python in the patch files. This way it helps simplify the batch files and eliminates some places of mistakes.

When building the Console Applications these files should be set up to copy to the output folder automatically.

This folder also contains files that should be copied to make the embedded python interpreters work.
