
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IPStackDLL.Interfaces;


namespace IPStackApi.Models {

    [Table("IPDetails")]
    public class IPDetailsEntity : IIPDetails
    {
        [Key]
        public required string Ip {get; set;} 
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string Continent { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}