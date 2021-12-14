using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.ViewModels
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "Kategori Adı Alanı Gereklidir.")]
        [StringLength(15,ErrorMessage ="Açıklama Alanı en fazla 15 karakter olabilir.")]
        [Display(Name ="Kategori Adı")]
        public string CategoryName { get; set; }
        [Display(Name ="Açıklama")]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int ProductCount { get; set; }

    }
}
