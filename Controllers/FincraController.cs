using Fincra.Interfaces;
using Fincra.Models.Dtos.Request;
using Fincra.Models.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Fincra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FincraController : ControllerBase
    {
        private readonly IPayoutService _payoutService;
        public FincraController(IPayoutService payoutService)
        {
            _payoutService = payoutService;
        }


        [Route("VerifyAccountNumber")]
        [HttpPost]
        public async Task<IActionResult> VerifyAccountNumber([FromBody] VerifyAccountNumber verifyAccountNumber)
        {
            var response = await _payoutService.VerifyAccountNumber(verifyAccountNumber);
            if (response == null)
                return BadRequest(new BaseResponse<dynamic>(null, HttpStatusCode.BadRequest, "Failed to verify account"));
            return Ok(new BaseResponse<AccountVerification>(response, HttpStatusCode.Created, "Banks fetched"));
        }

        [Route("CreatePayout")]
        [HttpPost]
        public async Task<IActionResult> CreatePayout([FromBody] Payout createPayout)
        {
            var response = await _payoutService.Payout(createPayout);
            if (response == null)
                return BadRequest(new BaseResponse<dynamic>(null, HttpStatusCode.BadRequest, "Transaction failed"));

            return Created("", new BaseResponse<PayoutResponse>(response, HttpStatusCode.Created, "Created successfully"));
        }

        [Route("ListBanks")]
        [HttpGet]
        public async Task<IActionResult> GetListBanks([FromQuery] BankFilter filter)
        {
            var response = await _payoutService.Banks(filter);
            if (response == null)
                return BadRequest(new BaseResponse<dynamic>(null, HttpStatusCode.BadRequest, "Failed to get banks"));

            return Ok(new BaseResponse<List<Bank>>(response, HttpStatusCode.Created, "Banks fetched"));
        }
    }
}
