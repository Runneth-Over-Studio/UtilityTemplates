using Avalonia.Controls;
using SukiApp.Presentation.Desktop.Base;
using System.Runtime.InteropServices;

namespace SukiApp.Presentation.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Title = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? AppInfo.AppDisplayName : string.Empty;
    }
}
