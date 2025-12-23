using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.DTOs;

namespace TuiApp.Business.Modules.HardwareTelem.CPU;

public sealed class CpuTelemetryReader : ICpuTelemetryReader
{
    private readonly Process _process;
    private readonly int _logicalCores;

    private DateTimeOffset? _lastWallTime;
    private TimeSpan? _lastCpuTime;

    public CpuTelemetryReader()
    {
        _process = Process.GetCurrentProcess();
        _logicalCores = Math.Max(1, Environment.ProcessorCount);
    }

    public Task<CpuTelemetrySampleEventArgs> ReadAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _process.Refresh();

        var now = DateTimeOffset.Now;
        var cpu = _process.TotalProcessorTime;

        double percent = 0;

        if (_lastWallTime is not null && _lastCpuTime is not null)
        {
            var wallDelta = (now - _lastWallTime.Value).TotalMilliseconds;
            var cpuDelta = (cpu - _lastCpuTime.Value).TotalMilliseconds;

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

        var sample = new CpuTelemetrySampleEventArgs()
        {
            CapturedAt = now,
            ProcessCpuPercent = percent,
            TotalProcessorTime = cpu,
            LogicalCores = _logicalCores
        };

        return Task.FromResult(sample);
    }
}
