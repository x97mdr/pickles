cd .\deploy\chocolatey
..\..\packages\nuget\nuget.exe install Pickles -Source %cd% -OutputDirectory .\packages
..\..\packages\nuget\nuget.exe install PicklesUI -Source %cd% -OutputDirectory .\packages
cd ..\..

cd .\deploy\nuget
..\..\packages\nuget\nuget.exe install Pickles -Source %cd% -OutputDirectory .\packages
..\..\packages\nuget\nuget.exe install Pickles.CommandLine -Source %cd% -OutputDirectory .\packages
..\..\packages\nuget\nuget.exe install Pickles.MSBuild -Source %cd% -OutputDirectory .\packages
cd ..\..
