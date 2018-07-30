Set-Location -Path externals
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
    git pull -q
    try
    {
        $stream = [System.IO.StreamReader]::new($env:NEWSMAKE_REPO_HEAD)
        $env:NEWSMAKE_NEW_COMMIT_ID = $stream.ReadLine()
    }
    finally
    {
        $stream.close()
    }
    Set-Location -Path ..
}
if (!($env:NEWSMAKE_NEW_COMMIT_ID -eq $env:NEWSMAKE_CURRENT_COMMIT_ID))
{
    Set-Location -Path newsmake/build
    msbuild newsmake.sln /p:Configuration=Release /p:Platform="Win32" /nologo /verbosity:m /m
    Set-Location -Path ../../
}
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
$env:newsmakeprogpth = Join-Path (Get-Location) externals/newsmake/build/Release/newsmake
Set-Location -Path ../Misc/NEWS
$proc = [System.Diagnostics.Process]::new()
$proc.StartInfo.FileName = $env:newsmakeprogpth
$proc.StartInfo.Arguments = ""
$proc.StartInfo.RedirectStandardOutput = true
$proc.StartInfo.UseShellExecute = false
$proc.StartInfo.CreateNoWindow = true
proc.Start()
$newsmake_out = $proc.StandardOutput.ReadToEnd()
$proc.WaitForExit()
$proc.Dispose()
Write-Output $newsmake_out
Set-Location -Path ../..
