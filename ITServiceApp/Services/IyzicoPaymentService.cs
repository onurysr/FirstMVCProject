using AutoMapper;
using ITServiceApp.Models.Identity;
using ITServiceApp.Models.Payment;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MUsefulMethods;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public IyzicoPaymentService(IConfiguration configuration, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;

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

        private CreatePaymentRequest InitialPaymentRequest(PaymentModel model)
        {
            var paymentRequest = new CreatePaymentRequest
            {
                Installment = model.Installment,
                Locale = Locale.TR.ToString(),
                ConversationId = GenerateConversationId(),
                Price = model.Price.ToString(new CultureInfo("en-Us")),
                PaidPrice = model.PaidPrice.ToString(new CultureInfo("en-Us")),
                BasketId = StringHelpers.GenerateUniqueCode(),
                Currency = Currency.TRY.ToString(),
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.SUBSCRIPTION.ToString(),
                PaymentCard = _mapper.Map<PaymentCard>(model.CardModel)
            };

            var user = _userManager.FindByIdAsync(model.UserId).Result;

            var buyer = new Buyer
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                GsmNumber = user.PhoneNumber,
                Email = user.Email,
                IdentityNumber = "1111111110",
                LastLoginDate = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                RegistrationDate = $"{user.CreatedDate:yyyy-MM-dd HH:mm:ss}",
                RegistrationAddress = "Cihannuma Mah. Barbaros Bulvarı no:9 Beşiktaş",
                Ip = model.Ip,
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34732"
            };

            paymentRequest.Buyer = buyer;

            var billingAddress = new Address
            {
                ContactName = $"{user.Name} {user.Surname}",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Cihannuma Mah. Barbaros Bulvarı no:9 Beşiktaş",
                ZipCode = "34732"
            };
            paymentRequest.BillingAddress = billingAddress;

            var basketItem = new List<BasketItem>();

            var firstBasketItem = new BasketItem
            {
                Id = "BI101",
                Name = "Binocular",
                Category1 = "Collectibles",
                Category2 = "Accessories",
                ItemType = BasketItemType.VIRTUAL.ToString(),
                Price = model.Price.ToString(new CultureInfo("en-Us"))
            };

            basketItem.Add(firstBasketItem);
            paymentRequest.BasketItems = basketItem;

            return paymentRequest;
        }
        public InstallmentModel CheckInstallments(string binNumber, decimal price)
        {
            if (binNumber.Length > 6)
            {
                binNumber = binNumber.Substring(0, 6);
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
            var request = InitialPaymentRequest(model);

            var payment = Payment.Create(request, _options);
            return _mapper.Map<PaymentResponseModel>(payment);

        }
    }
}
