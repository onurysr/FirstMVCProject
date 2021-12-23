using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Kullanıcı Adı Alanı Gereklidir")]
        [Display(Name="Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="İsim alanı gerekli")]
        [StringLength(50)]
        [Display(Name ="Ad")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyadı alanı gerekli")]
        [StringLength(50)]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "E-posta alanı gerekli")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre alanı gerekli")]
        [StringLength(100,MinimumLength =6,ErrorMessage ="Şifre minimum 6 karakter olmalı")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifre Tekrar alanı Gerekli")]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Şifreler Uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
