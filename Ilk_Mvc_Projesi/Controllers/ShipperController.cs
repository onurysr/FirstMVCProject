using Ilk_Mvc_Projesi.Models;
using Ilk_Mvc_Projesi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc_Project1.Controllers
{
    public class ShipperController : Controller
    {
        private readonly NorthwindContext _context;
        public ShipperController(NorthwindContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Shippers.Include(x => x.Orders).ThenInclude(x => x.OrderDetails).OrderBy(x => x.CompanyName).ToList();
            return View(model);
        }

        public IActionResult Detail(int? id)
        {
            var shipper = _context.Shippers.Include(x => x.Orders).ThenInclude(x => x.OrderDetails).FirstOrDefault(x => x.ShipperId == id);
            if (shipper == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(shipper);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ShipperViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(model));
            }

            var shipper = new Shipper()
            {
                CompanyName = model.CompanyName,
                Phone = model.Phone
            };
            _context.Shippers.Add(shipper);
            try
            {
                _context.SaveChanges();
                return RedirectToAction(nameof(Detail), new { id = shipper.ShipperId });
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, $"{model.CompanyName} eklenirken Hata oluştu.");
                return View(model);
            }
        }

        public IActionResult Delete(int? shipperId)
        {
            var silinecek = _context.Shippers.FirstOrDefault(x => x.ShipperId == shipperId);
            if (silinecek == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Shippers.Remove(silinecek);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }
            TempData["silinen_shipper"] = silinecek.CompanyName;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            var shipper = _context.Shippers.FirstOrDefault(x => x.ShipperId == id);
            if (shipper == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new ShipperViewModel()
            {
                ShipperId = shipper.ShipperId,
                CompanyName = shipper.CompanyName,
                Phone = shipper.Phone
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ShipperViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var shipper = _context.Shippers.FirstOrDefault(x => x.ShipperId == model.ShipperId);
            try
            {
                shipper.CompanyName = model.CompanyName;
                shipper.Phone = model.Phone;
                _context.Shippers.Update(shipper);
                _context.SaveChanges();
                return RedirectToAction(nameof(Detail), new { id = shipper.ShipperId });
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, $"{model.CompanyName} eklenirken Hata oluştu.");
                return View(model);
            }
        }
    }
}

