pushd src\Pickles\Pickles.CommandLine && ..\.nuget\nuget.exe pack Pickles.CommandLine.csproj -Properties Configuration=Release
popd
pushd src\Pickles\Pickles.MSBuild && ..\.nuget\nuget.exe pack Pickles.MSBuild.csproj -Properties Configuration=Release 
popd
pushd src\Pickles\Pickles.NAnt && ..\.nuget\nuget.exe pack Pickles.NAnt.csproj -Properties Configuration=Release 
popd
pushd src\Pickles\Pickles.PowerShell && ..\.nuget\nuget.exe pack Pickles.nuspec -Version 0.3
popd