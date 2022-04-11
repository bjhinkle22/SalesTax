using SalesTax.Domain.Interfaces;
using SalesTax.Domain.Models;
using System.Net.Http.Json;

namespace SalesTax.Domain.Services
{
    public class TaxRateService : ITaxRateService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TaxRateService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SalesTaxModel> CalculateSalesTax(OrderModel order)
        {
            var client = _httpClientFactory.CreateClient("TaxJarAPIBase");

            HttpResponseMessage response = await client.PostAsJsonAsync($"taxes/", order);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SalesTaxModel>();
            }
            else
            {
                var returnModel = new SalesTaxModel();

                returnModel.tax = new TaxModel(); 
                returnModel.tax.jurisdictions = new JurisdictionsModel();

                returnModel.IsError = true;

                return await Task.FromResult(returnModel);
            }
        }

        public async Task<LocationTaxRateModel> GetTaxRates(string zip)
        {
            var client = _httpClientFactory.CreateClient("TaxJarAPIBase");
            try
            {
                return await client.GetFromJsonAsync<LocationTaxRateModel>($"rates/{zip}");
            }
            catch (Exception)
            {
                var returnModel = new LocationTaxRateModel();
                returnModel.IsError = true;
                return returnModel;
            }
        }
    }
}