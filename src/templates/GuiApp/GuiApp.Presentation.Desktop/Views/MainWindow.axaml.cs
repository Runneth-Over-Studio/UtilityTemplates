using Avalonia.Controls;
using GuiApp.Presentation.Desktop.Base;
using System.Runtime.InteropServices;

namespace GuiApp.Presentation.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Title = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? AppInfo.AppDisplayName : string.Empty;
    }
}