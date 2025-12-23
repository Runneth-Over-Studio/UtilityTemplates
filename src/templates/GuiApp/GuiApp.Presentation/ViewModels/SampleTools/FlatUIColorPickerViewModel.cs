using GuiApp.Business.Modules.Sample.ApplicationServices;
using GuiApp.Business.Modules.Sample.DTOs;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using System.Collections.Generic;

namespace GuiApp.Presentation.ViewModels;

public partial class FlatUIColorPickerViewModel : BaseViewModel
{
    public List<FlatColorDto> FlatColors { get; set; }

    public FlatUIColorPickerViewModel(ISampleToolsService sampleToolsService)
    {
        FlatColors = [.. sampleToolsService.GetFlatColors()];
    }
}
