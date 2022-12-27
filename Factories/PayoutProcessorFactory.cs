using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fincra.Configs;
using Fincra.Interfaces;
using Fincra.Interfaces.Factories;
using Fincra.Models.ThirdParty.Request;
using Fincra.Models.ThirdParty.Response;
using Fincra.Processors;
using Microsoft.Extensions.Configuration;

namespace Fincra.Factories
{
    public class PayoutProcessorFactory
    {
        private readonly Dictionary<FincraPayoutType, IPayoutProcessor> processors;
        public PayoutProcessorFactory(IHttpDataClient httpDataClient, IMapper mapper, IConfiguration configuration)
        {
            processors = new Dictionary<FincraPayoutType, IPayoutProcessor>();

            processors.Add(FincraPayoutType.BANK_ACCOUNT, new BankAccountProcessor(httpDataClient, mapper, configuration));
            processors.Add(FincraPayoutType.MOBILE_MONEY_WALLET, new MobileMoneyWalletProcessor(httpDataClient, mapper, configuration));
            processors.Add(FincraPayoutType.WALLET, new WalletProcessor(httpDataClient, mapper, configuration));
        }

        public async Task<PayoutResponse> GetProcessor(PayoutRequest payout, FincraPayoutType type, Models.Dtos.Request.Beneficiary beneficiary) => await processors[type].Process(payout, beneficiary);
    }
}