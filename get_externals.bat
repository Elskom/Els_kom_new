@echo off
IF NOT EXIST "externals/aes" (
git clone https://github.com/BrianGladman/aes.git ./externals/aes/
)
IF NOT EXIST "externals/cpython" (
git clone https://github.com/python/cpython.git ./externals/cpython/ --branch 3.6
)
