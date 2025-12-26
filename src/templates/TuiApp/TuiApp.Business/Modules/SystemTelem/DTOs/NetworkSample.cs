using System;

namespace TuiApp.Business.Modules.SystemTelem.DTOs;

public sealed record NetworkSample
{
    public DateTimeOffset CapturedAt { get; init; }
    public long BytesSentPerSecond { get; init; }
    public long BytesReceivedPerSecond { get; init; }
    public long TotalBytesSent { get; init; }
    public long TotalBytesReceived { get; init; }
    public long LatencyMilliseconds { get; init; }
}
