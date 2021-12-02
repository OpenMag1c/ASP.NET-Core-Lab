using System.Text.Json.Serialization;

namespace DAL.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Genres
    {
        AllGenres,
        ActionRPG,
        Shooter,
        Strategy,
        Puzzle,
        MOBA,
        MusicGame,
        RPG
    }
}