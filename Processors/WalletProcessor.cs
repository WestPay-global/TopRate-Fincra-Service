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
    public class WalletProcessor : IPayoutProcessor
    {
        private readonly IHttpDataClient _httpDataClient;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public WalletProcessor(IHttpDataClient httpDataClient, IMapper mapper, IConfiguration configuration)
        {
            _httpDataClient = httpDataClient;
            _mapper = mapper;
            _configuration = configuration;
        }


        public async Task<PayoutResponse> Process(PayoutRequest payout, Models.Dtos.Request.Beneficiary beneficiary)
        {
            PayoutResponse payoutResponse = null;
            var payoutRequest = _mapper.Map<TransferRequest>(payout);
            payoutRequest.BeneficiaryWalletNumber = beneficiary.AccountNumber;

            string url = _configuration["FincraBaseURL"] + "/disbursements/payouts/wallets";
            Dictionary<string, string> headers = new Dictionary<string, string>() { { "api-key", _configuration["APIKey"] } };
            var response = await _httpDataClient.MakeRequest<FincraBaseResponse<PayoutResponse>>(payoutRequest, url, headers);
            if (response != null)
                payoutResponse = response.Data;
            return payoutResponse;
        }
    }
}