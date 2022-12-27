using System.Threading.Tasks;
using Fincra.Models.ThirdParty.Request;
using Fincra.Models.ThirdParty.Response;

namespace Fincra.Interfaces.Factories
{
    public interface IPayoutProcessor
    {
        Task<PayoutResponse> Process(PayoutRequest payout, Models.Dtos.Request.Beneficiary beneficiary);
    }
}