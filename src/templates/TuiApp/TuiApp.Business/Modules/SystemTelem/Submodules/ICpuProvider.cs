using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem.Submodules;

/// <summary>
/// Defines a contract for gathering CPU utilization metrics and system resource statistics.
/// </summary>
public interface ICpuProvider
{
    /// <summary>
    /// Gets the number of logical processor cores available on the system.
    /// </summary>
    int GetLogicalCoreCount();

    /// <summary>
    /// Samples system-wide resource counts including processes, threads, and handles.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the sampling operation.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
    /// The task result contains a <see cref="SystemResourcesSample"/>.
    /// </returns>
    Task<SystemResourcesSample> SampleSystemResourcesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Samples the current system-wide CPU utilization percentage.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the sampling operation.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
    /// The task result contains a <see cref="CpuSample"/>.
    /// </returns>
    Task<CpuSample> SampleCPUAsync(CancellationToken cancellationToken = default);
}
