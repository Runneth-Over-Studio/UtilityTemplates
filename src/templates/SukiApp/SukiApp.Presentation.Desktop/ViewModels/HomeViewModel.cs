using Material.Icons;
using SukiApp.Presentation.Desktop.Base;

namespace SukiApp.Presentation.Desktop.ViewModels;

public partial class HomeViewModel : NavMenuPage
{
    public string WelcomeMessage { get; }

    public HomeViewModel() : base("Home", MaterialIconKind.Home, int.MinValue)
    {
        WelcomeMessage = $"Welcome to Avalonia and SukiUI!";
    }
}
