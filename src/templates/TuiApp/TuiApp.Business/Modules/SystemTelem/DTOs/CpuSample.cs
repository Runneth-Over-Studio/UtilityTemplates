using System;

namespace TuiApp.Business.Modules.SystemTelem.DTOs;

public sealed record CpuSample()
{
    public DateTimeOffset CapturedAt { get; init; }
    public double SystemCpuPercent { get; init; }
}
