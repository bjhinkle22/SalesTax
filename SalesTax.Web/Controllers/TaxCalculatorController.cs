using Microsoft.AspNetCore.Mvc;
using SalesTax.Domain.Interfaces;
using SalesTax.Domain.Models;

namespace SalesTax.Web.Controllers
{
    public class TaxCalculatorController : Controller
    {
        private readonly ITaxRateService _taxRateService;

        public TaxCalculatorController(ITaxRateService taxRateService)
        {
            _taxRateService = taxRateService;
        }

        [HttpGet]
        public IActionResult CalculateSalesTax()
        {
            //Create empty Model to hold values
            OrderModel model = new();

            //Return Form with empty Model
            return View("CalculateSalesTax", model);
        }

        [HttpPost]
        public IActionResult CalculateSalesTax(OrderModel model)
        {
            //Call Service to Calculate Sales tax based on given order information
            var response = _taxRateService.CalculateSalesTax(model);


            //Check response code
            if (response.Result.IsError == false)
            {
                //Return response and View to display results
                return View("CalculateSalesTaxResults", response);
            }
            else
            {
                //Return model with Error Flag set to true
                return View("CalculateSalesTaxResults", response);
            }
        }
    }
}
