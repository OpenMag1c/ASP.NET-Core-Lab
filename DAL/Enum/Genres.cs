using System.Text.Json.Serialization;

namespace DAL.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Genres
    {
        ActionRPG,
        Shooter,
        Strategy,
        Puzzle,
        MOBA,
        MusicGame,
        RPG,

        AllGenres = 0
    }
}