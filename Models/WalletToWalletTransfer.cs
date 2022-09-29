using System.ComponentModel.DataAnnotations;

namespace Fincra.Models
{    
    public class WalletToWalletTransfer
    {
        [Required]
        public string amount { get; set; }
        
        [Required]
        public string business { get; set; }

        [Required]
        public string customerReference { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string beneficiaryWalletNumber { get; set; }
    }
}
