using System.Text.Json.Serialization;

namespace Business.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        Nothing,
        Asc,
        Desc
    }
}