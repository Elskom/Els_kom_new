@echo off

cd Plugins
git clone -q https://github.com/Elskom/Els_kom_plugins.git
cd ..
msbuild Plugins\Els_kom_plugins\DefaultPlugins.sln /m /verbosity:minimal
cd "bin/%CONFIGURATION%/"
"Els_kom" -p "./Els_kom_new.%CONFIGURATION%.%APPVEYOR_BUILD_WORKER_IMAGE%.zip"
cd ../..
