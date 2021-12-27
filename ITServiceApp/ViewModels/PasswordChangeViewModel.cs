using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.ViewModels
{
    public class PasswordChangeViewModel
    {

        [Required(ErrorMessage = "Eski Şifre alanı gerekli")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre minimum 6 karakter olmalı")]
        [Display(Name = "Eski Şifre")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Yeni Şifre alanı gerekli")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre minimum 6 karakter olmalı")]
        [Display(Name = "Yeni Şifre")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Şifre Tekrar alanı Gerekli")]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Şifreler Uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
