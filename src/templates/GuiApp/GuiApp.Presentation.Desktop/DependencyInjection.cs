using GuiApp.Presentation.Desktop.Settings;
using Microsoft.Extensions.DependencyInjection;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using System;

namespace GuiApp.Presentation.Desktop;

public static class DependencyInjection
{
    /// <summary>
    /// Register internal presentation-tier services.
    /// </summary>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection RegisterInternalPresentationServices(this IServiceCollection services, string applicationDataDirectory)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationDataDirectory);

        services.AddSingleton<IDesktopSettings>(_ => new DesktopSettings(applicationDataDirectory));
        services.AddSingleton<ThemeService>();

        // View Models
        foreach (Type assemblyType in typeof(App).Assembly.GetTypes())
        {
            if (assemblyType.IsClass
                && !assemblyType.IsAbstract
                && typeof(BaseViewModel).IsAssignableFrom(assemblyType))
            {
                services.AddTransient(assemblyType);
            }
        }

        return services;
    }
}
