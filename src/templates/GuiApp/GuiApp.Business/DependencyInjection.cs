using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
#if (SampleBehaviors)
using GuiApp.Business.Modules.Sample.ApplicationServices;
using GuiApp.Business.Modules.Sample.DomainServices;
#endif

namespace GuiApp.Business;

public static class DependencyInjection
{
    /// <summary>
    /// Registers internal business-tier services.
    /// </summary>
    public static IServiceCollection RegisterInternalBusinessServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

#if (SampleBehaviors)
        services.AddScoped<FlatUIColorPicker>()
            .AddScoped<LineSorter>()
            .AddScoped<UUIDGenerator>()
            .AddScoped<ISampleToolsService, SampleToolsService>();
#endif

        return services;
    }
}
