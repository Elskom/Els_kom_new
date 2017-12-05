@echo off
call get_externals.bat
cd externals\cpython
git pull
REM build/rebuild python if needed.
IF EXIST "PCbuild\win32\python.exe" (
"PCbuild\win32\python.exe" "..\..\Tools\get_python_build_hash.py"
) ELSE (
call "PCbuild\build.bat"
)
cd ..\aes
git pull
cd ..\..
echo building site.zip...
"externals\cpython\PCbuild\win32\python.exe" "Tools/private_encryption.py" -e None
"externals\cpython\PCbuild\win32\python.exe" "Tools/make_site_zip.py"
