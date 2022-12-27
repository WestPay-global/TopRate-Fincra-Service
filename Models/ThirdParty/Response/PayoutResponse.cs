namespace Fincra.Models.ThirdParty.Response
{
    public class PayoutResponse
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string CustomerReference { get; set; }
        public string Status { get; set; }
    }
}