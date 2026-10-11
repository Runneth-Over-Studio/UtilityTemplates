using RunnethOverStudio.AppToolkit.Modules.ComponentModel;

namespace SukiApp.Presentation.Desktop.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    public string WelcomeMessage { get; }

    public HomeViewModel()
    {
        WelcomeMessage = $"Welcome to Avalonia and SukiUI!";
    }
}
