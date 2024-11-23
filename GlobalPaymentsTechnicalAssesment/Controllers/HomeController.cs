using System.Diagnostics;
using GlobalPaymentsTechnicalAssesment.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlobalPaymentsTechnicalAssesment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
