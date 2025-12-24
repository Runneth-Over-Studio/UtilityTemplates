using GuiApp.Business.Modules.Sample.ApplicationServices;
using GuiApp.Business.Modules.Sample.DomainServices;
using GuiApp.Data;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System.Reflection;

namespace GuiApp.Business;

public static class DependencyInjection
{
    /// <summary>
    /// Adds business-tier services.
    /// Dependent on <see cref="ILogger"/>.
    /// </summary>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        // Infrastructure.
        services.AddSingleton<IEventSystem, EventSystem>()
            .AddDataAccessServices();

        // Internal business domain.
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddScoped<FlatUIColorPicker, FlatUIColorPicker>()
            .AddScoped<LineSorter, LineSorter>()
            .AddScoped<UUIDGenerator, UUIDGenerator>();

        // Orchestrated public-facing (application) services.
        services.AddScoped<ISampleToolsService, SampleToolsService>();

        return services;
    }
}
