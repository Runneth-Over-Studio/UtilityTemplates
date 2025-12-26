using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem.Submodules;

/// <summary>
/// Defines a contract for gathering network traffic statistics and active connection information.
/// </summary>
public interface INetworkProvider
{
    /// <summary>
    /// Samples current network traffic statistics including throughput rates and latency.
    /// </summary>
    Task<NetworkSample> SampleNetworkAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of currently active TCP connections on the system.
    /// </summary>
    List<ActiveConnection> GetActiveConnections();
}
