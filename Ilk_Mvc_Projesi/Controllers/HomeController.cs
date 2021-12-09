using Ilk_Mvc_Projesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; //injection

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var data = UrunManager.GetUrunLer();
            //_logger.LogInformation("Home index tetiklenmiştir."); 
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
