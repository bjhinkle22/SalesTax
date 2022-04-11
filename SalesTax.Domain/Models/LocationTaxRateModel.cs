using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTax.Domain.Models
{
    public class LocationTaxRateModel
    {
        public RateModel rate { get; set; }
        public bool IsError { get; set; } = false;
    }
}