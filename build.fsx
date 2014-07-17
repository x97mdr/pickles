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
let version = "0.2"  // or retrieve from CI server
let commitHash = Git.Information.getCurrentSHA1(".")
 

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; cmdDir; testDir; deployDir]
)

Target "AssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "./src/app/Calculator/Properties/AssemblyInfo.cs"
        [Attribute.Title "Calculator Command line tool"
         Attribute.Description "Sample project for FAKE - F# MAKE"
         Attribute.Guid "A539B42C-CB9F-4a23-8E57-AF4E7CEE5BAA"
         Attribute.Product "Calculator"
         Attribute.Version version
         Attribute.FileVersion version
         Attribute.Metadata("githash", commitHash)]

    CreateCSharpAssemblyInfo "./src/app/CalculatorLib/Properties/AssemblyInfo.cs"
        [Attribute.Title "Calculator library"
         Attribute.Description "Sample project for FAKE - F# MAKE"
         Attribute.Guid "EE5621DB-B86B-44eb-987F-9C94BCC98441"
         Attribute.Product "Calculator"
         Attribute.Version version
         Attribute.FileVersion version
         Attribute.Metadata("githash", commitHash)]
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
  //==> "AssemblyInfo"
  ==> "BuildCmd"
  //==> "FxCop"
//  ==> "Test"
  //==> "Zip"
  ==> "Default"


// start build
RunTargetOrDefault "Default"