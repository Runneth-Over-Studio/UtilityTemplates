using System;
using System.Collections.Generic;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem.Messages;

public sealed class SystemResourceSampleEventArgs() : EventArgs
{
    public DateTimeOffset CapturedAt { get; init; }

    public double SystemCpuPercent { get; init; }

    public long MemoryUsedInBytes { get; init; }

    public int TotalProcesses { get; init; }
    public int TotalThreads { get; init; }
    public int TotalHandles { get; init; }

    public long NetworkBytesSentPerSecond { get; init; }
    public long NetworkBytesReceivedPerSecond { get; init; }
    public long NetworkTotalBytesSent { get; init; }
    public long NetworkTotalBytesReceived { get; init; }
    public long NetworkLatencyMilliseconds { get; init; }
    public List<ActiveConnection> ActiveConnections { get; init; } = [];
}
