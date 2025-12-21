using System;

namespace TuiApp.Business.Modules.Scheduling;

public interface IRepeatingScheduler
{
    Task RunAsync(
        TimeSpan interval,
        Func<CancellationToken, Task> tick,
        CancellationToken cancellationToken = default);
}
