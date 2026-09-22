using GuiApp.Business;
using GuiApp.Data;
using GuiApp.Presentation.Desktop;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RunnethOverStudio.AppToolkit.Modules.Messaging;
using Serilog;
using System;
using System.IO;

namespace GuiApp.DesktopApp;

internal static class DependencyInjection
{
    internal static IServiceCollection BuildServiceCollection()
    {
        string applicationDataDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GuiApp");
        Directory.CreateDirectory(applicationDataDirectory);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(applicationDataDirectory, "log.txt"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

        IServiceCollection services = new ServiceCollection();

        services.AddLogging(configure => configure.AddSerilog(Log.Logger))
            .AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(App)))
            .AddSingleton<IEventSystem, EventSystem>();

        services.RegisterInternalDataServices();
        services.RegisterInternalBusinessServices();
        services.RegisterInternalPresentationServices();

        return services;
    }
}
