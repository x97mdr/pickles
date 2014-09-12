// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake

// Properties
let deployDir  = "./deploy/"
let cmdDir = "./build/exe/"
let msBuildDir = "./build/msbuild/"
let powerShellDir = "./build/powershell/"
let packagingDir = "./packaging/"

// version info
let version = "0.18.1"  // or retrieve from CI server

// Targets
Target "Clean" (fun _ ->
    CleanDirs [packagingDir; ]
)


Target "CreatePackageCommandLine" (fun _ ->
    // Copy all the package files into a package folder
    CopyFiles packagingDir [cmdDir + "pickles.exe"; cmdDir + "NLog.config" ]

    NuGet (fun p -> 
        {p with
            OutputPath = deployDir
            WorkingDir = packagingDir
            Version = version
            Publish = false }) 
            "src/Pickles/Pickles.CommandLine/Pickles.CommandLine.nuspec"
)

Target "CreatePackageMsBuild" (fun _ ->
    // Copy all the package files into a package folder
    CopyFiles packagingDir [msBuildDir + "PicklesDoc.Pickles.MSBuild.Tasks.dll"; ]

    NuGet (fun p -> 
        {p with
            OutputPath = deployDir
            WorkingDir = packagingDir
            Version = version
            Publish = false }) 
            "src/Pickles/Pickles.MSBuild/Pickles.MSBuild.nuspec"
)

Target "CreatePackagePowerShell" (fun _ ->
    // Copy all the package files into a package folder
    CopyFiles packagingDir [powerShellDir + "PicklesDoc.Pickles.PowerShell.dll"; "src/Pickles/Pickles.PowerShell/init.ps1"  ]

    NuGet (fun p -> 
        {p with
            OutputPath = deployDir
            WorkingDir = packagingDir
            Version = version
            Publish = false }) 
            "src/Pickles/Pickles.PowerShell/Pickles.nuspec"
)

Target "CreatePackage" (fun _ -> 
    trace ("Starting build of nuget packages version " + version)
    DeleteDir packagingDir
)

// Dependencies
"Clean"
  ==> "CreatePackageCommandLine"
  ==> "CreatePackageMsBuild"
  ==> "CreatePackagePowerShell"
  ==> "CreatePackage"


// start build
RunTargetOrDefault "CreatePackage"