using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;
using TuiApp.Business.Modules.SystemTelem.Submodules;

namespace TuiApp.Business.Modules.SystemTelem;

/// <summary>
/// Gathers comprehensive system telemetry snapshots including operating system, runtime,
/// process, machine, memory, and CPU information.
/// </summary>
/// <remarks>
/// This implementation collects system information from various sources including the .NET runtime,
/// operating system APIs, and the current process. It aggregates data from memory and CPU providers
/// to create a complete snapshot of the system state at a point in time.
/// </remarks>
public sealed class SystemTelemGatherer : ISystemTelemGatherer
{
    private readonly IMemoryProvider _memoryProvider;
    private readonly ICpuProvider _cpuProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemTelemGatherer"/> class.
    /// </summary>
    /// <param name="memoryProvider">The provider for gathering physical memory information.</param>
    /// <param name="cpuProvider">The provider for gathering CPU and logical core information.</param>
    public SystemTelemGatherer(IMemoryProvider memoryProvider, ICpuProvider cpuProvider)
    {
        _memoryProvider = memoryProvider;
        _cpuProvider = cpuProvider;
    }

    /// <inheritdoc/>
    public Task<SystemSnapshot> GatherSnapshotAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Process proc = Process.GetCurrentProcess();

        OsInfo os = new(
            Description: RuntimeInformation.OSDescription,
            FrameworkDescription: RuntimeInformation.FrameworkDescription,
            RuntimeIdentifier: RuntimeInformation.RuntimeIdentifier,
            Version: Environment.OSVersion?.VersionString
        );

        DotNetInfo dotnet = new(
            FrameworkDescription: RuntimeInformation.FrameworkDescription,
            EnvironmentVersion: Environment.Version.ToString(),
            ProcessorCount: Environment.ProcessorCount
        );

        ProcessInfo process = new(
            ProcessId: proc.Id,
            ProcessName: proc.ProcessName,
            WorkingDirectory: TryGetCwd(),
            WorkingSetBytes: proc.WorkingSet64
        );

        MachineInfo machine = new(
            MachineName: Environment.MachineName,
            Is64BitOs: Environment.Is64BitOperatingSystem,
            Is64BitProcess: Environment.Is64BitProcess
        );

        SystemSnapshot snapshot = new(
            CapturedAt: DateTimeOffset.Now,
            Os: os,
            DotNet: dotnet,
            Process: process,
            Machine: machine,
            PhysicalMemory: _memoryProvider.TryGetPhysicalMemory(),
            LogicalCores: _cpuProvider.GetLogicalCoreCount()
        );

        return Task.FromResult(snapshot);
    }

    private static string? TryGetCwd()
    {
        try { return Environment.CurrentDirectory; }
        catch { return null; }
    }
}
