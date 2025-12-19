using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using GuiApp.Presentation.Base.Extensions;
using GuiApp.Presentation.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GuiApp.Presentation.Views;

public partial class UUIDGeneratorView : UserControl
{
    public UUIDGeneratorView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }

    private async void CopyBtn_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is UUIDGeneratorViewModel viewModel && viewModel.UUID != null)
        {
            IClipboard? clipboard = TopLevel.GetTopLevel(this)?.Clipboard;

            if (clipboard != null)
            {
                DataTransfer dataObject = new();
                dataObject.Add(DataTransferItem.Create(DataFormat.Text, viewModel.UUID));

                await clipboard.SetDataAsync(dataObject);
            }
        }
    }
}
