using System.Runtime.InteropServices;

namespace TuiApp.Business.Modules.HardwareTelem.Memory;

public sealed class SystemMemoryProvider : ISystemMemoryProvider
{
    public (long? TotalBytes, long? AvailableBytes) TryGetPhysicalMemory()
    {
        try
        {
            if (OperatingSystem.IsWindows())
                return GetWindowsMemory();

            if (OperatingSystem.IsLinux())
                return GetLinuxMemory();

            if (OperatingSystem.IsMacOS())
                return GetMacMemory();
        }
        catch
        {
            // best effort; return nulls
        }

        return (null, null);
    }

    // ---------- Windows ----------
    private static (long? TotalBytes, long? AvailableBytes) GetWindowsMemory()
    {
        var status = new MEMORYSTATUSEX
        {
            dwLength = (uint)Marshal.SizeOf<MEMORYSTATUSEX>()
        };

        if (!GlobalMemoryStatusEx(ref status))
            return (null, null);

        return ((long)status.ullTotalPhys, (long)status.ullAvailPhys);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

    // ---------- Linux ----------
    private static (long? TotalBytes, long? AvailableBytes) GetLinuxMemory()
    {
        // Parse /proc/meminfo for MemTotal and MemAvailable (kB)
        const string path = "/proc/meminfo";
        if (!File.Exists(path))
            return (null, null);

        long? totalKb = null;
        long? availKb = null;

        foreach (var line in File.ReadLines(path))
        {
            // e.g. "MemTotal:       16307384 kB"
            if (line.StartsWith("MemTotal:", StringComparison.OrdinalIgnoreCase))
                totalKb = ParseKb(line);

            if (line.StartsWith("MemAvailable:", StringComparison.OrdinalIgnoreCase))
                availKb = ParseKb(line);

            if (totalKb is not null && availKb is not null)
                break;
        }

        return (ToBytes(totalKb), ToBytes(availKb));

        static long? ParseKb(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) return null;
            return long.TryParse(parts[1], out var kb) ? kb : null;
        }

        static long? ToBytes(long? kb) => kb is null ? null : kb.Value * 1024L;
    }

    // ---------- macOS ----------
    private static (long? TotalBytes, long? AvailableBytes) GetMacMemory()
    {
        // Total via sysctl hw.memsize; Available is trickier (vm_stat parsing).
        // We'll provide Total and leave Available null for minimal demo correctness.
        long? total = null;

        try
        {
            total = SysctlLong("hw.memsize");
        }
        catch { /* ignore */ }

        return (total, null);
    }

    private static long SysctlLong(string name)
    {
        // sysctlbyname(const char *name, void *oldp, size_t *oldlenp, void *newp, size_t newlen);
        nuint len = (nuint)sizeof(long);

        var result = sysctlbyname(name, out long value, ref len, IntPtr.Zero, 0);
        if (result != 0)
            throw new InvalidOperationException($"sysctlbyname failed for {name}.");

        return value;
    }

    [DllImport("libc")]
    private static extern int sysctlbyname(
        [MarshalAs(UnmanagedType.LPWStr)] string name,
        out long oldp,
        ref nuint oldlenp,
        IntPtr newp,
        nuint newlen);
}
