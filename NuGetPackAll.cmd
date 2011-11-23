cd src\Pickles
cd Pickles.CommandLine
call NugetPack.cmd
cd ..\Pickles.MsBuild
call NugetPack.cmd
cd ..\Pickles.NAnt
call NugetPack.cmd
cd \..\..