using Microsoft.Extensions.Diagnostics.ResourceMonitoring;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem.Submodules;

/// <summary>
/// Provides system memory usage statistics and physical memory information.
/// </summary>
/// <remarks>
/// This provider leverages <see cref="IResourceMonitor"/> from Microsoft.Extensions.Diagnostics.ResourceMonitoring.
/// </remarks>
public sealed class MemoryProvider : IMemoryProvider
{
    private readonly IResourceMonitor _resourceMonitor;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryProvider"/> class.
    /// </summary>
    /// <param name="resourceMonitor">
    /// The resource monitor used to gather memory utilization metrics from the system.
    /// </param>
    public MemoryProvider(IResourceMonitor resourceMonitor)
    {
        _resourceMonitor = resourceMonitor;
    }

    /// <inheritdoc/>
    public (long? TotalBytes, long? AvailableBytes) TryGetPhysicalMemory()
    {
        var utilization = _resourceMonitor.GetUtilization(TimeSpan.FromSeconds(1));

        ulong totalBytes = utilization.SystemResources.GuaranteedMemoryInBytes;
        ulong usedBytes = utilization.MemoryUsedInBytes;
        ulong availableBytes = totalBytes - usedBytes;

        return ((long)totalBytes, (long)availableBytes);
    }

    /// <inheritdoc/>
    /// <exception cref="OperationCanceledException">
    /// Thrown when the <paramref name="cancellationToken"/> is canceled before the operation completes.
    /// </exception>
    public Task<MemorySample> SampleMemoryAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var utilization = _resourceMonitor.GetUtilization(TimeSpan.FromSeconds(1));

        MemorySample sample = new()
        {
            CapturedAt = DateTimeOffset.Now,
            MemoryUsedInBytes = (long)utilization.MemoryUsedInBytes
        };

        return Task.FromResult(sample);
    }
}
