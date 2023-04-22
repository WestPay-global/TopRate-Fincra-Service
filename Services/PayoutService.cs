
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fincra.Configs;
using Fincra.Factories;
using Fincra.Interfaces;
using Fincra.Models.Dtos.Request;
using Fincra.Models.Dtos.Response;
using Fincra.Models.ThirdParty.Response;
using Microsoft.Extensions.Configuration;

namespace Fincra.Services
{
    public class PayoutService : IPayoutService
    {
        private readonly IHttpDataClient _httpDataClient;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _headers;

        public PayoutService(IHttpDataClient httpDataClient, IMapper mapper, IConfiguration configuration)
        {
            _httpDataClient = httpDataClient;
            _mapper = mapper;
            _configuration = configuration;
            _headers = new Dictionary<string, string>() { { "api-key", _configuration["APIKey"] } };
        }

        public async Task<List<Bank>> Banks(BankFilter filter)
        {
            List<Bank> banks = null;
            string url = _configuration["FincraBaseURL"] + $"/core/banks?currency={filter.Currency}&country={filter.Country}";

            var response = await _httpDataClient.MakeRequest<FincraBaseResponse<List<Bank>>>(url, _headers);
            if (response != null)
                banks = response.Data;
            return banks;
        }


        public async Task<Models.Dtos.Response.PayoutResponse> Payout(Payout createPayout)
        {
            Models.Dtos.Response.PayoutResponse response = null;
            PayoutStepSelectorFactory processorFactory = new PayoutStepSelectorFactory(_httpDataClient, _mapper, _configuration);
            StepType stepType = createPayout.DestinationCurrecy.ToString().ToUpper() == AvailableCurrency.NGN.ToString() ? StepType.SAME : StepType.CROSS;
            var payoutResponse = await processorFactory.GetStep(createPayout, stepType);
            if (payoutResponse != null)
                response = new Models.Dtos.Response.PayoutResponse
                {
                    Status = payoutResponse.Status.ToLower() == "successful",
                    TransactionId = payoutResponse.CustomerReference,
                    Reference = payoutResponse.Reference
                };
            return response;
        }

        public async Task<AccountVerification> VerifyAccountNumber(VerifyAccountNumber verifyAccountNumber)
        {
            AccountVerification accountVerification = null;
            string url = _configuration["FincraBaseURL"] + "/core/accounts/resolve";

            var response = await _httpDataClient.MakeRequest<FincraBaseResponse<AccountVerification>>(verifyAccountNumber, url, _headers);
            if (response != null)
                accountVerification = response.Data;
            return accountVerification;
        }
    }
}