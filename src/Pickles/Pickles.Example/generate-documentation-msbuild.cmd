rem the below path may vary on your machine, modify it to point to the MSBuild.exe for the .NET Framework 4.0
set msbuild_path=C:\Windows\Microsoft.NET\Framework\v4.0.30319

%msbuild_path%\msbuild project-msbuild.proj
pause