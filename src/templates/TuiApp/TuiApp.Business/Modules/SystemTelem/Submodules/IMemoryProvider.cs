using System;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem.Submodules;

public interface IMemoryProvider
{
    /// <summary>
    /// Returns total/available physical memory if available for the OS.
    /// </summary>
    (long? TotalBytes, long? AvailableBytes) TryGetPhysicalMemory();

    Task<MemorySample> SampleMemoryAsync(CancellationToken cancellationToken = default);
}
