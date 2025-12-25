using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.Jobs;
using TuiApp.Business.Modules.SystemTelem.Submodules;

namespace TuiApp.Business.Modules.Scheduling.Submodules;

/// <summary>
/// Orchestrates the execution of scheduled background jobs at specified intervals.
/// </summary>
/// <remarks>
/// It leverages the <see cref="IRepeatingScheduler"/> to run jobs at regular intervals with support for cancellation.
/// </remarks>
public sealed class ScheduledJobOrchestrator
{
    private readonly IRepeatingScheduler _scheduler;
    private readonly SampleSystemResourcesJob _sampleSystemResourcesJob;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduledJobOrchestrator"/> class.
    /// </summary>
    /// <param name="scheduler">The scheduler responsible for running jobs at regular intervals.</param>
    /// <param name="bus">The event system used to publish messages.</param>
    /// <param name="cpuProvider">The provider for CPU telemetry samples.</param>
    /// <param name="memoryProvider">The provider for memory telemetry samples.</param>
    public ScheduledJobOrchestrator(IRepeatingScheduler scheduler, IEventSystem bus, ICpuProvider cpuProvider, IMemoryProvider memoryProvider)
    {
        _scheduler = scheduler;
        _sampleSystemResourcesJob = new SampleSystemResourcesJob(bus, cpuProvider, memoryProvider);
    }

    /// <summary>
    /// Runs all scheduled jobs at the specified interval until cancellation is requested.
    /// </summary>
    /// <param name="interval">The time interval between job executions.</param>
    /// <param name="ct">A cancellation token that can be used to stop the scheduled jobs.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation of all scheduled jobs.
    /// The task completes when the <paramref name="ct"/> is canceled or all jobs have finished.
    /// </returns>
    public Task RunAsync(TimeSpan interval, CancellationToken ct)
    {
        Task sampleSystemResourcesTask = _scheduler.RunAsync(interval, _sampleSystemResourcesJob.TickAsync, ct);

        return Task.WhenAll(sampleSystemResourcesTask);
    }
}
