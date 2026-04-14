using System.Text.Json.Serialization;

public class RespawnVars
{
    [JsonPropertyName("secondsFromLastRespawn")]
    public int SecondsFromLastRespawn { get; set; }

    [JsonPropertyName("respawnSeed")]
    public long RespawnSeed { get; set; }

    [JsonPropertyName("obstacleClearCounter")]
    public int ObstacleClearCounter { get; set; }
}