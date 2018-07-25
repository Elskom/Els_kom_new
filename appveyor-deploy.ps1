if ($env:APPVEYOR_REPO_TAG -eq "false")
{
    $env:APPVEYOR_REPO_TAG_NAME = "v" + $env:APPVEYOR_BUILD_VERSION
}
$env:APPVEYOR_CACHE_ENTRY_ZIP_ARGS = "-t7z -m0=lzma -mx=9"
