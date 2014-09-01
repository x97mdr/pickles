@echo off
cls

cd ".\src\Pickles\packages\NuGet.CommandLine.2.8.2\tools\"

".\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "..\..\..\..\..\packages" "-ExcludeVersion"

cd ..\..\..\..\..

"packages\FAKE\tools\Fake.exe" build.fsx
