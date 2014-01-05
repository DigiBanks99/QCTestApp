IF DEFINED FrameworkDir32 goto SKIP:
IF DEFINED VS120COMNTOOLS (call "%VS120COMNTOOLS%\..\..\VC\bin\vcvars32.bat") ELSE IF DEFINED VS110COMNTOOLS (call "%VS110COMNTOOLS%\..\..\VC\bin\vcvars32.bat") ELSE IF DEFINED VS100COMNTOOLS (call "%VS100COMNTOOLS%\..\..\VC\bin\vcvars32.bat") ELSE IF DEFINED VS90COMNTOOLS (call "%VS90COMNTOOLS%\..\..\VC\bin\vcvars32.bat") ELSE (call "C:\Program Files\Microsoft Visual Studio 9.0\VC\bin\vcvars32.bat")
:SKIP
IF DEFINED CHECKOUTPATH ( cd %CHECKOUTPATH%\QCTestDB) ELSE (cd ..\QCTestDB)

@title Build And Deploy DB

MSBuild /m QCTestDB.sln  /consoleloggerparameters:ErrorsOnly /verbosity:minimal
msbuild /m /t:Publish /p:SqlPublishProfilePath=Debug.publish.xml QCTestDB.sln  /consoleloggerparameters:ErrorsOnly /verbosity:minimal

cd ..\Generators
CreateBaseData.bat
pause