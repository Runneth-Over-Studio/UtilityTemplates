using CommunityToolkit.Mvvm.ComponentModel;
using Spectre.Console;
using System;
using System.Collections.ObjectModel;
using System.Timers;
using TuiApp.Presentation.Base;

namespace TuiApp.Presentation.ViewModels;

public partial class NetworkViewModel : BaseViewModel, IDisposable
{
    [ObservableProperty]
    private double _downloadSpeed;

    [ObservableProperty]
    private double _uploadSpeed;

    [ObservableProperty]
    private int _latency;

    [ObservableProperty]
    private int _packetsSent;

    [ObservableProperty]
    private int _packetsReceived;

    [ObservableProperty]
    private ObservableCollection<ConnectionInfo> _activeConnections = [];

    private System.Timers.Timer? _timer;

    public NetworkViewModel()
    {
        InitializeData();
        StartMonitoring();
    }

    private void InitializeData()
    {
        ActiveConnections =
        [
            new() { Remote = "192.168.1.100:443", Protocol = "HTTPS", BytesTransferred = 0 },
            new() { Remote = "10.0.0.50:3306", Protocol = "MySQL", BytesTransferred = 0 },
            new() { Remote = "172.16.0.25:6379", Protocol = "Redis", BytesTransferred = 0 }
        ];
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
        DownloadSpeed = Random.Shared.NextDouble() * 50;
        UploadSpeed = Random.Shared.NextDouble() * 20;
        Latency = 10 + Random.Shared.Next(-5, 20);
        PacketsSent += Random.Shared.Next(100, 1000);
        PacketsReceived += Random.Shared.Next(100, 1000);

        // Update connections
        foreach (var conn in ActiveConnections)
        {
            conn.BytesTransferred += Random.Shared.Next(1000, 50000);
        }
    }

    public Color GetLatencyColor()
    {
        if (Latency < 20) return Color.Green;
        if (Latency < 50) return Color.Yellow;
        return Color.Red;
    }

    public void Dispose()
    {
        _timer?.Stop();
        _timer?.Dispose();
    }

    public class ConnectionInfo
    {
        public string Remote { get; set; } = "";
        public string Protocol { get; set; } = "";
        public long BytesTransferred { get; set; }
    }
}
