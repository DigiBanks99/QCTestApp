echo off
cd /D %~dp0
cd ..\Generators
%comspec% /k ""%VS120COMNTOOLS%\..\..\VC\bin\vcvars32.bat"" x86
echo on