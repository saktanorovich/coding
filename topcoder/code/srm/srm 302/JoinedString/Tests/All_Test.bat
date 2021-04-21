@echo off
if %1. == . goto noparm
if exist %1.res del %1.res
echo Program to test: %1 >> %1.res
echo ................ >> %1.res 
for %%i in (01,02,03,04,05,06,07,08,09,10,11,12) do call test.bat %%i %1 %2 %3
if exist %2 del %2
if exist %3 del %3
exit
:noparm
@echo Usage: test_all filename
@echo filename must be without extension!