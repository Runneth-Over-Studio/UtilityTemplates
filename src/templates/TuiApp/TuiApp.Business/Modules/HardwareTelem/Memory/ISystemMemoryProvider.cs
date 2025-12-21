using System;

namespace TuiApp.Business.Modules.HardwareTelem.Memory;

public interface ISystemMemoryProvider
{
    /// <summary>
    /// Returns total/available physical memory if available for the OS.
    /// </summary>
    (long? TotalBytes, long? AvailableBytes) TryGetPhysicalMemory();
}
