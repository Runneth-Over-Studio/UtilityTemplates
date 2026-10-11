using Material.Icons;
using SukiApp.Presentation.Desktop.Models;

namespace SukiApp.Presentation.Desktop.ViewModels;

public partial class HomeViewModel : NavMenuPage
{
    public string WelcomeMessage { get; }

    public HomeViewModel() : base("Home", MaterialIconKind.Home, int.MinValue)
    {
        WelcomeMessage = $"Welcome to Avalonia and SukiUI!";
    }
}
