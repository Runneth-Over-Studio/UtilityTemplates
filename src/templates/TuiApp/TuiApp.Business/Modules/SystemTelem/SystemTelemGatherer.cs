using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.DTOs;

namespace TuiApp.Business.Modules.SystemTelem;

public sealed class SystemTelemGatherer : ISystemTelemGatherer
{
    public Task<SystemSnapshot> GatherSnapshotAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var proc = Process.GetCurrentProcess();

        var os = new OsInfo(
            Description: RuntimeInformation.OSDescription,
            FrameworkDescription: RuntimeInformation.FrameworkDescription,
            RuntimeIdentifier: RuntimeInformation.RuntimeIdentifier,
            Version: Environment.OSVersion?.VersionString
        );

        var dotnet = new DotNetInfo(
            FrameworkDescription: RuntimeInformation.FrameworkDescription,
            EnvironmentVersion: Environment.Version.ToString(),
            ProcessorCount: Environment.ProcessorCount
        );

        var process = new ProcessInfo(
            ProcessId: proc.Id,
            ProcessName: proc.ProcessName,
            WorkingDirectory: TryGetCwd(),
            WorkingSetBytes: proc.WorkingSet64
        );

        var machine = new MachineInfo(
            MachineName: Environment.MachineName,
            Is64BitOs: Environment.Is64BitOperatingSystem,
            Is64BitProcess: Environment.Is64BitProcess
        );

        var snapshot = new SystemSnapshot(
            CapturedAt: DateTimeOffset.Now,
            Os: os,
            DotNet: dotnet,
            Process: process,
            Machine: machine
        );

        return Task.FromResult(snapshot);
    }

    private static string? TryGetCwd()
    {
        try { return Environment.CurrentDirectory; }
        catch { return null; }
    }
}
