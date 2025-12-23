using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.HardwareTelem.CPU;
using TuiApp.Business.Modules.HardwareTelem.Jobs;
using TuiApp.Business.Modules.Scheduling;

namespace TuiApp.Business.Modules.HardwareTelem;

public sealed class HardwareTelemetryOrchestrator
{
    private readonly IRepeatingScheduler _scheduler;
    private readonly CpuReadJob _cpu;
    private readonly RamReadJob _ram;

    public HardwareTelemetryOrchestrator(IRepeatingScheduler scheduler, ICpuTelemetryReader cpu, IMemoryTelemetryReader memory, IEventSystem bus)
    {
        _scheduler = scheduler;
        _cpu = new CpuReadJob(cpu, bus);
        _ram = new RamReadJob(memory, bus);
    }

    public Task RunAsync(TimeSpan interval, CancellationToken ct)
    {
        var cpuTask = _scheduler.RunAsync(interval, _cpu.TickAsync, ct);
        var ramTask = _scheduler.RunAsync(interval, _ram.TickAsync, ct);
        return Task.WhenAll(cpuTask, ramTask);
    }
}
