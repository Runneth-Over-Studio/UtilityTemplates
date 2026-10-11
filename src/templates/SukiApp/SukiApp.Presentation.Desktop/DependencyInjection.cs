using Microsoft.Extensions.DependencyInjection;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using SukiApp.Presentation.Desktop.Base;
using SukiUI.Dialogs;
using SukiUI.Toasts;
using System;

namespace SukiApp.Presentation.Desktop;

public static class DependencyInjection
{
    /// <summary>
    /// Register internal presentation-tier services.
    /// </summary>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection RegisterInternalPresentationServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // Convention-based registration uses runtime reflection for all view models. Revisit for trimming or Native AOT compatibility.
        foreach (Type assemblyType in typeof(App).Assembly.GetTypes())
        {
            if (assemblyType.IsClass && !assemblyType.IsAbstract)
            {
                if (typeof(NavMenuPage).IsAssignableFrom(assemblyType))
                {
                    services.AddTransient(typeof(NavMenuPage), assemblyType);
                }
                else if (typeof(BaseViewModel).IsAssignableFrom(assemblyType))
                {
                    services.AddTransient(assemblyType);
                }
            }
        }

        services.AddSingleton<ISukiToastManager, SukiToastManager>();
        services.AddSingleton<ISukiDialogManager, SukiDialogManager>();

        return services;
    }
}
