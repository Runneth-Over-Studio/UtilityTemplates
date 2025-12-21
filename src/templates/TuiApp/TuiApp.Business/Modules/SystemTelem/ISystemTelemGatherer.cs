using TuiApp.Business.Modules.DTOs;

namespace TuiApp.Business.Modules.SystemTelem;

public interface ISystemTelemGatherer
{
    Task<SystemSnapshot> GatherSnapshotAsync(CancellationToken cancellationToken = default);
}
