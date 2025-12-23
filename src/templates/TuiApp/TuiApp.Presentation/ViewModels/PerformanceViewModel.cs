using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TuiApp.Presentation.Base;

namespace TuiApp.Presentation.ViewModels;

public partial class PerformanceViewModel : BaseViewModel, IDisposable
{
    [ObservableProperty]
    private double _cpuUsage = 0.0D;

    [ObservableProperty]
    private double _memoryUsage = 0.0D;

    [ObservableProperty]
    private int _processCount = 0;

    [ObservableProperty]
    private int _threadCount = 0;

    [ObservableProperty]
    private int _handleCount = 0;

    [ObservableProperty]
    private List<double> _cpuHistory = [0.0D];

    [ObservableProperty]
    private List<double> _memoryHistory = [0.0D];

    private System.Timers.Timer? _timer;

    public PerformanceViewModel()
    {
        StartMonitoring();
    }

    private void StartMonitoring()
    {
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        UpdateMetrics();
    }

    private void UpdateMetrics()
    {
        // CPU with smooth variation
        CpuUsage = Math.Max(0, Math.Min(100, CpuUsage + Random.Shared.Next(-10, 11)));
        CpuHistory.Add(CpuUsage);
        if (CpuHistory.Count > 10) CpuHistory.RemoveAt(0);

        // Memory with slower variation
        MemoryUsage = Math.Max(0, Math.Min(100, MemoryUsage + Random.Shared.Next(-5, 6)));
        MemoryHistory.Add(MemoryUsage);
        if (MemoryHistory.Count > 10) MemoryHistory.RemoveAt(0);

        // Other metrics
        ProcessCount = 150 + Random.Shared.Next(-10, 11);
        ThreadCount = 2000 + Random.Shared.Next(-50, 51);
        HandleCount = 50000 + Random.Shared.Next(-500, 501);
    }

    public string GetHistoryBar(List<double> history)
    {
        if (history.Count == 0) return "";
        return string.Join(" ", history.Select(v => v switch
        {
            < 25 => "▁",
            < 50 => "▃",
            < 75 => "▅",
            _ => "█"
        }));
    }

    public void Dispose()
    {
        _timer?.Stop();
        _timer?.Dispose();
    }
}
