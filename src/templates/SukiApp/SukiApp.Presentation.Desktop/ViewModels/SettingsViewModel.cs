using Material.Icons;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using SukiApp.Presentation.Desktop.Base;
using SukiApp.Presentation.Desktop.Models;

namespace SukiApp.Presentation.Desktop.ViewModels;

public partial class SettingsViewModel : NavMenuPage
{
    public string AppDisplayName { get; }

    public string ApplicationInfo { get; }

    public string CopyHolder { get; }

    public string AppDescription { get; }

    public string PrivacyURL { get; }

    public string IssuesURL { get; }

    public SettingsViewModel() : base("Settings", MaterialIconKind.CogOutline, int.MaxValue)
    {
        AppDisplayName = AppInfo.AppDisplayName;
        ApplicationInfo = $"{AppInfo.AppDisplayName} - {AppInfo.Version}";
        CopyHolder = AppInfo.CopyHolder;
        AppDescription = AppInfo.AppDescription;
        PrivacyURL = AppInfo.PrivacyURL;
        IssuesURL = AppInfo.IssuesURL;
    }
}
