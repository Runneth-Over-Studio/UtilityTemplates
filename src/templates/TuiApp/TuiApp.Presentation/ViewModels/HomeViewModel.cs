using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using System;
using TuiApp.Business.Modules.Scheduling;
using TuiApp.Presentation.Base;

namespace TuiApp.Presentation.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _description = AppInfo.Description;

    [ObservableProperty]
    private string _designPattern = AppInfo.DesignPattern;

    [ObservableProperty]
    private string _appearance = AppInfo.Appearance;

    [ObservableProperty]
    private string _licenseURL = AppInfo.LicenseURL;

    public HomeViewModel()
    {
        IRepeatingScheduler repeatingScheduler = Ioc.Default.GetRequiredService<IRepeatingScheduler>();
        repeatingScheduler.InitiateScheduledJobsAsync(TimeSpan.FromSeconds(3), default);
    }
}
