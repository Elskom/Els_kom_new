# Els_kom_new

![Els_kom Icon](./els_kom.png)

![.NET Core](https://github.com/Elskom/Els_kom_new/workflows/.NET%20Core/badge.svg?branch=main&event=push)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/1ec89deb386140fa983b577708d952e2)](https://app.codacy.com/gh/Elskom/Els_kom_new)

This is a version of Els_kom for Elsword and Grand Chase KOM Files.

Note: The ``Current`` build also includes the status of the latest pull request.

## To Build

1. Run ``git clone https://github.com/Elskom/Els_kom_new.git``. Do not download the master branch as a zip file or build might fail or crash Visual Studio 2019. Also this command obviously requires git for windows to be installed and in your path.
2. Run ``dotnet tool install -g newsmake`` to optionally build the changelog / news file. Eventually I would love to make a Visual Studio project template for newsmake and make the tool support that.
3. Open the ``pcbuild.sln`` solution file in Visual Studio 2019 and then select ``Release``, ``Any CPU`` and hit ``Build Solution``. Alternatively you can use this command: ``dotnet build --configuration Release --ignore-failed-sources``
4. Get a cup of Tea. Build might take a bit.
5. If build of any of the projects in the solution fails because of a xml documentation file is in use that is a known MSBuild issue. Just ignore it and rebuild the solution until no errors show up. MSBuild is retardid sometimes.
