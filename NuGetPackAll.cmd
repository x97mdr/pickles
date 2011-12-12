cd src\Pickles
cd Pickles
call NugetPack.cmd %1
cd ..\Pickles.CommandLine
call NugetPack.cmd
cd ..\Pickles.MsBuild
call NugetPack.cmd
cd ..\Pickles.NAnt
call NugetPack.cmd
cd \..\..