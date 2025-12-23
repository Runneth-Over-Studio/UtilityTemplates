using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.DTOs;

namespace TuiApp.Business.Modules.HardwareTelem.CPU;

public interface ICpuTelemetryReader
{
    /// <summary>
    /// Returns CPU utilization for the current process based on a sample delta.
    /// First call typically returns 0.
    /// </summary>
    Task<CpuTelemetrySampleEventArgs> ReadAsync(CancellationToken cancellationToken = default);
}
