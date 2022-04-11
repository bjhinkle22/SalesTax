using Microsoft.AspNetCore.Mvc;
using SalesTax.Domain.Interfaces;
using SalesTax.Domain.Models;

namespace SalesTax.Web.Controllers
{
    public class TaxRateLookupController : Controller
    {
        private readonly ITaxRateService _taxRateService;

        public TaxRateLookupController(ITaxRateService taxRateService)
        {
            _taxRateService = taxRateService;
        }

        [HttpGet]
        public IActionResult GetTaxRates()
        {
            //Create empty Model to hold values
            LocationTaxRateModel model = new();

            //Return Form with empty Model
            return View("TaxRateSearch", model);
        }

        [HttpPost]
        public IActionResult GetTaxRates(LocationTaxRateModel model)
        {
                //Call Service to get Tax rates with provided ZIP
                var response = _taxRateService.GetTaxRates(model.rate.zip);

                //Check response code
                if(response.Result.IsError == false)
                {
                    //Return response and View to display results
                    return View("TaxRateResults", response);
                }
                else
                {
                    //Return model with Error Flag set to true
                    return View("TaxRateSearch", response.Result);
                }
        }
    }
}