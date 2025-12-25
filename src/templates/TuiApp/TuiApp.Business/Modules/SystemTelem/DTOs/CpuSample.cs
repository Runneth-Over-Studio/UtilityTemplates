using System;

namespace TuiApp.Business.Modules.SystemTelem.DTOs;

public sealed record CpuSample()
{
    public DateTimeOffset CapturedAt { get; init; }
    public double ProcessCpuPercent { get; init; }
    public TimeSpan TotalProcessorTime { get; init; }
}
