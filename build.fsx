// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

//RestorePackages()

// Properties
let buildDir = "./build/"
let cmdDir = "./build/cmd/"
let testDir  = "./test/"
let deployDir = "./deploy/"

// version info
let version = "0.18.0"  // or retrieve from CI server
 

// Targets
Target "Clean" (fun _ ->
    CleanDirs [cmdDir; buildDir; testDir; deployDir]
)

Target "AssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "./src/Pickles/VersionInfo.cs"
        [Attribute.Product "Pickles"
         Attribute.Company "Pickles"
         Attribute.Copyright "Copyright (c) Jeffrey Cameron 2010-2012, PicklesDoc 2012-2013"
         Attribute.Trademark ""
         Attribute.Culture ""
         Attribute.ComVisible false
         Attribute.Version version
         Attribute.FileVersion version]
)

Target "BuildApp" (fun _ ->
    !! "src/Pickles/**/*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "AppBuild-Output: "
)

Target "BuildCmd" (fun _ ->
    !! "src/Pickles/Pickles.CommandLine/Pickles.CommandLine.csproj"
      |> MSBuildRelease cmdDir "Build"
      |> Log "AppBuild-Output: "
)

Target "Test" (fun _ ->
    !! (testDir + "/NUnit.Test.*.dll")
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true;
             OutputFile = testDir + "TestResults.xml" })
)


Target "Zip" (fun _ ->
    !! (buildDir + "/**/*.*")
        -- "*.zip"
        |> Zip buildDir (deployDir + "Calculator." + version + ".zip")
)

Target "FxCop" (fun () ->  
    !! (buildDir + @"\**\*.dll") 
    ++ (buildDir + @"\**\*.exe") 
    |> FxCop 
        (fun p -> 
            {p with 
              // override default parameters
              ReportFileName = testDir + "FXCopResults.xml"
              FailOnError = FxCopErrorLevel.Error
              ToolPath = "tools/fxcop/FxCopCmd.exe"})
)

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

// Dependencies
"Clean"
  ==> "AssemblyInfo"
  ==> "BuildCmd"
  //==> "FxCop"
//  ==> "Test"
  //==> "Zip"
  ==> "Default"


// start build
RunTargetOrDefault "Default"