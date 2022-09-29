namespace Fincra.Models
{
   
    public class BeneficiaryDisbursment
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string accountHolderName { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string accountNumber { get; set; }
        public string type { get; set; }
        public string email { get; set; }
        public string bankCode { get; set; }
    }

    public class DisbursmentPayout
    {
        public DisbursmentPayout()
        {
            this.business = "62c2d46d3acaf70bd6329611";
            this.paymentDestination = "bank_account";
        }
        public string business { get; set; }
        public string sourceCurrency { get; set; }
        public string destinationCurrency { get; set; }
        public string amount { get; set; }
        public string quoteReference { get; set; }
        public string description { get; set; }
        public string paymentDestination { get; set; }
        public BeneficiaryDisbursment beneficiary { get; set; }
    }


}
