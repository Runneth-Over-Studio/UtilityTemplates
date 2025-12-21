using System;

namespace TuiApp.Business.Modules.Scheduling;

public sealed class PeriodicTimerScheduler : IRepeatingScheduler
{
    public async Task RunAsync(
        TimeSpan interval,
        Func<CancellationToken, Task> tick,
        CancellationToken cancellationToken = default)
    {
        using var timer = new PeriodicTimer(interval);

        // Optional: run once immediately before waiting
        await tick(cancellationToken);

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            await tick(cancellationToken);
        }
    }
}
