using CommunityToolkit.Mvvm.ComponentModel;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;
using RunnethOverStudio.AppToolkit.Modules.Messaging;
using Spectre.Console;
using System.Collections.ObjectModel;
using System.Linq;
using TuiApp.Business.Modules.SystemTelem.Messages;
using TuiApp.Presentation.Models;

namespace TuiApp.Presentation.ViewModels;

public partial class NetworkViewModel : BaseViewModel
{
    private const long BYTES_PER_MB = 1024L * 1024L;

    [ObservableProperty]
    private double _downloadSpeedMB;

    [ObservableProperty]
    private double _uploadSpeedMB;

    [ObservableProperty]
    private int _latency;

    [ObservableProperty]
    private ObservableCollection<ConnectionInfo> _activeTCPConnections = [];

    public NetworkViewModel(IEventSystem eventSystem)
    {
        eventSystem.Subscribe<SystemResourceSampleEventArgs>(OnSystemSampled);
    }

    private void OnSystemSampled(object? sender, SystemResourceSampleEventArgs e)
    {
        UploadSpeedMB = e.NetworkBytesSentPerSecond / (double)BYTES_PER_MB;
        DownloadSpeedMB = e.NetworkBytesReceivedPerSecond / (double)BYTES_PER_MB;
        Latency = (int)e.NetworkLatencyMilliseconds;

        ActiveTCPConnections = new ObservableCollection<ConnectionInfo>(e.ActiveConnections
            .Where(ac => string.Equals("TCP", ac.Protocol))
            .Take(5)
            .Select(ac => new ConnectionInfo()
            {
                Remote = ac.RemoteEndPoint,
                State = ac.State
            }));
    }
}
