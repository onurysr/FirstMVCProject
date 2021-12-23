using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name ="Kullanıcı Adı")]
        [Required(ErrorMessage ="Kullancıı Adı Alanı Gerekli")]
        public string UserName { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre Adı Alanı Gerekli")]
        [StringLength(50,MinimumLength =6,ErrorMessage ="Şifreniz Minimum 6 karakterli olmalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
