using GuiApp.Presentation.Desktop.Desktop.Base.Controls.RibbonControls;
using GuiApp.Presentation.Desktop.Desktop.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Desktop.Desktop.Base.Controls;

public partial class LogsRibbonControl : BaseRibbonControl
{
    public LogsRibbonControl()
    {
        InitializeComponent();
        this.DataContext = Ioc.Default.GetService<LogsViewModel>();
    }
}
