using Microsoft.Extensions.DependencyInjection;
using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System;
using TuiApp.Business.Modules.Scheduling;
using TuiApp.Business.Modules.SystemTelem;
using TuiApp.Business.Modules.SystemTelem.Submodules;

namespace TuiApp.Business;

public static class DependencyInjection
{
    /// <summary>
    /// Adds business-tier services.
    /// </summary>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        // For the purposes of this sample application, the NuGet package Microsoft.Extensions.Diagnostics.ResourceMonitoring is used.
        services.AddResourceMonitoring();

        services.AddSingleton<IEventSystem, EventSystem>()
            .AddSingleton<IRepeatingScheduler, PeriodicTimerScheduler>()
            .AddScoped<ICpuProvider, CpuProvider>()
            .AddScoped<IMemoryProvider, MemoryProvider>()
            .AddScoped<INetworkProvider, NetworkProvider>()
            .AddScoped<ISystemTelemGatherer, SystemTelemGatherer>();

        return services;
    }
}
