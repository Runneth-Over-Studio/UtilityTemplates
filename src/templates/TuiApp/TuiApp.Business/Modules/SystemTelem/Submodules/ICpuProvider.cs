using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem.Submodules;

public interface ICpuProvider
{
    int GetLogicalCoreCount();

    /// <summary>
    /// Returns CPU utilization for the current process based on a sample delta.
    /// First call typically returns 0.
    /// </summary>
    Task<CpuSample> SampleCPUAsync(CancellationToken cancellationToken = default);
}
