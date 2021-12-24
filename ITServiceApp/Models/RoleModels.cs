using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.Models
{
    public static class RoleModels
    {
        public static string Admin = "Admin";
        public static string User = "User";

        public static ICollection<string> Roles => new List<string>() { Admin, User };
    }
}
