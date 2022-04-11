using SalesTax.Domain.Models;

namespace SalesTax.Domain.Interfaces
{
    public interface ITaxRateService
    {
        Task<LocationTaxRateModel> GetTaxRates(string zip);
        Task<SalesTaxModel> CalculateSalesTax(OrderModel order);
    }
}