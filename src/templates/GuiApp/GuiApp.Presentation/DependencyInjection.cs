using Microsoft.Extensions.DependencyInjection;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using System;

namespace GuiApp.Presentation.Desktop;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInternalPresentationServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

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
