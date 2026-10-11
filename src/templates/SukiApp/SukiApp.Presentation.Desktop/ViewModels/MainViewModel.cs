using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using SukiApp.Presentation.Desktop.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SukiApp.Presentation.Desktop.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public IAvaloniaReadOnlyList<NavMenuPage> NavMenuPages { get; }

    [ObservableProperty]
    private BaseViewModel _activePage;

    public MainViewModel(IEnumerable<NavMenuPage> navMenuPages)
    {
        if (navMenuPages is null || !navMenuPages.Any())
        {
            throw new ArgumentException("At least one NavMenuPage must be provided.", nameof(navMenuPages));
        }

        NavMenuPages = new AvaloniaList<NavMenuPage>(navMenuPages.OrderBy(x => x.Index).ThenBy(x => x.DisplayName));
        ActivePage = NavMenuPages[0];
    }
}
