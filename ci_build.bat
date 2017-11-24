call get_externals.bat
cd externals\cpython
REM update cpython extenal if needed.
git pull
cd ..\aes
REM update aes extenal if needed.
git pull
cd ..\..
REM rebuild / build cpython.
call externals\cpython\PCBuild\build.bat
