using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.DTOs;
using TuiApp.Business.Modules.HardwareTelem.CPU;

namespace TuiApp.Business.Modules.HardwareTelem.Jobs;

public sealed class CpuReadJob
{
    private readonly ICpuTelemetryReader _cpu;
    private readonly IEventSystem _bus;

    public CpuReadJob(ICpuTelemetryReader cpu, IEventSystem bus)
    {
        _cpu = cpu;
        _bus = bus;
    }

    public async Task TickAsync(CancellationToken ct)
    {
        try
        {
            CpuTelemetrySampleEventArgs sample = await _cpu.ReadAsync(ct);
            _bus.Publish(this, sample);
        }
        catch (Exception ex)
        {
            _bus.Publish(this,  new TelemetryErrorEventArgs()
            {
                CapturedAt = DateTimeOffset.Now,
                Source = "CPU",
                Message = ex.Message,
                ExceptionType = ex.GetType().Name
            });
        }
    }
}
