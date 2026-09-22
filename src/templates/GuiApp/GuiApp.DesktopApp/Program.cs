using Avalonia;
using CommunityToolkit.Mvvm.DependencyInjection;
using GuiApp.Presentation.Desktop;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace GuiApp.DesktopApp;

internal class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        ServiceProvider? serviceProvider = null;

        try
        {
            IServiceCollection services = DependencyInjection.BuildServiceCollection();
            serviceProvider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(serviceProvider);

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        finally
        {
            serviceProvider?.Dispose();
            Log.CloseAndFlush();
        }
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }
}
