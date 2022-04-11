using System.ComponentModel.DataAnnotations;

namespace SalesTax.Domain.Models
{
    public class RateModel
    {
        public string city { get; set; }
        public string city_rate { get; set; }
        public string combined_district_rate { get; set; }
        public string combined_rate { get; set; }
        public string country { get; set; }
        public string country_rate { get; set; }
        public string county { get; set; }
        public string county_rate { get; set; }
        public bool freight_taxable { get; set; }
        public string state { get; set; }
        public string state_rate { get; set; }
        [Required]
        [RegularExpression("^[0-9]{5}", ErrorMessage = "Please enter a valid 5 digit Zip Code.")]
        public string zip { get; set; }
    }
}