using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;
using TuiApp.Business.Modules.SystemTelem.Messages;
using TuiApp.Business.Modules.SystemTelem.Submodules;

namespace TuiApp.Business.Modules.SystemTelem.Jobs;

public sealed class SampleSystemResourcesJob
{
    private readonly IEventSystem _bus;
    private readonly ICpuProvider _cpu;
    private readonly IMemoryProvider _memory;

    public SampleSystemResourcesJob(IEventSystem bus, ICpuProvider cpuProvider, IMemoryProvider memoryProvider)
    {
        _bus = bus;
        _cpu = cpuProvider;
        _memory = memoryProvider;
    }

    public async Task TickAsync(CancellationToken ct)
    {
        try
        {
            Task<CpuSample> cpuTask = _cpu.SampleCPUAsync(ct);
            Task<MemorySample> memoryTask = _memory.SampleMemoryAsync(ct);
            await Task.WhenAll(cpuTask, memoryTask);

            CpuSample cpuSample = cpuTask.Result;
            MemorySample memorySample = memoryTask.Result;

            _bus.Publish(this, new SystemResourceSampleEventArgs()
            {
                CapturedAt = DateTimeOffset.Now,
                ProcessCpuPercent = cpuSample.ProcessCpuPercent,
                TotalProcessorTime = cpuSample.TotalProcessorTime,
                MemoryWorkingSetBytes = memorySample.ProcessWorkingSetBytes,
                MemoryPrivateBytes = memorySample.ProcessPrivateBytes
            });
        }
        catch (Exception ex)
        {
            _bus.Publish(this,  new TelemetryErrorEventArgs()
            {
                CapturedAt = DateTimeOffset.Now,
                Message = ex.Message,
                ExceptionType = ex.GetType().Name
            });
        }
    }
}
