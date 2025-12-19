using GuiApp.Presentation.Base.Controls.RibbonControls;
using GuiApp.Presentation.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Base.Controls;

public partial class LogsRibbonControl : BaseRibbonControl
{
    public LogsRibbonControl()
    {
        InitializeComponent();
        this.DataContext = Ioc.Default.GetService<LogsViewModel>();
    }
}
