using Avalonia.Controls;
using GuiApp.Presentation.Desktop.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Desktop.Views;

public partial class LogsView : UserControl
{
    public LogsView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}