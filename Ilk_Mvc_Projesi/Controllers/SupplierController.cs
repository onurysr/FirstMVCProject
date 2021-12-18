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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var supplier = new Supplier()
            {
                CompanyName = model.CompanyName,
                City = model.City,
                Country = model.Country,
                ContactName = model.ContactName

            };

            try
            {
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
                return RedirectToAction(nameof(Detail), new { supplier.SupplierId });
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        public IActionResult Delete(int? id)
        {
            var silinecekSupp = _context.Suppliers.FirstOrDefault(x => x.SupplierId == id);
            if (silinecekSupp == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Suppliers.Remove(silinecekSupp);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Update(int? id)
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
                Country = data.Country
            };

            return View(model);

        }

        [HttpPost]
        public IActionResult Update(SupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var supplier = _context.Suppliers.FirstOrDefault(x => x.SupplierId == model.SupplierId);
            if (supplier == null)
            {
                return RedirectToAction(nameof(Index));
            }
            supplier.CompanyName = model.CompanyName;
            supplier.ContactName = model.ContactName;
            supplier.Country = model.Country;
            supplier.City = model.City;
            try
            {
                _context.Suppliers.Update(supplier);
                _context.SaveChanges();
                return RedirectToAction(nameof(Detail), new { supplier.SupplierId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

    }
}
