using Avalonia.Controls;
using GuiApp.Presentation.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Views;

public partial class LogsView : UserControl
{
    public LogsView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}