using System.Threading.Tasks;
using AutoMapper;
using Fincra.Interfaces;
using Fincra.Interfaces.Factories;
using Fincra.Models.Dtos.Request;
using Fincra.Models.ThirdParty.Response;
using Microsoft.Extensions.Configuration;

namespace Fincra.Selectors
{
    public class SameCurrencySelector : IStepSelector
    {

        private readonly IHttpDataClient _httpDataClient;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public SameCurrencySelector(IHttpDataClient httpDataClient, IMapper mapper, IConfiguration configuration)
        {
            _httpDataClient = httpDataClient;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Task<PayoutResponse> Select(Payout payout)
        {
            throw new System.NotImplementedException();
        }
    }
}
