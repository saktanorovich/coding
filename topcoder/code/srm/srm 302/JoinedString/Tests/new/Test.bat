@echo off
@echo Test %1
@echo Test %1 >> %2.res
if exist %3 del %3
if exist %4 del %4
copy %1.in %3 > nul

Timer.exe %2.exe 1000 32768 >> %2.res

if exist %4 goto next
@echo No output file! >> %2.res
goto end
:next
Tester.exe %3 %4 %1.out
:end