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
            var category = _context.Categories.Include(x=>x.Products).ThenInclude(x=>x.OrderDetails).ThenInclude(x=>x.Order).FirstOrDefault(x => x.CategoryId == id); //thenInclude eklediğimiz verinin bağlantısını ekelemk için kullanılır.

            var category2 = from cat in _context.Categories //buda yukarıdaki gibi sql sorgusu gibi yapılıyor.
                            join prod in _context.Products on cat.CategoryId equals prod.CategoryId
                            join odetail in _context.OrderDetails on prod.ProductId equals odetail.ProductId
                            where cat.CategoryId == id
                            select cat;
            var model = category2.FirstOrDefault();

            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
