#addin "Newtonsoft.Json"
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
    var settings = new DotNetCoreCleanSettings
     {         
         Configuration = configuration      
     };

    DotNetCoreClean(solutionFile, settings);
});


Task("Restore-NuGet-Packages")
    .Does(() =>
{
    DotNetCoreRestore(solutionFile);
    // For old versions of csproj files
    NuGetRestore(solutionFile);
});


Task("Pack-Nuget")
    .Does(() => 
{
    var nugetPackageDir = Directory("./artifacts");
    EnsureDirectoryExists(nugetPackageDir);
    string version = GitVersion().NuGetVersionV2;

    var settings = new DotNetCorePackSettings
    {
        ArgumentCustomization = args=>args.Append("/p:PackageVersion=" + version),
        Configuration = configuration,
        OutputDirectory = nugetPackageDir,
        NoRestore = true,
        IncludeSymbols = true
    };

    DotNetCorePack("./src/AmbientContext.LogService.Serilog/AmbientContext.LogService.Serilog.csproj", settings);
});


Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Update-Version")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        NoRestore = true    
    };

    DotNetCoreBuild(solutionFile, settings);
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
    XUnit2(@".\AmbientContext.LogService.Serilog.Tests\bin\" + configuration + @"\AmbientContext.LogService.Serilog.Tests.dll",
        new XUnit2Settings {
            Parallelism = ParallelismOption.All,
            HtmlReport = false,
            NoAppDomain = true
        });
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
    .IsDependentOn("Pack-Nuget");
    
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
