
using System.ComponentModel.DataAnnotations;

namespace Fincra.Models.Dtos.Request
{
    public class Beneficiary
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string AccountHolderName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Email { get; set; }

        public string BankCode { get; set; }

        [Required]
        public string Country { get; set; }

        public string SortCode { get; set; }

        public string MobileMoneyCode { get; set; }
    }
}