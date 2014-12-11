@ECHO OFF

REM this script supposes you set the chocolatey API key by running this command:
REM   NuGet SetApiKey <your key here> -source https://chocolatey.org/
REM Mind the trailing slash!

cd .\deploy\chocolatey\

choco push pickles.%1.nupkg
choco push picklesui.%1.nupkg

cd ..\..\
