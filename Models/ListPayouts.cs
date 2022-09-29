using System.Collections.Generic;

namespace Fincra.Models
{
    public class ListPayouts
    {
        public List<string> status { get; set; }
        public string business { get; set; }
        public string sourceCurrency { get; set; }
        public string destinationCurrency { get; set; }
        public string subAccount { get; set; }
        public string page { get; set; }
        public string perPage { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
    }
}
