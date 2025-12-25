using System;

namespace TuiApp.Business.Modules.SystemTelem.DTOs;

public sealed record MemorySample()
{
    public DateTimeOffset CapturedAt { get; init; }
    public long ProcessWorkingSetBytes { get; init; }
    public long ProcessPrivateBytes { get; init; }
}
