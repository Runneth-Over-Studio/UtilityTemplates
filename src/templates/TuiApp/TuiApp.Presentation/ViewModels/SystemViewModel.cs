using CommunityToolkit.Mvvm.ComponentModel;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Presentation.ViewModels;

public partial class SystemViewModel : BaseViewModel
{
    private const long BYTES_PER_GB = 1024L * 1024L * 1024L;

    [ObservableProperty]
    private string _machineName = string.Empty;

    [ObservableProperty]
    private string _osDescription = string.Empty;

    [ObservableProperty]
    private int _logicalCores = 0;

    [ObservableProperty]
    private int _totalMemoryGB = 0;

    private readonly ISystemTelemGatherer _systemTelemGatherer;

    public SystemViewModel(ISystemTelemGatherer systemTelemGatherer)
    {
        _systemTelemGatherer = systemTelemGatherer;
    }

    public override async Task InitializeAsync()
    {
        SystemSnapshot systemSnapshot = await _systemTelemGatherer.GatherSnapshotAsync();

        MachineName = systemSnapshot.Machine.MachineName;
        OsDescription = systemSnapshot.Os.Description;
        LogicalCores = systemSnapshot.LogicalCores;
        TotalMemoryGB = (int)((systemSnapshot.PhysicalMemory.TotalBytes ?? 0) / BYTES_PER_GB);
    }
}
