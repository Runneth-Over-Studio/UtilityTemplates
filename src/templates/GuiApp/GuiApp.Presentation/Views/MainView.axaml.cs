using Avalonia.Controls;
using GuiApp.Presentation.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}
