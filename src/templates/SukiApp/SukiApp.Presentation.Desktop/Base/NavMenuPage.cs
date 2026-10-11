using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;

namespace SukiApp.Presentation.Desktop.Models;

public abstract partial class NavMenuPage(string displayName, MaterialIconKind icon, int index = 0) : BaseViewModel
{
    [ObservableProperty] private string _displayName = displayName;
    [ObservableProperty] private MaterialIconKind _icon = icon; // Material Icons: https://pictogrammers.com/library/mdi/
    [ObservableProperty] private int _index = index;
}
