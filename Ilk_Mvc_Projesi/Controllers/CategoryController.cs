using Ilk_Mvc_Projesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.Controllers
{
    public class CategoryController : Controller
    {
        private readonly NorthwindContext _context; // dependency injection 1 kere newlenecek oturum kapanana kadar
        public CategoryController(NorthwindContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {         
            var data = _context.Categories.Include(x=> x.Products).OrderBy(x => x.CategoryName).ToList(); //Include diğer tablodan veri çekmeye yaryıor
            return View(data);
        }

        public IActionResult Detail(int? id) //CategoryId den ürün bulma
        {
            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
