using Microsoft.Extensions.Diagnostics.ResourceMonitoring;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem.Submodules;

/// <summary>
/// Provides CPU utilization metrics and system resource statistics.
/// </summary>
/// <remarks>
/// This provider leverages <see cref="IResourceMonitor"/> from Microsoft.Extensions.Diagnostics.ResourceMonitoring.
/// </remarks>
public sealed class CpuProvider : ICpuProvider
{
    private readonly IResourceMonitor _resourceMonitor;
    private readonly int _logicalCores;

    /// <summary>
    /// Initializes a new instance of the <see cref="CpuProvider"/> class.
    /// </summary>
    /// <param name="resourceMonitor">
    /// The resource monitor used to gather CPU utilization metrics from the system.
    /// </param>
    public CpuProvider(IResourceMonitor resourceMonitor)
    {
        _resourceMonitor = resourceMonitor;
        _logicalCores = Math.Max(1, Environment.ProcessorCount);
    }

    /// <inheritdoc/>
    public int GetLogicalCoreCount()
    {
        return _logicalCores;
    }

    /// <inheritdoc/>
    /// <exception cref="OperationCanceledException">
    /// Thrown when the <paramref name="cancellationToken"/> is canceled before the operation completes.
    /// </exception>
    public Task<SystemResourcesSample> SampleSystemResourcesAsync(CancellationToken cancellationToken = default)
    {
        // Run on thread pool to avoid blocking
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            var processes = Process.GetProcesses();
            int processCount = processes.Length;
            int threadCount = 0;
            int handleCount = 0;

            Parallel.ForEach(processes, (process, state) =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    state.Stop();
                    return;
                }

                try
                {
                    Interlocked.Add(ref threadCount, process.Threads.Count);
                    Interlocked.Add(ref handleCount, process.HandleCount);
                }
                catch { }
                finally
                {
                    process.Dispose();
                }
            });

            return new SystemResourcesSample
            {
                CapturedAt = DateTimeOffset.Now,
                TotalProcesses = processCount,
                TotalThreads = threadCount,
                TotalHandles = handleCount
            };
        }, cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="OperationCanceledException">
    /// Thrown when the <paramref name="cancellationToken"/> is canceled before the operation completes.
    /// </exception>
    public Task<CpuSample> SampleCPUAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var utilization = _resourceMonitor.GetUtilization(TimeSpan.FromSeconds(1));

        CpuSample sample = new()
        {
            CapturedAt = DateTimeOffset.Now,
            SystemCpuPercent = utilization.CpuUsedPercentage * 100.0
        };

        return Task.FromResult(sample);
    }
}
