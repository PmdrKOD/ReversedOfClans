using System.Text.Json.Serialization;

public class Obstacle
{
    [JsonPropertyName("data")]
    public int Data { get; set; }

    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }
}
