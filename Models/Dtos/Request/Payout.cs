using System.ComponentModel.DataAnnotations;
using Fincra.Configs;

namespace Fincra.Models.Dtos.Request
{
    public class Payout
    {
        [Required]
        public string DestinationCurrecy { get; set; }

        [Required]
        public string Amount { get; set; }

        [Required]
        public FincraPayoutType PaymentDestination { get; set; }

        [Required]
        public string CustomerReference { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Beneficiary Beneficiary { get; set; }
    }
}