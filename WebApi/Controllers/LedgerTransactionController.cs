using System;
using System.Collections.Generic;
using System.Security.Claims;
using BusinessLogic.LedgerTransactions;
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
            return Ok(_transactionService.GetAccountTransactions(GetCurrentUserAccountId()));
        }

        [HttpPost]
        [Route("withdrawal")]
        public ActionResult Withdrawal([FromBody] InputLedgerTransactionDto ledgerTransactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide a valid amount");
            }

            try
            {
                return Ok(_transactionService.MakeWithdrawal(new LedgerTransactionDto()
                {
                    AccountId = GetCurrentUserAccountId(),
                    Amount = ledgerTransactionDto.Amount
                }));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("deposit")]
        public ActionResult Deposit([FromBody] InputLedgerTransactionDto ledgerTransactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide a valid positive amount");
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
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("balanceinquiry")]
        public ActionResult BalanceInquiry()
        {
            try
            {
                return Ok(_transactionService.GetCurrentBalance(GetCurrentUserAccountId()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
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
