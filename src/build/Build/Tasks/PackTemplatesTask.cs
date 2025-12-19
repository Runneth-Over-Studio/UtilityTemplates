using Build.Tasks.Standard;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Pack;
using Cake.Common.Xml;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using System.Diagnostics;

namespace Build.Tasks;

[TaskName("Pack Templates")]
[IsDependentOn(typeof(CompileProjectsTask))]
[TaskDescription("Packs the project templates into NuGet packages.")]
public sealed class PackTemplatesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        context.Log.Information("Packing templates...");

        string templatesDir = Path.Combine(context.SourceDirectory, "templates");
        string templateProject = Path.Combine(templatesDir, "Template", "UtilityTemplates.csproj");
        string outputDir = Path.Combine(context.AbsolutePathToRepo, "artifacts", "templates");

        Directory.CreateDirectory(outputDir);

        context.Log.Information("Restoring template package project...");
        context.DotNetRestore(templateProject);

        context.Log.Information("Packing all templates into single package...");

        context.DotNetPack(templateProject, new DotNetPackSettings
        {
            Configuration = context.Config.ToString(),
            OutputDirectory = outputDir,
            NoRestore = true
        });

        string version = context.XmlPeek(templateProject, "/Project/PropertyGroup/PackageVersion");
        string packageId = context.XmlPeek(templateProject, "/Project/PropertyGroup/PackageId");
        string packageFileName = $"{packageId}.{version}.nupkg";

        stopwatch.Stop();
        double completionTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 1);
        context.Log.Information($"Template packing complete ({completionTime}s)");
        context.Log.Information($"Templates available at: {outputDir}");
        context.Log.Information($"Install with: dotnet new install {Path.Combine(outputDir, packageFileName)}");
    }
}

/*
# Install all templates with one command
dotnet new install RunnethOverStudio.UtilityTemplates

# List installed templates
dotnet new list

# Use either template
dotnet new ros.guiapp -n MyGuiApp
dotnet new ros.tuiapp -n MyTuiApp

# Uninstall when needed
dotnet new uninstall RunnethOverStudio.UtilityTemplates
*/