using Avalonia.Controls;
using GuiApp.Presentation.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}