using Microsoft.AspNetCore.Mvc;
using SalesTax.Domain.Models;
using SalesTax.Web.Models;
using System.Diagnostics;

namespace SalesTax.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}