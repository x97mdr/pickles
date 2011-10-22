Pickles Example Project
=======================

This project is meant to give the user an idea of how to run Pickles.  At this time there are 3 different ways to generate documentation from Gherkin feature files.

1. Console application (generate-documentation-cli.cmd)
2. NAnt task (generate-documentation-nant.cmd)
3. MSBuild task (generate-documentation-msbuild.cmd)

You can use the syntax in these batch files to see how each task is launched and configured.

Prerequisites
-------------

- .NET Framework 4.0
- You will need to build the project prior to running the examples.  This is because the example reference the artifacts of the build process.  You can build the project by running the build-release.cmd file at the root of the working folder.