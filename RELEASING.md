For each release:
* Bump the version number in `AssemblyVersion.cs`
* Update `ChamberLib.nuspec`:
    * Update the `releaseNotes`
    * Update the `dependencies` if needed
* Cut a release in GitHub
* Build the assembly from the new tag
* Build the nupkg
* Upload the assembly to the GitHub release
* Upload the new nupkg to nuget.org
