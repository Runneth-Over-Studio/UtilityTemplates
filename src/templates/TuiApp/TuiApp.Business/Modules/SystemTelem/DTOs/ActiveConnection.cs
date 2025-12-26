using System;

namespace TuiApp.Business.Modules.SystemTelem.DTOs;

public sealed record ActiveConnection
{
    public string LocalEndPoint { get; init; } = string.Empty;
    public string RemoteEndPoint { get; init; } = string.Empty;
    public string Protocol { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public int? ProcessId { get; init; }
}
