namespace Fincra.Models.ThirdParty.Request
{
    public class TransferRequest
    {
        public string Business { get; set; }
        public string BeneficiaryWalletNumber { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string CustomerReference { get; set; }
    }
}
