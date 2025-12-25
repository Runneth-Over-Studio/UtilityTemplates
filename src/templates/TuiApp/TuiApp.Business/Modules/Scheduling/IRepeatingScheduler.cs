using System;
using System.Threading;
using System.Threading.Tasks;

namespace TuiApp.Business.Modules.Scheduling;

/// <summary>
/// Defines a contract for scheduling and executing recurring tasks at specified intervals.
/// </summary>
/// <remarks>
/// Implementations of this interface provide mechanisms to run periodic jobs with support
/// for cancellation and asynchronous execution. This is commonly used for background tasks
/// such as telemetry sampling, health checks, or periodic data synchronization.
/// </remarks>
public interface IRepeatingScheduler
{
    /// <summary>
    /// Runs the specified asynchronous task repeatedly at the given interval until cancellation is requested.
    /// </summary>
    /// <param name="interval">The time interval between successive executions of the task.</param>
    /// <param name="tick">
    /// A function that represents the work to be performed on each interval.
    /// The function receives a <see cref="CancellationToken"/> and returns a <see cref="Task"/>.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to stop the recurring execution.
    /// Defaults to <see cref="CancellationToken.None"/> if not provided.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task completes when the <paramref name="cancellationToken"/> is canceled.
    /// </returns>
    /// <remarks>
    /// The <paramref name="tick"/> function is invoked repeatedly at the specified <paramref name="interval"/>.
    /// Each invocation waits for the previous execution to complete before starting the next interval timer.
    /// </remarks>
    Task RunAsync(TimeSpan interval, Func<CancellationToken, Task> tick, CancellationToken cancellationToken = default);

    /// <summary>
    /// Initiates all configured scheduled jobs to run at the specified interval.
    /// </summary>
    /// <param name="interval">The time interval between successive executions of scheduled jobs.</param>
    /// <param name="ct">A cancellation token that can be used to stop all scheduled jobs.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation of all scheduled jobs.
    /// The task completes when the <paramref name="ct"/> is canceled or all jobs have finished.
    /// </returns>
    /// <remarks>
    /// This method serves as an entry point for starting the scheduler with pre-configured jobs.
    /// It typically orchestrates multiple calls to <see cref="RunAsync"/> for different job types.
    /// </remarks>
    public Task InitiateScheduledJobsAsync(TimeSpan interval, CancellationToken ct);
}
