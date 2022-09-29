namespace Fincra.Models
{
 
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BeneficiaryMobile
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string accountHolderName { get; set; }
        public string accountNumber { get; set; }
        public string type { get; set; }
        public string country { get; set; }
        public string mobileMoneyCode { get; set; }
    }

    public class MobilePayoutModel
    {
        public MobilePayoutModel()
        {
            this.business = "62c2d46d3acaf70bd6329611";
            this.paymentDestination = "mobile_money_wallet";
        }
        public string business { get; set; }
        public string sourceCurrency { get; set; }
        public string destinationCurrency { get; set; }
        public string amount { get; set; }
        public string quoteReference { get; set; }
        public string paymentDestination { get; set; }
        public string description { get; set; }
        public BeneficiaryMobile beneficiary { get; set; }
    }


}
