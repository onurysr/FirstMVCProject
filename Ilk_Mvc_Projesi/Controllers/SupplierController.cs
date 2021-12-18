using Ilk_Mvc_Projesi.Models;
using Ilk_Mvc_Projesi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.Controllers
{
    public class SupplierController : Controller
    {
        private readonly NorthwindContext _context;
        public SupplierController(NorthwindContext context)
        {
            _context = context;
        }
        private int _pagesize = 8;
        public IActionResult Index(int? page = 1)
        {
            var model = _context.Suppliers
                .OrderBy(x => x.CompanyName)
                .Skip((page.GetValueOrDefault() - 1) * _pagesize)
                .Take(_pagesize)
                .ToList();

            ViewBag.Page = page.GetValueOrDefault(1);
            ViewBag.Limit = (int)Math.Ceiling(_context.Suppliers.Count() / (double)_pagesize);
            return View(model);
        }

        public IActionResult Detail(int? id)
        {
            var data = _context.Suppliers.FirstOrDefault(x => x.SupplierId == id);
            if (data == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new SupplierViewModel()
            {
                SupplierId = data.SupplierId,
                City = data.City,
                CompanyName = data.CompanyName,
                ContactName = data.ContactName,
                ContactTitle = data.ContactTitle,
                Country = data.Country
            };

            return View(model);
        }

    }
}
