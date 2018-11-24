@echo off

cd "bin/%CONFIGURATION%/"
"Els_kom" -p "./Els_kom_new.%CONFIGURATION%.%APPVEYOR_BUILD_WORKER_IMAGE%.zip"
cd ../..
