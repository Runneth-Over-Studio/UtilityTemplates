using System;

namespace TuiApp.Business.Modules.SystemTelem.Messages;

public sealed class SystemResourceSampleEventArgs() : EventArgs
{
    public DateTimeOffset CapturedAt { get; init; }
    public double ProcessCpuPercent { get; init; }
    public TimeSpan TotalProcessorTime { get; init; }
    public long MemoryWorkingSetBytes { get; init; }
    public long MemoryPrivateBytes { get; init; }
}
