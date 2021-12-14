using Ilk_Mvc_Projesi.Models;
using Ilk_Mvc_Projesi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoryApiController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult GetCategories()
        {
            try
            {
                var categories = _context.Categories
                    .Include(x=>x.Products)
                    .OrderBy(x => x.CategoryName)
                    .Select(x=>new CategoryViewModel()
                    { 
                         CategoryId =x.CategoryId,
                          CategoryName = x.CategoryName,
                           Description = x.Description,
                            ProductCount = x.Products.Count
                    })
                    .ToList();
                return Ok(categories);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
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
                return Ok(new
                {
                    Message = "Kategori Ekleme işlemi Başarılı",
                    model = category
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        /*[Route("~/api/categoryapi/updatecategory/{id?}")] *///custom route
        public IActionResult UpdateCategory(int? id,CategoryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound("Kategori Bulunamadı");
            }

            category.CategoryName = model.CategoryName;
            category.Description = model.Description;

            try
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return Ok(new
                {
                    Message = "Kategori Güncelleştirme Başarılı",
                    Model = category
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult DeleteCategory(int? id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound("Böyle bir kategori Yok");
            }

            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return Ok("Silme İşlemi Başarılı");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
