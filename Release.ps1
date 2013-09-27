git pull origin master

start-process ".\tools\nant-0.85\binaries\nant.exe" "-buildfile:project.build pack" -Wait

$version = "0.11.0"

$out = .\src\Pickles\.nuget\nuget.exe pack .\src\Pickles\Pickles.MSBuild\Pickles.MSBuild.nuspec -Version $version
Write-Host $out
Move-Item .\Pickles.MSBuild.$version.nupkg deploy -Force

$out = .\src\Pickles\.nuget\nuget.exe pack .\src\Pickles\Pickles.CommandLine\Pickles.CommandLine.nuspec -Version $version
Write-Host $out
Move-Item .\Pickles.CommandLine.$version.nupkg deploy -Force

# New-Item -ItemType directory -Path .\deploy\Html -Force
# $out = .\src\Pickles\Pickles.CommandLine\bin\Debug\Pickles.exe -f=.\src\Pickles\Examples\Features\ -o=.\deploy\Html --df=Html
# Write-Host $out