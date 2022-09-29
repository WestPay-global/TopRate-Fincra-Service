using Fincra.Models;
using Fincra.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Fincra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FincraController : ControllerBase
    {
        private readonly IFincra _fincra;
        public FincraController(IFincra fincra)
        {
            this._fincra = fincra;
        }


        [Route("VerifyAccountNumber")]
        [HttpPost]
        public async Task<IActionResult> VerifyAccountNumber([FromBody] VerifyAccountNumber verifyAccountNumber)
        {
            return Ok(await _fincra.VerifyAccountNumber(verifyAccountNumber));
        }

        [Route("CreatePayout")]
        [HttpPost]
        public async Task<string> CreatePayout([FromBody] CreatePayoutVm createPayout) 
        {
            return await _fincra.CreatePayoutFun(createPayout);
        }

        [Route("WalletToWalletTransfer")]
        [HttpPost]
        public async Task<IActionResult> WalletToWalletTransfer([FromBody] WalletToWalletTransfer request)
        {
            return Ok(await _fincra.WalletToWalletTransfer(request));
        }
        
        [Route("FetchPayoutByReference")]
        [HttpGet]
        public async Task<IActionResult> FetchPayoutByReference([Required][FromQuery] string transactionReference)
        {
            return Ok(await _fincra.FetchPayoutByReference(transactionReference));
        }
        
        [Route("FetchPayoutByCustomerReference")]
        [HttpGet]
        public async Task<IActionResult> FetchPayoutByCustomerReference([Required][FromQuery] string customerReference)
        {
            return Ok(await _fincra.FetchPayoutByCustomerReference(customerReference));
        }
        
        [Route("Get/ListPayouts")]
        [HttpGet]
        public async Task<IActionResult> GetListPayouts([FromQuery] ListPayouts listPayouts)
        {
            return Ok(await _fincra.GetListPayouts(listPayouts));
        }
        
        [Route("Get/ListBanks")]
        [HttpGet]
        public async Task<IActionResult> GetListBanks([FromQuery] ListBanks listBanks)
        {
            return Ok(await _fincra.GetListBanks(listBanks));
        }
    }
}
