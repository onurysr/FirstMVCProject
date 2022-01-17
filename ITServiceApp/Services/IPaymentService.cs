using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.Services
{
    public interface IPaymentService
    {
        public void CheckInstallments(string binNumber, decimal price);
        public void Pay();
    }
}
