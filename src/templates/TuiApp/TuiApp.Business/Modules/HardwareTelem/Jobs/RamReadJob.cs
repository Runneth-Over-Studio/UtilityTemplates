using RunnethOverStudio.AppToolkit.Modules.Messaging;
using TuiApp.Business.Modules.DTOs;
using TuiApp.Business.Modules.HardwareTelem.CPU;

namespace TuiApp.Business.Modules.HardwareTelem.Jobs;

public sealed class RamReadJob
{
    private readonly IMemoryTelemetryReader _memory;
    private readonly IEventSystem _bus;

    public RamReadJob(IMemoryTelemetryReader memory, IEventSystem bus)
    {
        _memory = memory;
        _bus = bus;
    }

    public async Task TickAsync(CancellationToken ct)
    {
        try
        {
            MemoryTelemetrySampleEventArgs sample = await _memory.ReadAsync(ct);
            _bus.Publish(this, sample);
        }
        catch (Exception ex)
        {
            _bus.Publish(this, new TelemetryErrorEventArgs()
            {
                CapturedAt = DateTimeOffset.Now,
                Source = "Memory",
                Message = ex.Message,
                ExceptionType = ex.GetType().Name
            });
        }
    }
}
