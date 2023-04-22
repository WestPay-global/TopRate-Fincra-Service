using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fincra.Configs;
using Fincra.Interfaces;
using Fincra.Interfaces.Factories;
using Fincra.Models.Dtos.Request;
using Fincra.Models.ThirdParty.Response;
using Fincra.Selectors;
using Microsoft.Extensions.Configuration;

namespace Fincra.Factories
{
    public class PayoutStepSelectorFactory
    {
        private readonly Dictionary<StepType, IStepSelector> steps;
        public PayoutStepSelectorFactory(IHttpDataClient httpDataClient, IMapper mapper, IConfiguration configuration)
        {
            steps = new Dictionary<StepType, IStepSelector>();

            steps.Add(StepType.SAME, new SameCurrencySelector(httpDataClient, mapper, configuration));
            steps.Add(StepType.CROSS, new CrossCurrencySelector(httpDataClient, mapper, configuration));
        }

        public async Task<PayoutResponse> GetStep(Payout payout, StepType type) => await steps[type].Select(payout);
    }
}