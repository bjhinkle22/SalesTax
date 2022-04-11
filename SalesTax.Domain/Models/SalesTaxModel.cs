namespace SalesTax.Domain.Models
{
    public class SalesTaxModel
    {
        public TaxModel tax { get; set; }
        public bool IsError { get; set; }
    }
}