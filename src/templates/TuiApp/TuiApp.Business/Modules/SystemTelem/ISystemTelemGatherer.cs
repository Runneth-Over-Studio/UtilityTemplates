using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem;

/// <summary>
/// Defines a contract for gathering system telemetry snapshots containing operating system,
/// runtime, process, and machine information.
/// </summary>
public interface ISystemTelemGatherer
{
    /// <summary>
    /// Asynchronously gathers a comprehensive snapshot of current system telemetry data.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the snapshot gathering operation.
    /// Defaults to <see cref="CancellationToken.None"/> if not provided.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
    /// The task result contains a <see cref="SystemSnapshot"/> with system telemetry data.
    /// </returns>
    Task<SystemSnapshot> GatherSnapshotAsync(CancellationToken cancellationToken = default);
}
