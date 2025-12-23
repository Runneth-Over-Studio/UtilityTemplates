using System;
using System.Threading;
using System.Threading.Tasks;

namespace TuiApp.Business.Modules.Scheduling;

public interface IRepeatingScheduler
{
    Task RunAsync(
        TimeSpan interval,
        Func<CancellationToken, Task> tick,
        CancellationToken cancellationToken = default);
}
