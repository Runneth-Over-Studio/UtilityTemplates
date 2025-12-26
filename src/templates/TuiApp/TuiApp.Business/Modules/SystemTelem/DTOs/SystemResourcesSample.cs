using System;

namespace TuiApp.Business.Modules.SystemTelem.DTOs;

public sealed record SystemResourcesSample
{
    public DateTimeOffset CapturedAt { get; init; }
    public int TotalProcesses { get; init; }
    public int TotalThreads { get; init; }
    public int TotalHandles { get; init; }
}
