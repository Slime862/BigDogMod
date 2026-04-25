using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace BaseLib.Config;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ConfigSectionAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}

public abstract class ModConfig
{
    private readonly Dictionary<string, object?> _defaults;
    private readonly Dictionary<string, PropertyInfo> _configPropertyMap;

    protected ModConfig()
    {
        ConfigProperties = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            .Where(property => property.GetMethod != null && property.SetMethod != null)
            .ToList();

        _configPropertyMap = ConfigProperties.ToDictionary(property => property.Name, StringComparer.Ordinal);
        _defaults = ConfigProperties.ToDictionary(property => property.Name, property => property.GetValue(null), StringComparer.Ordinal);
        Load();
    }

    private static string SettingsRoot => Path.Combine(AppContext.BaseDirectory, "config");

    internal List<PropertyInfo> ConfigProperties { get; }

    public virtual void Changed()
    {
    }

    public virtual void Save()
    {
        Directory.CreateDirectory(SettingsRoot);
        var values = _configPropertyMap.ToDictionary(pair => pair.Key, pair => SerializeValue(pair.Value.GetValue(null)), StringComparer.Ordinal);
        var json = JsonSerializer.Serialize(values, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(GetPath(), json);
    }

    protected virtual void Load()
    {
        var path = GetPath();
        if (!File.Exists(path))
        {
            return;
        }

        try
        {
            var json = File.ReadAllText(path);
            var values = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
            if (values == null)
            {
                return;
            }

            foreach (var (name, property) in _configPropertyMap)
            {
                if (!values.TryGetValue(name, out var value))
                {
                    continue;
                }

                property.SetValue(null, DeserializeValue(property.PropertyType, value));
            }
        }
        catch
        {
            RestoreDefaultsNoConfirm();
        }
    }

    protected void RestoreDefaultsNoConfirm()
    {
        foreach (var (name, value) in _defaults)
        {
            _configPropertyMap[name].SetValue(null, value);
        }
    }

    protected static string GetBaseLibLabelText(string key) => key switch
    {
        "RestoreDefaultsButton" => "Restore defaults",
        _ => key,
    };

    protected string GetLabelText(string key)
    {
        return key;
    }

    private string GetPath()
    {
        return Path.Combine(SettingsRoot, $"{GetType().Name}.json");
    }

    private static object? SerializeValue(object? value)
    {
        return value;
    }

    private static object? DeserializeValue(Type targetType, JsonElement value)
    {
        if (targetType == typeof(bool))
        {
            return value.GetBoolean();
        }

        if (targetType == typeof(int))
        {
            return value.GetInt32();
        }

        if (targetType == typeof(float))
        {
            return value.GetSingle();
        }

        if (targetType == typeof(double))
        {
            return value.GetDouble();
        }

        if (targetType == typeof(string))
        {
            return value.GetString();
        }

        if (targetType.IsEnum)
        {
            var enumText = value.GetString();
            return enumText == null ? Activator.CreateInstance(targetType) : Enum.Parse(targetType, enumText);
        }

        return JsonSerializer.Deserialize(value.GetRawText(), targetType);
    }
}

public abstract class SimpleModConfig : ModConfig;

public static class ModConfigRegistry
{
    private static readonly Dictionary<string, ModConfig> ModConfigs = new(StringComparer.OrdinalIgnoreCase);

    public static void Register(string modId, ModConfig config)
    {
        ModConfigs[modId] = config;
    }
}
