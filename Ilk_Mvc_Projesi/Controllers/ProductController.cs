using Ilk_Mvc_Projesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.Controllers
{
    public class ProductController : Controller
    {
        private readonly NorthwindContext _context;

        public ProductController(NorthwindContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Products.Include(x=> x.Category).OrderBy(x => x.ProductId).ToList();
            return View(model);
        }

        public IActionResult Detail(int? id)
        {
            var Product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (Product == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(Product);
        }

    }
}
