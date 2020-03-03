using System;
using System.Collections.Generic;
using System.Security.Claims;
using Domain.LedgerTransactions;
using Domain.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LedgerTransactionController : ControllerBase
    {
        private readonly ILedgerTransactionService _transactionService;
        public LedgerTransactionController(ILedgerTransactionService transactionService)
        {
            this._transactionService = transactionService;
        }

        [HttpGet]
        [Route("gettransactions")]
        public async Task<ActionResult<IEnumerable<LedgerTransactionDto>>> GetTransactions([FromQuery]LedgerTransactionRequestDto dto)
        {
            int accountId = GetCurrentUserAccountId();

            try
            {
                return Ok(await _transactionService.GetAccountTransactionsAsync(GetCurrentUserAccountId(), dto.Skip, dto.PageSize));
            }
            catch
            {
                return BadRequest(new ErrorResult("Oops, something went wrong! Please try again"));
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<LedgerTransactionResultDto>> Create([FromBody] InputLedgerTransactionDto ledgerTransactionDto)
        { 
            try
            {
                LedgerTransactionResultDto result;

                if(ledgerTransactionDto.TransactionType == LedgerTransactionTypeEnum.Deposit)
                {
                    result = await _transactionService.MakeDepositAsync(ledgerTransactionDto, GetCurrentUserAccountId());
                }
                else
                {
                    result = await _transactionService.MakeWithdrawalAsync(ledgerTransactionDto, GetCurrentUserAccountId());
                }

                if (result.ResultType != LedgerTransactionResultTypeEnum.Success)
                {
                    return BadRequest(new ErrorResult(result.ResultType.GetDescription()));
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest(new ErrorResult("Oops, something went wrong! Please try again"));
            }
        }

        [HttpPut]
        [Route("edit")]
        public async Task<ActionResult<LedgerTransactionResultDto>> Edit([FromBody]InputLedgerTransactionDto ledgerTransactionDto)
        { 
            try
            {
                LedgerTransactionResultDto result = await _transactionService.EditTransactionAsync(ledgerTransactionDto, GetCurrentUserAccountId());

                if (result.ResultType != LedgerTransactionResultTypeEnum.Success)
                {
                    return BadRequest(new ErrorResult(result.ResultType.GetDescription()));
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest(new ErrorResult("Oops, something went wrong! Please try again"));
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult<LedgerTransactionResultDto>> Delete([FromBody]int transactionId)
        { 
            try
            {
                LedgerTransactionResultDto result = await _transactionService.DeleteTransactionAsync(transactionId, GetCurrentUserAccountId());

                if (result.ResultType != LedgerTransactionResultTypeEnum.Success)
                {
                    return BadRequest(new ErrorResult(result.ResultType.GetDescription()));
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest(new ErrorResult("Oops, something went wrong! Please try again"));
            }
        }

        [HttpGet]
        [Route("balanceinquiry")]
        public async Task<ActionResult<decimal>> BalanceInquiry()
        {
            try
            {
                return Ok(await _transactionService.GetCurrentBalanceAsync(GetCurrentUserAccountId()));
            }
            catch
            {
                return BadRequest(new ErrorResult("Oops, something went wrong! Please try again"));
            }
        }

        // quick and dirty method for extracting the account id from the ClaimsIdentity
        private int GetCurrentUserAccountId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return Convert.ToInt32(identity.FindFirst("AccountId").Value);           
        }
    }
}
