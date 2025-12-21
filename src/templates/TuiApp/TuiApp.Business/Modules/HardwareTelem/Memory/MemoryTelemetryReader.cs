using System.Diagnostics;
using TuiApp.Business.Modules.DTOs;
using TuiApp.Business.Modules.HardwareTelem.Memory;

namespace TuiApp.Business.Modules.HardwareTelem.CPU;

public sealed class MemoryTelemetryReader : IMemoryTelemetryReader
{
    private readonly Process _process;
    private readonly ISystemMemoryProvider _systemMemory;

    public MemoryTelemetryReader(ISystemMemoryProvider systemMemory)
    {
        _systemMemory = systemMemory;
        _process = Process.GetCurrentProcess();
    }

    public Task<MemoryTelemetrySampleEventArgs> ReadAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _process.Refresh();

        var (totalPhys, availPhys) = _systemMemory.TryGetPhysicalMemory();

        var sample = new MemoryTelemetrySampleEventArgs()
        {
            CapturedAt = DateTimeOffset.Now,
            ProcessWorkingSetBytes = _process.WorkingSet64,
            ProcessPrivateBytes = _process.PrivateMemorySize64,
            TotalPhysicalBytes = totalPhys,
            AvailablePhysicalBytes = availPhys
        };

        return Task.FromResult(sample);
    }
}
