@echo off
cls

cd ".\src\Pickles\packages\NuGet.CommandLine.2.8.3\tools\"

".\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "..\..\..\..\..\packages" "-ExcludeVersion"

cd ..\..\..\..\..

"packages\FAKE\tools\Fake.exe" build.fsx --envvar version 0.18.2
"packages\FAKE\tools\Fake.exe" nuget.fsx --envvar version 0.18.2
"packages\FAKE\tools\Fake.exe" chocolatey.fsx --envvar version 0.18.2