using GuiApp.Business;
using GuiApp.Data;
using GuiApp.Integrations;
using GuiApp.Presentation.Desktop;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RunnethOverStudio.AppToolkit.Core;
using RunnethOverStudio.AppToolkit.Modules.Access;
using Serilog;
using System;
using System.IO;

namespace GuiApp.DesktopApp;

internal static class DependencyInjection
{
    internal static IServiceCollection BuildServiceCollection()
    {
        string applicationDataDirectory = GetApplicationDataDirectory();

        Serilog.Log.Logger = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(applicationDataDirectory, "log.txt"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

        IServiceCollection services = new ServiceCollection();

        services.AddLogging(configure => configure.AddSerilog(Serilog.Log.Logger))
            .AddSingleton((sp) => sp.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(App)))
            .AddScoped<IFileSystemAccess, FileSystemAccess>()
            .AddHttpClient();

        services.RegisterInternalDataServices()
            .RegisterInternalIntegrationsServices()
            .RegisterInternalBusinessServices()
            .RegisterInternalPresentationServices();

        return services;
    }

    private static string GetApplicationDataDirectory()
    {
        using ILoggerFactory bootstrapLoggerFactory = LoggerFactory.Create(builder => builder.AddProvider(NullLoggerProvider.Instance));

        FileSystemAccess fileSystemAccess = new(bootstrapLoggerFactory.CreateLogger<IFileSystemAccess>());
        ProcessResult<string> appDirectoryPathResult = fileSystemAccess.GetOrCreateAppDirectoryPath();

        if (appDirectoryPathResult.IsSuccessful)
        {
            return appDirectoryPathResult.Value;
        }

        throw new InvalidOperationException("Failed to get or create application data directory.", appDirectoryPathResult.Error);
    }
}
