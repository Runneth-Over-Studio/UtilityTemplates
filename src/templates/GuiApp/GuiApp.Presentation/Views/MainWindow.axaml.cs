using Avalonia.Controls;
using GuiApp.Presentation.Base;
using System.Runtime.InteropServices;

namespace GuiApp.Presentation.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Title = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? AppInfo.AppDisplayName : string.Empty;
    }
}