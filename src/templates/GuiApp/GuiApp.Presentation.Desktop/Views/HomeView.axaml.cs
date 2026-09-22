using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using GuiApp.Presentation.Desktop.Base.Extensions;

namespace GuiApp.Presentation.Desktop.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}
