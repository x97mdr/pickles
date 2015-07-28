@echo off
set "picklesVersion=1.2.0"
set "fakeVersion=3.36.0"

cls

"src\Pickles\packages\FAKE.%fakeVersion%\tools\Fake.exe" build.fsx --envvar version %picklesVersion%
"src\Pickles\packages\FAKE.%fakeVersion%\tools\Fake.exe" nuget.fsx --envvar version %picklesVersion%
"src\Pickles\packages\FAKE.%fakeVersion%\tools\Fake.exe" chocolatey.fsx --envvar version %picklesVersion%

call unzip.cmd %picklesVersion%

FOR %%A IN (testRunnerCmd testRunnerMsBuild testRunnerPowerShell) DO (
call %%A.cmd %picklesVersion%
if errorlevel 1 goto handleerror1orhigher
)


@ECHO all fine
goto end

:handleerror1orhigher

@ECHO Something went wrong!
goto end

:end
