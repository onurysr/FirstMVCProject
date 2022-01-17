using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.Models.Payment
{
    public class PaymentResponseModel
    {
        public string Price { get; set; }
        public string PaidPrice { get; set; }
        public int? Installment { get; set; }
        public string Currency { get; set; }
        public string PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public int? FraudStatus { get; set; }
        public string MerchantComissionRate { get; set; }
        public string MerchantComissionRateAmount { get; set; }
        public string PosComissionFee { get; set; }
        public string CardType { get; set; }
        public string CardAssociation { get; set; }
        public string CardFamily { get; set; }
        public string CardToken { get; set; }
        public string CardUserKey { get; set; }
        public string BinNumber { get; set; }
        public string LastFourDigits { get; set; }
        public string BasketId { get; set; }
    }
}
