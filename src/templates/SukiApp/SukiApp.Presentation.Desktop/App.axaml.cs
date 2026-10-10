using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using SukiApp.Presentation.Desktop.ViewModels;
using SukiApp.Presentation.Desktop.Views;
using System;

namespace SukiApp.Presentation.Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
#if DEBUG
        this.AttachDeveloperTools();
#endif
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Ioc.Default.GetRequiredService<MainViewModel>()
            };
        }
        else
        {
            throw new NotSupportedException("Only classic desktop lifetime is supported.");
        }

        base.OnFrameworkInitializationCompleted();
    }
}
