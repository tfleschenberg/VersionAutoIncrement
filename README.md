# VersionAutoIncrement

VersionAutoIncrement is a small tool to autoincrement the build number of C# projects.

## :memo: How to use the autoincrement

Copy the exe to the $(ProjectDir) of your project and add the following line to your pre build command line:
```
"$(ProjectDir)VersionAutoIncrement.exe" "$(ProjectDir)Properties\AssemblyInfo.cs"
```

Alternatively you may copy the exe somewhere on your system and adjust the path in the pre build command line accordingly.
