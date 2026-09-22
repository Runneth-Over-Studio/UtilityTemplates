using GuiApp.Presentation.Desktop.Base;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;

namespace GuiApp.Presentation.Desktop.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    public string AppDisplayName { get; }

    public string AppDescription { get; }

    public string LicenseURL { get; }

    public string DesignPatternBlurb { get; }

    public string AppearanceBlurb { get; }

    public HomeViewModel()
    {
        AppDisplayName = AppInfo.AppDisplayName;
        AppDescription = AppInfo.AppDescription;
        LicenseURL = AppInfo.LicenseURL;

        DesignPatternBlurb = "This solution follows a three-tier architecture with a dedicated desktop composition root. Presentation consumes Business capabilities, Data owns persistence concerns, and DesktopApp wires the application together. " +
            "For the presentation layer, the MVVM design pattern is used. " +
            "MVVM separates views from models, which allows for projects that are cleaner, easier to extend, and testable. " +
            "The executable host owns startup, dependency composition, and structured file logging using Serilog.";

        AppearanceBlurb = "Avalonia uses a Fluent Design System that emphasizes modern, clean aesthetics, smooth animations, and intuitive interactions. " +
            "It provides a consistent and polished look-and-feel across different platforms, while giving developers flexibility with its styling system.";
    }
}
