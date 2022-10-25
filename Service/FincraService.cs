using AutoMapper;
using Fincra.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Fincra.Service
{
    public class FincraService : IFincra
    {
        private readonly string api_key;
        private readonly HttpClient apiClient;
        private string baseUrl = @"https://sandboxapi.fincra.com";
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public FincraService(IMapper mapper, IConfiguration configuration)
        {
            this.api_key = "R0qiVNOA7gxfLBpAhNLbvv1JeyiHCV0Y";
            apiClient = new HttpClient();

            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("api-key", this.api_key);
            _mapper = mapper;
            _configuration = configuration;
        }

        private async Task<Beneficiary> GetBeneficiaryAsync(string url)
        {
            var responseMsg = await apiClient.GetAsync(url);

            var responseStr = await responseMsg.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<Root>(responseStr,
                new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture });

            return response.data;
        }
        public async Task<dynamic> CreatePayoutFun(CreatePayoutVm createPayoutVm)
        {
            try
            {
                string beneficaryUrl = $"{_configuration["BeneficiaryServiceBaseUrl"]}api/Beneficary/";
                string beneficiaryUrl = beneficaryUrl + $@"GetBeneficary/ById?id={createPayoutVm.beneficiaryId}";
                var beneficiary = await GetBeneficiaryAsync(beneficiaryUrl);

                if (createPayoutVm.paymentDestination == "mobile_money_wallet" && createPayoutVm.destinationCurrency == "KES")
                {
                    MobilePayoutModel mobilePayoutModel = new MobilePayoutModel();

                    mobilePayoutModel.sourceCurrency = createPayoutVm.sourceCurrency;
                    mobilePayoutModel.destinationCurrency = createPayoutVm.destinationCurrency;
                    mobilePayoutModel.description = createPayoutVm.description;
                    mobilePayoutModel.amount = createPayoutVm.amount;

                    mobilePayoutModel.beneficiary = _mapper.Map<BeneficiaryMobile>(beneficiary);

                    return await MobilePayout(mobilePayoutModel);
                }
                else if (createPayoutVm.paymentDestination == "bank_account" && createPayoutVm.destinationCurrency == "NGN")
                {
                    DisbursmentPayoutNG disbursmentPayoutNG = new DisbursmentPayoutNG();

                    disbursmentPayoutNG.sourceCurrency = createPayoutVm.sourceCurrency;
                    disbursmentPayoutNG.destinationCurrency = createPayoutVm.destinationCurrency;
                    disbursmentPayoutNG.description = createPayoutVm.description;
                    disbursmentPayoutNG.amount = createPayoutVm.amount;

                    disbursmentPayoutNG.paymentDestination = beneficiary.paymentDestination;

                    disbursmentPayoutNG.beneficiary = _mapper.Map<BeneficiaryDisbursmentNG>(beneficiary);

                    return await PayoutNigeria(disbursmentPayoutNG);
                }
                else if (createPayoutVm.paymentDestination == "bank_account" && createPayoutVm.destinationCurrency == "KES")
                {
                    DisbursmentPayout disbursmentPayout = new DisbursmentPayout();

                    disbursmentPayout.sourceCurrency = "USD";
                    disbursmentPayout.destinationCurrency = createPayoutVm.destinationCurrency;
                    disbursmentPayout.description = createPayoutVm.description;
                    disbursmentPayout.amount = createPayoutVm.amount.ToString();

                    disbursmentPayout.paymentDestination = beneficiary.paymentDestination;

                    disbursmentPayout.beneficiary = _mapper.Map<BeneficiaryDisbursment>(beneficiary);

                    return await DisbursmentPayout(disbursmentPayout);
                }
                else
                    return "Fincra can only transfer in NGN and KES";
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }

        public async Task<dynamic> FetchPayoutByCustomerReference(string customerReference)
        {
            string url = this.baseUrl + $"/payouts/customer-reference/{customerReference}";

            HttpResponseMessage result = await apiClient.GetAsync(url);

            var responseStr = await result.Content.ReadAsStringAsync();

            return responseStr;
        }

        public async Task<dynamic> FetchPayoutByReference(string transactionReference)
        {
            string url = this.baseUrl + $"/payouts/reference/{transactionReference}";

            HttpResponseMessage result = await apiClient.GetAsync(url);

            var responseStr = await result.Content.ReadAsStringAsync();
            return responseStr;
        }

        public async Task<dynamic> GetListBanks(ListBanks listBanks)
        {
            var client = new RestClient($"https://sandboxapi.fincra.com/core/banks?currency={listBanks.currency}&country={listBanks.country}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application / json");
            request.AddHeader("api-key", this.api_key);

            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        public async Task<dynamic> GetListPayouts(ListPayouts listPayouts)
        {
            string url = this.baseUrl + @"/payouts";
            var json = JsonConvert.SerializeObject(listPayouts);

            var client = new RestClient(url);

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-key", this.api_key);

            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            return response.Content;
        }

        public async Task<dynamic> WalletToWalletTransfer(WalletToWalletTransfer walletToWalletTransfer)
        {
            string url = this.baseUrl + @"/payouts/wallets";

            var json = JsonConvert.SerializeObject(walletToWalletTransfer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var responseMsg = await apiClient.PostAsync(url, data);

            var responseStr = await responseMsg.Content.ReadAsStringAsync();

            return responseStr;
        }

        public async Task<dynamic> VerifyAccountNumber(VerifyAccountNumber request)
        {
            string url = @"https://sandboxapi.fincra.com/core/accounts/resolve";

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var responseMsg = await apiClient.PostAsync(url, data);

            var responseStr = await responseMsg.Content.ReadAsStringAsync();


            return responseStr;
        }

        public async Task<dynamic> PayoutNigeria(DisbursmentPayoutNG request)
        {
            string url = this.baseUrl + $"/disbursements/payouts";
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await apiClient.PostAsync(url, data);

            var responseStr = await result.Content.ReadAsStringAsync();

            return responseStr;
        }

        public async Task<dynamic> DisbursmentPayout(DisbursmentPayout request)
        {
            string url = this.baseUrl + $"/disbursements/payouts";
            GenerateQuote generateQuote = new GenerateQuote();

            generateQuote.destinationCurrency = request.destinationCurrency;
            generateQuote.sourceCurrency = request.sourceCurrency;
            generateQuote.beneficiaryType = request.beneficiary.type;
            generateQuote.amount = request.amount;
            generateQuote.paymentDestination = request.paymentDestination;

            var quoteResponse = await GetReference(generateQuote);
            request.quoteReference = quoteResponse.data.reference;
            request.amount = quoteResponse.data.quotedAmount;

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await apiClient.PostAsync(url, data);

            var responseStr = await result.Content.ReadAsStringAsync();

            return responseStr;
        }

        public async Task<dynamic> MobilePayout(MobilePayoutModel request)
        {
            string url = this.baseUrl + $"/disbursements/payouts";
            GenerateQuote generateQuote = new GenerateQuote();

            generateQuote.destinationCurrency = request.destinationCurrency;
            generateQuote.sourceCurrency = request.sourceCurrency;
            generateQuote.beneficiaryType = request.beneficiary.type;
            generateQuote.amount = request.amount;
            generateQuote.paymentDestination = "mobile_money_wallet";

            var quoteResponse = await GetReference(generateQuote);
            request.quoteReference = quoteResponse.data.reference;
            request.amount = quoteResponse.data.quotedAmount;

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await apiClient.PostAsync(url, data);

            var responseStr = await result.Content.ReadAsStringAsync();

            return responseStr;
        }


        private async Task<QuoteResponse> GetReference(GenerateQuote request)
        {
            string url = this.baseUrl + $"/quotes/generate";
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await apiClient.PostAsync(url, data);

            var responseStr = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<QuoteResponse>(responseStr);
                return response;
            }

            throw new System.Exception(responseStr);
        }

    }
}
