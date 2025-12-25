using CommunityToolkit.Mvvm.ComponentModel;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using Spectre.Console;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using TuiApp.Business.Modules.SystemTelem;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Presentation.ViewModels;

public partial class SystemViewModel : BaseViewModel, IDisposable
{
    [ObservableProperty]
    private double _cpuUsage;

    [ObservableProperty]
    private double _memoryUsage;

    [ObservableProperty]
    private double _diskIo;

    [ObservableProperty]
    private int _temperature;

    [ObservableProperty]
    private ObservableCollection<ServiceInfo> _services = [];

    [ObservableProperty]
    private ObservableCollection<EventInfo> _recentEvents = [];

    private readonly ISystemTelemGatherer _systemTelemGatherer;
    private Timer? _timer;

    public SystemViewModel(ISystemTelemGatherer systemTelemGatherer)
    {
        _systemTelemGatherer = systemTelemGatherer;
        

        InitializeData();
        
    }

    private void InitializeData()
    {
        Services =
        [
            new() { Name = "Web Server", Status = "Running", IsRunning = true },
            new() { Name = "Database", Status = "Running", IsRunning = true },
            new() { Name = "Cache", Status = "Running", IsRunning = true },
            new() { Name = "Message Queue", Status = "Running", IsRunning = true }
        ];

        RecentEvents.Add(new EventInfo
        {
            Time = DateTime.Now.ToString("HH:mm:ss"),
            Message = "System started",
            Color = Color.Green
        });
    }

    public override async Task InitializeAsync()
    {
        SystemSnapshot systemSnapshot = await _systemTelemGatherer.GatherSnapshotAsync();

        StartMonitoring();
    }

    private void StartMonitoring()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        UpdateMetrics();
    }

    private void UpdateMetrics()
    {
        CpuUsage = Math.Max(0, Math.Min(100, CpuUsage + Random.Shared.Next(-10, 11)));
        MemoryUsage = Math.Max(0, Math.Min(100, MemoryUsage + Random.Shared.Next(-5, 6)));
        DiskIo = Math.Max(0, Random.Shared.NextDouble() * 150);
        Temperature = 45 + Random.Shared.Next(-5, 15);

        // Occasionally add events
        if (Random.Shared.Next(0, 5) == 0)
        {
            var events = new[]
            {
                ("Backup completed successfully", Color.Green),
                ("New user logged in", Color.Cyan1),
                ("Cache cleared", Color.Yellow),
                ("High memory usage detected", Color.Orange1),
                ("Network spike detected", Color.Yellow),
            };
            var evt = events[Random.Shared.Next(events.Length)];
            RecentEvents.Add(new EventInfo
            {
                Time = DateTime.Now.ToString("HH:mm:ss"),
                Message = evt.Item1,
                Color = evt.Item2
            });
            if (RecentEvents.Count > 20) RecentEvents.RemoveAt(0);
        }
    }

    public Color GetMetricColor(double value)
    {
        if (value < 50) return Color.Green;
        if (value < 75) return Color.Yellow;
        return Color.Red;
    }

    public Color GetTempColor()
    {
        if (Temperature < 60) return Color.Green;
        if (Temperature < 75) return Color.Yellow;
        return Color.Red;
    }

    public void Dispose()
    {
        _timer?.Stop();
        _timer?.Dispose();

        GC.SuppressFinalize(this);
    }

    public class ServiceInfo
    {
        public string Name { get; set; } = "";
        public string Status { get; set; } = "";
        public bool IsRunning { get; set; }
    }

    public class EventInfo
    {
        public string Time { get; set; } = "";
        public string Message { get; set; } = "";
        public Color Color { get; set; }
    }
}
