using System;

namespace TuiApp.Business.Modules.DTOs;

public sealed class CpuTelemetrySampleEventArgs() : EventArgs
{
    public DateTimeOffset CapturedAt { get; init; }
    public double ProcessCpuPercent { get; init; }
    public TimeSpan TotalProcessorTime { get; init; }
    public int LogicalCores { get; init; }
}
