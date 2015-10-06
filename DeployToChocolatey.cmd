@ECHO OFF

REM this script supposes you set the chocolatey API key by running this command:
REM   choco apikey -s"https://chocolatey.org/" -k="<your key here>"
REM Mind the trailing slash!

cd .\deploy\chocolatey\

choco push pickles.%1.nupkg
choco push picklesui.%1.nupkg

cd ..\..\
