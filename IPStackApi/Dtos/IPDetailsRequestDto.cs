using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;

namespace IPStackApi.Dtos
{
    public class IPDetailsRequestDto
    {
        [JsonPropertyName("IP")]
        public required string Ip {get; set;}
        [JsonPropertyName("City")]
        public required string City { get; set; }
        [JsonPropertyName("Country_name")]
        public required string Country { get; set; }
        [JsonPropertyName("Continent_name")]
        public required string Continent { get; set; }
        [JsonPropertyName("Latitude")]
        public required double Latitude { get; set; }
        [JsonPropertyName("Longitude")]
        public required double Longitude { get; set; }
    }
}