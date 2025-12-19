using System;

namespace GuiApp.Business.Modules.Sample.Events;

public class GuidGenerated : EventArgs
{
    public required string UUID { get; init; }
}
