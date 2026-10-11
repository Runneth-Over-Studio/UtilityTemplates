using Material.Icons;
using RunnethOverStudio.AppToolkit.Modules.ComponentModel;

namespace SukiApp.Presentation.Desktop.Base;

public abstract class NavMenuPage(string displayName, MaterialIconKind icon, int index = 0) : BaseViewModel
{
    public string DisplayName { get; } = displayName;

    public MaterialIconKind Icon { get; } = icon; // Material Icons: https://pictogrammers.com/library/mdi/

    public int Index { get; } = index;
}
