using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorConsole.Core;
using System;
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

            ConfigureServices(builder.Services);

            IHost host = builder.Build();
            await host.RunAsync();

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Application terminated unexpectedly: {ex}");
            return 1;
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register application services here.
    }
}