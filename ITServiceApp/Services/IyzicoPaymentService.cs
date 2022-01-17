using AutoMapper;
using ITServiceApp.Models.Payment;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.Services
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration; //appsetinge erişmek için
        private readonly IyzicoPaymentOptions _options;
        private readonly IMapper _mapper;

        public IyzicoPaymentService(IConfiguration configuration,IyzicoPaymentOptions options, IMapper mapper)
        {
            _configuration = configuration;
            _options = options;
            _mapper = mapper;

            var section = _configuration.GetSection(IyzicoPaymentOptions.Key);
            _options = new IyzicoPaymentOptions()
            {
                ApiKey = section["ApiKey"],
                SecretKey = section["SecretKey"],
                BaseUrl = section["BaseUrl"],
                ThreedsCallbackUrl = section["ThreedsCallbackUrl"]
            };
        }
        public void CheckInstallments(string binNumber, decimal price)
        {
            throw new NotImplementedException();
        }

        public PaymentResponseModel Pay(PaymentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
