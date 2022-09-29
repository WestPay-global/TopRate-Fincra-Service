using System;

namespace Fincra.Models
{
   public class Data
    {
        public string sourceCurrency { get; set; }
        public string destinationCurrency { get; set; }
        public double sourceAmount { get; set; }
        public string destinationAmount { get; set; }
        public string action { get; set; }
        public string transactionType { get; set; }
        public string fee { get; set; }
        public string initialAmount { get; set; }
        public string quotedAmount { get; set; }
        public string rate { get; set; }
        public string amountToCharge { get; set; }
        public string amountToReceive { get; set; }
        public string reference { get; set; }
        public DateTime expireAt { get; set; }
    }

    public class QuoteResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }


}
