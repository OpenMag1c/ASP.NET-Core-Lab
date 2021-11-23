using System.Text.Json.Serialization;

namespace DAL.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Platforms
    {
        PersonalComputer,
        Console,
        Mobile,
        Web,
        VirtualReality,

        NoPlatform = 0
    }
}