using System;

namespace GuiApp.Business.Modules.Sample.Events;

public class TextSorted : EventArgs
{
    public required string? SortedText { get; init; }
}
