using CommunityToolkit.Mvvm.ComponentModel;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem;
using TuiApp.Business.Modules.SystemTelem.DTOs;
using TuiApp.Business.Modules.SystemTelem.Messages;

namespace TuiApp.Presentation.ViewModels;

public partial class PerformanceViewModel : BaseViewModel
{
    private const long BYTES_PER_GB = 1024L * 1024L * 1024L;

    [ObservableProperty]
    private double _cpuUsagePercent = 0.0D;

    [ObservableProperty]
    private double _memoryUsagePercent = 0.0D;

    [ObservableProperty]
    private double _memoryTotalGB = 0.0D;

    [ObservableProperty]
    private double _memoryAvailableGB = 0.0D;

    [ObservableProperty]
    private int _processCount = 0;

    [ObservableProperty]
    private int _threadCount = 0;

    [ObservableProperty]
    private int _handleCount = 0;

    [ObservableProperty]
    private List<double> _cpuHistory = [];

    [ObservableProperty]
    private List<double> _memoryHistory = [];

    private readonly ISystemTelemGatherer _systemTelemGatherer;
    private long _totalMemoryBytes = 0;

    public PerformanceViewModel(IEventSystem eventSystem, ISystemTelemGatherer systemTelemGatherer)
    {
        _systemTelemGatherer = systemTelemGatherer;

        eventSystem.Subscribe<SystemResourceSampleEventArgs>(OnSystemSampled);
    }

    public override async Task InitializeAsync()
    {
        SystemSnapshot systemSnapshot = await _systemTelemGatherer.GatherSnapshotAsync();
        _totalMemoryBytes = systemSnapshot.PhysicalMemory.TotalBytes ?? 0;
        MemoryTotalGB = _totalMemoryBytes / (double)BYTES_PER_GB;
    }

    private void OnSystemSampled(object? sender, SystemResourceSampleEventArgs e)
    {
        CpuUsagePercent = e.SystemCpuPercent;

        MemoryUsagePercent = _totalMemoryBytes > 0 ? (e.MemoryUsedInBytes / (double)_totalMemoryBytes) * 100.0 : 0;
        MemoryAvailableGB = (_totalMemoryBytes - e.MemoryUsedInBytes) / (double)BYTES_PER_GB;

        ProcessCount = e.TotalProcesses;
        ThreadCount = e.TotalThreads;
        HandleCount = e.TotalHandles;

        // Update history collections (thread-safe on regular List)
        lock (CpuHistory)
        {
            CpuHistory.Add(CpuUsagePercent);
            if (CpuHistory.Count > 10) CpuHistory.RemoveAt(0);
        }

        lock (MemoryHistory)
        {
            MemoryHistory.Add(MemoryUsagePercent);
            if (MemoryHistory.Count > 10) MemoryHistory.RemoveAt(0);
        }

        // Notify view to refresh
        OnPropertyChanged(nameof(CpuHistory));
        OnPropertyChanged(nameof(MemoryHistory));
    }
}
