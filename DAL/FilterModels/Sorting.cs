using System.Text.Json.Serialization;

namespace DAL.FilterModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        Nothing,
        Asc,
        Desc
    }
}