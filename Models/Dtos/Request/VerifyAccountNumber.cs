using System.ComponentModel.DataAnnotations;

namespace Fincra.Models.Dtos.Request
{
    public class VerifyAccountNumber
    {
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string BankCode { get; set; }
    }
}
