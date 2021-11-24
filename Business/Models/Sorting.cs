using System.Text.Json.Serialization;

namespace Business.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        Asc,
        Desc,

        Nothing = 0
    }
}