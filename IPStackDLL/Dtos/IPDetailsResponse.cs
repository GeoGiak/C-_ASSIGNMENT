
using System.Text.Json.Serialization;

namespace IPStackDLL.Dtos
{
    public class IPDetailsResponse
    {
        [JsonPropertyName("city")]
        public required string City {get; set;}
        [JsonPropertyName("country_name")]
        public required string CountryName { get; set; }
        [JsonPropertyName("continent_name")]
        public required string ContinentName { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}