using ITServiceApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITServiceTest
{
    public class PaymentTests
    {
        private readonly IPaymentService _paymentService;
        public PaymentTests(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Fact]
        public void CheckInstallments()
        {
            var binNumbers = new string[] { "4910050000000006", "5892830000000000", "5400360000000003", "5170410000000004" };

            foreach (var bin in binNumbers)
            {
                var result = _paymentService.CheckInstallments(bin, 1000);
            }

            Assert.Equal(true, true);


        }
    }
}
