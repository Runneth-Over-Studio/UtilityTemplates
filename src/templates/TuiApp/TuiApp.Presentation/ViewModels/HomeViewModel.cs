using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using System;
using TuiApp.Business.Modules.Scheduling;

namespace TuiApp.Presentation.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _description = "This is a sample application generated from Runneth Over Studio's utility apps template.";

    [ObservableProperty]
    private string _designPattern = "This solution follows a three-tier architecture, Data > Business > Presentation. " +
        "For the presentation layer, the MVVM design pattern is used. " +
        "MVVM separates views from models, which allows for projects that are cleaner, easier to extend, and testable.";

    [ObservableProperty]
    private string _appearance = "This application uses RazorConsole which leverages Spectre.Console...";

    [ObservableProperty]
    private string _licenseURL = "https://github.com/Runneth-Over-Studio/UtilityTemplates/blob/master/license";

    public HomeViewModel()
    {
        IRepeatingScheduler repeatingScheduler = Ioc.Default.GetRequiredService<IRepeatingScheduler>();
        repeatingScheduler.InitiateScheduledJobsAsync(TimeSpan.FromSeconds(3), default);
    }
}
