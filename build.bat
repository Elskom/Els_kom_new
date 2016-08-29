@echo off
set platf=x86
set platf2=x64
set conf=Release
goto :choice

:build_x86_debug
IF EXIST "%SystemDrive%\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" (
"%SystemDrive%\Program Files (x86)\MSBuild\14.0\Bin\msbuild" "%~dp0Els_kom_Solution.sln" /nologo /verbosity:minimal /clp:Summary /p:Platform=%platf%
)
IF EXIST "%SystemDrive%\Program Files\MSBuild\14.0\Bin\msbuild.exe" (
"%SystemDrive%\Program Files\MSBuild\14.0\Bin\msbuild" "%~dp0Els_kom_Solution.sln" /nologo /verbosity:minimal /clp:Summary /p:Platform=%platf%
)
goto :choice2

:build_x86_release
IF EXIST "%SystemDrive%\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" (
"%SystemDrive%\Program Files (x86)\MSBuild\14.0\Bin\msbuild" "%~dp0Els_kom_Solution.sln" /nologo /verbosity:minimal /clp:Summary /p:Configuration=%conf% /p:Platform=%platf%
)
IF EXIST "%SystemDrive%\Program Files\MSBuild\14.0\Bin\msbuild.exe" (
"%SystemDrive%\Program Files\MSBuild\14.0\Bin\msbuild" "%~dp0Els_kom_Solution.sln" /nologo /verbosity:minimal /clp:Summary /p:Configuration=%conf% /p:Platform=%platf%
)
goto :choice3

:build_x64_debug
IF EXIST "%SystemDrive%\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" (
"%SystemDrive%\Program Files (x86)\MSBuild\14.0\Bin\msbuild" "%~dp0Els_kom_Solution.sln" /nologo /verbosity:minimal /clp:Summary /p:Platform=%platf2%
)
IF EXIST "%SystemDrive%\Program Files\MSBuild\14.0\Bin\msbuild.exe" (
"%SystemDrive%\Program Files\MSBuild\14.0\Bin\msbuild" "%~dp0Els_kom_Solution.sln" /nologo /verbosity:minimal /clp:Summary /p:Platform=%platf2%
)
goto :choice4

:build_x64_release
IF EXIST "%SystemDrive%\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" (
"%SystemDrive%\Program Files (x86)\MSBuild\14.0\Bin\msbuild" "%~dp0Els_kom_Solution.sln" /nologo /verbosity:minimal /clp:Summary /p:Configuration=%conf% /p:Platform=%platf2%
)
IF EXIST "%SystemDrive%\Program Files\MSBuild\14.0\Bin\msbuild.exe" (
"%SystemDrive%\Program Files\MSBuild\14.0\Bin\msbuild" "%~dp0Els_kom_Solution.sln" /nologo /verbosity:minimal /clp:Summary /p:Configuration=%conf% /p:Platform=%platf2%
)
goto :eof

:choice
echo Do you want to Build 32 Bit Debug? [Y/N]
set /P c=
if /I "%c%" EQU "Y" goto :build_x86_debug
if /I "%c%" EQU "N" goto :choice2
goto :choice

:choice2
echo Do you want to Build 32 Bit Release? [Y/N]
set /P c=
if /I "%c%" EQU "Y" goto :build_x86_release
if /I "%c%" EQU "N" goto :choice3
goto :choice2

:choice3
echo Do you want to Build 64 Bit Debug? [Y/N]
set /P c=
if /I "%c%" EQU "Y" goto :build_x64_debug
if /I "%c%" EQU "N" goto :choice4
goto :choice3

:choice4
echo Do you want to Build 64 Bit Release? [Y/N]
set /P c=
if /I "%c%" EQU "Y" goto :build_x64_release
if /I "%c%" EQU "N" goto :eof
goto :choice4

:eof
pause
