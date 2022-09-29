using Fincra.Models;
using System.Threading.Tasks;

namespace Fincra.Service
{
    public interface IFincra
    {
        Task<dynamic> CreatePayoutFun(CreatePayoutVm createPayout);
        Task<dynamic> WalletToWalletTransfer(WalletToWalletTransfer walletToWalletTransfer);
        Task<dynamic> FetchPayoutByReference(string transactionReference);
        Task<dynamic> FetchPayoutByCustomerReference(string customerReference);
        Task<dynamic> GetListPayouts(ListPayouts listPayouts);
        Task<dynamic> GetListBanks(ListBanks listBanks);

        Task<dynamic> VerifyAccountNumber(VerifyAccountNumber request);

        Task<dynamic> PayoutNigeria(DisbursmentPayoutNG request);
        Task<dynamic> DisbursmentPayout(DisbursmentPayout request);
        Task<dynamic> MobilePayout(MobilePayoutModel request);

    }
}
