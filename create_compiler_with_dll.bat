@echo off
cmd /c .\compile.bat compiler_creator.cs /nologo
.\compiler_creator.exe
del .\compiler_creator.exe
set csc=