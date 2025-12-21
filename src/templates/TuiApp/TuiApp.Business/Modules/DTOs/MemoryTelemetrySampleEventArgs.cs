using System;

namespace TuiApp.Business.Modules.DTOs;

public sealed class MemoryTelemetrySampleEventArgs() : EventArgs
{
    public DateTimeOffset CapturedAt { get; init; }
    public long ProcessWorkingSetBytes { get; init; }
    public long ProcessPrivateBytes { get; init; }
    public long? TotalPhysicalBytes { get; init; }
    public long? AvailablePhysicalBytes { get; init; }
}
