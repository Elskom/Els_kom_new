@echo off
cd %~dp0
"%SystemDrive%\python35\Scripts\pyinstaller.exe" --console -F -i "%~dp0\pyc.ico" -m "%~dp0\manifest.xml" --upx-dir "%~dp0\upx" --distpath "%~dp0\Build" "%~dp0\kompact_new.py"
"%SystemDrive%\python35\Scripts\pyinstaller.exe" --console -F -i "%~dp0\pyc.ico" -m "%~dp0\manifest.xml" --upx-dir "%~dp0\upx" --distpath "%~dp0\Build" "%~dp0\komextract_new.py"
pause