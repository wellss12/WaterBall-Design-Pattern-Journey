using System.Text.Json;
using System.Text.Json.Serialization;
using Chapter._4._4.H.日誌框架.Domain.LogFramework;

namespace Chapter._4._4.H.日誌框架.Json;

public class LoggerConfig
{
    public LevelThreshold? LevelThreshold { get; set; }
    public ExporterConfig? Exporter { get; set; }

    public LayoutType? Layout { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = new();

    [JsonIgnore]
    public Dictionary<string, LoggerConfig> Children
    {
        get
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };

            return AdditionalData.ToDictionary(
                data => data.Key,
                data => JsonSerializer.Deserialize<LoggerConfig>(data.Value.GetRawText(), options))!;
        }
    }
}