using System.Threading.Tasks;
using AutoMapper;
using Fincra.Configs;
using Fincra.Factories;
using Fincra.Interfaces;
using Fincra.Interfaces.Factories;
using Fincra.Models.Dtos.Request;
using Fincra.Models.ThirdParty.Request;
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

        public async Task<PayoutResponse> Select(Payout payout)
        {
            PayoutResponse payoutResponse = null;
            var payoutRequest = _mapper.Map<PayoutRequest>(payout);
            payoutRequest.Business = _configuration["BusinessId"];
            payoutRequest.SourceCurrency = AvailableCurrency.NGN.ToString();
            PayoutProcessorFactory processorFactory = new PayoutProcessorFactory(_httpDataClient, _mapper, _configuration);
            payoutResponse = await processorFactory.GetProcessor(payoutRequest, payout.PaymentDestination, payout.Beneficiary);
            return payoutResponse;
        }
    }
}
