if ($env:APPVEYOR_REPO_TAG -eq "false")
{
    git submodule update -q --init --recursive
}
if ($env:APPVEYOR_REPO_TAG -eq "true")
{
    git submodule update -q --init --recursive
}
Set-Location -Path externals/newsmake/build
cmake ..
msbuild newsmake.sln /p:Configuration=Release /p:Platform="Win32" /nologo /verbosity:m /m
Set-Location -Path ../../../Misc/NEWS
$env:newsmakeprogpth = Join-Path (Get-Location) ../../externals/newsmake/build/Release/newsmake
Start-Process -FilePath $env:newsmakeprogpth -Wait -NoNewWindow
Set-Location -Path ../..
nuget restore
