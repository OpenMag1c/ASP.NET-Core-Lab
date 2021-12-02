using System.Text.Json.Serialization;

namespace DAL.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Platforms
    {
        NoPlatform,
        PersonalComputer,
        Console,
        Mobile,
        Web,
        VirtualReality
    }
}