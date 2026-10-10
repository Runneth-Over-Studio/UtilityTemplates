using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GuiApp.Presentation.Desktop.Settings;

internal sealed class DesktopSettings : IDesktopSettings
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() },
        WriteIndented = true
    };

    private readonly string _settingsPath;

    public DesktopSettings(string applicationDataDirectory)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationDataDirectory);
        _settingsPath = Path.Combine(applicationDataDirectory, "desktop-settings.json");
    }

    public ThemePreference GetThemePreference()
    {
        return Read().Theme;
    }

    public void SetThemePreference(ThemePreference preference)
    {
        DesktopSettingsData settings = Read();
        settings.Theme = preference;
        File.WriteAllText(_settingsPath, JsonSerializer.Serialize(settings, SerializerOptions));
    }

    private DesktopSettingsData Read()
    {
        if (!File.Exists(_settingsPath))
        {
            return new DesktopSettingsData();
        }

        try
        {
            string json = File.ReadAllText(_settingsPath);
            return JsonSerializer.Deserialize<DesktopSettingsData>(json, SerializerOptions)
                ?? new DesktopSettingsData();
        }
        catch (JsonException)
        {
            return new DesktopSettingsData();
        }
    }

    private sealed class DesktopSettingsData
    {
        public ThemePreference Theme { get; set; } = ThemePreference.System;
    }
}
