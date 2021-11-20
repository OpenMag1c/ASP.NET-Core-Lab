using System.Text.Json.Serialization;

namespace Business.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AscDesc
    {
        Asc = 0,
        Desc = 1,

        Nothing = -1
    }
}