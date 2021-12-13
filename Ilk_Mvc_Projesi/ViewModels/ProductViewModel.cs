using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Bu alan boş olamaz")]
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? supplierId { get; set; }
        public string CompanyName { get; set; }
        [Range(0,999999999,ErrorMessage = "Ürün Fiyatı 0-999999999 olmalı.")]
        public decimal? UnitPrice { get; set; }

    }
}
