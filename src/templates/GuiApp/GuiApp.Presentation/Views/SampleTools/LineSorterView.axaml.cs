using Avalonia.Controls;
using Avalonia.Interactivity;
using GuiApp.Presentation.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Views;

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