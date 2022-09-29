using System.ComponentModel.DataAnnotations;

namespace Fincra.Models
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        [Required]
        public string country { get; set; }

        [Required]
        public string state { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string street { get; set; }

        [Required]
        public string zip { get; set; }
    }

    public class Root 
    {
        public string code { get; set; }
        public string message { get; set; }
        public Beneficiary data  { get; set; }
    }
    public class Beneficiary
    {
        public Beneficiary()
        {
            //this.type = "individual";
        }
        public string country { get; set; }
        public Address address { get; set; }
        //public Document document { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobileMoneyCode { get; set; }
        
        [Required]
        public string type { get; set; }

        [Required]
        public string accountHolderName { get; set; }

        [Required]
        public string accountNumber { get; set; }
        public string bankSwiftCode { get; set; }
        public string sortCode { get; set; }
        public string registrationNumber { get; set; }
        public string bankCode { get; set; }
        public string paymentDestination { get; set; }
    }

    public class Document
    {
        [Required]
        public string type { get; set; }

        [Required]
        public string number { get; set; }

        [Required]
        public string issuedCountryCode { get; set; }

        [Required]
        public string issuedBy { get; set; }
        
        [Required]
        public string issuedDate { get; set; }
        public string expirationDate { get; set; }
    }

    public class CreatePayout
    {
        public CreatePayout()
        {
            //this.paymentDestination = "bank_account";
        }
        [Required]
        public string sourceCurrency { get; set; }
        
        [Required]
        public string destinationCurrency { get; set; }

        [Required]
        public Beneficiary beneficiary { get; set; }
        public string paymentDestination { get; set; }

        [Required]
        public string amount { get; set; }

        [Required]
        public string business { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string customerReference { get; set; }
        public string paymentScheme { get; set; }
        public string quoteReference { get; set; }
    }

    public class CreatePayoutVm
    {
        //[Required]
        public string sourceCurrency { get; set; }

        [Required]
        public string destinationCurrency { get; set; }

        [Required]
        public string beneficiaryId { get; set; }
     //   [Required]
        public string paymentDestination { get; set; }

        [Required]
        public string amount { get; set; }

        [Required]
        public string business { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string customerReference { get; set; }
        public string paymentScheme { get; set; }
        public string quoteReference { get; set; }
    }
}
