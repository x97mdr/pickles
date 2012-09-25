#!/bin/bash
# To use this on your local machine, change the $MONO and $NUGET variables to point to the locations where you have your mono and NuGet executables
# NOTE - I have been able to get NuGet to execute only when using version 2.10.9, commit d30c777c4d did not work as it gave timeout errors 

MONO="/opt/mono-2.10.9/bin/mono --runtime=v4.0.30319"
NUGET=/home/jeffrey/.local/share/NuGet/NuGet.exe

$MONO $NUGET install "./src/Pickles/Pickles/packages.config" -OutputDirectory "./src/Pickles/packages"
$MONO $NUGET install "./src/Pickles/Pickles/packages.config" -OutputDirectory "./src/Pickles.CommandLine/packages"
$MONO $NUGET install "./src/Pickles/Pickles.Example/packages.config" -OutputDirectory "./src/Pickles/packages"
$MONO $NUGET install "./src/Pickles/Pickles.Example.xUnit/packages.config" -OutputDirectory "./src/Pickles/packages"
$MONO $NUGET install "./src/Pickles/Pickles.MSBuild/packages.config" -OutputDirectory "./src/Pickles/packages"
$MONO $NUGET install "./src/Pickles/Pickles.NAnt/packages.config" -OutputDirectory "./src/Pickles/packages"
$MONO $NUGET install "./src/Pickles/Pickles.PowerShell/packages.config" -OutputDirectory "./src/Pickles/packages"
$MONO $NUGET install "./src/Pickles/Pickles.Test/packages.config" -OutputDirectory "./src/Pickles/packages"
$MONO $NUGET install "./src/Pickles/Pickles.UserInterface/packages.config" -OutputDirectory "./src/Pickles/packages"
