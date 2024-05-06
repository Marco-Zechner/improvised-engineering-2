@echo off
setlocal

:: Set the destination path
set "DEST=C:\Users\marco\AppData\Roaming\SpaceEngineers\Mods\Improvised Engineering 2\Data\Scripts\improvised-engineering-2"

:: Create the destination directory if it does not exist
if not exist "%DEST%" mkdir "%DEST%"

:: Copy all .cs files from the same directory as the batch file
xcopy *.cs "%DEST%" /Y

:: Copy all directories except bin and obj
for /d %%D in (*) do (
    if /i not "%%D"=="bin" if /i not "%%D"=="obj" if /i not "%%D"=="reference" (
        xcopy "%%D" "%DEST%\%%D" /S /E /I /Y
    )
)

echo Done