Set-Location -Path externals
$env:nsmkpth = Join-Path (Get-Location) newsmake
if(!(Test-Path -Path $env:nsmkpth))
{
    git clone -q https://github.com/Elskom/newsmake.git
    Set-Location -Path newsmake/build
    cmake ..
}
else
{
    Set-Location -Path newsmake
    git pull -q
    Set-Location -Path ..
}
Set-Location -Path newsmake/build
msbuild newsmake.sln /p:Configuration=Release /p:Platform="Win32" /nologo /verbosity:m /m
Set-Location -Path ../../
$env:zlibnetpth = Join-Path (Get-Location) ZLIB.NET
if(!(Test-Path -Path $env:zlibnetpth))
{
    git clone -q https://github.com/Elskom/ZLIB.NET.git
}
else
{
    # reclone to ensure up to date.
    rmdir ZLIB.NET
    git clone -q https://github.com/Elskom/ZLIB.NET.git
}
Set-Location -Path ../Misc/NEWS
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
