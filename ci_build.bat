@echo off
call get_externals.bat
cd externals\cpython
git pull
REM build/rebuild python if needed.
"PCbuild\win32\python.exe" "..\..\Python\get_python_build_hash.py"
cd ..\aes
git pull
cd ..\..
