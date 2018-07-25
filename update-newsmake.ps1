Set-Location -Path externals
Set-Location -Path newsmake
try
{
    $env:NEWSMAKE_REPO_HEAD = Join-Path (Get-Location) .git/HEAD
    $stream = [System.IO.StreamReader]::new($env:NEWSMAKE_REPO_HEAD)
    $env:NEWSMAKE_CURRENT_COMMIT_ID = $stream.ReadLine()
}
finally
{
    $stream.close()
}
Set-Location -Path ..
$env:nsmkpth = Join-Path (Get-Location) newsmake
if(!(Test-Path -Path $env:nsmkpth))
{
    git clone -q https://github.com/Elskom/newsmake.git
    Set-Location -Path newsmake/build
    cmake ..
    Set-Location -Path ../..
}
else
{
    Set-Location -Path newsmake
    git pull -q
    Set-Location -Path ..
}
Set-Location -Path newsmake/build
try
{
    $stream = [System.IO.StreamReader]::new($env:NEWSMAKE_REPO_HEAD)
    $env:NEWSMAKE_NEW_COMMIT_ID = $stream.ReadLine()
}
finally
{
    $stream.close()
}
if (!($env:NEWSMAKE_NEW_COMMIT_ID -eq $env:NEWSMAKE_CURRENT_COMMIT_ID))
{
    msbuild newsmake.sln /p:Configuration=Release /p:Platform="Win32" /nologo /verbosity:m /m
}
Set-Location -Path ../../
$env:zlibnetpth = Join-Path (Get-Location) ZLIB.NET
if(!(Test-Path -Path $env:zlibnetpth))
{
    git clone -q https://github.com/Elskom/ZLIB.NET.git --branch patches
}
else
{
    # reclone to ensure up to date.
    rmdir ZLIB.NET
    git clone -q https://github.com/Elskom/ZLIB.NET.git --branch patches
}
Set-Location -Path ../Misc/NEWS
"../../externals/newsmake/build/Release/newsmake"
Set-Location -Path ../..
