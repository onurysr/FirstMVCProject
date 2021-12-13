using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.ViewModels
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage = "Bu alan boş bırakalamaz")]
        [Display(Name = "Adı")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakalamaz")]
        [Display(Name = "Soyadı")]
        public string LastName { get; set; }
        [Display(Name = "Kıdemi")]
        public string Title { get; set; }

        public int EmployeeId { get; set; }
    }
}
