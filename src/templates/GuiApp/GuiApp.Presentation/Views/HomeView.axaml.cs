using Avalonia.Controls;
using GuiApp.Presentation.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}