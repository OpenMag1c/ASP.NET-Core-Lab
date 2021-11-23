using System.Text.Json.Serialization;

namespace Business.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        Asc = 1,
        Desc = 2,

        Nothing = 0
    }
}