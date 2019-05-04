# Els_kom_new

![Els_kom Icon](./els_kom.png)

|       | Build | VS2017 | VS2019 | VS2019 Preview |
|:---------------:  |:---------------:  |:---------------:  |:---------------:  |:---------------:  |
| Master: | [![Build status](https://ci.appveyor.com/api/projects/status/5ikdee6h3qy6lyum/branch/master?svg=true&passingText=Master%20-%20OK&pendingText=Master%20-%20Pending&failingText=Master%20-%20Failing)](https://ci.appveyor.com/project/AraHaan/els-kom-new) |
| Current: | [![Build status](https://ci.appveyor.com/api/projects/status/5ikdee6h3qy6lyum?svg=true&passingText=Current%20-%20OK&pendingText=Current%20-%20Pending&failingText=Current%20-%20Failing)](https://ci.appveyor.com/project/AraHaan/els-kom-new) |

This is a version of Els_kom for Elsword and Grand Chase KOM Files.

Note: The ``Current`` build also includes the status of the latest pull request.

## To Build

1. Run ``git clone https://github.com/Elskom/Els_kom_new.git``. Do not download the master branch as a zip file or build might fail or crash Visual Studio 2017 or Visual Studio 2019. Also this command obviously requires git for windows to be installed and in your path.
2. Run ``dotnet tool install -g newsmake`` to optionally build the changelog / news file. Eventually I would love to make a Visual Studio project template for newsmake and make the tool support that.
3. Open the ``pcbuild.sln`` solution file in Visual Studio 2017 or in Visual Studio 2019 and then select ``Release``, ``Any CPU`` and hit ``Build Solution``. Alternatively you can use this command: ``dotnet build --configuration Release --ignore-failed-sources``
4. Get a cup of Tea. Build might take a bit.
5. If build of any of the projects in the solution fails because of a xml documentation file is in use that is a known MSBuild issue. Just ignore it and rebuild the solution until no errors show up. MSBuild is retardid sometimes.