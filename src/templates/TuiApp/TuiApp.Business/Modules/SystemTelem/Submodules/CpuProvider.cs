using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem.Submodules;

public sealed class CpuProvider : ICpuProvider
{
    private readonly Process _process;
    private readonly int _logicalCores;

    private DateTimeOffset? _lastWallTime;
    private TimeSpan? _lastCpuTime;

    public CpuProvider()
    {
        _process = Process.GetCurrentProcess();
        _logicalCores = Math.Max(1, Environment.ProcessorCount);
    }

    public int GetLogicalCoreCount()
    {
        return _logicalCores;
    }

    public Task<CpuSample> SampleCPUAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _process.Refresh();

        DateTimeOffset now = DateTimeOffset.Now;
        TimeSpan cpu = _process.TotalProcessorTime;

        double percent = 0;

        if (_lastWallTime is not null && _lastCpuTime is not null)
        {
            double wallDelta = (now - _lastWallTime.Value).TotalMilliseconds;
            double cpuDelta = (cpu - _lastCpuTime.Value).TotalMilliseconds;

            if (wallDelta > 0)
            {
                // CPU% over interval; normalize by logical cores.
                percent = (cpuDelta / wallDelta) * 100.0 / _logicalCores;

                // Keep it sane for display (can spike due to timer jitter).
                if (percent < 0) percent = 0;
                if (percent > 100) percent = 100;
            }
        }

        _lastWallTime = now;
        _lastCpuTime = cpu;

        CpuSample sample = new CpuSample()
        {
            CapturedAt = now,
            ProcessCpuPercent = percent,
            TotalProcessorTime = cpu
        };

        return Task.FromResult(sample);
    }
}
