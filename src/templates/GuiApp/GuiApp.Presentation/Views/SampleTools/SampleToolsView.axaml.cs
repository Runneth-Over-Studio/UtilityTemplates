using Avalonia.Controls;
using GuiApp.Presentation.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Views;

public partial class SampleToolsView : UserControl
{
    public SampleToolsView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}