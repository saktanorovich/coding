@echo off

set testId=%1

@echo Test %testId%
@echo Test %testId% >> %resfile%

copy .\tests\%testId%.in %infile% > nul

timer.exe %exefile% %time% %memory% >> %resfile%

if exist %outfile% goto next

@echo No output file! >> %resfile%

goto end
:next
	rem java -cp testlib4j.jar;Check.jar ru.ifmo.testlib.CheckerFramework Check .\tests\%testId%.in %outfile% .\tests\%testId%.out
	copy %outfile% .\tests\%testId%.out > nul
:end

rem if exist %infile% del %infile%
rem if exist %outfile% del %outfile%
