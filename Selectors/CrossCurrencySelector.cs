using System.Collections.Generic;
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
    public class CrossCurrencySelector : IStepSelector
    {
        private readonly IHttpDataClient _httpDataClient;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> headers;

        public CrossCurrencySelector(IHttpDataClient httpDataClient, IMapper mapper, IConfiguration configuration)
        {
            _httpDataClient = httpDataClient;
            _mapper = mapper;
            _configuration = configuration;
            headers = new Dictionary<string, string>() { { "api-key", _configuration["APIKey"] } };
        }

        public async Task<PayoutResponse> Select(Payout payout)
        {
            PayoutResponse payoutResponse = null;
            var quoteRequest = _mapper.Map<GenerateQuote>(payout);
            quoteRequest.SourceCurrency = AvailableCurrency.USD.ToString();
            quoteRequest.TransactionType = _configuration["Quote:FeeBearer"];
            quoteRequest.FeeBearer = _configuration["Quote:TransactionType"];
            quoteRequest.Action = "receive";

            var quoteResponse = await GenerateQuote(quoteRequest);
            if (quoteResponse != null)
            {
                var payoutRequest = _mapper.Map<PayoutRequest>(payout);
                payoutRequest.Business = _configuration["BusinessId"];
                payoutRequest.QuoteReference = quoteResponse.Reference;
                payoutRequest.Amount = quoteResponse.QuotedAmount;
                payoutRequest.SourceCurrency = quoteResponse.SourceCurrency;
                PayoutProcessorFactory processorFactory = new PayoutProcessorFactory(_httpDataClient, _mapper, _configuration);
                payoutResponse = await processorFactory.GetProcessor(payoutRequest, payout.PaymentDestination, payout.Beneficiary);
            }
            return payoutResponse;
        }


        private async Task<QuoteResponse> GenerateQuote(GenerateQuote generateQuote)
        {
            QuoteResponse quoteResponse = null;
            string url = _configuration["FincraBaseURL"] + "/quotes/generate";

            var response = await _httpDataClient.MakeRequest<FincraBaseResponse<QuoteResponse>>(generateQuote, url, headers);
            if (response != null)
                quoteResponse = response.Data;
            return quoteResponse;
        }
    }
}