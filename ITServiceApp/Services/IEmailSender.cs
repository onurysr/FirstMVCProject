using ITServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.Services
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message);
    }
}
