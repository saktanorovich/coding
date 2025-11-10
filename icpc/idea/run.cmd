@echo off

set infile="input.txt"
set outfile="output.txt"
set exefile="java -Xmx1024M -Xss64M -cp . Main"
set resfile="results.res"
set time=40000
set memory=1048576

if exist %resfile% del %resfile%

echo Program to test: %exefile% >> %resfile%

echo ................ >> %resfile%

for %%i in (.\tests\*.in) do call test.cmd %%~ni

pause