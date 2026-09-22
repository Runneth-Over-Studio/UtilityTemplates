using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using GuiApp.Presentation.Desktop.Base.Extensions;

namespace GuiApp.Presentation.Desktop.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}
