using System.Threading.Tasks;
using Fincra.Models.Dtos.Request;
using Fincra.Models.ThirdParty.Response;

namespace Fincra.Interfaces.Factories
{
    public interface IStepSelector
    {
        Task<PayoutResponse> Select(Payout payout);
    }
}