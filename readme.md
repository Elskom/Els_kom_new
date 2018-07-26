# Els_kom_new

![Els_kom Icon](https://github.com/Elskom/Els_kom_new/blob/icon/els_kom.png)

|       | Build | VS2017 (x86) | VS2017 (x64) | VS2017 Preview (x86) | VS2017 Preview (x64) |
|:---------------:  |:---------------:  |:---------------:  |:---------------:  |:---------------:  |:---------------:  |
| Master: | [![Build status](https://ci.appveyor.com/api/projects/status/5ikdee6h3qy6lyum/branch/master?svg=true&passingText=Master%20-%20OK&pendingText=Master%20-%20Pending&failingText=Master%20-%20Failing)](https://ci.appveyor.com/project/AraHaan/els-kom-new) |
| Current: | [![Build status](https://ci.appveyor.com/api/projects/status/5ikdee6h3qy6lyum?svg=true&passingText=Current%20-%20OK&pendingText=Current%20-%20Pending&failingText=Current%20-%20Failing)](https://ci.appveyor.com/project/AraHaan/els-kom-new) |

This is a version of Els_kom for Elsword and Grand Chase KOM Files.

## To Build

1. Run ``git clone https://github.com/Elskom/Els_kom_new.git``. Do not download the master branch as a zip file or build might fail or crash Visual Studio 2017. Also this command obviously requires git for windows to be installed and in your path.
2. Run ``update-newsmake.ps1`` to build the changelog (optional but recommended). Make sure that msbuild from your VS2017 install is in your global path environment variable first before running this. This will also pull changes and automatically rebuild newsmake.
3. Open the ``PCbuild/pcbuild.sln`` solution file in Visual Studio 2017 and then select ``Release``, ``x86`` or ``x64`` and hit ``Build Solution``. Alternatively you can use the same msbuild command above but change ``Win32`` to ``x86`` or ``x64``, and ``newsmake.sln`` to ``PCbuild/pcbuild.sln``.
4. Get a cup of Tea. Build might take a bit.
