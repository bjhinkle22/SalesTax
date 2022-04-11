using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTax.Domain.Models
{
    public class OrderModel
    {
        public string from_country { get; set; } = "US";
        [RegularExpression("^[0-9]{5}", ErrorMessage = "Please enter a valid 5 digit Zip Code.")]
        public string from_zip { get; set; } = "92093";
        public string from_state { get; set; } = "CA";
        public string from_city { get; set; } = "La Jolla";
        public string from_street { get; set; } = "9500 Gilman Drive";
        public string to_country { get; set; } = "US";
        [RegularExpression("^[0-9]{5}", ErrorMessage = "Please enter a valid 5 digit Zip Code.")]
        public string to_zip { get; set; } = "90002";
        public string to_state { get; set; } = "CA";
        public string to_city { get; set; } = "Los Angeles";
        public string to_street { get; set; } = "1335 E 103rd St";
        public int amount { get; set; }
        public float shipping { get; set; }
    }
}