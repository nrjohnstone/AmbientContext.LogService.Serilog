#addin "Newtonsoft.Json"
#addin "Cake.Powershell"
#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=xunit.runner.console"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var solutionDir = Directory("./");
var projectDir = solutionDir + Directory("./src/AmbientContext.LogService.Serilog");
var solutionFile = solutionDir + File("AmbientContext.LogService.Serilog.sln");
var buildDir = projectDir + Directory("bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////


Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});


Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore(solutionFile);
});


Task("Pack")
    .Does(() => 
{
    EnsureDirectoryExists("./artifacts");
    string version = GitVersion().NuGetVersion;

    var binDir = Directory("./bin") ;
    var nugetPackageDir = Directory("./artifacts");

    var nugetFilePaths = GetFiles("./src/AmbientContext.LogService.Serilog/*.csproj");

    var nuGetPackSettings = new NuGetPackSettings
    {   
        Version = version,
        BasePath = binDir + Directory(configuration),
        OutputDirectory = nugetPackageDir,
        ArgumentCustomization = args => args.Append("-Prop Configuration=" + configuration)
    };

    NuGetPack(nugetFilePaths, nuGetPackSettings);
});


Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Update-Version")
    .Does(() =>
{
    DotNetCoreRestore();

    // Use MSBuild
    MSBuild(solutionFile, settings =>
    settings.SetConfiguration(configuration));
});


Task("Rebuild")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{ });


Task("Run-Unit-Tests")
    .Does(() =>
{
    //var testAssemblies = GetFiles(".\\test\\AmbientContext.Tests\\bin\\" + configuration + "\\net451\\*\\AmbientContext.Tests.dll");
    //Console.WriteLine(testAssemblies.Count());
    //XUnit2(testAssemblies);
});


Task("Update-Version")
    .Does(() => 
{
    GitVersion(new GitVersionSettings { UpdateAssemblyInfo = true,
		UpdateAssemblyInfoFilePath = projectDir + File(@"Properties\AssemblyVersionInfo.cs") });
    
	string version = GitVersion().NuGetVersion;
	Console.WriteLine("Current NuGetVersion=" + version);
	
    if (AppVeyor.IsRunningOnAppVeyor) {
        AppVeyor.UpdateBuildVersion(version);
    }
    var projectFiles = System.IO.Directory.EnumerateFiles(@".\", "project.json", SearchOption.AllDirectories).ToArray();

    foreach(var file in projectFiles)
    {
        var project = Newtonsoft.Json.Linq.JObject.Parse(System.IO.File.ReadAllText(file, Encoding.UTF8));

        project["version"].Replace(version);

        System.IO.File.WriteAllText(file, project.ToString(), Encoding.UTF8);
    }
});


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Pack");
    
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
