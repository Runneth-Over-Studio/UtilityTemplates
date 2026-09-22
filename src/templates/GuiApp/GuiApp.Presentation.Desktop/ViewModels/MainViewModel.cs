using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuiApp.Presentation.Desktop.Models;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GuiApp.Presentation.Desktop.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly SettingsViewModel _settingsViewModel;

    [ObservableProperty]
    private BaseViewModel _currentPage;

    [ObservableProperty]
    private bool _isPaneOpen;

    [ObservableProperty]
    private string _pageTitle;

    [ObservableProperty]
    private MenuPaneItemTemplate? _selectedPaneItem;

    public ObservableCollection<MenuPaneItemTemplate> PaneItems { get; }

    public MainViewModel(HomeViewModel homeViewModel, SettingsViewModel settingsViewModel)
    {
        ArgumentNullException.ThrowIfNull(homeViewModel);

        _settingsViewModel = settingsViewModel ?? throw new ArgumentNullException(nameof(settingsViewModel));

        List<MenuPaneItemTemplate> paneItemTemplates =
        [
            // Icon key ref: https://pictogrammers.com/library/mdi/

            new MenuPaneItemTemplate(homeViewModel, "Home", "Home")
        ];

        IsPaneOpen = false;
        PaneItems = new ObservableCollection<MenuPaneItemTemplate>(paneItemTemplates);
        SelectedPaneItem = PaneItems[0];
        CurrentPage = SelectedPaneItem.Content;
        PageTitle = SelectedPaneItem.Label;
    }

    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    [RelayCommand]
    private void Settings()
    {
        SelectedPaneItem = null;
        CurrentPage = _settingsViewModel;
        PageTitle = "Settings";
    }

    partial void OnSelectedPaneItemChanged(MenuPaneItemTemplate? value)
    {
        if (value is null)
        {
            return;
        }

        CurrentPage = value.Content;
        PageTitle = value.Label;
    }
}
