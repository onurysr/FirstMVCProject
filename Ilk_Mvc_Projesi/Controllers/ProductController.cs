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
    public class ProductController : Controller
    {
        private readonly NorthwindContext _context;

        public ProductController(NorthwindContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Products.Include(x => x.OrderDetails)
                .OrderBy(x => x.ProductName).ToList();
            return View(model);
        }

        public IActionResult Detail(int? id)
        {
            var model = _context.Products.Include(x => x.OrderDetails).FirstOrDefault(x => x.ProductId == id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = new Product()
            {
                ProductName = model.ProductName,
                UnitPrice = model.UnitPrice,
                UnitsInStock = (short)model.UnitsInStock

            };
            _context.Products.Add(product);
            try
            {
                _context.SaveChanges();
                return RedirectToAction(nameof(Detail), new { id = product.ProductId });
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, $"{model.ProductName} eklenirken bir hata oluştu. Tekrar Deneyin");
                ModelState.AddModelError(string.Empty, $"{model.UnitPrice} eklenirken bir hata oluştu. Tekrar Deneyin");
                ModelState.AddModelError(string.Empty, $"{model.UnitsInStock} eklenirken bir hata oluştu. Tekrar Deneyin");
                return View(model);
            }
        }

        public IActionResult Delete(int? productId)
        {
            var silinecek = _context.Products.FirstOrDefault(x => x.ProductId == productId);
            try
            {
                _context.Products.Remove(silinecek);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }
            TempData["silinen_urun"] = silinecek.ProductName;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new ProductViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitPrice = (decimal)product.UnitPrice,
                UnitsInStock = (int)product.UnitsInStock


            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = _context.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
            try
            {
                product.ProductName = model.ProductName;
                product.UnitPrice = model.UnitPrice;
                product.UnitsInStock = (short)model.UnitsInStock;

                _context.Products.Update(product);

                _context.SaveChanges();
                return RedirectToAction(nameof(Detail), new { id = product.ProductId });
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, $"{model.ProductName} eklenirken bir hata oluştu. Tekrar Deneyin");
                ModelState.AddModelError(string.Empty, $"{model.UnitPrice} eklenirken bir hata oluştu. Tekrar Deneyin");
                ModelState.AddModelError(string.Empty, $"{model.UnitsInStock} eklenirken bir hata oluştu. Tekrar Deneyin");
                return View(model);
            }
        }
    }
}
