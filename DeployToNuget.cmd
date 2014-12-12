@ECHO OFF

REM this script supposes you set the nuget API key by running this command:
REM   NuGet SetApiKey <your key here>

cd .\src\Pickles\packages\NuGet.CommandLine.*\tools

nuget push ..\..\..\..\..\deploy\Pickles.%1.nupkg
nuget push ..\..\..\..\..\deploy\Pickles.CommandLine.%1.nupkg
nuget push ..\..\..\..\..\deploy\Pickles.MSBuild.%1.nupkg

cd ..\..\..\..\..\
