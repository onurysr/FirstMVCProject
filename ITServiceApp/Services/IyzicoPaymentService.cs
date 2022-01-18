using AutoMapper;
using ITServiceApp.Models.Payment;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private string GenerateConversationId()
        {
            return MUsefulMethods.StringHelpers.GenerateUniqueCode();
        }
        public InstallmentModel CheckInstallments(string binNumber, decimal price)
        {
            if (binNumber.Length> 6)
            {
                binNumber = binNumber.Substring(0, 0);
            }
            var conversationId = GenerateConversationId();
            var request = new RetrieveInstallmentInfoRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = conversationId,
                BinNumber = binNumber,
                Price = price.ToString(new CultureInfo("en-US")),
            };

            var result = InstallmentInfo.Retrieve(request, _options);
            if (result.Status == "failure")
            {
                throw new Exception(result.ErrorMessage);
            }

            if (result.ConversationId != conversationId)
            {
                throw new Exception("Hatalı istek olusturuldu");
            }

            var resultModel = _mapper.Map<InstallmentModel>(result.InstallmentDetails[0]);

            return resultModel;
        }

        public PaymentResponseModel Pay(PaymentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
