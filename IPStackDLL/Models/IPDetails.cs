
using IPStackDLL.Interfaces;

namespace IPStackDLL.Models 
{
    public class IPDetails : IIPDetails
    {
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string Continent { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}