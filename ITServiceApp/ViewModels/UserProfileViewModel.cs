using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.ViewModels
{
    public class UserProfileViewModel
    {
        
        [Required(ErrorMessage = "İsim alanı gerekli")]
        [StringLength(50)]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyadı alanı gerekli")]
        [StringLength(50)]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "E-posta alanı gerekli")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
