if ($env:APPVEYOR_REPO_TAG -eq "false")
{
    git submodule update --init --recursive
}
if ($env:APPVEYOR_REPO_TAG -eq "true")
{
    git submodule update --init --recursive
}
Set-Location -Path externals/newsmake/build
cmake ..
msbuild newsmake.sln /p:Configuration=Release /p:Platform="Win32" /nologo /verbosity:m /m
Set-Location -Path ../../../Misc/NEWS
$env:newsmakeprogpth = Join-Path (Get-Location) ../../externals/newsmake/build/Release/newsmake
# works locally. Why the hell does this not work on AppVeyor.
Start-Process -FilePath $env:newsmakeprogpth -Wait -NoNewWindow
Set-Location -Path ../..
if ($env:PLATFORM -eq "x64")
{
    # ensure file is present in 64 bit build.
    if(!(Test-Path -Path bin/x64/Release))
    {
        mkdir bin/x64/Release
    }
    Move-Item -Path bin\x86\Release\news.txt -Destination bin\x64\Release\news.txt
}
