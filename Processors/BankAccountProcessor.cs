using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fincra.Interfaces;
using Fincra.Interfaces.Factories;
using Fincra.Models.ThirdParty.Request;
using Fincra.Models.ThirdParty.Response;
using Microsoft.Extensions.Configuration;

namespace Fincra.Processors
{
    public class BankAccountProcessor : IPayoutProcessor
    {
        private readonly IHttpDataClient _httpDataClient;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public BankAccountProcessor(IHttpDataClient httpDataClient, IMapper mapper, IConfiguration configuration)
        {
            _httpDataClient = httpDataClient;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<PayoutResponse> Process(PayoutRequest payout, Models.Dtos.Request.Beneficiary beneficiary)
        {
            PayoutResponse payoutResponse = null;
            payout.Beneficiary = _mapper.Map<BankAccountBeneficiary>(beneficiary);

            string url = _configuration["FincraBaseURL"] + "/disbursements/payouts";
            Dictionary<string, string> headers = new Dictionary<string, string>() { { "api-key", _configuration["APIKey"] } };
            var response = await _httpDataClient.MakeRequest<FincraBaseResponse<PayoutResponse>>(payout, url, headers);
            if (response != null)
                payoutResponse = response.Data;
            return payoutResponse;
        }
    }
}