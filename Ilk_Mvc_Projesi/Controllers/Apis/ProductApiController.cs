using Ilk_Mvc_Projesi.Models;
using Ilk_Mvc_Projesi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.Controllers.Apis
{


    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly NorthwindContext _context;
        public ProductApiController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel model)
        {
            var product = new Product()
            {
                CategoryId = model.CategoryId,
                ProductName = model.ProductName,
                UnitPrice = model.UnitPrice
            };

            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok(new
                {
                    Message = "Ürün Ekleme İşlemi Başarılı",
                    Model = product
                });
            }
            catch (Exception ex)
            {

                return BadRequest($"Bir Hata oluştu:{ex.Message}");
            }
        }
    }
}
