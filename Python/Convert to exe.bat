@echo off
cd %~dp0
"%SystemDrive%\Python352\Scripts\pyinstaller.exe" --console -F -i "%~dp0\pyc.ico" -m "%~dp0\manifest.xml" --upx-dir "%~dp0\upx" --distpath "%~dp0\Build" "%~dp0\kompact_new.py"
"%SystemDrive%\Python352\Scripts\pyinstaller.exe" --console -F -i "%~dp0\pyc.ico" -m "%~dp0\manifest.xml" --upx-dir "%~dp0\upx" --distpath "%~dp0\Build" "%~dp0\komextract_new.py"
pause
"%SystemDrive%\Python352x64\Scripts\pyinstaller.exe" --console -F -i "%~dp0\pyc.ico" -m "%~dp0\manifest.xml" --upx-dir "%~dp0\upx" --distpath "%~dp0\Build" "%~dp0\kompact_new.py"
"%SystemDrive%\Python352x64\Scripts\pyinstaller.exe" --console -F -i "%~dp0\pyc.ico" -m "%~dp0\manifest.xml" --upx-dir "%~dp0\upx" --distpath "%~dp0\Build" "%~dp0\komextract_new.py"
pause