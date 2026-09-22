using Avalonia.Controls;
using Avalonia.Interactivity;
using GuiApp.Presentation.Desktop.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Desktop.Views;

public partial class LineSorterView : UserControl
{
    public LineSorterView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }

    private void SelectAllBtn_Click(object? sender, RoutedEventArgs e)
    {
        SortTextTB.Focus();
        SortTextTB.SelectAll();
    }
}