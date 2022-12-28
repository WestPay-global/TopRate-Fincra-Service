using System.Text.Json.Serialization;

namespace Fincra.Models.ThirdParty.Request
{
    public class PayoutRequest
    {
        public string Business { get; set; }
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public string PaymentDestination { get; set; }
        public string CustomerReference { get; set; }
        public Beneficiary Beneficiary { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string QuoteReference { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string PaymentScheme { get; set; }
    }
}