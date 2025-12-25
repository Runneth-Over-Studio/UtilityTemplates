using System;

namespace TuiApp.Business.Modules.SystemTelem.Messages;

public sealed class TelemetryErrorEventArgs() : EventArgs
{
    public DateTimeOffset CapturedAt { get; init; }
    public required string Message { get; init; }
    public string? ExceptionType { get; init; } = null;
}
