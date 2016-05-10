@ECHO OFF

REM this script supposes you set the nuget API key by running this command:
REM   NuGet SetApiKey <your key here> -Source https://www.nuget.org/api/v2/package

.\packages\nuget\nuget push .\deploy\nuget\Pickles.%1.nupkg -Source https://www.nuget.org/api/v2/package
.\packages\nuget\nuget push .\deploy\nuget\Pickles.CommandLine.%1.nupkg -Source https://www.nuget.org/api/v2/package
.\packages\nuget\nuget push .\deploy\nuget\Pickles.MSBuild.%1.nupkg -Source https://www.nuget.org/api/v2/package
