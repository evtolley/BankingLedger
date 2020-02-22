using System;
using System.Collections.Generic;
using System.Security.Claims;
using Domain.LedgerTransactions;
using Domain;
using Domain.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<LedgerTransactionDto>> GetTransactions([FromQuery]LedgerTransactionRequestDto dto)
        {
            int accountId = GetCurrentUserAccountId();

            try
            {
                return Ok(_transactionService.GetAccountTransactions(GetCurrentUserAccountId(), dto.Skip, dto.PageSize));
            }
            catch
            {
                return BadRequest(new ErrorResult("Oops, something went wrong! Please try again"));
            }
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<LedgerTransactionResultDto> Create([FromBody] InputLedgerTransactionDto ledgerTransactionDto)
        { 
            try
            {
                LedgerTransactionResultDto result;

                if(ledgerTransactionDto.TransactionType == LedgerTransactionTypeEnum.Deposit)
                {
                    result = _transactionService.MakeDeposit(ledgerTransactionDto, GetCurrentUserAccountId());
                }
                else
                {
                    result = _transactionService.MakeWithdrawal(ledgerTransactionDto, GetCurrentUserAccountId());
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

        [HttpGet]
        [Route("balanceinquiry")]
        public ActionResult<decimal> BalanceInquiry()
        {
            try
            {
                return Ok(_transactionService.GetCurrentBalance(GetCurrentUserAccountId()));
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
