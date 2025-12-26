using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;
using TuiApp.Business.Modules.SystemTelem.Messages;
using TuiApp.Business.Modules.SystemTelem.Submodules;

namespace TuiApp.Business.Modules.SystemTelem.Jobs;

public sealed class SampleSystemResourcesJob
{
    private readonly IEventSystem _bus;
    private readonly ICpuProvider _cpu;
    private readonly IMemoryProvider _memory;
    private readonly INetworkProvider _network;

    public SampleSystemResourcesJob(IEventSystem bus, ICpuProvider cpuProvider, IMemoryProvider memoryProvider, INetworkProvider networkProvider)
    {
        _bus = bus;
        _cpu = cpuProvider;
        _memory = memoryProvider;
        _network = networkProvider;
    }

    public async Task TickAsync(CancellationToken ct)
    {
        try
        {
            List<ActiveConnection> activeConnections = _network.GetActiveConnections();

            Task<CpuSample> cpuTask = _cpu.SampleCPUAsync(ct);
            Task<SystemResourcesSample> systemResourcesTask = _cpu.SampleSystemResourcesAsync(ct);
            Task<MemorySample> memoryTask = _memory.SampleMemoryAsync(ct);
            Task<NetworkSample> networkTask = _network.SampleNetworkAsync(ct);
            await Task.WhenAll(cpuTask, systemResourcesTask, memoryTask, networkTask);

            CpuSample cpuSample = cpuTask.Result;
            SystemResourcesSample systemResourcesSample = systemResourcesTask.Result;
            MemorySample memorySample = memoryTask.Result;
            NetworkSample networkSample = networkTask.Result;

            _bus.Publish(this, new SystemResourceSampleEventArgs()
            {
                CapturedAt = DateTimeOffset.Now,
                SystemCpuPercent = cpuSample.SystemCpuPercent,
                MemoryUsedInBytes = memorySample.MemoryUsedInBytes,
                TotalProcesses = systemResourcesSample.TotalProcesses,
                TotalThreads = systemResourcesSample.TotalThreads,
                TotalHandles = systemResourcesSample.TotalHandles,
                NetworkBytesSentPerSecond = networkSample.BytesSentPerSecond,
                NetworkBytesReceivedPerSecond = networkSample.BytesReceivedPerSecond,
                NetworkTotalBytesSent = networkSample.TotalBytesSent,
                NetworkTotalBytesReceived = networkSample.TotalBytesReceived,
                NetworkLatencyMilliseconds = networkSample.LatencyMilliseconds,
                ActiveConnections = activeConnections
            });
        }
        catch (Exception ex)
        {
            _bus.Publish(this,  new TelemetryErrorEventArgs()
            {
                CapturedAt = DateTimeOffset.Now,
                Message = ex.Message,
                ExceptionType = ex.GetType().Name
            });
        }
    }
}
