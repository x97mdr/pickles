$version = $args[0]
$formats = @("Html", "Dhtml", "Word", "Excel", "Dita")
foreach ($format in $formats)
{
    # Setup variables
    $FeatureDirectory = ".\src\Pickles\Examples"
    $OutputDirectory = ".\Output\" + $format
    $DocumentationFormat = $format
    $SystemUnderTestName = "Pickles"
    $SystemUnderTestVersion = $version
    $TestResultsFormat = ""
    $TestResultsFile = ""

    # Import the Pickles-comandlet
    $pathToModule = ".\deploy\pickles-" + $version + "\powershell\PicklesDoc.Pickles.PowerShell.dll"
    Import-Module $pathToModule

    # Call pickles
    Pickle-Features -FeatureDirectory $FeatureDirectory  `
                    -OutputDirectory $OutputDirectory  `
                    -DocumentationFormat $DocumentationFormat `
                    -SystemUnderTestName $SystemUnderTestName  `
                    -SystemUnderTestVersion $SystemUnderTestVersion  `
                    -TestResultsFormat $TestResultsFormat  `
                    -TestResultsFile $TestResultsFile
}
