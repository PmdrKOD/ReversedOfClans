using System.Text.Json.Serialization;

public class Building
{
    [JsonPropertyName("data")]
    public int Data { get; set; }

    [JsonPropertyName("lvl")]
    public int Level { get; set; }

    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }
}
