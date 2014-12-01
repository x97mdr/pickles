// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake

// Properties
let cmdDir = "./build/exe/"
let deployDir  = "./deploy/chocolatey/"
let packagingDir = "./packaging/"
let chocoDir = "./chocolatey/"

// version info
let version = environVar "version" // or retrieve from CI server

Target "Clean" (fun _ ->
    CleanDirs [deployDir; packagingDir]
)

Target "CreatePackage CMD" (fun _ ->
    CopyFiles packagingDir [cmdDir + "Pickles.exe"; cmdDir + "NLog.config"]
    WriteFile (packagingDir + "version.ps1") [("$version = \"" + version + "\"")]
    NuGet (fun p ->
        {p with
            OutputPath = deployDir
            WorkingDir = packagingDir
            Version = version
            Publish = false })
            (chocoDir + "Pickles.nuspec")
)


Target "Default" (fun _ ->
    trace ("Starting build of Pickles version " + version)
    DeleteDir packagingDir
)


// Dependencies
"Clean"
  ==> "CreatePackage CMD"
  ==> "Default"


// start build
RunTargetOrDefault "Default"

