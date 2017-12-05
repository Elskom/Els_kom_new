This Folder is for Building the Python parts of Els_kom.

Python 3.6 or newer will be cloned in ../externals/cpython with get_externals.bat using git.

To Make the embedded pythons work:

If komformat.py updates you must update the one in etc/site.zip as well to reflect the changes and use them.
Otherwise the unpacker /packers will not use those changes.
