using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem.Submodules;

/// <summary>
/// Defines a contract for gathering system memory usage statistics and physical memory information.
/// </summary>
public interface IMemoryProvider
{
    /// <summary>
    /// Retrieves the total physical memory and currently available memory on the system.
    /// </summary>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item><description><c>TotalBytes</c> - The total guaranteed physical memory in bytes</description></item>
    /// <item><description><c>AvailableBytes</c> - The currently available memory in bytes (calculated as total minus used)</description></item>
    /// </list>
    /// </returns>
    (long? TotalBytes, long? AvailableBytes) TryGetPhysicalMemory();

    /// <summary>
    /// Samples the current memory usage on the system.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the sampling operation.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
    /// The task result contains a <see cref="MemorySample"/> with the current memory usage in bytes.
    /// </returns>
    Task<MemorySample> SampleMemoryAsync(CancellationToken cancellationToken = default);
}
