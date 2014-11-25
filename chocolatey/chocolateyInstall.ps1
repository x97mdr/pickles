
$packageName = "pickles"
$toolsPath = Split-Path -parent $MyInvocation.MyCommand.Definition

# Newer choco.exe will support passing the package's version number to this script; so this
# can eventually be cleaned up and version.ps1 removed.
Write-Debug "toolsPath is: $toolsPath"
$versionFile = Join-Path $toolsPath "version.ps1"
Import-Module $versionFile
$url = "https://github.com/picklesdoc/pickles/releases/download/v$version/Pickles-exe-$version.zip"

Install-ChocolateyZipPackage $packageName $url $toolsPath
