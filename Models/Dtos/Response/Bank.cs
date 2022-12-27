using System;

namespace Fincra.Models.Dtos.Response
{
    public class Bank
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Boolean IsMobileVerified { get; set; }
    }
}