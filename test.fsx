// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake


// Properties
let testDir  = "./test/"

// version info
let version = "0.18.1"  // or retrieve from CI server

Target "Test" (fun _ ->
    !! (testDir + "PicklesDoc.Pickles.Test.dll")
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true;
             OutputFile = testDir + "TestResults.xml" })
)


// start build
RunTargetOrDefault "Test"