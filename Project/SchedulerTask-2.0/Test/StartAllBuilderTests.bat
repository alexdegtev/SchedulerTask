@echo off
for /f %%x in ('dir /b ..\TestData\TestDataBuilder') do (
	echo %%x
	..\BuilderConsole\bin\Debug\BuilderConsole.exe ..\TestData\TestDataBuilder\%%x\
)