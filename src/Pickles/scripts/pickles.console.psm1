function Pickle-Features($FeatureDirectory, @OutputDirectory, @Language) {
    Write-Host "FeatureDirectory: " + $FeatureDirectory
    Write-Host "OutputDirectory: " + $OutputDirectory
    Write-Host "Langauge: " + $Language
}

Register-TabExpansion 'Pickle-Features' @{
    FeatureDirectory = { 
        "AspNet",
        "Self",
        "Kayak",
        "Wcf"
    }
    
    OutputDirectory = { 
		"C:\temp\out"
    }
    
    Langauge = {
        "sv",
        "en"
    }
}

Export-ModuleMember Pickle-Features