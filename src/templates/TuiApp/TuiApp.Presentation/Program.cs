using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorConsole.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TuiApp.Presentation;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.UseRazorConsole<App>();

            RegisterServices(builder.Services);
            IHost host = builder.Build();
            Ioc.Default.ConfigureServices(host.Services);

            await host.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Application terminated unexpectedly: {ex}");
            return 1;
        }
    }

    private static void RegisterServices(IServiceCollection services)
    {
        Business.DependencyInjection.AddBusinessServices(services);

        // Auto-register all ViewModels by convention
        IEnumerable<Type> viewModelTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("ViewModel"));

        foreach (Type viewModelType in viewModelTypes)
        {
            services.AddScoped(viewModelType);
        }
    }
}