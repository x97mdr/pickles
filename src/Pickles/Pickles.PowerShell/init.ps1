param($installPath, $toolsPath, $package)

Import-Module (Join-Path $toolsPath Pickles.PowerShell.dll)

Write-Host "#############################"
Write-Host "Pickles installed."
Write-Host "Comand usage (switches in brackets are optional):"
Write-Host "Pickle-Features -FeatureDirectory -OutputDirectory [-Language] [-TestResultsFile] [-SystemUnderTestName] [-SystemUnderTestVersion]"
Write-Host "#############################"
