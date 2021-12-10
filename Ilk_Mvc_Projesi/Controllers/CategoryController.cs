using Ilk_Mvc_Projesi.Models;
using Ilk_Mvc_Projesi.ViewModels;
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

        [HttpGet]
        public IActionResult Index()
        {
            var data = _context.Categories.Include(x => x.Products).OrderBy(x => x.CategoryName).ToList(); //Include diğer tablodan veri çekmeye yaryıor
            return View(data);
        }

        public IActionResult Detail(int? id) //CategoryId den ürün bulma
        {
            var category = _context.Categories.Include(x => x.Products).ThenInclude(x => x.OrderDetails).ThenInclude(x => x.Order).FirstOrDefault(x => x.CategoryId == id); //thenInclude eklediğimiz verinin bağlantısını ekelemk için kullanılır.

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
        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = new Category()
            {
                CategoryName = model.CategoryName,
                Description = model.Description
            };
            _context.Categories.Add(category);
            try
            {
                _context.SaveChanges();
                return RedirectToAction("Detail"/*, new { id = category.CategoryId }*/); // detay tablosu/id sitesine yönlendirir.
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"{model.CategoryName} eklenirken bir hata oluştu tekrar deneyin"); //ilk sıraya hata nerdeyse oraya yazmamız lazım ama hata bir tabloda olmadığı için boş yolladık
            }

            return View();
        }

        public IActionResult Delete(int? categoryId)
        {
            var silinecek = _context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            try
            {
                _context.Categories.Remove(silinecek);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Detail), new { id = categoryId }); //nameof string içinde yazar, typeof ise tipi getirir.
            }

            TempData["Silinen_Kategori"] = silinecek.CategoryName;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new CategoryViewModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == model.CategoryId);
            try
            {
                category.CategoryName = model.CategoryName;
                category.Description = model.Description;

                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Detail"/*, new { id = category.CategoryId }*/); // detay tablosu/id sitesine yönlendirir.
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"{model.CategoryName} eklenirken bir hata oluştu tekrar deneyin"); //ilk sıraya hata nerdeyse oraya yazmamız lazım ama hata bir tabloda olmadığı için boş yolladık
                return View();
            }

            
        }
    }
}
