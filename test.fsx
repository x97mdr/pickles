// include Fake lib
#r @"src\Pickles\packages\FAKE.3.36.0\tools\FakeLib.dll"
open Fake


// Properties
let testDir  = "./test/"

Target "Test" (fun _ ->
    !! (testDir + "PicklesDoc.Pickles.Test.dll")
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true;
             OutputFile = testDir + "TestResults.xml" })
)


// start build
RunTargetOrDefault "Test"