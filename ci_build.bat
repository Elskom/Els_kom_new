@echo off
call get_externals.bat
cd externals\cpython
git pull
REM build/rebuild python if needed.
IF EXIST ".\PCbuild\win32\python.exe" (
"PCbuild\win32\python.exe" "..\..\Python\get_python_build_hash.py"
) ELSE (
call PCBuild\build.bat
)
cd ..\aes
git pull
cd ..\..
