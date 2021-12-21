using LayoutKullanımı.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayoutKullanımı.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new List<Urun>()
            {
                new Urun() { Id = 1, Ad = "Kalem",Fiyat = 5},
                new Urun()
                {
                    Id = 2,
                    Ad = "Kitap",
                    Fiyat = 10
                }
            };
            return View(model);
        }
    }
}
