using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReversedOfClans.Packets.Messages.Server
{
    public class BaseData
    {
        [JsonPropertyName("buildings")]
        public List<Building> Buildings { get; set; } = new List<Building>();

        [JsonPropertyName("obstacles")]
        public List<Obstacle> Obstacles { get; set; } = new List<Obstacle>();

        [JsonPropertyName("traps")]
        public List<Trap> Traps { get; set; } = new List<Trap>();

        [JsonPropertyName("decos")]
        public List<Deco> Decos { get; set; } = new List<Deco>();

        [JsonPropertyName("respawnVars")]
        public RespawnVars RespawnVars { get; set; } = new RespawnVars();
    }


    public class BaseConverJSON
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

     
        public static string ToJson(BaseData baseData)
        {
            return JsonSerializer.Serialize(baseData, Options);
        }

      
        public static BaseData FromJson(string json)
        {
            return JsonSerializer.Deserialize<BaseData>(json, Options);
        }

  
        public static void SaveToFile(BaseData baseData, string filePath)
        {
            string json = ToJson(baseData);
            File.WriteAllText(filePath, json);
        }

        public static BaseData LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return FromJson(json);
        }

        public static BaseData CreateDefaultBase()
        {
            return new BaseData
            {
                Buildings = new List<Building>
                {
                    new Building { Data = 1000001, Level = 7, X = 10, Y = 19 },
                    new Building { Data = 1000011, Level = 5, X = 10, Y = 29 },
                    new Building { Data = 1000011, Level = 5, X = 7, Y = 25 },
                    new Building { Data = 1000011, Level = 5, X = 7, Y = 14 },
                    new Building { Data = 1000010, Level = 5, X = 14, Y = 24 },
                    new Building { Data = 1000010, Level = 5, X = 15, Y = 24 },
                    new Building { Data = 1000010, Level = 5, X = 16, Y = 24 },
                    new Building { Data = 1000010, Level = 5, X = 17, Y = 24 },
                    new Building { Data = 1000010, Level = 5, X = 17, Y = 23 },
                    new Building { Data = 1000010, Level = 5, X = 17, Y = 22 },
                    new Building { Data = 1000010, Level = 5, X = 17, Y = 19 },
                    new Building { Data = 1000010, Level = 5, X = 5, Y = 33 },
                    new Building { Data = 1000010, Level = 5, X = 5, Y = 34 },
                    new Building { Data = 1000010, Level = 5, X = 5, Y = 35 },
                    new Building { Data = 1000010, Level = 5, X = 5, Y = 36 }
                },
                Obstacles = new List<Obstacle>
                {
                    new Obstacle { Data = 8000020, X = 34, Y = 30 },
                    new Obstacle { Data = 8000020, X = 34, Y = 12 },
                    new Obstacle { Data = 8000020, X = 35, Y = 9 },
                    new Obstacle { Data = 8000020, X = 37, Y = 4 },
                    new Obstacle { Data = 8000020, X = 37, Y = 20 },
                    new Obstacle { Data = 8000020, X = 2, Y = 24 },
                    new Obstacle { Data = 8000020, X = 2, Y = 14 }
                },
                Traps = new List<Trap>
                {
                    new Trap { Data = 12000001, X = 10, Y = 35 },
                    new Trap { Data = 12000001, X = 29, Y = 24 },
                    new Trap { Data = 12000001, X = 29, Y = 19 },
                    new Trap { Data = 12000001, X = 30, Y = 19 },
                    new Trap { Data = 12000001, X = 30, Y = 24 },
                    new Trap { Data = 12000001, X = 11, Y = 7 },
                    new Trap { Data = 12000001, X = 21, Y = 21 },
                    new Trap { Data = 12000001, X = 21, Y = 20 },
                    new Trap { Data = 12000000, X = 29, Y = 21 },
                    new Trap { Data = 12000000, X = 24, Y = 7 },
                    new Trap { Data = 12000000, X = 16, Y = 7 },
                    new Trap { Data = 12000000, X = 23, Y = 35 },
                    new Trap { Data = 12000000, X = 14, Y = 35 },
                    new Trap { Data = 12000000, X = 23, Y = 26 },
                    new Trap { Data = 12000000, X = 23, Y = 15 },
                    new Trap { Data = 12000001, X = 25, Y = 21 },
                    new Trap { Data = 12000001, X = 23, Y = 28 },
                    new Trap { Data = 12000001, X = 23, Y = 13 },
                    new Trap { Data = 12000001, X = 17, Y = 20 },
                    new Trap { Data = 12000001, X = 17, Y = 21 }
                },
                Decos = new List<Deco>
                {
                    new Deco { Data = 18000001, X = 33, Y = 25 },
                    new Deco { Data = 18000001, X = 33, Y = 17 },
                    new Deco { Data = 18000002, X = 36, Y = 36 },
                    new Deco { Data = 18000002, X = 36, Y = 6 },
                    new Deco { Data = 18000002, X = 33, Y = 6 },
                    new Deco { Data = 18000002, X = 33, Y = 36 }
                },
                RespawnVars = new RespawnVars
                {
                    SecondsFromLastRespawn = 0,
                    RespawnSeed = 1529463799,
                    ObstacleClearCounter = 0
                }
            };
        }
    }
}
