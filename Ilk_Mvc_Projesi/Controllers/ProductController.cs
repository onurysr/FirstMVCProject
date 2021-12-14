using Ilk_Mvc_Projesi.Models;
using Ilk_Mvc_Projesi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        private int _pageSize = 10;
        public IActionResult Index(int? page = 1)
        {
            var model = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .OrderBy(x => x.Category.CategoryName)
                .ThenBy(x => x.ProductName)
                .Skip((page.GetValueOrDefault() - 1) * _pageSize) // 3. sayfanın verileri 20den başlması lazım o yüzden böyle yaptık
                .Take(_pageSize)
                .ToList();

            //ViewBag.Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();
            //ViewBag.Suppliers = _context.Suppliers.OrderBy(x => x.CompanyName).ToList();

            ViewBag.Page = page.GetValueOrDefault(1);
            ViewBag.Limit = (int)Math.Ceiling(_context.Products.Count() / (double)_pageSize);
            return View(model);
        }

        public IActionResult Detail(int? id)
        {
            var data = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .FirstOrDefault(x => x.ProductId == id);

            if (data == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new ProductViewModel()
            {
                ProductId = data.ProductId,
                ProductName = data.ProductName,
                CategoryId = data.CategoryId,
                CategoryName = data.Category.CategoryName,
                supplierId = data.SupplierId,
                CompanyName = data.Supplier?.CompanyName,
                UnitPrice = data.UnitPrice
            };
            return View(model);
        }

        public IActionResult Update(int? id)
        {
            var data = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .FirstOrDefault(x => x.ProductId == id);

            if (data == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new ProductViewModel()
            {
                ProductId = data.ProductId,
                ProductName = data.ProductName,
                CategoryId = data.CategoryId,
                CategoryName = data.Category.CategoryName,
                supplierId = data.SupplierId,
                CompanyName = data.Supplier?.CompanyName,
                UnitPrice = data.UnitPrice
            };
            //burdan sonrası comboboxları eklemek için method yaptık yaptık

            ViewBag.CategoryList = GetCategoryList();
            ViewBag.SupplierList = GetSupplierList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = GetCategoryList();
                ViewBag.SupplierList = GetSupplierList();
                return View(model);
            }

            var product = _context.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }
            product.ProductName = model.ProductName;
            product.UnitPrice = model.UnitPrice;
            product.CategoryId = model.CategoryId;
            product.SupplierId = model.supplierId;
            try
            {
                
                _context.Products.Update(product);
                _context.SaveChanges();
                TempData["Mesaj"] = $"{product.ProductName} ürünü başarı ile güncellendi";
                return RedirectToAction(nameof(Detail), new { id = product.ProductId });
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.CategoryList = GetCategoryList();
                ViewBag.SupplierList = GetSupplierList();
                return View(model);
            }
            
        }

        private List<SelectListItem> GetCategoryList()
        {
            var categories = _context.Categories.OrderBy(x => x.CategoryName).ToList(); //categories'in içinde veritabanından Categoty tablosunu attık

            var categoryList = new List<SelectListItem>() //categorylist listeli oluşturduk tipi selectListItem
            {
                new SelectListItem("Kategori yok",null) //comboboxa gelecek ad ve karşılığındaki id bir tane oluşturduk
            };

            foreach (var category in categories) //foreachle categoryliste categories içindeki category adı ve idleri atadık
            {
                categoryList.Add(new SelectListItem(category.CategoryName, category.CategoryId.ToString())); //buradan category combosu için ekleme işlemi yaptık foreachle kaç tane varsa eklicek.
            }
            return categoryList;
        }
        private List<SelectListItem> GetSupplierList() //yukarıdakilerin aynısı
        {
            var suppliers = _context.Suppliers.OrderBy(x => x.CompanyName).ToList();

            //aynısını Supllier içinde yaptık.
            var supplierList = new List<SelectListItem>()
            {
                new SelectListItem("Tedarikçi yok",null)
            };

            foreach (var supplier in suppliers)
            {
                supplierList.Add(new SelectListItem(supplier.CompanyName, supplier.SupplierId.ToString()));
            }

            return supplierList;
        }

        //        public IActionResult Create()
        //        {
        //            return View();
        //        }

        //        [HttpPost]
        //        public IActionResult Create(ProductViewModel model)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return View(model);
        //            }

        //            var product = new Product()
        //            {
        //                ProductName = model.ProductName,
        //                UnitPrice = model.UnitPrice,
        //                UnitsInStock = (short)model.UnitsInStock

        //            };
        //            _context.Products.Add(product);
        //            try
        //            {
        //                _context.SaveChanges();
        //                return RedirectToAction(nameof(Detail), new { id = product.ProductId });
        //            }
        //            catch (Exception)
        //            {

        //                ModelState.AddModelError(string.Empty, $"{model.ProductName} eklenirken bir hata oluştu. Tekrar Deneyin");
        //                ModelState.AddModelError(string.Empty, $"{model.UnitPrice} eklenirken bir hata oluştu. Tekrar Deneyin");
        //                ModelState.AddModelError(string.Empty, $"{model.UnitsInStock} eklenirken bir hata oluştu. Tekrar Deneyin");
        //                return View(model);
        //            }
        //        }

        //        public IActionResult Delete(int? productId)
        //        {
        //            var silinecek = _context.Products.FirstOrDefault(x => x.ProductId == productId);
        //            try
        //            {
        //                _context.Products.Remove(silinecek);
        //                _context.SaveChanges();
        //            }
        //            catch (Exception)
        //            {

        //                return RedirectToAction(nameof(Index));
        //            }
        //            TempData["silinen_urun"] = silinecek.ProductName;
        //            return RedirectToAction(nameof(Index));
        //        }

        //        public IActionResult Update(int? id)
        //        {
        //            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
        //            if (product == null)
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }

        //            var model = new ProductViewModel()
        //            {
        //                ProductId = product.ProductId,
        //                ProductName = product.ProductName,
        //                UnitPrice = (decimal)product.UnitPrice,
        //                UnitsInStock = (int)product.UnitsInStock


        //            };

        //            return View(model);
        //        }

        //        [HttpPost]
        //        public IActionResult Update(ProductViewModel model)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return View(model);
        //            }

        //            var product = _context.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
        //            try
        //            {
        //                product.ProductName = model.ProductName;
        //                product.UnitPrice = model.UnitPrice;
        //                product.UnitsInStock = (short)model.UnitsInStock;

        //                _context.Products.Update(product);

        //                _context.SaveChanges();
        //                return RedirectToAction(nameof(Detail), new { id = product.ProductId });
        //            }
        //            catch (Exception)
        //            {

        //                ModelState.AddModelError(string.Empty, $"{model.ProductName} eklenirken bir hata oluştu. Tekrar Deneyin");
        //                ModelState.AddModelError(string.Empty, $"{model.UnitPrice} eklenirken bir hata oluştu. Tekrar Deneyin");
        //                ModelState.AddModelError(string.Empty, $"{model.UnitsInStock} eklenirken bir hata oluştu. Tekrar Deneyin");
        //                return View(model);
        //            }
        //        }
    }
}
