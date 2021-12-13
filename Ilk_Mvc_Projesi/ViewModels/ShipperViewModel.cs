using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.ViewModels
{
    public class ShipperViewModel
    {

        public int ShipperId { get; set; }
        [StringLength(40, ErrorMessage = "Şirket adı 40 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Bu Alan Boş Olamaz")]
        public string CompanyName { get; set; }
        [StringLength(15, ErrorMessage = "Telefon numarası 15 karakterden fazla olamaz.")]
        public string Phone { get; set; }
    }
}
