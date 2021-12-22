using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
        [Required,StringLength(50)]
        [PersonalData]
        public string Name { get; set; }

        [Required, StringLength(50)]
        [PersonalData]
        public string Surname { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
