using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.Scheduling.Submodules;
using TuiApp.Business.Modules.SystemTelem.Submodules;

namespace TuiApp.Business.Modules.Scheduling;

/// <summary>
/// Provides a scheduler implementation using <see cref="PeriodicTimer"/> to execute recurring tasks at specified intervals.
/// </summary>
/// <remarks>
/// This scheduler leverages the .NET <see cref="PeriodicTimer"/> class introduced in .NET 6 to provide
/// efficient, timer-based periodic execution of asynchronous tasks. It orchestrates background jobs
/// such as system resource sampling through the <see cref="ScheduledJobOrchestrator"/>.
/// The scheduler executes tasks immediately upon starting, then continues at regular intervals until cancellation.
/// </remarks>
public sealed class PeriodicTimerScheduler : IRepeatingScheduler
{
    private readonly ScheduledJobOrchestrator _scheduledJobOrchestrator;

    /// <summary>
    /// Initializes a new instance of the <see cref="PeriodicTimerScheduler"/> class.
    /// </summary>
    /// <param name="bus">The event system used to publish telemetry and diagnostic events.</param>
    /// <param name="cpuProvider">The provider for CPU telemetry samples.</param>
    /// <param name="memoryProvider">The provider for memory telemetry samples.</param>
    /// <remarks>
    /// This constructor creates and configures the internal <see cref="ScheduledJobOrchestrator"/>
    /// with the provided dependencies, which will manage the execution of scheduled jobs.
    /// </remarks>
    public PeriodicTimerScheduler(IEventSystem bus, ICpuProvider cpuProvider, IMemoryProvider memoryProvider)
    {
        _scheduledJobOrchestrator = new ScheduledJobOrchestrator(this, bus, cpuProvider, memoryProvider);
    }

    /// <inheritdoc/>
    public async Task RunAsync(TimeSpan interval, Func<CancellationToken, Task> tick, CancellationToken cancellationToken = default)
    {
        using PeriodicTimer timer = new(interval);

        // Run once immediately before waiting
        await tick(cancellationToken);

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            await tick(cancellationToken);
        }
    }

    /// <inheritdoc/>
    public Task InitiateScheduledJobsAsync(TimeSpan interval, CancellationToken ct)
    {
        return _scheduledJobOrchestrator.RunAsync(interval, ct);
    }
}
