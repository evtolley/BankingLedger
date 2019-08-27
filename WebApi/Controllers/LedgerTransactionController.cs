using System;
using System.Collections.Generic;
using System.Security.Claims;
using BusinessLogic.LedgerTransactions;
using Core;
using Core.ExtensionMethods;
using Core.LedgerTransactions;
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
        public ActionResult<IEnumerable<LedgerTransactionDto>> GetTransactions()
        {
            int accountId = GetCurrentUserAccountId();

            try
            {
                return Ok(_transactionService.GetAccountTransactions(GetCurrentUserAccountId()));
            }
            catch
            {
                return BadRequest(new ErrorResult("Oops, something went wrong! Please try again"));
            }
        }

        [HttpPost]
        [Route("withdrawal")]
        public ActionResult<LedgerTransactionResultDto> Withdrawal([FromBody] InputLedgerTransactionDto ledgerTransactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResult("Please provide a valid amount between $0.01 and $1,000,000"));
            }

            try
            {
                var result = _transactionService.MakeWithdrawal(new LedgerTransactionDto()
                {
                    AccountId = GetCurrentUserAccountId(),
                    Amount = ledgerTransactionDto.Amount
                });

                if(result.ResultType == LedgerTransactionResultTypeEnum.InsufficientFunds)
                {
                    return BadRequest(result.ResultType.GetDescription());
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new ErrorResult("Oops, something went wrong! Please try again"));
            }
        }

        [HttpPost]
        [Route("deposit")]
        public ActionResult<LedgerTransactionResultDto> Deposit([FromBody] InputLedgerTransactionDto ledgerTransactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResult("Please provide a valid amount between $0.01 and $1,000,000"));
            }

            try
            {
                return Ok(_transactionService.MakeDeposit(new LedgerTransactionDto()
                {
                    AccountId = GetCurrentUserAccountId(),
                    Amount = ledgerTransactionDto.Amount
                }));
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
