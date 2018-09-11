if ($env:APPVEYOR_REPO_TAG -eq "false")
{
    $env:APPVEYOR_REPO_TAG_NAME = "v" + $env:APPVEYOR_BUILD_VERSION
}
