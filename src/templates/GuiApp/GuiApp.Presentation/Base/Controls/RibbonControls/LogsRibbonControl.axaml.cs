using GuiApp.Presentation.Desktop.Base.Controls.RibbonControls;
using GuiApp.Presentation.Desktop.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Desktop.Base.Controls;

public partial class LogsRibbonControl : BaseRibbonControl
{
    public LogsRibbonControl()
    {
        InitializeComponent();
        this.DataContext = Ioc.Default.GetService<LogsViewModel>();
    }
}
