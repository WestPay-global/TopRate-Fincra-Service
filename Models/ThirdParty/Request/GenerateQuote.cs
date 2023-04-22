namespace Fincra.Models.ThirdParty.Request
{
    public class GenerateQuote
    {
        public string Action { get; set; }
        public string TransactionType { get; set; }
        public string FeeBearer { get; set; }
        public string BeneficiaryType { get; set; }
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public string Amount { get; set; }
        public string Business { get; set; }
        public string PaymentDestination { get; set; }
    }
}
