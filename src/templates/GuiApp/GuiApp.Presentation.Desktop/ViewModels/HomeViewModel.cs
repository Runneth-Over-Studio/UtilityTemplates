using RunnethOverStudio.AppToolkit.Modules.ComponentModel;

namespace GuiApp.Presentation.Desktop.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    public string WelcomeMessage { get; }

    public HomeViewModel()
    {
        WelcomeMessage = "Welcome to Avalonia! Includes navigation, settings view, theming support, and OS secure credential storage.";
    }
}
