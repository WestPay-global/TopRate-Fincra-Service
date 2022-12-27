using System.Collections.Generic;
using System.Threading.Tasks;
using Fincra.Models.Dtos.Request;
using Fincra.Models.Dtos.Response;

namespace Fincra.Interfaces
{
    public interface IPayoutService
    {
        Task<Models.Dtos.Response.PayoutResponse> Payout(Payout createPayout);
        Task<Models.Dtos.Response.PayoutResponse> Process(Payout createPayout);
        Task<AccountVerification> VerifyAccountNumber(VerifyAccountNumber verifyAccountNumber);
        Task<List<Bank>> Banks(BankFilter filter);
    }
}