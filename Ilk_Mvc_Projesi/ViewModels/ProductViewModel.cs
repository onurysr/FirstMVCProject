using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Ürün Adı Boş olamaz")]
        [Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Fiyat Boş olamaz")]
        [Display(Name = "Fiyat")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "Stok Sayısı Boş olamaz")]
        [Display(Name = "Stok Sayısı")]
        public int UnitsInStock { get; set; }

        public int ProductId { get; set; }

    }
}
