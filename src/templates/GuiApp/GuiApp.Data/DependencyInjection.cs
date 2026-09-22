using Microsoft.Extensions.DependencyInjection;
using System;

namespace GuiApp.Data;

public static class DependencyInjection
{
    /// <summary>
    /// Registers internal data-tier services.
    /// </summary>
    public static IServiceCollection RegisterInternalDataServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // Register application-owned persistence services here.

        return services;
    }
}
