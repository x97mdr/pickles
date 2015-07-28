@echo off
set "fakeVersion=3.36.0"

"src\Pickles\packages\FAKE.%fakeVersion%\tools\Fake.exe" test.fsx
