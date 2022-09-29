namespace Fincra.Models
{ 
    public class GenerateQuote
    {
        public GenerateQuote()
        {
            this.business = "62c2d46d3acaf70bd6329611";
            this.transactionType = "disbursement";
            this.feeBearer = "business";
            this.action = "receive";
        }
        public string action { get; set; }
        public string transactionType { get; set; }
        public string feeBearer { get; set; }
        public string beneficiaryType { get; set; }
        public string sourceCurrency { get; set; }
        public string destinationCurrency { get; set; }
        public string amount { get; set; }
        public string business { get; set; }
        public string paymentDestination { get; set; }
    }
}
