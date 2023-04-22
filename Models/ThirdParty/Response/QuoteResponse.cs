using System;

namespace Fincra.Models.ThirdParty.Response
{
    public class QuoteResponse
    {
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public double SourceAmount { get; set; }
        public string DestinationAmount { get; set; }
        public string Action { get; set; }
        public string TransactionType { get; set; }
        public string Fee { get; set; }
        public string InitialAmount { get; set; }
        public string QuotedAmount { get; set; }
        public string Rate { get; set; }
        public string AmountToCharge { get; set; }
        public string AmountToReceive { get; set; }
        public string Reference { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
