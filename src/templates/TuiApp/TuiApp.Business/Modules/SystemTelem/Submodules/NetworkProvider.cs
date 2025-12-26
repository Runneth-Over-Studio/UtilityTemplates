using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using TuiApp.Business.Modules.SystemTelem.DTOs;

namespace TuiApp.Business.Modules.SystemTelem.Submodules;

/// <summary>
/// Provides network traffic statistics and active connection information.
/// </summary>
public sealed class NetworkProvider : INetworkProvider
{
    private long _lastBytesSent = 0;
    private long _lastBytesReceived = 0;
    private DateTimeOffset _lastSampleTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkProvider"/> class.
    /// </summary>
    /// <remarks>
    /// Captures initial network statistics to establish a baseline for calculating throughput rates in subsequent samples.
    /// </remarks>
    public NetworkProvider()
    {
        (long sent, long received) = GetTotalNetworkBytes();

        _lastBytesSent = sent;
        _lastBytesReceived = received;
        _lastSampleTime = DateTimeOffset.Now;
    }

    /// <inheritdoc/>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the sampling operation.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
    /// The task result contains a <see cref="NetworkSample"/> with network traffic data.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Thrown when the <paramref name="cancellationToken"/> is canceled before the operation completes.
    /// </exception>
    public async Task<NetworkSample> SampleNetworkAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        long latencyMilliseconds = await GetLatencyAsync();

        DateTimeOffset now = DateTimeOffset.Now;
        var (currentBytesSent, currentBytesReceived) = GetTotalNetworkBytes();

        // Calculate deltas
        long bytesSentDelta = currentBytesSent - _lastBytesSent;
        long bytesReceivedDelta = currentBytesReceived - _lastBytesReceived;
        double secondsElapsed = (now - _lastSampleTime).TotalSeconds;

        // Calculate per-second rates
        long bytesSentPerSecond = secondsElapsed > 0
            ? (long)(bytesSentDelta / secondsElapsed)
            : 0;
        long bytesReceivedPerSecond = secondsElapsed > 0
            ? (long)(bytesReceivedDelta / secondsElapsed)
            : 0;

        // Update last values
        _lastBytesSent = currentBytesSent;
        _lastBytesReceived = currentBytesReceived;
        _lastSampleTime = now;

        var sample = new NetworkSample
        {
            CapturedAt = now,
            BytesSentPerSecond = Math.Max(0, bytesSentPerSecond),
            BytesReceivedPerSecond = Math.Max(0, bytesReceivedPerSecond),
            TotalBytesSent = currentBytesSent,
            TotalBytesReceived = currentBytesReceived,
            LatencyMilliseconds = latencyMilliseconds
        };

        return sample;
    }

    /// <inheritdoc/>
    /// <returns>
    /// A list of <see cref="ActiveConnection"/> objects representing active TCP connections.
    /// Returns an empty list if unable to retrieve connection information.
    /// </returns>
    /// <remarks>
    /// Any errors during enumeration are silently handled and result in an empty list being returned.
    /// </remarks>
    public List<ActiveConnection> GetActiveConnections()
    {
        var connections = new List<ActiveConnection>();

        try
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnections = properties.GetActiveTcpConnections();

            foreach (var conn in tcpConnections)
            {
                connections.Add(new ActiveConnection
                {
                    LocalEndPoint = conn.LocalEndPoint.ToString(),
                    RemoteEndPoint = conn.RemoteEndPoint.ToString(),
                    Protocol = "TCP",
                    State = conn.State.ToString(),
                    ProcessId = null // Not directly available without platform-specific code
                });
            }
        }
        catch
        {
            // Best effort - return empty list on failure
        }

        return connections;
    }

    private static async Task<long> GetLatencyAsync()
    {
        long latencyMilliseconds = -1;

        try
        {
            using Ping ping = new();
            PingReply reply = await ping.SendPingAsync("1.1.1.1", 1000);

            if (reply.Status == IPStatus.Success)
            {
                latencyMilliseconds = reply.RoundtripTime;
            }
        }
        catch { }

        return latencyMilliseconds;
    }

    private static (long BytesSent, long BytesReceived) GetTotalNetworkBytes()
    {
        long totalBytesSent = 0;
        long totalBytesReceived = 0;

        try
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.OperationalStatus == OperationalStatus.Up
                          && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback);

            foreach (var ni in interfaces)
            {
                var stats = ni.GetIPv4Statistics();
                totalBytesSent += stats.BytesSent;
                totalBytesReceived += stats.BytesReceived;
            }
        }
        catch
        {
            // Best effort - return zeros on failure
        }

        return (totalBytesSent, totalBytesReceived);
    }
}
