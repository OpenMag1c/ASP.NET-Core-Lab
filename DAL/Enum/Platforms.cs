using System.Text.Json.Serialization;

namespace DAL.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Platforms
    {
        PersonalComputer = 0,
        Console = 1,
        Mobile = 2,
        Web = 3,
        VirtualReality = 4,

        NoPlatform = -1
    }
}