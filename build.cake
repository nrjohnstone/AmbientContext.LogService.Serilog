#addin "Newtonsoft.Json"
#addin "Cake.Powershell"
#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=xunit.runner.console"
#addin "Cake.ExtendedNuGet"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var solutionDir = Directory("./src");
var solutionFile = solutionDir + File("AmbientContext.LogService.Serilog.sln");
var buildDir = Directory("./src/AmbientContext.LogService.Serilog/bin") + Directory(configuration);

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


Task("Build")
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


Task("Pack-Nuget")
    .Does(() => 
{
    var settings = new DotNetCorePackSettings
    {
        Configuration = "Release",
        OutputDirectory = "./artifacts/"
    };

    DotNetCorePack("./src/AmbientContext.LogService.Serilog/", settings);
});


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
    GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true});
    string version = GitVersion().FullSemVer;
    var projectFiles = System.IO.Directory.EnumerateFiles(@".\", "project.json", SearchOption.AllDirectories).ToArray();

    foreach(var file in projectFiles)
    {
        var project = Newtonsoft.Json.Linq.JObject.Parse(System.IO.File.ReadAllText(file, Encoding.UTF8));

        project["version"].Replace(version);

        System.IO.File.WriteAllText(file, project.ToString(), Encoding.UTF8);
    }
});


Task("Get-DotNetCli")
    .Does(() =>
{         
    string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    string dotNetPath = userProfile + @"Local\Microsoft\dotnet";
    
    if (!Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine).Contains(dotNetPath))
    {
        DownloadFile("https://raw.githubusercontent.com/dotnet/cli/rel/1.0.0/scripts/obtain/dotnet-install.ps1", "./tools/dotnet-install.ps1");
        var version = "1.0.0-preview2-003121";
        StartPowershellFile("./tools/dotnet-install.ps1", args =>
            {
                args.Append("Version", version);
            });

        
        string path = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine) + ";" + dotNetPath;
        Console.WriteLine(path);
        Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.Machine);
        Environment.SetEnvironmentVariable("Path", path);
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests");
    
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
