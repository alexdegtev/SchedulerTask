@echo off
for /f %%x in ('dir /b ..\TestData\TestDataBuilder') do (
	echo %%x
	..\DebuggerConsole\bin\Debug\DebuggerConsole.exe ..\TestData\TestDataBuilder\%%x\
)