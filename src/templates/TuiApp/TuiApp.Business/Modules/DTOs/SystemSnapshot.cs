using System;

namespace TuiApp.Business.Modules.DTOs;

public sealed record SystemSnapshot(
    DateTimeOffset CapturedAt,
    OsInfo Os,
    DotNetInfo DotNet,
    ProcessInfo Process,
    MachineInfo Machine
);

public sealed record OsInfo(
    string Description,
    string FrameworkDescription,
    string RuntimeIdentifier,
    string? Version
);

public sealed record DotNetInfo(
    string FrameworkDescription,
    string? EnvironmentVersion,
    int ProcessorCount
);

public sealed record ProcessInfo(
    int ProcessId,
    string ProcessName,
    string? WorkingDirectory,
    long WorkingSetBytes
);

public sealed record MachineInfo(
    string MachineName,
    bool Is64BitOs,
    bool Is64BitProcess
);
