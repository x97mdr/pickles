// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

// Properties
let buildDir = "./build/"
let cmdDir = "./build/exe/"
let msBuildDir = "./build/msbuild/"
let nantDir = "./build/nant/"
let powerShellDir = "./build/powershell/"
let guiDir = "./build/gui/"
let testDir  = "./test/"
let deployDir = "./deploy/"

// version info
let version = "0.18.1"  // or retrieve from CI server
 

// Targets
Target "Clean" (fun _ ->
    CleanDirs [cmdDir; msBuildDir; nantDir; powerShellDir; guiDir; buildDir; testDir; deployDir]
)

Target "AssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "./src/Pickles/VersionInfo.cs"
        [Attribute.Product "Pickles"
         Attribute.Company "Pickles"
         Attribute.Copyright "Copyright (c) Jeffrey Cameron 2010-2012, PicklesDoc 2012-present"
         Attribute.Trademark ""
         Attribute.Culture ""
         Attribute.ComVisible false
         Attribute.Version version
         Attribute.FileVersion version]
)

Target "BuildCmd" (fun _ ->
    !! "src/Pickles/Pickles.CommandLine/Pickles.CommandLine.csproj"
      |> MSBuildRelease cmdDir "Build"
      |> Log "AppBuild-Output: "
)

Target "BuildMsBuild" (fun _ ->
    !! "src/Pickles/Pickles.MsBuild/Pickles.MsBuild.csproj"
      |> MSBuildRelease msBuildDir "Build"
      |> Log "AppBuild-Output: "
)

Target "BuildNAnt" (fun _ ->
    !! "src/Pickles/Pickles.NAnt/Pickles.NAnt.csproj"
      |> MSBuildRelease nantDir "Build"
      |> Log "AppBuild-Output: "
)

Target "BuildPowerShell" (fun _ ->
    !! "src/Pickles/Pickles.PowerShell/Pickles.PowerShell.csproj"
      |> MSBuildRelease powerShellDir "Build"
      |> Log "AppBuild-Output: "
)

Target "BuildGui" (fun _ ->
    !! "src/Pickles/Pickles.UserInterface/Pickles.UserInterface.csproj"
      |> MSBuildRelease guiDir "Build"
      |> Log "AppBuild-Output: "
)

Target "BuildTest" (fun _ ->
    !! "src/Pickles/Pickles.Test/Pickles.Test.csproj"
      |> MSBuildRelease testDir "Build"
      |> Log "AppBuild-Output: "
)

Target "Zip" (fun _ ->
    !! (buildDir + "/**/*.*")
        -- "*.zip"
        |> Zip buildDir (deployDir + "Pickles-" + version + ".zip")
)

Target "Default" (fun _ ->
    trace ("Starting build of Pickles version " + version)
)

// Dependencies
"Clean"
  ==> "AssemblyInfo"
  ==> "BuildCmd"
  ==> "BuildMsBuild"
  ==> "BuildNAnt"
  ==> "BuildPowerShell"
  ==> "BuildGui"
  ==> "BuildTest"
  ==> "Zip"
  ==> "Default"


// start build
RunTargetOrDefault "Default"