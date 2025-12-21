using TuiApp.Business.Modules.DTOs;

namespace TuiApp.Business.Modules.HardwareTelem.CPU;

public interface IMemoryTelemetryReader
{
    Task<MemoryTelemetrySampleEventArgs> ReadAsync(CancellationToken cancellationToken = default);
}
