namespace Fincra.Models.ThirdParty.Request
{
    public class BankAccountBeneficiary : Beneficiary
    {
        public string BankCode { get; set; }
        public string SortCode { get; set; }
    }
}