$version = "0.13.0"

git pull origin master

start-process ".\tools\nant-0.85\binaries\nant.exe" "-buildfile:project.build pack" -Wait

$out = .\src\Pickles\.nuget\nuget.exe pack .\src\Pickles\Pickles.MSBuild\Pickles.MSBuild.nuspec -Version $version
Write-Host $out
Move-Item .\Pickles.MSBuild.$version.nupkg deploy -Force

$out = .\src\Pickles\.nuget\nuget.exe pack .\src\Pickles\Pickles.CommandLine\Pickles.CommandLine.nuspec -Version $version
Write-Host $out
Move-Item .\Pickles.CommandLine.$version.nupkg deploy -Force

Copy-Item .\src\Pickles\Pickles.CommandLine\bin\Release -Destination .\deploy\pickles-$version\cmd -Recurse -Force
Copy-Item .\src\Pickles\Pickles.MSBuild\bin\Release -Destination .\deploy\pickles-$version\msbuild -Recurse -Force
Copy-Item .\src\Pickles\Pickles.NAnt\bin\Release -Destination .\deploy\pickles-$version\nant -Recurse -Force
Copy-Item .\src\Pickles\Pickles.PowerShell\bin\Release -Destination .\deploy\pickles-$version\powershell -Recurse -Force
Copy-Item .\src\Pickles\Pickles.UserInterface\bin\Release -Destination .\deploy\pickles-$version\ui -Recurse -Force

# New-Item -ItemType directory -Path .\deploy\Html -Force
# $out = .\src\Pickles\Pickles.CommandLine\bin\Debug\Pickles.exe -f=.\src\Pickles\Examples\Features\ -o=.\deploy\Html --df=Html
# Write-Host $out