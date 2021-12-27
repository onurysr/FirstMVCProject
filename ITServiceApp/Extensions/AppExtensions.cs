using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITServiceApp.Extensions
{
    public static class AppExtensions
    {
        public static string GetUserId(this HttpContext context) //httpcontext classına method eklemek için bunu kullanıyoz.
        {
            return context.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        public static string ToFullErrorString(this ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState.Values)
            {
                foreach (var error in entry.Errors)
                {
                    messages.Add(error.ErrorMessage);
                }
                
            }
            return string.Join(" ", messages);
        }
    }
}
